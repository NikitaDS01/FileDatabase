using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDB.Core.File
{
    public class DataFile
    {
        public static readonly DataFile Empty = new DataFile(new FileInfo(string.Empty), string.Empty);
        public FileInfo Path { get; private set; }
        public string Coding { get; private set; }
        public DataFile(FileInfo path, string coding)
        {
            Path = path;
            Coding = coding;
        }
    }
}
