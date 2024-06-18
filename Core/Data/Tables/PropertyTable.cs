using FileDB.Core.Attribute;

namespace FileDB.Core.Data.Tables
{
    public struct PropertyTable
    {
        [SerializeNameRecord(Name = "Название таблицы")]
        public string NameTable { get; set;} = string.Empty;
        [SerializeNameRecord(Name = "Создана")]
        public DateTime CreateTable { get; set; } = DateTime.Now;
        [SerializeNameRecord(Name = "Обновлен")]
        public DateTime LastUpdateTable { get; set; } = DateTime.Now;

        [SerializeNameRecord(Name = "Указывать таблицу")]
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