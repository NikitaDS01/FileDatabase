using System.Text;

namespace FileDB.Core.Writer
{
    public class WriterFileTxt : IWriter
    {
        private readonly FileInfo _path;
        public WriterFileTxt(FileInfo fileInfoIn)
        {
            _path = fileInfoIn;
        }
        public WriterFileTxt(string pathIn)
        {
            if (string.IsNullOrEmpty(pathIn))
                throw new NullReferenceException(nameof(pathIn));

            _path = new FileInfo(pathIn);
        }
        public void Write(string valueIn)
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path.FullName, FileMode.Create, FileAccess.Write);
                byte[] buffer = Encoding.Default.GetBytes(valueIn);

                file.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
        }

        public async void WriteAsync(string valueIn)
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path.FullName, FileMode.Create, FileAccess.Write);
                byte[] buffer = Encoding.Default.GetBytes(valueIn);

                await file.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
        }
    }
}
