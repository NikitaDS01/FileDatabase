namespace FileDB.Core.Data.TypeElement
{
    public class FiledInt : AbstractRecordField
    {
        private readonly int _value;
        public FiledInt(string nameIn,int valueIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
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
            if(!(fieldIn is FiledInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value == (int)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FiledInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value > (int)fieldIn.Value;
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FiledInt) || fieldIn.Name != this.Name)
                return false;
            else
                return _value < (int)fieldIn.Value;
        }
    }
}