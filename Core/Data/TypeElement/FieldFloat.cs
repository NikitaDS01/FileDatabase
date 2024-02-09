namespace FileDB.Core.Data.TypeElement
{
    public class FieldFloat : AbstractRecordField
    {
        private readonly float _value;
        public FieldFloat(string nameIn, float valueIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _value = valueIn;
        }

        public override object Value => _value;

        public override string Convert()
        {
            return $"[{Name}][Float]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldFloat) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (float)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldFloat) || fieldIn.Name != this.Name)
                return false;
            else
                return _value > (float)fieldIn.Value;
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldFloat) || fieldIn.Name != this.Name)
                return false;
            else
                return _value < (float)fieldIn.Value;
        }
    }
}