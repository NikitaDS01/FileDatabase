using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Reader;
using FileDB.Core.Writer;

namespace FileDB.Core.Data.Tables
{
    public class Table
    {
        private readonly DirectoryInfo _directoryTable;
        private readonly int _lastIndex;
        private readonly Table _parentTable;
        private readonly string _name;

        public Table(DirectoryInfo directoryIn, PropertyTable propertyIn, Table parentIn=null)
        {
            _directoryTable = directoryIn;
            _lastIndex = propertyIn.LastIndex;
            _name = propertyIn.NameTable;
            _parentTable = parentIn;
        }
        public FileInfo[] Files => _directoryTable.GetFiles();
        public DirectoryInfo[] Directories => _directoryTable.GetDirectories();
        public bool IsRoot => _parentTable == null;
        public string Name => _name;
        public Record ReadData(string pathIn)
        {
            var reader = new ReaderFileTxt(pathIn);
            string data = reader.Read();
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(pathIn));
            return ReaderData.Read(data);
        }
    }
}