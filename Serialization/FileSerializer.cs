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
        public static Record Serialize<In>(In objectIn)
        {
            return FileSerializer.Serialize(objectIn, new FileSerializerOptions());
        }
        public static Record Serialize<In>(In objectIn, FileSerializerOptions optionsIn)
        {
            if(objectIn == null) throw new ArgumentNullException(nameof(objectIn));

            var builder = new RecordBuilder();
            var propetries = GetPropertyName(objectIn.GetType());

            foreach (var propertyName in propetries)
            {
                var name = propertyName.Item2;
                var property = propertyName.Item1;
                if (ConvertEnum.IsDefaultValue(property.PropertyType))
                    builder.TryAdd(name, property.PropertyType, property.GetValue(objectIn));
            }
            return builder.GetRecord();

        }
        public static Out Deserialize<Out>(Record recordIn)
        {
            Type type = typeof(Out);
            var propetries = GetProperty(type);
            var lenght = propetries.Count();
            var constructors = type.GetConstructors(
                BindingFlags.Instance | BindingFlags.Public);

            if (constructors.Length == 0)
                throw new Exception("В данном классе нет конструкторов");

            var constructorFull = constructors.FirstOrDefault(constr =>
                constr.GetParameters().Length == lenght);
            var constructorEmpty = constructors.FirstOrDefault(constr =>
                constr.GetParameters().Length == 0);

            if (constructorFull != null)
            {
                var arguments = new object[lenght];
                for(int index = 0;index < lenght; index++)
                    arguments[index] = recordIn[index].GetValue();
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
        private static IEnumerable<(PropertyInfo, string)> GetPropertyName(System.Type typeIn)
        {
            var list = new List<(PropertyInfo, string)>();
            var propetries = typeIn.GetProperties(
                BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in propetries)
            {
                bool nonSerialize = false;
                string name = property.Name;
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is NonSerializeFileAttribute)
                        nonSerialize = true;
                    if (attribute is SerializeNameFileAttribute attributeName)
                        name = attributeName.Name;
                }
                if (nonSerialize)
                    continue;
                list.Add((property, name));
            }
            return list;
        }
        private static IEnumerable<PropertyInfo> GetProperty(System.Type typeIn)
        {
            var list = new List<PropertyInfo>();
            var propetries = typeIn.GetProperties(
                BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo property in propetries)
            {
                bool nonSerialize = false;
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is NonSerializeFileAttribute)
                        nonSerialize = true;
                }
                if (nonSerialize)
                    continue;
                list.Add(property);
            }
            return list;
        }
    }
}
