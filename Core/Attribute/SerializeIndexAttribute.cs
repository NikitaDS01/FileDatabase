namespace FileDB.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = true, Inherited =false)]
    public class SerializeIndexAttribute : System.Attribute
    {
        

    }
}