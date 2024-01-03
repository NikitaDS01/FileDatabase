using FileDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDB.Function
{
    public static class ConvertData
    {
        public static int ToInt(Element elementIn)
        {
            try
            {
                return Convert.ToInt32(elementIn.Value);
            }
            catch (FormatException ex)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является Int");
            }
        }
        public static float ToFloat(Element elementIn)
        {
            try
            {
                return Convert.ToSingle(elementIn.Value);
            }
            catch (FormatException ex)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является Float");
            }
        }
        public static DateTime ToDateTime(Element elementIn)
        {
            try
            {
                return Convert.ToDateTime(elementIn.Value);
            }
            catch (FormatException ex)
            {
                throw new Exception($"У элемента {nameof(elementIn)} значение не является DateTime");
            }
        }
    }
}
