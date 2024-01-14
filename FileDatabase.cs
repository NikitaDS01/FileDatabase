using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using FileDB.Core.File;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;
using FileDB.Serialization;

namespace FileDB
{
    public class FileDatabase
    {
        private const string SETTING_TABLE = ".settingfdb";
        public class SettingDatabase
        {
            public string Path { get; set; } = string.Empty;
        }

        private DirectoryInfo _rootDirectory;
        private Dictionary<string, Table> _tables;
        private bool _isLoadDatabase;
        public FileDatabase()
        {
            _isLoadDatabase = false;
            _tables = new Dictionary<string, Table>();
            _rootDirectory = null!;
        }
        public IReadOnlyCollection<Table> Tables => _tables.Values;
        public Table RootTable => _tables.Values.First(table => table.IsRoot);
        public Table this[string name] => _tables[name];
        
        public void Initialize(string pathIn)
        {
            this.Initialize(new SettingDatabase(){Path = pathIn});
        }
        public void Initialize(SettingDatabase settingIn)
        {
            _rootDirectory = new DirectoryInfo(settingIn.Path);
            this.LoadTables();
        }

        private void LoadTables()
        {
            FileInfo[] files = _rootDirectory.GetFiles(SETTING_TABLE, SearchOption.AllDirectories);
            foreach(var file in files)
            {
                var record = this.ReadData(file.FullName);
                var property = FileSerializer.Deserialize<PropertyTable>(record);
                Table parentTable = null;
                if(_tables.ContainsKey(property.ParentTable))
                    parentTable = _tables[property.ParentTable];

                var table = new Table(file.Directory, property, parentTable);
                _tables.Add(property.NameTable, table);
            }
        }
        public Record ReadData(string pathIn)
        {
            var reader = new ReaderFileTxt(pathIn);
            string data = reader.Read();
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(pathIn));
            return ReaderData.Read(data);
        }
        public void WriteData(string pathIn, Record recordIn)
        {
            var writer = new WriterFileTxt(pathIn);
            string data = WriterData.Write(recordIn);
            writer.Write(data);
        }
    }
}