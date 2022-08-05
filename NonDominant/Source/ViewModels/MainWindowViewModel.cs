using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace NonDominant.ViewModels
{
    class AppConfigComboItem
    {
        public AppConfigViewModel ViewModel { get; set; }
        public bool DefaultItem { get; set; }

        public string AppPath => ViewModel.AppPath;
        public string AppName => Path.GetFileName(AppPath);
        public string Display => DefaultItem ? "デフォルト" : AppName;

        public AppConfigComboItem(AppConfigViewModel viewModel, bool defaultItem = false)
        {
            ViewModel = viewModel;
            DefaultItem = defaultItem;
        }
    }

    class MainWindowViewModel : BindableBase, IDisposable
    {
        NonDominantCore Core { get; set; }

        public ObservableCollection<AppConfigComboItem> AppConfigItems { get; set; }
        public AppConfigComboItem? SelectedAppConfig { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand AddAppCommand { get; set; }
        public DelegateCommand RemoveAppCommand { get; set; }
        public DelegateCommand BrowseAppCommand { get; set; }

        public MainWindowViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => true);
            AddAppCommand = new DelegateCommand(AddApp, () => true);
            RemoveAppCommand = new DelegateCommand(RemoveApp, () => !SelectedAppConfig.DefaultItem);
            BrowseAppCommand = new DelegateCommand(BrowseApp, () => !SelectedAppConfig.DefaultItem);

            var config = ConfigFile.ReadOrCreate();
            Core = new NonDominantCore(config);

            AppConfigItems = new ObservableCollection<AppConfigComboItem>(
                Enumerable
                    .Empty<AppConfigComboItem>()
                    .Append(new AppConfigComboItem(new AppConfigViewModel(config.DefaultAppConfig), defaultItem: true))
                    .Concat(config
                                .AppConfigs
                                .Select(appConfig => new AppConfigComboItem(new AppConfigViewModel(appConfig)))
                                .OrderBy(item => item.AppPath)
                    )
            );

            SelectedAppConfig = AppConfigItems[0];
        }

        void OnSelectedAppConfigChanged()
        {
            RemoveAppCommand.RaiseCanExecuteChanged();
            BrowseAppCommand.RaiseCanExecuteChanged();
        }

        void Save()
        {
            var newConfig = new Config
            {
                DefaultAppConfig = AppConfigItems[0].ViewModel.ToModel(),
                AppConfigs = AppConfigItems
                                .Skip(1)
                                .Select(i => i.ViewModel.ToModel())
                                .ToList(),
            };

            try
            {
                ConfigFile.Write(newConfig);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString(), "設定ファイル書き込みエラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Core?.Dispose();
            Core = new(newConfig);
        }

        void AddApp()
        {
            var newItem = new AppConfigComboItem(new AppConfigViewModel(new AppConfig { AppPath = "NewApp.exe" }));
            AppConfigItems.Add(newItem);
            SortAppConfigItems();
            SelectedAppConfig = newItem;
        }

        void RemoveApp()
        {
            AppConfigItems.Remove(SelectedAppConfig);
            SortAppConfigItems();
            SelectedAppConfig = AppConfigItems[0];
        }

        void BrowseApp()
        {
            var dialog = new OpenFileDialog
            {
                Title = "アプリを選択",
                Filter = "アプリ (*.exe)|*.exe|全てのファイル (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                var targetAppConfig = SelectedAppConfig!;
                targetAppConfig.ViewModel.AppPath = dialog.FileName;

                SortAppConfigItems();

                // コンボボックスが更新されないことへのワークアラウンド
                SelectedAppConfig = null;
                SelectedAppConfig = targetAppConfig;
            }
        }

        void SortAppConfigItems()
        {
            AppConfigItems = new ObservableCollection<AppConfigComboItem>(
                AppConfigItems
                    .OrderBy(i => i.DefaultItem ? 0 : 1)
                    .ThenBy(i => i.AppName)
            );
        }

        public void Dispose()
        {
            Core?.Dispose();
        }
    }
}
