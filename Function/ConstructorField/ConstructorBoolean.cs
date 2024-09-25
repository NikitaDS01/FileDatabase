using System.Collections;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorBoolean : IConstructorField
    {
        private const string TYPE = "Bool";
        public IList GetArrayType(int countIn)
        {
            return new bool[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(bool))
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
            return new FieldBoolean(nameIn, Convert.ToBoolean(valueIn), isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldBoolean(nameIn, (bool)valueIn, isIndex);
        }
    }
}