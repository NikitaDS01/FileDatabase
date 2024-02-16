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
    public class ConstructorInt : IConstructorField
    {
        private const string TYPE = "Int";
        public IList GetArrayType(int countIn)
        {
            return new int[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if(typeIn == typeof(int))
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
            return new FieldInt(nameIn, Convert.ToInt32(valueIn), isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldInt(nameIn, (int)valueIn, isIndex);
        }
    }
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
    public class ConstructorLink : IConstructorField
    {
        private const string TYPE = "Link";
        public IList GetArrayType(int countIn)
        {
            return new string[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
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
            return new FieldLinkObject(nameIn, valueIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldLinkObject(nameIn, (string)valueIn);
        }   
    }
}