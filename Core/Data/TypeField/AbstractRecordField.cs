using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDB.Core.Data.TypeField
{
    public abstract class AbstractRecordField
    {
        private readonly string _name;
        private readonly bool _isIndexField;
        public AbstractRecordField(string nameIn, bool isIndexIn = false)
        {
            _name = nameIn;
            _isIndexField = isIndexIn;
        }
        public string Name => _name;
        public bool IsIndex => _isIndexField;
        public abstract object Value {get;}
        public abstract string Convert();
        public abstract bool EqualsField(AbstractRecordField fieldIn);
        public abstract bool LargeField(AbstractRecordField fieldIn);
        public abstract bool LessField(AbstractRecordField fieldIn);
        public bool LargeAndEqualsField(AbstractRecordField fieldIn)
        {
            return this.EqualsField(fieldIn) || this.LargeField(fieldIn);
        }
        public bool LessAndEqualsField(AbstractRecordField fieldIn)
        {
            return this.EqualsField(fieldIn) || this.LessField(fieldIn);
        }
    }
}