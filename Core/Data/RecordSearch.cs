using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Data.Tables;
using FileDB.Core.Data.TypeField;
using FileDB.Function;

namespace FileDB.Core.Data
{
    public class RecordSearch
    {      
        public enum ConditionType
        {
            Equals = 1,
            Large = 2,
            Less = 4,
            NotEquals = 8
        }
        private struct FieldSearch
        {
            
            public string Name {get; set;} = string.Empty;
            public object Value {get; set;} = string.Empty;
            public bool IsConsiderValue {get;set;} = false;
            public ConditionType Condition {get;set;} = ConditionType.Equals;
            public FieldSearch() { }

            public AbstractRecordField? GetRecordField()
            {
                if(!FunctionField.TypeIsDefault(Value.GetType()))
                    return null;
                
                return FunctionField.ValueToField(Name, Value, false);
            }
        }
    
        private FieldSearch[] _fields;
        private int _count;
        public RecordSearch(int countFieldIn)
        {
            _fields = new FieldSearch[countFieldIn];
            _count = 0;
        }
        public RecordSearch Add(string nameIn)
        {
            _fields[_count] = new FieldSearch() {Name = nameIn};
            _count++;
            return this;
        }
        public RecordSearch Add(string nameIn, object valueIn)
        {
            _fields[_count] = new FieldSearch() {
                Name = nameIn,
                Value = valueIn,
                IsConsiderValue = true};
            _count++;
            return this;
        }
        public RecordSearch Add(string nameIn, object valueIn, ConditionType typeIn)
        {
            _fields[_count] = new FieldSearch() {
                Name = nameIn,
                Value = valueIn,
                IsConsiderValue = true,
                Condition = typeIn};
            _count++;
            return this;
        }
    
        public bool IsCondition(Record recordIn)
        {
            foreach(var field in _fields)
            {
                if(!recordIn.ContainField(field.Name)) 
                    return false;

                if(field.IsConsiderValue)
                {
                    AbstractRecordField? tmpField;
                    if(!recordIn.TryGetField(field.Name, out tmpField))
                        return false;
                    
                    AbstractRecordField? recordField = field.GetRecordField();
                    if(recordField == null) return false;

                    //Console.WriteLine("Tmp: {0} - {1}", tmpField, tmpField.Value);
                    //Console.WriteLine("Record: {0} - {1}", recordField, recordField.Value);
                    switch((int)field.Condition)
                    {
                        case 1: if(!recordField.EqualsField(tmpField!)) return false; break;
                        case 2: if(!recordField.LargeField(tmpField!)) return false; break;
                        case 3: if(!recordField.LessField(tmpField!)) return false; break;
                        case 4: if(!recordField.LessAndEqualsField(tmpField!)) return false; break;
                        case 5: if(!recordField.LargeAndEqualsField(tmpField!)) return false; break;
                        case 8: if(recordField.Equals(tmpField!)) return false; break;
                    }
                }
            }
            return true;
        }
    }
}