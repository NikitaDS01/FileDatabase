using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.File;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;
using FileDB.Serialization;

namespace FileDB.Core.Data.Tables
{
    public partial class Table
    {
        private const string SETTING_TABLE = "*.fdb";
        private readonly DirectoryInfo _directoryTable;
        private readonly string _name;
        private readonly DateTime _lastUpdate;
        private readonly Dictionary<string, Table> _rootTables;
        private int _lastIndex;


        public Table(DirectoryInfo directoryIn, PropertyTable propertyIn)
        {
            _rootTables = new Dictionary<string, Table>();
            _directoryTable = directoryIn;
            _lastIndex = propertyIn.LastIndex;
            _name = propertyIn.NameTable;
            _lastUpdate = propertyIn.LastUpdate;
        }
        #region Property
        
        public FileInfo[] Files => _directoryTable.GetFiles();
        public DirectoryInfo[] Directories => _directoryTable.GetDirectories();
        public PropertyTable Property => this.GetProperty();
        public bool IsActual => _directoryTable.LastWriteTime == _lastUpdate;
        public string Name => _name;
        public string Path => _directoryTable.FullName;
        public FileInfo PropertyFileFBD => new FileInfo(FunctionFile.FullFileName(_directoryTable.FullName, 
                _name, TypeFormat.FDB));
        
        #endregion

        #region WorkDataTable PublicMethod 
        
        public void WriteArray(IEnumerable<Record> recordsIn)
        {
            foreach(var record in recordsIn)
            {
                string path = FunctionFile.FullFileName(_directoryTable.FullName,
                    GetNameFile(record), File.TypeFormat.TXT); 
                    
                var writer = new WriterFileTxt(path);
                string data = WriterData.Write(record);
                writer.Write(data);
                _lastIndex++;
            }
            UpdateActual();
        }
        public void WriteArray<In>(IEnumerable<In> objectsIn)
        {
            var records = new List<Record>();
            foreach(var item in objectsIn)
            {
                var record = FileSerializer.Serialize<In>(item);
                records.Add(record);
            }
            this.WriteArray(records);
        }
        public void Write( Record recordIn)
        {
            this.WriteArray(new Record[1] {recordIn});
        }
        public void Write<In>(In objectIn)
        {
            var record = FileSerializer.Serialize<In>(objectIn);
            this.WriteArray(new Record[1] {record});
        }

        #endregion

        #region ChangeTable PublicMethod
        
        public Table CreateChildTable(string nameIn)
        {
            var property = new PropertyTable() {NameTable = nameIn};
            DirectoryInfo info = _directoryTable.CreateSubdirectory(nameIn);
            Record record = FileSerializer.Serialize<PropertyTable>(property);
            this.WriteData(FunctionFile.FullFileName(info.FullName, 
                property.NameTable, TypeFormat.FDB), record);
            
            var table = new Table(info, property);
            _rootTables.Add(table.Name, table);
            return table;
        }
        public void Initialize()
        {
            DirectoryInfo[] directoryTables = _directoryTable.GetDirectories();
            FileInfo[] files = this.GetFileFDB(directoryTables);
            foreach(var file in files)
            {
                if(file == null)
                    throw new ArgumentNullException(nameof(file));

                Console.WriteLine(file.Name);
                var record = this.ReadData(file.FullName);
                var property = FileSerializer.Deserialize<PropertyTable>(record);
                var table = new Table(file.Directory, property);
                _rootTables.Add(property.NameTable, table);
                table.Initialize();
            }
        }
        
        #endregion

        private void UpdateActual()
        {   
            Record record = FileSerializer.Serialize<PropertyTable>(GetProperty());
            this.WriteData(PropertyFileFBD.FullName, record);
        }
        private string GetNameFile(Record recordIn)
        {
            if(recordIn.IsIndex)
                return _name + '_' + recordIn.IndexElement.Value;
            else
                return _name + '_' + _lastIndex.ToString();
        }
        private PropertyTable GetProperty()
        {
            var property = new PropertyTable { LastIndex = _lastIndex, NameTable = _name,
                LastUpdate = DateTime.Now};
            return property;
        }
        private void WriteData(string pathIn, Record recordIn)
        {
            var writer = new WriterFileTxt(pathIn);
            string data = WriterData.Write(recordIn);
            writer.Write(data);
        }
        private Record ReadData(string pathIn)
        {
            var reader = new ReaderFileTxt(pathIn);
            string data = reader.Read();
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(pathIn));
            return ReaderData.Read(data);
        }
        private List<Record> GiveAllRecord()
        {
            List<Record> list = new List<Record>();
            foreach(var file in Files)
            {
                var reader = new ReaderFileTxt(file.FullName);
                string data = reader.Read();
                list.Add(ReaderData.Read(data));
            }
            return list;
        }
        private List<Record> GiveRecord(string nameFieldIn)
        {
            List<Record> list = new List<Record>();
            foreach(var file in Files)
            {
                var reader = new ReaderFileTxt(file.FullName);
                string data = reader.Read();
                var record = ReaderData.Read(data);
                if(record.ContainField(nameFieldIn))
                    list.Add(record);
            }
            return list;
        }
        private FileInfo[] GetFileFDB(DirectoryInfo[] directories)
        {
            var files = new FileInfo[directories.Length];
            for(int index = 0; index < directories.Length;index++)
            {
                var directory = directories[index];
                var tmp = directory.GetFiles(SETTING_TABLE, SearchOption.TopDirectoryOnly);
                files[index] = tmp.First();
            }
            return files;
        }
    }
}