using FileDB.Core.Data;
using FileDB.Core.Attribute;
using FileDB.Function;
using System.Reflection;

namespace FileDB.Serialization
{
    public static class FileSerializer
    {
        public class FileSerializerOptions
        {
            public FileSerializerOptions() { }
        }
        private class PropertyRecordInfo
        {
            public PropertyRecordInfo(PropertyInfo infoIn)
            {
                NameProperty = infoIn.Name;
                Property = infoIn;
            } 
            public PropertyInfo Property {get;set;}
            public bool IsIndex {get;set;} = false;
            public string NameProperty {get;set;}
        }

        public static Record Serialize<In>(In objectIn)
        {
            return FileSerializer.Serialize(objectIn, new FileSerializerOptions());
        }
        public static Record Serialize<In>(In objectIn, FileSerializerOptions optionsIn)
        {
            if(objectIn == null) throw new ArgumentNullException(nameof(objectIn));

            var builder = new RecordBuilder();
            var properties = GetProperty(objectIn.GetType());

            foreach (var setting in properties)
            {
                var name = setting.NameProperty;
                var property = setting.Property;
                var isIndex = setting.IsIndex;
                if (FunctionField.TypeIsDefault(property.PropertyType))
                    builder.TryAdd(name, property.GetValue(objectIn), isIndex);
            }
            return builder.GetRecord();

        }
        public static Out Deserialize<Out>(Record recordIn)
        {
            Type type = typeof(Out);
            var length = GetProperty(type).Count();
            var constructors = type.GetConstructors(
                BindingFlags.Instance | BindingFlags.Public);

            if (constructors.Length == 0)
                throw new Exception("В данном классе нет конструкторов");

            var constructorFull = constructors.FirstOrDefault(constr =>
                constr.GetParameters().Length == length);
            var constructorEmpty = constructors.FirstOrDefault(constr =>
                constr.GetParameters().Length == 0);

            if (constructorFull != null)
            {
                var arguments = new object[length];
                for(int index = 0;index < length; index++)
                    arguments[index] = recordIn[index].Value;
                return (Out)constructorFull?.Invoke(arguments);
            }
            else if (constructorEmpty != null)
            {
                Console.WriteLine($"{constructorEmpty.Name} {constructorEmpty.GetParameters().Length}");
                return (Out)constructorEmpty?.Invoke(new object[] { });
            }
            else
            {
                throw new Exception("В данном классе нет подходящих конструкторов");
            }
        }
        private static IEnumerable<PropertyRecordInfo> GetProperty(System.Type typeIn)
        {
            var list = new List<PropertyRecordInfo>();
            var properties = typeIn.GetProperties(
                BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in properties)
            {
                bool nonSerialize = false;
                var settings = new PropertyRecordInfo(property);
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is NonSerializeFileAttribute)
                        nonSerialize = true;
                    if (attribute is SerializeNameRecordAttribute attributeName)
                        settings.NameProperty = attributeName.Name;
                    if(attribute is SerializeIndexAttribute)
                        settings.IsIndex = true;
                }
                if (nonSerialize)
                    continue;
                    
                list.Add(settings);
            }
            return list;
        }
    }
}
