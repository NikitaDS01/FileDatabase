using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Data.TypeField;

namespace FileDB.Core.Data.Tables
{
    public partial class Table
    {
        public class ParameterSearch
        {
            public enum SortBy
            {
                None = -1,
                ASC = 1,
                DESC = 2
            }
            public int Take {get;set;} = 100;
            public int Skip {get;set;} = 0;
            public SortBy TypeSort {get;set;} = SortBy.None; 

        }
        public Record Min(string nameFieldIn, ParameterSearch parameterIn)
        {
            var records = this.GiveFilterRecord(new RecordSearch(1).Add(nameFieldIn), 
                parameterIn);
            if(records.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(records));
            if(records.Count == 1)
                return records[0];

            int minIndex = 0;
            
            var type = records[0].GetField(nameFieldIn);
            if(type is FieldString || type is FieldBoolean)
                throw new ArgumentException(nameof(nameFieldIn));

            for(int index =0; index < records.Count; index++)
            {
                var element = records[index].GetField(nameFieldIn);
                var minElement = records[minIndex].GetField(nameFieldIn);
                if(element.LessField(minElement))
                    minIndex = index;
            }
            return records[minIndex];
        }
        public Record Min(string nameFieldIn)
        {
            return this.Min(nameFieldIn, new ParameterSearch());
        }
        public Record Max(string nameFieldIn,ParameterSearch parameterIn)
        {
            var records = this.GiveFilterRecord(new RecordSearch(1).Add(nameFieldIn), parameterIn);
            if(records.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(records));
            if(records.Count == 1)
                return records[0];

            int maxIndex = 0;

            var type = records[0].GetField(nameFieldIn);
            if(type is FieldString || type is FieldBoolean)
                throw new ArgumentException(nameof(nameFieldIn));


            for(int index =0; index < records.Count; index++)
            {
                var element = records[index].GetField(nameFieldIn);
                var minElement = records[maxIndex].GetField(nameFieldIn);
                if(element.LargeField(minElement))
                    maxIndex = index;
            }
            return records[maxIndex];
        }
        public Record Max(string nameFieldIn)
        {
            return this.Max(nameFieldIn, new ParameterSearch());
        }
        public float Average(string nameFieldIn, ParameterSearch parameterIn)
        {
            var records = this.GiveFilterRecord(new RecordSearch(1).Add(nameFieldIn), parameterIn);
            if(records.Count == 0)
                return 0.0f;
        
            float sum = 0;

            var type = records[0].GetField(nameFieldIn);
            if(!(type is FieldFloat) && !(type is FieldInt))
                throw new ArgumentException(nameof(nameFieldIn));

            for(int index =0; index < records.Count; index++)
            {
                var element = records[index].GetField(nameFieldIn);
                sum += Convert.ToSingle(element.Value);
            }
            return sum / records.Count;
        }
        public float Average(string nameFieldIn)
        {
            return this.Average(nameFieldIn, new ParameterSearch());
        }
        public float Sum(string nameFieldIn, ParameterSearch parameterIn)
        {
            var records = this.GiveFilterRecord(new RecordSearch(1).Add(nameFieldIn), parameterIn);
            if(records.Count == 0)
                return 0.0f;
        
            float sum = 0;

            var type = records[0].GetField(nameFieldIn);
            if(!(type is FieldFloat) && !(type is FieldInt))
                throw new ArgumentException(nameof(nameFieldIn));

            for(int index =0; index < records.Count; index++)
            {
                var element = records[index].GetField(nameFieldIn);
                sum += Convert.ToSingle(element.Value);
            }
            return sum;
        }
        public float Sum(string nameFieldIn)
        {
            return this.Sum(nameFieldIn, new ParameterSearch());
        }
        public Record[] Select(RecordSearch searchIn, ParameterSearch parameterIn)
        {
            var list = this.GiveFilterRecord(searchIn,parameterIn);
            return list.ToArray();
        }
        public Record[] Select(RecordSearch searchIn)
        {
            return this.Select(searchIn, new ParameterSearch());
        }
        public Record? SelectOne(RecordSearch searchIn)
        {
            return this.GiveOneRecord(searchIn);
        }
        public Record? LinkRecord(RecordLink linkIn)
        {
            if(System.IO.File.Exists(linkIn.FullName))
                return ReadData(linkIn.FullName);
            return null;
        }
    }
}