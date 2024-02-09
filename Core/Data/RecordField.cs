using FileDB.Function;

namespace FileDB.Core.Data
{
    public class RecordField
    {
        private readonly string _name;
        private readonly TypeValue _type;
        private readonly string _value = null!;
        private readonly bool _isIndexField;

        public RecordField(string name, TypeValue type, string value, bool isIndexIn = false)
        {
            _isIndexField = isIndexIn;
            _name = name;
            _type = type;
            _value = value;
        }
        public RecordField(string name, TypeValue type, object value, bool isIndexIn = false )
        {
            if(value == null) throw new ArgumentNullException(nameof(value));
            
            _isIndexField = isIndexIn;
            _name = name;
            _type = type;
            _value = value.ToString();
        }
        public string Name => _name;
        public TypeValue Type => _type;
        public string Value => _value;
        public bool IsIndexElement => _isIndexField;

        public object GetValue()
        {
            switch(Type)
            {
                case TypeValue.Text: return _value;
                case TypeValue.Int: return ConvertData.ToInt(this);
                case TypeValue.Float: return ConvertData.ToFloat(this);
                case TypeValue.DateTime: return ConvertData.ToDateTime(this);
                default: return _value;
            }
        }
        public string Convert()
        {
            return $"[{Name}][{Type}]:[{Value}]";
        }
        public override string ToString()
        {
            return $"{Name}:{Value}";
        }
        public override bool Equals(object? obj)
        {
            if(obj == null || !(obj is RecordField))
            {
                return false;
            }
            else
            {
                var element = obj as RecordField;
                return this.Name == element.Name &&
                    this.Type == element.Type &&
                    this.Value == element.Value;
            }
        }
        public static bool operator ==(RecordField el1, RecordField el2)
        {
            return el1.Name == el2.Name;
        }
        public static bool operator !=(RecordField el1, RecordField el2)
        {
            return el1.Name != el2.Name;
        }
        public static bool IsEqualsType(RecordField el1, RecordField el2)
        {
            return el1.Type == el2.Type;
        }
    }
}
