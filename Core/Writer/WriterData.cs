using FileDB.Core.Data;
using FileDB.Core.File;

namespace FileDB.Core.Writer
{
    public static class WriterData
    {
        public static string Write(Record recordIn)
        {
            return recordIn.ConvertData();
        }
    }
}
