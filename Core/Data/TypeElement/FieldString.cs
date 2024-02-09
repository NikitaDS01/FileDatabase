namespace FileDB.Core.Data.TypeElement
{
    public class FieldString : AbstractRecordField
    {
        private readonly string _value;
        public FieldString(string nameIn, string valueIn, bool isIndexIn = false) 
            : base(nameIn, isIndexIn)
        {
            _value = valueIn;
        }
        public override object Value => _value;
        public override string Convert()
        {
            return $"[{Name}][Text]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldString) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (string)fieldIn.Value;
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