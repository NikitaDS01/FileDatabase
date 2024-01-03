namespace FileDB.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = true, Inherited =false)]
    public class SerializeFileAttribute : System.Attribute
    {
        public string Name { get; set; }
    }
}
