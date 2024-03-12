using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using FileDB.Core.File;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;
using FileDB.Serialization;
using System.Reflection;

namespace FileDB
{
    public class FileDatabase
    {
        public const string SETTING_TABLE = "*.tbl";
        public const string SETTING_DATABASE = "*.fdb";
        public class PropertyDatabase
        {
            public string NameDatabase { get; set; } = string.Empty;
            public DateTime CreateDatabase { get; set; } = DateTime.Now;
            public PropertyDatabase()
            {
                NameDatabase = string.Empty;
                CreateDatabase = DateTime.Now;
            }
            public PropertyDatabase(string nameDatabase, DateTime createDatabase)
            {
                NameDatabase = nameDatabase;
                CreateDatabase = createDatabase;
            }
        }

        private string _name;
        private DateTime _createDateTime;
        private DirectoryInfo _databaseDirectory;
        private Dictionary<string, Table> _rootTables;
        private bool _isLoadDatabase;

        public FileDatabase(DirectoryInfo directoryIn)
        {
            _isLoadDatabase = false;
            _rootTables = new Dictionary<string, Table>();
            _databaseDirectory = directoryIn;
        }

        public string Name => _name;
        public Table this[string name] => _rootTables[name];
        public IReadOnlyCollection<Table> RootTables => _rootTables.Values;

        public bool ContainTable(string nameIn)
        {
            Table table;
            foreach(var root in RootTables)
            {
                if(root.TryGetTable(nameIn, out table!))
                    return true;
            }
            return false;
        } 
        public bool TryGetTable(string nameIn, out Table? tableOut)
        {
            tableOut = null;
            foreach(var root in RootTables)
            {
                if(root.TryGetTable(nameIn, out tableOut))
                    return true;
            }
            return false;
        }
        public bool ContainRootTable(string nameIn)
        {
            return _rootTables.ContainsKey(nameIn);
        }
        public Table CreateRootTable(string nameIn)
        {
            var property = new PropertyTable() {NameTable = nameIn};
            return this.CreateRootTable(property);
        }
        public Table CreateRootTable(PropertyTable propertyIn)
        {
            if(string.IsNullOrEmpty(propertyIn.NameTable))
                throw new ArgumentNullException(nameof(propertyIn.NameTable));

            DirectoryInfo info = _databaseDirectory.CreateSubdirectory(propertyIn.NameTable);
            Record record = FileSerializer.Serialize<PropertyTable>(propertyIn);
            this.WriteData(FunctionFile.FullFileName(info.FullName, 
                propertyIn.NameTable, TypeFormat.TBL), record);
            
            var table = new Table(info, propertyIn);
            _rootTables.Add(table.Name, table);
            return table;
        }
        public void CreateDatabase(PropertyDatabase settingIn)
        {
            if (string.IsNullOrEmpty(settingIn.NameDatabase))
                throw new ArgumentNullException(nameof(_name));
            if (_isLoadDatabase == true)
                throw new Exception("База уже создана");

            _databaseDirectory.Create();
            settingIn.CreateDatabase = DateTime.Now;
            this.SetProperty(settingIn);
            this.SetFileDatabase();

            _isLoadDatabase = true;
        }
        public void Initialize()
        {
            if(_isLoadDatabase)
                throw new Exception("Таблица уже создана");

            PropertyDatabase propertyDB = this.GetFileDatabase();
            this.SetProperty(propertyDB);

            DirectoryInfo[] directoryTables = _databaseDirectory.GetDirectories();
            FileInfo[] files = this.GetFileTable(directoryTables);
            foreach(var file in files)
            {
                if(file == null)
                    throw new ArgumentNullException(nameof(file));

                var record = this.ReadData(file.FullName);
                var property = FileSerializer.Deserialize<PropertyTable>(record);
                var table = new Table(file.Directory!, property);
                _rootTables.Add(property.NameTable, table);
                table.Initialize();
            }
            _isLoadDatabase = true;
        }
       
        /// <summary>
        /// Возвращает запись, по ссылочной записи
        /// </summary>
        /// <param name="linkIn">Запись с ссылкой</param>
        /// <returns>Если запись существует, то возвращает её, иначе null</returns>
        public Record? LinkRecord(RecordLink linkIn)
        {
            if(File.Exists(linkIn.FullName))
                return ReadData(linkIn.FullName);
            return null;
        }
        
        private FileInfo[] GetFileTable(DirectoryInfo[] directories)
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
        private Record ReadData(string pathIn)
        {
            var reader = new ReaderFileTxt(pathIn);
            string data = reader.Read();
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(pathIn));
            return ReaderData.Read(data);
        }
        private void WriteData(string pathIn, Record recordIn)
        {
            var writer = new WriterFileTxt(pathIn);
            string data = WriterData.Write(recordIn);
            writer.Write(data);
        }
        private void AppendData(string pathIn, string value)
        {
            var writer = new AppendFileText(pathIn);
            writer.Write(value);
        }
        private PropertyDatabase GetFileDatabase()
        {
            FileInfo[] info = _databaseDirectory.GetFiles(
                SETTING_DATABASE, SearchOption.TopDirectoryOnly);
            if (info.Length == 0)
                throw new Exception("Файл базы данных не найден");

            var file = info.First();
            Record record = this.ReadData(file.FullName);
            PropertyDatabase property = FileSerializer.Deserialize<PropertyDatabase>(record);
            return property;
        }        
        private void SetFileDatabase()
        {
            var property = this.GetProperty();
            var record = FileSerializer.Serialize<PropertyDatabase>(property);
            this.WriteData(FunctionFile.FullFileName(_databaseDirectory.FullName,
                property.NameDatabase, TypeFormat.FDB), record);
        }
        private PropertyDatabase GetProperty()
        {
            var property = new PropertyDatabase
            {
                NameDatabase = _name,
                CreateDatabase = DateTime.Now
            };
            return property;
        }
        private void SetProperty(PropertyDatabase propertyIn)
        {
            _name = propertyIn.NameDatabase;
            _createDateTime = propertyIn.CreateDatabase;
        }
    }
}