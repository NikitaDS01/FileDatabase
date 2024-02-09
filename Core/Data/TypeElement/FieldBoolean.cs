namespace FileDB.Core.Data.TypeElement
{
    public class FiledBoolean : AbstractRecordField
    {
        private readonly bool _value;
        public FiledBoolean(string nameIn, bool valueIn, bool isIndexIn = false) 
            : base(nameIn, isIndexIn)
        {
            _value = valueIn;
        }
        public override object Value => _value;
        public override string Convert()
        {
            return $"[{Name}][Bool]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FiledBoolean) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (bool)fieldIn.Value;
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