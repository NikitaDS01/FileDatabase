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
    }
}
