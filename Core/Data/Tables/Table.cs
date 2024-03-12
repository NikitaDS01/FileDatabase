using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly DirectoryInfo _directoryTable;
        private readonly string _name;
        private readonly DateTime _lastUpdate;
        private readonly Dictionary<string, Table> _childTables;
        private readonly bool _isRecordNameTable;
        private bool _isInit = false;

        public Table(DirectoryInfo directoryIn, PropertyTable propertyIn)
        {
            _childTables = new Dictionary<string, Table>();
            _directoryTable = directoryIn;
            _name = propertyIn.NameTable;
            _lastUpdate = propertyIn.LastUpdate;
            _isInit = false;
            _isRecordNameTable = propertyIn.IsNameTable;
        }

        #region Property
        
        public FileInfo[] Files => _directoryTable.GetFiles("*.txt");
        public DirectoryInfo[] Directories => _directoryTable.GetDirectories();
        public PropertyTable Property => this.GetProperty();
        public bool IsActual => _directoryTable.LastWriteTime == _lastUpdate;
        public string Name => _name;
        public string Path => _directoryTable.FullName;
        public FileInfo PropertyFileFBD => new FileInfo(FunctionFile.FullFileName(_directoryTable.FullName, 
                _name, TypeFormat.TBL));
        public IReadOnlyCollection<Table> ChildTables => _childTables.Values;
        public int CountRecord => Files.Length;

        #endregion

        #region WorkDataTable PublicMethod 
        
        public void WriteArray(IEnumerable<Record> recordsIn)
        {
            foreach(var record in recordsIn)
            {
                string path = GetNameFile(record); 

                var writer = new WriterFileTxt(path);
                string data = WriterData.Write(record);
                writer.Write(data);
            }
            UpdatePropertyTable();
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
        public void WriteOne( Record recordIn)
        {
            this.WriteArray(new Record[1] {recordIn});
        }
        public void WriteOne<In>(In objectIn)
        {
            var record = FileSerializer.Serialize<In>(objectIn);
            this.WriteArray(new Record[1] {record});
        }

        public int UpdateArray(Record recordChange)
        {
            var changeRecords = new List<Record>();
            var listRecord = this.GiveAllRecord(new ParameterSearch {Take = int.MaxValue});
            for(int index = 0; index < listRecord.Count; index++)
            {
                if(listRecord[index].Modification(recordChange))
                    changeRecords.Add(listRecord[index]);
            }
            this.WriteArray(changeRecords);
            return changeRecords.Count;
        }
        public int UpdateArray(RecordSearch recordSearch, Record recordChange)
        {
            var changeRecords = new List<Record>();
            var listRecord = this.GiveFilterRecord(recordSearch, new ParameterSearch {Take = int.MaxValue});
            for(int index = 0; index < listRecord.Count; index++)
            {
                if(listRecord[index].Modification(recordChange))
                    changeRecords.Add(listRecord[index]);
            }
            this.WriteArray(changeRecords);
            return changeRecords.Count;
        }
        public void UpdateOne(RecordSearch recordSearch, Record recordChange)
        {
            Record? record = this.GiveOneRecord(recordSearch);
            if(record == null)
                return;

            record.Modification(recordChange);
            this.WriteOne(record);
        }

        public int DeleteArray(RecordSearch recordSearch)
        {
            var listRecord = this.GiveFilterRecord(recordSearch, new ParameterSearch {Take = int.MaxValue});
            for(int index = 0; index < listRecord.Count; index++)
            {
                string path = FunctionFile.FullFileName(_directoryTable.FullName,
                    GetNameFile(listRecord[index]), File.TypeFormat.TXT);
                this.DeleteData(path);        
            }
            UpdatePropertyTable();
            return listRecord.Count;
        }
        public void DeleteOne(RecordSearch recordSearch)
        {
            Record? record = this.GiveOneRecord(recordSearch);
            if(record == null)
                return;

            string path = GetNameFile(record); 
            Console.WriteLine(path);
            this.DeleteData(path);  
            UpdatePropertyTable();
        }

        #endregion

        #region ChangeTable PublicMethod
        
        public bool TryGetTable(string nameIn, out Table? tableOut)
        {
            if(this.Name == nameIn)
            {
                tableOut = this;
                return true;
            }

            tableOut = null;
            foreach(var child in ChildTables)
            {
                if(child.TryGetTable(nameIn, out tableOut))
                {
                    return true;
                }
            }
            return false;
        }
        public Table CreateChildTable(PropertyTable propertyIn)
        {
            DirectoryInfo info = _directoryTable.CreateSubdirectory(propertyIn.NameTable);
            Record record = FileSerializer.Serialize<PropertyTable>(propertyIn);
            this.WriteData(FunctionFile.FullFileName(info.FullName, 
                propertyIn.NameTable, TypeFormat.TBL), record);
            
            var table = new Table(info, propertyIn);
            _childTables.Add(table.Name, table);
            return table;
        }
        public Table CreateChildTable(string nameIn)
        {
            var property = new PropertyTable() {NameTable = nameIn};
            return this.CreateChildTable(property);
        }
        public Table CreateChildRecord<In>(In objectIn)
        {
            var record = FileSerializer.Serialize(objectIn);
            return this.CreateChildRecord(record);
        }
        public Table CreateChildRecord(Record recordIn)
        {
            var path = this.GetNameFile(recordIn);
            var property = new PropertyTable{NameTable=System.IO.Path.GetFileName(path)};
            return this.CreateChildTable(property);
        }
        public void Initialize()
        {
            if(_isInit)
                throw new Exception("Таблица уже создана");

            DirectoryInfo[] directoryTables = _directoryTable.GetDirectories();
            FileInfo[] files = this.GetFileTable(directoryTables);
            foreach(var file in files)
            {
                if(file == null)
                    continue;
                var record = this.ReadData(file.FullName);
                var property = FileSerializer.Deserialize<PropertyTable>(record);
                var table = new Table(file.Directory!, property);
                _childTables.Add(property.NameTable, table);
                table.Initialize();
            }
            _isInit = true;
        }
        
        #endregion

        private void UpdatePropertyTable()
        {   
            Record record = FileSerializer.Serialize<PropertyTable>(GetProperty());
            this.WriteData(PropertyFileFBD.FullName, record);
        }
        private string GetNameFile(Record recordIn)
        {
            StringBuilder builder = new StringBuilder();
            if(this._isRecordNameTable)
                builder.Append(_name+'_');
            
            if(recordIn.IsIndex)
                builder.Append(recordIn.IndexElement.Value);
            else
                builder.Append(CountRecord);

            return FunctionFile.FullFileName(_directoryTable.FullName,
                    builder.ToString(), File.TypeFormat.TXT); 
        }
        private PropertyTable GetProperty()
        {
            var property = new PropertyTable { NameTable = _name,
                LastUpdate = DateTime.Now, IsNameTable= _isRecordNameTable};
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
        private void DeleteData(string pathIn)
        {
            var info = new FileInfo(pathIn);
            if(!info.Exists)
                throw new ArgumentException(nameof(pathIn));
            
            info.Delete();
        }
        private void AppendData(string pathIn, string value)
        {
            var writer = new AppendFileText(pathIn);
            writer.Write(value);
        }

        private FileInfo[] GetFileTable(DirectoryInfo[] directories)
        {
            var files = new FileInfo[directories.Length];

            for (int index = 0; index < directories.Length; index++)
            {
                var directory = directories[index];
                var tmp = directory.GetFiles(FileDatabase.SETTING_TABLE, SearchOption.TopDirectoryOnly);
                if (tmp.Length == 0)
                    continue;
                files[index] = tmp.First();
            }
            return files;
        }

        private List<Record> GiveAllRecord(ParameterSearch parameterIn)
        {
            var files = this.Files;
            if(parameterIn.Skip > files.Length)
                return new List<Record>();

            List<Record> list = new List<Record>();
            for(int index =parameterIn.Skip; index < files.Length; index++)
            {
                if(index - parameterIn.Skip >= parameterIn.Take)
                    break;

                list.Add(this.ReadData(files[index].FullName));
            }
            return list;
        }
        private List<Record> GiveFilterRecord(RecordSearch recordSearch, ParameterSearch parameterIn)
        {
            var files = this.Files;
            if(parameterIn.Skip > files.Length)
                return new List<Record>();

            int count = 0;
            List<Record> list = new List<Record>();
            for(int index =parameterIn.Skip; index < files.Length; index++)
            {
                if(count >= parameterIn.Take)
                    break;
                
                //Console.WriteLine(files[index].FullName);
                var record = this.ReadData(files[index].FullName);
                if(recordSearch.IsCondition(record))
                {
                    count++;
                    list.Add(record);
                }
            }
            //Console.WriteLine(list.Count);
            return list;
        }
        private Record? GiveOneRecord(RecordSearch recordSearch)
        {
            var files = this.Files;
            for(int index = 0; index < files.Length; index++)
            {
                var record = this.ReadData(files[index].FullName);
                if(recordSearch.IsCondition(record))
                    return record;
            }
            return null;
        }
        
    }
}