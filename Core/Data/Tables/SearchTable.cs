using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDB.Core.Data.Tables
{
    public partial class Table
    {
        public Record Min(string nameFieldIn, TypeValue typeIn)
        {
            var records = this.GiveRecord(nameFieldIn);
            if(records.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(records));
            if(records.Count == 1)
                return records[0];

            int minIndex = 0;
            
            if(records[0].GetField(nameFieldIn).Type != typeIn)
                throw new Exception(nameof(records));

            for(int index =0; index < records.Count; index++)
            {
                var element = records[index].GetField(nameFieldIn);
                var minElement = records[minIndex].GetField(nameFieldIn);
               //if(element.GetValue() < minElement.GetValue())
            }
            return records[0];
        }
        
    }

}