using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDB.Core.Writer
{
    public static class WriterData
    {
        public static string Write(Record recordIn)
        {
            return recordIn.ConvertData();
        }
    }
}
