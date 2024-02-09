using System.Collections;
using System.Text;

namespace FileDB.Core.Data
{
    public class Record : IEnumerable
    {
        private readonly Element[] _elements;
        private readonly int _indexElement;

        public Record(Element[] elements)
        {
            _elements = elements;
            _indexElement = -1;
            for(int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i].IsIndexElement)
                {
                    _indexElement = i;
                    break;
                }
            }
        }

        public int Length => _elements.Length;
        public bool IsIndex => _indexElement != -1;
        public Element IndexElement => _elements[_indexElement];

        public Element this[int index]
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
        public bool TryGetElement(string nameIn, out Element? elementOut)
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
                builder.AppendLine(element.Convert());
            return builder.ToString();
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var element in _elements) 
                builder.AppendLine(element.ToString());
            return builder.ToString();
        }
    }
}
