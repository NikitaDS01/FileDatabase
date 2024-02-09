namespace FileDB.Core.Data.TypeElement
{
    public class FieldDateTime : AbstractRecordField
    {
        private readonly DateTime _value;
        public FieldDateTime(string nameIn,DateTime valueIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _value = valueIn;
        }

        public override object Value => _value;

        public override string Convert()
        {
            return $"[{Name}][DateTime]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldDateTime) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (DateTime)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldDateTime) || fieldIn.Name != this.Name)
                return false;
            else
                return _value > (DateTime)fieldIn.Value;
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldDateTime) || fieldIn.Name != this.Name)
                return false;
            else
                return _value < (DateTime)fieldIn.Value;
        }
    }
}