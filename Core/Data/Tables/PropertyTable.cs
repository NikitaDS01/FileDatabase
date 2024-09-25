using FileDB.Core.Attribute;

namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        [SerializeNameRecord(Name = "�������� �������")]
        public string NameTable { get; set;} = string.Empty;
        [SerializeNameRecord(Name = "�������")]
        public DateTime CreateTable { get; set; } = DateTime.Now;
        [SerializeNameRecord(Name = "��������")]
        public DateTime LastUpdateTable { get; set; } = DateTime.Now;

        [SerializeNameRecord(Name = "��������� �������")]
        public bool IsNameTable { get; set; } = true;


        public PropertyTable()
        {
            NameTable = string.Empty;
            LastUpdateTable = DateTime.Now;
            CreateTable = DateTime.Now;
            IsNameTable = true;
        }
        public PropertyTable(string name, DateTime lastUpdateIn,
            DateTime createIn, bool isNameTableIn)
        {
            NameTable = name;
            LastUpdateTable = lastUpdateIn;
            CreateTable = createIn;
            IsNameTable = isNameTableIn;
        }
    }
}