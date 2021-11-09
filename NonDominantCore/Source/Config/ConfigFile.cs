using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NonDominant
{
    public static class ConfigFile
    {
        public static readonly FileInfo FileInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml"));

        public static Config Read()
        {
            if (!FileInfo.Exists) throw new FileNotFoundException("設定ファイルがありません。");

            try
            {
                var serializer = new XmlSerializer(typeof(Config));
                using (var stream = File.Open(FileInfo.FullName, FileMode.Open))
                using (var xmlReader = XmlReader.Create(stream))
                {
                    return (Config)serializer.Deserialize(xmlReader);
                }
            }
            catch (Exception e)
            {
                throw new Exception("設定ファイルの読み込みに失敗しました。", e);
            }
        }

        public static Config ReadOrCreate()
        {
            if (!FileInfo.Exists) return new Config();
            return Read();
        }

        public static void Write(Config config)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Config));
                using (var stream = File.Open(FileInfo.FullName, FileMode.Create))
                using (var xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Encoding = new UnicodeEncoding(false, false),
                    Indent = true,
                }))
                {
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(xmlWriter, config, ns);
                }
            }
            catch (Exception e)
            {
                throw new Exception("設定ファイルの書き込みに失敗しました。", e);
            }
        }
    }
}
