using System.IO;

namespace DataRepository
{
    public abstract class ARepository
    {
        //protected static string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Config\\Resource\\";

        protected static string resourceFolderPath = Path.GetDirectoryName(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)) + "\\LifesAssistant\\Config\\Resource\\";

        protected static string configFolderPath = Path.GetDirectoryName(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)) + "\\LifesAssistant\\Config\\";

        public void CheckFileExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public void CheckFolderExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
