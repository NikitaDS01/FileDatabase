using FileDB.Core.Data;

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
            return TryAdd(name, value.GetType(), value,false);
        }
        public bool TryAdd(string name, System.Type typeIn, object value,bool isIndex)
        {
            if(value == null)
                return false;
            if (typeIn != value.GetType())
                return false;
            var type = ConvertEnum.ToValue(typeIn);
            if (type == TypeValue.None )
                return false;

            _elements.Add(new Element(name, type, value, isIndex));
            return true;
        }
        public Record GetRecord()
        {
            return new Record(_elements.ToArray());
        }
    }
}
