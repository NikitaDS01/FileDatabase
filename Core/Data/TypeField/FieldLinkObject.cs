using FileDB.Core.Data;

namespace FileDB.Core.Data.TypeField
{
    public class FieldLinkObject : AbstractRecordField
    {
        private string _tableName;
        private string _number;
        public FieldLinkObject(string nameIn, string tableIn, string numberIn) 
            : base(nameIn, false)
        {
            _tableName = tableIn;
            _number = numberIn;            
        }
        public FieldLinkObject(string nameIn, RecordLink linkIn)
            : base(nameIn, false)
        {
            _tableName = linkIn.TableName;
            _number = linkIn.Number;
        }
        public RecordLink Link => new RecordLink(_tableName, _number);
        public override object Value => this.Link;

        public override string Convert()
        {
            return $"[{Name}][Link]:[{_tableName} {_number}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldLinkObject) || fieldIn.Name != this.Name)
                return false;
            else
                return (string)Value == (string)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }
    }
}