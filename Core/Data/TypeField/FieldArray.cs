using FileDB.Function;
using System.Collections;
using System.Text;

namespace FileDB.Core.Data.TypeField
{
    public class FieldArray : AbstractRecordField
    {
        private IList? _value;
        private int _count = 0;
        private int _length = 0;
        public FieldArray(string nameIn, int count, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _count = 0;
            _length = count;
            _value = null;
        }
        public FieldArray(string nameIn, IList objects, bool isIndexIn = false)
            : base(nameIn, isIndexIn)
        {
            var type = objects[0].GetType();
            _value = FunctionField.GetArrayType(type, objects.Count);
            
            for(int index = 0; index < objects.Count; index++)
            {
                _value[index] = objects[index];
            }
            _count = objects.Count;
        }

        public override object Value => _value;
        public int Length => _length; 
        public void AddField(object field)
        {
            if(_value == null && 
                FunctionField.TypeIsDefault(field.GetType()))
                _value = FunctionField.GetArrayType(field.GetType(), _length);
     
            _value[_count] = field;
            _count++;
        }
        public float Sum()
        {
            if(Length < 1)
                return 0.0f;
            if(Length == 1)
                return System.Convert.ToSingle(_value[0]);

            float sum = 0;
            var type = _value[0].GetType();
            if(type != typeof(int) && type != typeof(float))
                throw new TypeAccessException(nameof(type));
            
            for(int index = 0; index < Length; index++)
                sum += System.Convert.ToSingle(_value[index]);
                
            return sum;
        }
        public float Average()
        {
            if(Length < 1)
                return 0.0f;
            if(Length == 1)
                return System.Convert.ToSingle(_value[0]);

            float sum = 0;
            var type = _value[0].GetType();
            if(type != typeof(int) || type != typeof(float))
                throw new TypeAccessException(nameof(type));
            
            for(int index = 0; index < Length; index++)
                sum += System.Convert.ToSingle(_value[index]);
                
            return sum / Length;
        }
        public override string Convert()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"[{Name}][Array]:[{Length}]");
            for (int index = 0; index < _value.Count; index++)
            {
                var name = $"{Name}{index}";
                var field = FunctionField.ValueToField(name, _value[index], false).Convert();
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
