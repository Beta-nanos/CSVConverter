using System.Collections.Generic;
using System.Text;

namespace CSVConverterLogic
{
    public class JsonBuilder : ICsvConverter
    {
        private Dictionary<string, string> _typesObject;
        private readonly StringBuilder _stringbuilder;
        private string[] _headers;

        public JsonBuilder()
        {
            _stringbuilder = new StringBuilder();

        }
        public string GetData()
        {
            return _stringbuilder.ToString();
        }

        public void BuildFromCSV(CsvParsedObject csvParsedObject)
        {
            _headers = csvParsedObject.GetHeaders();
            _stringbuilder.Append("[\n");
            int count = csvParsedObject.GetRowsCount();
            for (int i = 1; i < count; ++i)
            {
                string[] row = csvParsedObject.GetRowAt(i);
                BuildFromRow(row);
                _stringbuilder.Append(",\n");
            }
            _stringbuilder.Length = _stringbuilder.Length - 2;
            _stringbuilder.Append("\n]");
        }

        private void BuildFromRow(string[] row)
        {
            _stringbuilder.Append("\t{\n");
            for (int i = 0; i < _headers.Length; i++)
            {
                AddObject(_headers[i], row[i]);
                _stringbuilder.Append(",\n");
            }
            _stringbuilder.Length = _stringbuilder.Length - 2;
            _stringbuilder.Append("\n\t}");
        }

        private void AddObject(string header, string data)
        {
            if (!_typesObject[header].Equals("int"))
                data = "\"" + data + "\"";
            _stringbuilder.Append("\t\t\"" + header + "\": ");
            _stringbuilder.Append(data);
        }

        public void SetTypes(Dictionary<string, string> typesObject)
        {
            _typesObject = typesObject;
        }
    }
}
