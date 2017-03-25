using System;
using System.Collections.Generic;
using System.Text;

namespace CSVConverterLogic
{
    public class XMLBuilder : ICsvConverter
    {
        private Dictionary<string, string> typesObject;
        private StringBuilder sb;
        private string[] headers;
        public XMLBuilder()
        {
            sb = new StringBuilder();

        }

        public void BuildFromCSV(CsvParsedObject csvParsedObject)
        {
            headers = csvParsedObject.GetHeaders();
            sb.Append("<Rows>\n");
            int count = csvParsedObject.GetRowsCount();
            for (int i= 1; i< count; ++i){
                string[] row = csvParsedObject.GetRowAt(i);
                BuildFromRow(row);
            }
            sb.Append("</Rows>");
        }

        private void BuildFromRow(string[] row)
        {
            sb.Append("\t<Row>\n");
            for(int i=0; i < headers.Length; i++)
            {
                AddObject(headers[i], row[i]);
            }
            sb.Append("\t</Row>\n");
        }

        private void AddObject(string header, string data)
        {
            sb.Append("\t\t<" + header+">");
            sb.Append(data);
            sb.Append("</" + header + ">\n");
        }

        public string GetData()
        {
            return sb.ToString();
        }

        public void SetTypes(Dictionary<string, string> typesObject)
        {
            this.typesObject = typesObject;
        }
    }
}