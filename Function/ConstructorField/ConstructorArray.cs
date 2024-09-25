using System.Collections;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorArray : IConstructorField
    {
        private const string TYPE = "Array";
        public IList GetArrayType(int countIn)
        {
            return new object[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn.GetInterfaces().Contains(typeof(IList)))
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
            return new FieldArray(nameIn, Convert.ToInt32(valueIn), isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldArray(nameIn, (IList)valueIn, isIndex);
        }
    }
}