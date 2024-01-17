using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Data;

namespace FileDB.Function
{
    public static class FindMethod
    {
        public static Predicate<Element> Equals(string valueIn)
        {
            return (Element el)=> el.Value == valueIn;
        }
        public static Predicate<Element> NotEquals(string valueIn)
        {
            return (Element el)=> el.Value != valueIn;
        }
        public static Predicate<Element> Large(float valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToFloat(el) > valueIn;
        }
        public static Predicate<Element> Large(int valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToInt(el) > valueIn;
        }
        public static Predicate<Element> LargeOrEquals(float valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToFloat(el) >= valueIn;
        }
        public static Predicate<Element> LargeOrEquals(int valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToInt(el) >= valueIn;
        }
        public static Predicate<Element> Less(float valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToFloat(el) < valueIn;
        }
        public static Predicate<Element> Less(int valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToInt(el) < valueIn;
        }
        public static Predicate<Element> LessOrEquals(float valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToFloat(el) <= valueIn;
        }
        public static Predicate<Element> LessOrEquals(int valueIn)
        {
            return (Element el) => IsNumber(el) && 
                ConvertData.ToInt(el) <= valueIn;
        }
        private static bool IsNumber(Element elementIn)
        {
            return elementIn.Type == TypeValue.Int || 
                elementIn.Type == TypeValue.Float;
        }
    }
}