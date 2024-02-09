using FileDB.Core.Data;

namespace FileDB.Function
{
    public static class ConvertData
    {
        public static int ToInt(RecordField elementIn)
        {
            try
            {
                return Convert.ToInt32(elementIn.Value);
            }
            catch (FormatException)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является Int");
            }
        }
        public static float ToFloat(RecordField elementIn)
        {
            try
            {
                return Convert.ToSingle(elementIn.Value);
            }
            catch (FormatException)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является Float");
            }
        }
        public static DateTime ToDateTime(RecordField elementIn)
        {
            try
            {
                return Convert.ToDateTime(elementIn.Value);
            }
            catch (FormatException)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является DateTime");
            }
        }
    }
}
