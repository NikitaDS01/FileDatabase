using FileDB.Core.Data;
using FileDB.Core.File;
using FileDB.Core.Reader;
using FileDB.Core.Writer;
using FileDB.Function;

namespace FileDB
{
    public class FileDatabase
    {
        public class SettingDatabase
        {
            public string Path { get; set; } = string.Empty;
        }

        private readonly string _path;
        private readonly DirectoryInfo _directory;
        public FileDatabase(string path) : this(new SettingDatabase { Path = path }) 
        { }

        public FileDatabase(SettingDatabase settingIn)
        {
            if(string.IsNullOrEmpty(settingIn.Path))
                throw new NullReferenceException(nameof(settingIn.Path));

            _path = settingIn.Path;
            if(!ExistsDatabase)
                throw new FileNotFoundException(nameof(settingIn.Path));

            _directory = new DirectoryInfo(_path);
        }

        public bool ExistsDatabase => Directory.Exists(_path);
        public string[] PathDirectories => Directory.GetDirectories(_path);
        public string[] PathFiles => Directory.GetFiles(_path);
        public string[] PathDirectoriesAndFiles => Directory.GetFileSystemEntries(_path);
        public FileInfo[] Files => _directory.GetFiles();
        public DirectoryInfo[] Directories => _directory.GetDirectories();

        public FileInfo? TryFindFile(string nameFileIn, TypeFormat formatIn = TypeFormat.TXT)
        {
            string findFile = FunctionFile.FileName(nameFileIn, formatIn);
            foreach(FileInfo file in Files)
            {
                if(file.Name == findFile)
                    return file;
            }
            return null;
        }
        public DirectoryInfo? TryFindDirectory(string nameDirectoryIn)
        {
            foreach (DirectoryInfo directory in Directories)
            {
                if (directory.Name == nameDirectoryIn)
                    return directory;
            }
            return null;
        }
        public Record ReadData(string typeIn, string nameIn, TypeFormat formatIn)
        {
            return this.ReadData(new NameRecord(typeIn, nameIn, formatIn));
        }
        public Record ReadData(NameRecord nameFileIn)
        {
            string path = _path + '\\' + nameFileIn.GetName();
            var reader = new ReaderFileTxt(path);
            string data = reader.Read();
            if (string.IsNullOrEmpty(data))
                throw new NullReferenceException(nameof(path));

            return ReaderData.Read(data);
        }
        public void WriteData(string typeIn, string nameIn, TypeFormat formatIn, Record recordIn)
        {
            this.WriteData(new NameRecord(typeIn, nameIn, formatIn), recordIn);
        }
        public void WriteData(NameRecord nameFileIn, Record recordIn)
        {
            string path = _path + '\\' + nameFileIn.GetName();
            string data = WriterData.Write(recordIn);

            var reader = new WriterFileTxt(path);
            reader.Write(data);
        }
    }
}