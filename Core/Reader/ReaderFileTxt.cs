using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDB.Core.Reader
{
    public class ReaderFileTxt : IReader
    {
        private readonly string _path;
        public ReaderFileTxt(FileInfo fileInfoIn)
        {
            _path = fileInfoIn.FullName;
        }
        public ReaderFileTxt(string pathIn)
        {
            if (string.IsNullOrEmpty(pathIn))
                throw new NullReferenceException(nameof(pathIn));

            _path = pathIn;
        }
        public string Read()
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                file.Read(buffer, 0, buffer.Length);

                return Encoding.Default.GetString(buffer);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
            return string.Empty;
        }
        public async Task<string> ReadAsync()
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(_path, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                await file.ReadAsync(buffer, 0, buffer.Length);

                return Encoding.Default.GetString(buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
            return string.Empty;
        }
    }
}
