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
        private const string SETTING_TABLE = "*.fdb";
        public class SettingDatabase
        {
            public string Path { get; set; } = string.Empty;
        }

        private DirectoryInfo _databaseDirectory;
        private Dictionary<string, Table> _rootTables;
        private bool _isLoadDatabase;
        public FileDatabase(string pathIn) : this(new SettingDatabase{Path = pathIn})
        { }
        public FileDatabase(SettingDatabase settingIn)
        {
            _isLoadDatabase = false;
            _rootTables = new Dictionary<string, Table>();
            _databaseDirectory = new DirectoryInfo(settingIn.Path);
        }
        public Table this[string name] => _rootTables[name];
        
        public bool ContainRootTable(string nameIn)
        {
            return _rootTables.ContainsKey(nameIn);
        }
        public Table CreateRootTable(string nameIn)
        {
            var property = new PropertyTable() {NameTable = nameIn};
            DirectoryInfo info = _databaseDirectory.CreateSubdirectory(nameIn);
            Record record = FileSerializer.Serialize<PropertyTable>(property);
            this.WriteData(FunctionFile.FullFileName(info.FullName, 
                property.NameTable, TypeFormat.FDB), record);
            
            var table = new Table(info, property);
            _rootTables.Add(table.Name, table);
            return table;
        }
        public void Initialize()
        {
            DirectoryInfo[] directoryTables = _databaseDirectory.GetDirectories();
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
    }
}