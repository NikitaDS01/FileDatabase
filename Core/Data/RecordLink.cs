using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDB.Core.Data
{
    public class RecordLink
    {
        public string TableName {get;private set;}
        public string Number {get;private set;}
        public RecordLink(string tableIn, string numberIn) 
        {
            TableName = tableIn;
            Number = numberIn;            
        }
        public string FullName => string.Format("{0}_{1}", TableName, Number);
    }
}