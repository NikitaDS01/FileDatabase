using FileDB.Function;
using FileDB.Core.Data;
using FileDB.Core.Data.TypeField;

namespace FileDB.Core.Reader
{
    public static class ReaderData
    {
        private const char END_ROW = '\n';
        private const char END_DATA = ']';
        private const char BEGIN_DATA = '[';
        private const char FIELD_INDEX = '*';
        private const int COUNT_MARKUP = 3;
        public static Record Read(string textIn)
        {
            string[] rows = textIn.Split(END_ROW);
            var builder = new RecordBuilder();
            FieldArray array = null!;
            int countArray = 0;

            for(int index = 0; index < rows.Length; index++)
            {
                if (string.IsNullOrEmpty(rows[index]) || 
                    (rows[index][0] != BEGIN_DATA &&
                    rows[index][0] != FIELD_INDEX))
                {
                    continue;
                }
                var field = WriteString(rows[index]);
                if (field == null)
                    continue;

                if(field is FieldArray)
                {
                    array = field as FieldArray;
                    countArray = array.Length;
                    continue;
                }

                if (countArray == 0)
                {
                    builder.Add(field);
                }
                else
                {
                    array.AddField(field.Value);
                    countArray--;
                    if(countArray == 0)
                        builder.Add(array);
                }
            }
            return builder.GetRecord();            
        } 
        private static AbstractRecordField? WriteString(string rowIn)
        {
            string temp = rowIn;
            string[] data = new string[COUNT_MARKUP];
            int indexCurrent = 0;
            bool isIndex = false;

            if (rowIn.Count(c => c == BEGIN_DATA) != COUNT_MARKUP)
                return null;

            if (rowIn.Count(c => c == END_DATA) != COUNT_MARKUP)
                return null;

            if (rowIn.Contains(FIELD_INDEX))
                isIndex = true;

            while (indexCurrent != COUNT_MARKUP)
            {
                int indexBegin = temp.IndexOf(BEGIN_DATA);
                int indexEnd = temp.IndexOf(END_DATA);
                if (indexBegin == -1 || indexEnd == -1)
                    break;

                data[indexCurrent] = temp.Substring(indexBegin + 1, indexEnd - indexBegin - 1);
                indexCurrent++;

                temp = temp.Substring(indexEnd + 1, temp.Length - indexEnd - 1);
            }

            if (data[0] == null ||
                data[1] == null ||
                data[2] == null)
            {
                return null;
            }

            return FunctionField.StringToField(data[0], data[1], data[2], isIndex);
        }
    }
}
