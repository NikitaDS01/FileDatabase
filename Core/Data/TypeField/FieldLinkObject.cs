namespace FileDB.Core.Data.TypeField
{
    public class FieldLinkObject : AbstractRecordField
    {
        private FileInfo _fileLink;
        public FieldLinkObject(string nameIn, string pathIn) 
            : base(nameIn, false)
        {
            _fileLink = new FileInfo(pathIn);
            if(!_fileLink.Exists)
                throw new NullReferenceException(nameof(_fileLink));
            
        }
        public FileInfo File => _fileLink;
        public override object Value => _fileLink.FullName;

        public override string Convert()
        {
            return $"[{Name}][Link]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if(!(fieldIn is FieldLinkObject) || fieldIn.Name != this.Name)
                return false;
            else
                return _fileLink.FullName == (string)fieldIn.Value;
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }
    }
}