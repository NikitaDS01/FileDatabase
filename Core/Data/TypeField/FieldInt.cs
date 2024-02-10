namespace FileDB.Core.Data.TypeField
{
    public class FieldInt : AbstractRecordField
    {
        private readonly int _value;
        public FieldInt(string nameIn,int valueIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _value = valueIn;
        }

        public override object Value => _value;

        public override string Convert()
        {
            return $"[{Name}][Int]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (int)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value > (int)fieldIn.Value;
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value < (int)fieldIn.Value;
        }
    }
}