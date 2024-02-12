using FileDB.Function;
using System;
using System.Collections;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileDB.Core.Data.TypeField
{
    public class FieldArray : AbstractRecordField
    {
        private object[] _value;
        private int _count = 0;
        private int _length;
        public FieldArray(string nameIn, int count, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _length = count;
            _count = count;
            _value = new object[_length];
        }
        public FieldArray(string nameIn, ICollection objects, bool isIndexIn = false)
            : base(nameIn, isIndexIn)
        {
            _value = new object[objects.Count];
            var type = _value[0].GetType();
            for(int index = 0; index < objects.Count; index++)
            {
                var obj = objects.OfType<object>().ElementAtOrDefault(index);
                this.AddField(obj);
            }
            _length = objects.Count;
        }

        public override object Value => (ICollection)_value;
        public int Length => _length; 
        public void AddField(object field)
        {
            _value[_count] = field;
            _count++;
        }
        public override string Convert()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"[{Name}][Array]:[{Length}]");
            for (int index = 0; index < _value.Length; index++)
            {
                var name = $"{Name}{index}";
                var field = ConvertEnum.ValueToField(name, _value[index], false).Convert();
                builder.AppendLine(field);
            }
            return builder.ToString();
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if (fieldIn is not FieldArray array || fieldIn.Name != this.Name)
                return false;
            else
                return this.Length == array.Length;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }
    }
}
