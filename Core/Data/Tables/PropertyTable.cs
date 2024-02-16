namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        public int CountRecord { get; set;}
        public string NameTable { get; set;}
        public DateTime LastUpdate {get;set;}
        //public bool IsObjectId {get;set;}
        public PropertyTable()
        {
            CountRecord = 0;
            NameTable = string.Empty;
            LastUpdate = DateTime.Now;
          //  IsObjectId = false;
        }
        public PropertyTable(int indexIn, string name, 
            DateTime lastUpdateIn, bool isObjectIdIn)
        {
            CountRecord = indexIn;
            NameTable = name;
            LastUpdate = lastUpdateIn;
            //IsObjectId = isObjectIdIn;
        }
    }
}