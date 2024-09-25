using System.Collections;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorFloat : IConstructorField
    {
        private const string TYPE = "Float";
        public IList GetArrayType(int countIn)
        {
            return new float[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(float))
                return true;
            return false;
        }
        public bool IsDefaultValue(string typeIn)
        {
            if(typeIn == TYPE)
                return true;
            return false;
        }

        public AbstractRecordField StringToField(string nameIn, string valueIn, bool isIndexIn)
        {
            return new FieldFloat(nameIn, Convert.ToSingle(valueIn), isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldFloat(nameIn, (float)valueIn, isIndex);
        }
    }
}