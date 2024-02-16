using FileDB.Core.Data;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function
{
    public class RecordBuilder
    {
        private readonly List<AbstractRecordField> _elements;
        public RecordBuilder()
        {
            _elements = new List<AbstractRecordField>();
        }
        public void Add(AbstractRecordField fieldIn)
        {
            _elements.Add(fieldIn);
        }
        public bool TryAdd(string name, object value)
        {
            return TryAdd(name, value,false);
        }
        public bool TryAdd(string name, object value,bool isIndex)
        {
            var type = value.GetType();
            if(value == null)
                return false;
            if(!FunctionField.TypeIsDefault(type))
                return false;

            _elements.Add(FunctionField.ValueToField(name, value, isIndex));            
            return true;
        }
        public Record GetRecord()
        {
            return new Record(_elements.ToArray());
        }
    }
}
