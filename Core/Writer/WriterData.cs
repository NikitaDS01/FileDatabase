using FileDB.Core.Data;

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
