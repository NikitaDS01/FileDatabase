using FileDB.Core.File;

namespace FileDB.Core.Writer
{
    public interface IWriter
    {
        void Write(string value);
        void WriteAsync(string value);
    }
}
