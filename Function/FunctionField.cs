using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Function.ConstructorField;
using FileDB.Core.Data.TypeField;
using System.Collections;

namespace FileDB.Function
{
    public static class FunctionField
    {
        private static List<IConstructorField> _constructorField = 
            new List<IConstructorField>()
            {
                new ConstructorArray(),
                new ConstructorBoolean(),
                new ConstructorInt(),
                new ConstructorText(),
                new ConstructorFloat(),
                new ConstructorDateTime(),
                new ConstructorLink()
            };
        public static bool TypeIsDefault(System.Type typeIn)
        {
            foreach(var constructor in _constructorField)
            {
                if(constructor.IsDefaultValue(typeIn))
                    return true;
            }
            return false;
        }
        public static bool TypeIsDefault(string typeIn)
        {
            foreach(var constructor in _constructorField)
            {
                if(constructor.IsDefaultValue(typeIn))
                    return true;
            }
            return false;
        }
        public static IList GetArrayType(System.Type typeIn, int lengthArrayIn)
        {
            if(!TypeIsDefault(typeIn))
                throw new ArgumentException(nameof(typeIn));
            
            var constructor = GetConstructor(typeIn);
            return constructor.GetArrayType(lengthArrayIn);
        }
        public static AbstractRecordField StringToField(string nameIn,string typeIn, string valueIn, bool isIndexIn)
        {
            if(!TypeIsDefault(typeIn))
                throw new ArgumentException(nameof(typeIn));

            var constructor = GetConstructor(typeIn);
            return constructor.StringToField(nameIn,valueIn, isIndexIn);
        }
        public static AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndexIn)
        {
            var type = valueIn.GetType();
            if(!TypeIsDefault(type))
                throw new ArgumentException(nameof(type));

            var constructor = GetConstructor(type);
            return constructor.ValueToField(nameIn,valueIn, isIndexIn);
        }
        
        public static void AddConstructor(IConstructorField constructorIn)
        {
            if(_constructorField.Contains(constructorIn))
                return;
            _constructorField.Add(constructorIn);
        }

        private static IConstructorField GetConstructor(System.Type typeIn)
        {
            foreach(var constructor in _constructorField)
            {
                if(constructor.IsDefaultValue(typeIn))
                    return constructor;
            }
            throw new ArgumentException(nameof(typeIn));
        }
        private static IConstructorField GetConstructor(string typeIn)
        {
            foreach(var constructor in _constructorField)
            {
                if(constructor.IsDefaultValue(typeIn))
                    return constructor;
            }
            throw new ArgumentException(nameof(typeIn));
        }
    }
}