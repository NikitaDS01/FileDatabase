using FileDB.Core;

namespace FileDB.Function
{
    public static class ConvertEnum
    {
        public static string ToString(Enum valueIn)
        {
            return valueIn.ToString();
        }
        public static Out ToEnum<Out>(string valueIn)
        {
            return (Out)Enum.Parse(typeof(Out), valueIn, true);
        }
        public static TypeValue ToValue(System.Type typeIn)
        {
            switch (true)
            {
                case bool _ when typeIn == typeof(string): return TypeValue.Text;
                case bool _ when typeIn == typeof(int): return TypeValue.Int;
                case bool _ when typeIn == typeof(float): return TypeValue.Float;
                case bool _ when typeIn == typeof(DateTime): return TypeValue.DateTime;
                default: return TypeValue.None;
            }
        }
        public static bool IsDefaultValue(System.Type typeIn)
        {
            switch (true)
            {
                case bool _ when typeIn == typeof(string): return true;
                case bool _ when typeIn == typeof(int): return true;
                case bool _ when typeIn == typeof(float): return true;
                case bool _ when typeIn == typeof(DateTime): return true;
                default: return false;
            }
        }
    }
}
