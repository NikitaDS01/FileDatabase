using FileDB.Core.Data;

namespace FileDB.Core.Data.TypeField
{
    public class FieldLinkObject : AbstractRecordField
    {
        private RecordLink _link;
        public FieldLinkObject(string nameIn, string pathIn, string numberIn) 
            : base(nameIn, false)
        {
            _link = new RecordLink(pathIn);      
        }
        public FieldLinkObject(string nameIn, RecordLink linkIn)
            : base(nameIn, false)
        {
            _link = linkIn;
        }
        public RecordLink Link => _link;
        public override object Value => this.Link;

        public override string Convert()
        {
            return $"[{Name}][Link]:[{_link.FullName}]";
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