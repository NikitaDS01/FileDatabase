using FileDB.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileDB.Core.File
{
    public class NameRecord
    {
        private string _nameRecord;
        private TypeFormat _typeFormat;
        public NameRecord(string typeObjectIn, string nameObjectIn, int number, TypeFormat typeFormatIn)
        {
            _typeFormat = typeFormatIn;
            _nameRecord = $"{typeObjectIn}_{nameObjectIn}_{number}";
        }
        public NameRecord(string typeObjectIn, DateTime dateTimeIn, int number, TypeFormat typeFormatIn)
        {
            _typeFormat = typeFormatIn;
            _nameRecord = $"{typeObjectIn}_{dateTimeIn}_{number}";
        }
        public NameRecord(string typeObjectIn, int number, TypeFormat typeFormatIn)
        {
            _typeFormat = typeFormatIn;
            _nameRecord = $"{typeObjectIn}_{number}";
        }
        public NameRecord(string typeObjectIn, string nameObjectIn, TypeFormat typeFormatIn)
        {
            _typeFormat = typeFormatIn;
            _nameRecord = $"{typeObjectIn}_{nameObjectIn}";
        }
        public string GetName() => FunctionFile.FileName(_nameRecord, _typeFormat);
    }
}
