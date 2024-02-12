using FileDB.Core.Data;
using FileDB.Core.Data.TypeField;
using System;
using System.Collections;

namespace FileDB.Function
{
    public static class ConvertEnum
    {
        private enum TypeValue
        {
            None = -1,
            Text = 1,
            Int = 2,
            Float = 4,
            DateTime = 8,
            Bool = 16,
            Array = 32
        }
        public static string ToString(Enum valueIn)
        {
            return valueIn.ToString();
        }
        public static Out ToEnum<Out>(string valueIn)
        {
            return (Out)Enum.Parse(typeof(Out), valueIn, true);
        }
        public static AbstractRecordField StringToField(
            string nameIn, string typeIn, string valueIn, bool isIndexIn)
        {
            var type = ToEnum<TypeValue>(typeIn);
            switch (type)
            {
                case TypeValue.Text: 
                    return new FieldString(nameIn, valueIn, isIndexIn);
                case TypeValue.Int:
                    return new FieldInt(nameIn, Convert.ToInt32(valueIn), isIndexIn);
                case TypeValue.Float:
                    return new FieldFloat(nameIn, Convert.ToSingle(valueIn), isIndexIn);
                case TypeValue.DateTime: 
                    return new FieldDateTime(nameIn, Convert.ToDateTime(valueIn), isIndexIn);
                case TypeValue.Bool:
                    return new FieldBoolean(nameIn, Convert.ToBoolean(valueIn), isIndexIn);
                case TypeValue.Array:
                    return new FieldArray(nameIn, Convert.ToInt32(valueIn), isIndexIn);

                default: throw new ArgumentException(nameof(valueIn));
            }
        }
        public static AbstractRecordField ValueToField( 
            string nameIn, object valueIn, bool isIndex)
        {
            var type = valueIn.GetType();
            switch (true)
            {
                case bool _ when type == typeof(string): 
                    return new FieldString(nameIn, (string)valueIn, isIndex);
                case bool _ when type == typeof(int): 
                    return new FieldInt(nameIn, (int)valueIn, isIndex);
                case bool _ when type == typeof(float): 
                    return new FieldFloat(nameIn, (float)valueIn, isIndex);
                case bool _ when type == typeof(DateTime): 
                    return new FieldDateTime(nameIn, (DateTime)valueIn, isIndex);
                case bool _ when type == typeof(bool):
                    return new FieldBoolean(nameIn, (bool)valueIn, isIndex);
                case bool _ when type.GetInterfaces().Contains(typeof(ICollection)):
                    return new FieldArray(nameIn, (ICollection)valueIn, isIndex);

                default: throw new ArgumentException(nameof(valueIn));
            }
        }
        public static bool IsDefaultValue(System.Type typeIn)
        {
            switch (true)
            {
                case bool _ when typeIn == typeof(string): 
                    return true;
                case bool _ when typeIn == typeof(int): 
                    return true;
                case bool _ when typeIn == typeof(float): 
                    return true;
                case bool _ when typeIn == typeof(DateTime): 
                    return true;
                case bool _ when typeIn == typeof(bool): 
                    return true;
                case bool _ when typeIn.GetInterfaces().Contains(typeof(ICollection)):
                    return true;
                default: return false;
            }
        }
        public static object[] GetArrayType(System.Type typeIn, int countIn)
        {
            switch (true)
            {
                case bool _ when typeIn == typeof(string):
                    return new string[countIn];
                case bool _ when typeIn == typeof(int):
                    return new int[countIn];
                case bool _ when typeIn == typeof(float):
                    return new float[countIn];
                case bool _ when typeIn == typeof(DateTime):
                    return new DateTime[countIn];
                case bool _ when typeIn == typeof(bool):
                    return new bool[countIn];

                default: throw new ArgumentException(nameof(valueIn));
            }
        }
    }
}
