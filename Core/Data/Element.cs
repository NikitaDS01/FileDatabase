﻿using FileDB.Function;

namespace FileDB.Core.Data
{
    public class Element
    {
        private readonly string _name;
        private readonly TypeValue _type;
        private readonly string _value = null!;
        private readonly bool _isIndexField;

        public Element(string name, TypeValue type, string value, bool isIndexIn = false)
        {
            _isIndexField = isIndexIn;
            _name = name;
            _type = type;
            _value = value;
        }
        public Element(string name, TypeValue type, object value, bool isIndexIn = false )
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
    }
}
