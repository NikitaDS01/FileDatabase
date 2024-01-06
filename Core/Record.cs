using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileDB.Core
{
    public class Record : IEnumerable
    {
        private readonly Element[] _elements;

        public Record(Element[] elements)
        {
            _elements = elements;
        }
        public Record(int count)
        {
            _elements = new Element[count];
        }

        public int Length => _elements.Length;

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
