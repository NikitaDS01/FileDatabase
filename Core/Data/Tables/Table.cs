using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;

namespace FileDB.Core.Data.Tables
{
    public class Table
    {
        private readonly DirectoryInfo _directoryTable;
        private readonly Table _parentTable;
        private readonly string _name;
        private int _lastIndex;

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
        public Record FindOne(FindParameters parameters)
        {
            foreach(var file in Files)
            {
                var reader = new ReaderFileTxt(file.FullName);
                string data = reader.Read();
                if(data.Contains(valueIn))
                {
                    return ReaderData.Read(data);
                }
            }
            return null;
        }
        public Record[] Find(string valueIn)
        {
            List<Record> list = new List<Record>();
            foreach(var file in Files)
            {
                var reader = new ReaderFileTxt(file.FullName);
                string data = reader.Read();
                if(data.Contains(valueIn))
                {
                    list.Add(ReaderData.Read(data));
                }
            }
            return list.ToArray();
        }
        public void WriteArray(IEnumerable<Record> recordsIn)
        {
            foreach(var record in recordsIn)
            {
                string path = _directoryTable.FullName +'\\'+
                    FunctionFile.FileName(_lastIndex.ToString(), File.TypeFormat.TXT);
                    
                var writer = new WriterFileTxt(path);
                string data = WriterData.Write(record);
                writer.Write(data);
                _lastIndex++;
            }
        }
        public void Write( Record recordIn)
        {
            this.WriteArray(new Record[1] {recordIn});
        }
    }
}