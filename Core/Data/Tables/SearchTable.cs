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
                ASC = 1,
                DESC = 2
            }
            public int Take {get;set;} = 100;
            public int Skip {get;set;} = 0;
            public SortBy TypeSort {get;set;} = SortBy.ASC; 

        }
        public Record Min(string nameFieldIn, ParameterSearch parameterIn)
        {
            var records = this.GiveFilterRecord(nameFieldIn, parameterIn);
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
            var records = this.GiveFilterRecord(nameFieldIn, parameterIn);
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
            var records = this.GiveFilterRecord(nameFieldIn, parameterIn);
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
            var records = this.GiveFilterRecord(nameFieldIn, parameterIn);
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
    }
}