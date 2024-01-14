namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        public int LastIndex { get; set;}
        public string NameTable { get; set;}
        public string ParentTable { get; set;}
        public PropertyTable(int indexIn, string name, string parentTable)
        {
            LastIndex = indexIn;
            NameTable = name;
            ParentTable = parentTable;
        }
    }
}