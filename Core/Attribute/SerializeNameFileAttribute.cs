namespace FileDB.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = true, Inherited =false)]
    public class SerializeNameRecordAttribute : System.Attribute
    {
        public string Name { get; set; } = string.Empty;
    }
}
