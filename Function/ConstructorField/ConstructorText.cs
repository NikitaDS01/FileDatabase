using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public class ConstructorText : IConstructorField
    {
        private const string TYPE = "Text";
        public IList GetArrayType(int countIn)
        {
            return new string[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(string))
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
            return new FieldString(nameIn, valueIn, isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldString(nameIn, (string)valueIn, isIndex);
        }
    }
}