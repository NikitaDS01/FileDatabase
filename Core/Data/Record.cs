using System.Collections;
using System.Text;
using FileDB.Core.Data.TypeField;

namespace FileDB.Core.Data
{
    public class Record : IEnumerable
    {
        private readonly AbstractRecordField[] _elements;
        private readonly int _indexElement;

        public Record(params AbstractRecordField[] elements)
        {
            _elements = elements;
            _indexElement = -1;
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].IsIndex)
                {
                    _indexElement = i;
                    break;
                }
            }
        }

        public int Length => _elements.Length;
        public bool IsIndex => _indexElement != -1;
        public AbstractRecordField IndexElement => _elements[_indexElement];

        public AbstractRecordField this[int index]
        {
            get => _elements[index];
        }
    
        public IEnumerator GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public bool ContainField(string nameIn)
        {
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].Name == nameIn)
                    return true;
            }
            return false;
        }
        public AbstractRecordField GetField(string nameIn)
        {
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].Name == nameIn)
                {
                    return _elements[i];
                }
            }
            throw new ArgumentNullException(nameof(nameIn));
        }
        public bool TryGetField(string nameIn, out AbstractRecordField? elementOut)
        {
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].Name == nameIn)
                {
                    elementOut = _elements[i];
                    return true;
                }
            }
            elementOut = null;
            return false;
        }
        public bool TryGetField<Out>(string nameIn, out Out? elementOut) where Out : AbstractRecordField
        {
            elementOut = null;
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].Name == nameIn && _elements[i] is Out)
                {
                    elementOut = (Out)_elements[i];
                    return true;
                }
            }
            return false;
        }
        public bool Modification(Record newRecordIn)
        {
            if(!IndexElement.EqualsField(newRecordIn.IndexElement))
                return false;

            var newRecord = this;
            AbstractRecordField? tmpField;
            for(int index = 0; index < newRecord.Length; index++)
            {
                var field = newRecord[index];
                if(field.IsIndex)
                    continue;
                
                if(newRecordIn.TryGetField(field.Name, out tmpField))
                    newRecord._elements[index] = tmpField!;
                else
                    return false;
            }
            for(int index = 0; index < newRecord.Length; index++)
            {
                _elements[index] = newRecord[index];
            }
            return true;
        }
        public string ConvertData()
        {
            var builder = new StringBuilder();
            foreach (var element in _elements)
            {
                if(element.IsIndex)
                    builder.AppendLine("*"+element.Convert());
                else
                    builder.AppendLine(element.Convert());
            }
            return builder.ToString();
        }
        public override string ToString()
        {
            return ConvertData();
        }
    }
}
