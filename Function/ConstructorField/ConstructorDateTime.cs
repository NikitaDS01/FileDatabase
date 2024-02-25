using System.Collections;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorDateTime : IConstructorField
    {
        private const string TYPE = "DateTime";
        public IList GetArrayType(int countIn)
        {
            return new DateTime[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(DateTime))
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
            return new FieldDateTime(nameIn, Convert.ToDateTime(valueIn), isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldDateTime(nameIn, (DateTime)valueIn, isIndex);
        }
    }
}