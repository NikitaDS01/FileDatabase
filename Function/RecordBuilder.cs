using FileDB.Core;

namespace FileDB.Function
{
    public class RecordBuilder
    {
        private readonly List<Element> _elements;
        public RecordBuilder()
        {
            _elements = new List<Element>();
        }
        public bool TryAdd(string name, object value)
        {
            var type = ConvertEnum.ToValue(value.GetType());
            if (type == TypeValue.None) 
                return false;

            _elements.Add(new Element(name, type, value));
            return true;
        }
        public bool TryAdd(string name, System.Type typeIn, object value)
        {
            if (typeIn != value.GetType())
                return false;
            var type = ConvertEnum.ToValue(typeIn);
            if (type == TypeValue.None )
                return false;

            _elements.Add(new Element(name, type, value));
            return true;
        }
        public Record GetRecord()
        {
            return new Record(_elements.ToArray());
        }
    }
}
