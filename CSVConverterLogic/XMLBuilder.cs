using System.Collections.Generic;
using System.Text;

namespace CSVConverterLogic
{
    public class XmlBuilder : ICsvConverter
    {
        private Dictionary<string, string> _typesObject;
        private readonly StringBuilder _sb;
        private string[] _headers;
        public XmlBuilder()
        {
            _sb = new StringBuilder();

        }

        public void BuildFromCsv(CsvParsedObject csvParsedObject)
        {
            _headers = csvParsedObject.GetHeaders();
            _sb.Append("<Rows>\n");
            int count = csvParsedObject.GetRowsCount();
            for (int i= 1; i< count; ++i){
                string[] row = csvParsedObject.GetRowAt(i);
                BuildFromRow(row);
            }
            _sb.Append("</Rows>");
        }

        private void BuildFromRow(string[] row)
        {
            _sb.Append("\t<Row>\n");
            for(int i=0; i < _headers.Length; i++)
            {
                AddObject(_headers[i], row[i]);
            }
            _sb.Append("\t</Row>\n");
        }

        private void AddObject(string header, string data)
        {
            _sb.Append("\t\t<" + header+">");
            _sb.Append(data);
            _sb.Append("</" + header + ">\n");
        }

        public string GetData()
        {
            return _sb.ToString();
        }

        public void SetTypes(Dictionary<string, string> typesObject)
        {
            _typesObject = typesObject;
        }
    }
}