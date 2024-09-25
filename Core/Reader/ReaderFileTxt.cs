using FileDB.Core.File;
using System.Text;

namespace FileDB.Core.Reader
{
    public class ReaderFileTxt : IReader
    {
        private readonly FileInfo _path;
        public ReaderFileTxt(FileInfo fileInfoIn)
        {
            _path = fileInfoIn;
        }
        public ReaderFileTxt(string pathIn)
        {
            if (string.IsNullOrEmpty(pathIn))
                throw new NullReferenceException(nameof(pathIn));

            _path = new FileInfo(pathIn);
        }
        public DataFile Read()
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path.FullName, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                file.Read(buffer, 0, buffer.Length);

                return new DataFile(_path, Encoding.Default.GetString(buffer));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
            return DataFile.Empty;
        }
        public async Task<DataFile> ReadAsync()
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path.FullName, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                await file.ReadAsync(buffer, 0, buffer.Length);

                return new DataFile(_path, Encoding.Default.GetString(buffer));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
            return DataFile.Empty;
        }
    }
}
