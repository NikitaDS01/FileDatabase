using FileDB.Core.File;
using System.Xml.Linq;

namespace FileDB.Function
{
    public class FunctionFile
    {
        private const char SPLIT_FILE_NAME = '_';
        public static string FullFileName(string pathIn, string nameIn, TypeFormat formatIn)
        {
            return pathIn + '\\' + FileName(nameIn, formatIn);
        }
        public static string FileName(string nameIn, TypeFormat formatIn)
        {
            string format = string.Empty;
            switch (formatIn)
            {
                case TypeFormat.TXT: format = ".txt"; break;
                case TypeFormat.TBL: format = ".tbl"; break;
                case TypeFormat.FDB: format = ".fdb"; break;
            }
            return nameIn + format;
        }
        public static string[] SplitFileName(string fileNameIn)
        {
            int index = fileNameIn.IndexOf('.');
            string name = fileNameIn.Substring(0, index);
            return name.Split(SPLIT_FILE_NAME);
        }
    }
}
