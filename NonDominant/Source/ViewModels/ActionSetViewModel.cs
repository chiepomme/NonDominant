using Prism.Mvvm;
using System.Windows;

namespace NonDominant.ViewModels
{
    class ActionSetViewModel : BindableBase
    {
        public ActionKeyViewModel Down { get; set; }
        public ActionKeyViewModel LDown { get; set; }
        public ActionKeyViewModel RDown { get; set; }

        public ActionKeyViewModel Hold { get; set; }
        public ActionKeyViewModel LHold { get; set; }
        public ActionKeyViewModel RHold { get; set; }

        public ActionKeyViewModel Click { get; set; }
        public ActionKeyViewModel LClick { get; set; }
        public ActionKeyViewModel RClick { get; set; }

        public ActionKeyViewModel DoubleClick { get; set; }
        public ActionKeyViewModel LDoubleClick { get; set; }
        public ActionKeyViewModel RDoubleClick { get; set; }

        public ActionKeyViewModel Up { get; set; }
        public ActionKeyViewModel LUp { get; set; }
        public ActionKeyViewModel RUp { get; set; }

        public Visibility ZLVisibility { get; }
        public Visibility ZRVisibility { get; }

        public ActionSetViewModel(bool isLeft, ActionSet actionSet)
        {
            ZLVisibility = isLeft ? Visibility.Visible : Visibility.Collapsed;
            ZRVisibility = !isLeft ? Visibility.Visible : Visibility.Collapsed;

            Down = new ActionKeyViewModel(actionSet.Down);
            LDown = new ActionKeyViewModel(actionSet.LDown);
            RDown = new ActionKeyViewModel(actionSet.RDown);

            Hold = new ActionKeyViewModel(actionSet.Hold);
            LHold = new ActionKeyViewModel(actionSet.LHold);
            RHold = new ActionKeyViewModel(actionSet.RHold);

            Click = new ActionKeyViewModel(actionSet.Click);
            LClick = new ActionKeyViewModel(actionSet.LClick);
            RClick = new ActionKeyViewModel(actionSet.RClick);

            DoubleClick = new ActionKeyViewModel(actionSet.DoubleClick);
            LDoubleClick = new ActionKeyViewModel(actionSet.LDoubleClick);
            RDoubleClick = new ActionKeyViewModel(actionSet.RDoubleClick);

            Up = new ActionKeyViewModel(actionSet.Up);
            LUp = new ActionKeyViewModel(actionSet.LUp);
            RUp = new ActionKeyViewModel(actionSet.RUp);
        }

        public ActionSet ToModel() => new()
        {
            Down = Down.ToModel(),
            LDown = LDown.ToModel(),
            RDown = RDown.ToModel(),

            Hold = Hold.ToModel(),
            LHold = LHold.ToModel(),
            RHold = RHold.ToModel(),

            Click = Click.ToModel(),
            LClick = LClick.ToModel(),
            RClick = RClick.ToModel(),

            DoubleClick = DoubleClick.ToModel(),
            LDoubleClick = LDoubleClick.ToModel(),
            RDoubleClick = RDoubleClick.ToModel(),

            Up = Up.ToModel(),
            LUp = LUp.ToModel(),
            RUp = RUp.ToModel(),
        };
    }
}
