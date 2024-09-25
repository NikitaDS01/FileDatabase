using FileDB.Core.File;

namespace FileDB.Core.Reader
{
    public interface IReader
    {
        DataFile Read();
        Task<DataFile> ReadAsync();
    }
}
