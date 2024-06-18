using System.Collections;
using FileDB.Core.Data;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorLink : IConstructorField
    {
        private const string TYPE = "Link";
        public IList GetArrayType(int countIn)
        {
            return new string[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(RecordLink))
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
            string[] split = valueIn.Split(' ');
            return new FieldLinkObject(nameIn, split[0]);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldLinkObject(nameIn, (RecordLink)valueIn);
        }   
    }
}