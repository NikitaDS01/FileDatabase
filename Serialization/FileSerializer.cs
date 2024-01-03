using FileDB.Core;
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

            foreach (PropertyInfo property in objectIn.GetType().GetProperties(
            BindingFlags.Instance | BindingFlags.Public))
            {
                bool nonSerialize = false;
                string name = property.Name;
                foreach (var info in property.GetCustomAttributes())
                {
                    if(info is NonSerializeFileAttribute) 
                        nonSerialize = true;
                    if (info is SerializeFileAttribute attributeName)
                        name = attributeName.Name;
                }
                if (nonSerialize)
                    continue;
                builder.TryAdd(name, property.PropertyType, property.GetValue(objectIn));
            }
            return builder.GetRecord();

        }
        public static void Deserialize<Out>(Record recordIn)
        {
            Type type = typeof(Out);
            var property = type.GetProperties(
                BindingFlags.Instance | BindingFlags.Public);
            var constructors = type.GetConstructors(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var constructorFull = constructors.FirstOrDefault(
                con => con.GetParameters().Length == property.Length);
            var constructorNull = constructors.FirstOrDefault(
                con => con.GetParameters().Length == 0);

            if (constructorFull != null)
            {
                Console.WriteLine("Yes");
            }
            else if (constructorNull != null)
            {
                Console.WriteLine("Почти есть");
            }
            else
            {
                Console.WriteLine("Нет");
            }
        }
    }
}
