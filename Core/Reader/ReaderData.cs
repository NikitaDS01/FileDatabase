using FileDB.Function;
using FileDB.Core.Data;

namespace FileDB.Core.Reader
{
    public static class ReaderData
    {
        private const char END_ROW = '\n';
        private const char END_DATA = ']';
        private const char BEGIN_DATA = '[';
        private const int COUNT_MARKUP = 3;
        public static Record Read(string textIn)
        {
            string[] rows = textIn.Split(END_ROW);
            var elements = new List<Element>();
            for(int index = 0; index < rows.Length; index++)
            {
                if (string.IsNullOrEmpty(rows[index]) || 
                    rows[index][0] != BEGIN_DATA)
                {
                    continue;
                }
                Element? element = WriteString(rows[index]);
                if(element != null)
                    elements.Add(element);
            }
            return new Record(elements.ToArray());            
        } 
        private static Element? WriteString(string rowIn)
        {
            string temp = rowIn;
            string[] data = new string[COUNT_MARKUP];
            int indexCurrent = 0;

            if (rowIn.Count(c => c == BEGIN_DATA) != COUNT_MARKUP)
                return null;

            if (rowIn.Count(c => c == END_DATA) != COUNT_MARKUP)
                return null;

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

            return new Element(
                data[0],
                ConvertEnum.ToEnum<Data.TypeValue>(data[1]),
                data[2]);
        }
    }
}
