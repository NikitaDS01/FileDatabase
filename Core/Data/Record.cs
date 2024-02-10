using System.Collections;
using System.Text;
using FileDB.Core.Data.TypeField;

namespace FileDB.Core.Data
{
    public class Record : IEnumerable
    {
        private readonly AbstractRecordField[] _elements;
        private readonly int _indexElement;

        public Record(AbstractRecordField[] elements)
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
            var builder = new StringBuilder();
            foreach (var element in _elements) 
                builder.AppendLine(element.Convert());
            return builder.ToString();
        }
    }
}
