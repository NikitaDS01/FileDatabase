namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        public string NameTable { get; set;} = string.Empty;
        public DateTime LastUpdate {get;set;} = DateTime.Now;
        public bool IsNameTable {get;set;} = true;
        public PropertyTable()
        {
            NameTable = string.Empty;
            LastUpdate = DateTime.Now;
            IsNameTable = true;
        }
        public PropertyTable(string name, 
            DateTime lastUpdateIn, bool isNameTableIn)
        {
            NameTable = name;
            LastUpdate = lastUpdateIn;
            IsNameTable = isNameTableIn;
        }
    }
}