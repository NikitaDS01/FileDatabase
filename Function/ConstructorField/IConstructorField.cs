using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using FileDB.Core.Data.TypeField;

namespace FileDB.Function.ConstructorField
{
    public interface IConstructorField
    {
        AbstractRecordField StringToField(string nameIn, string valueIn, bool isIndex);
        AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex);
        bool IsDefaultValue(System.Type typeIn);
        bool IsDefaultValue(string typeIn);
        IList GetArrayType(int countIn);
    }
}