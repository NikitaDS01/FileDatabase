using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Reader;

namespace FileDB.Core.Data
{
    public class RecordLink
    {
        public FileInfo LinkFile {get;private set;}
        public RecordLink(string pathIn) 
        {
            LinkFile = new FileInfo(pathIn);
        }
        public bool Exists => LinkFile.Exists;
        public string FullName => LinkFile.FullName;
        public Record? GetRecord(RecordLink linkIn)
        {
            if(!this.Exists)
                return null;
                
            var reader = new ReaderFileTxt(this.FullName);
            string data = reader.Read();
            return ReaderData.Read(data);
        }
        public override string ToString()
        {
            return FullName;
        }
    }
}