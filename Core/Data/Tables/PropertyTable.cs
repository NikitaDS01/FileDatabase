namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        public int LastIndex { get; set;}
        public string NameTable { get; set;}
        public DateTime LastUpdate {get;set;}
        public PropertyTable()
        {
            LastIndex = 0;
            NameTable = string.Empty;
            LastUpdate = DateTime.Now;
        }
        public PropertyTable(int indexIn, string name, 
            DateTime lastUpdateIn)
        {
            LastIndex = indexIn;
            NameTable = name;
            LastUpdate = lastUpdateIn;
        }
    }
}