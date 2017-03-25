﻿using System.Collections.Generic;
using System.Text;

namespace CSVConverterLogic
{
    public class JSONBuilder : ICsvConverter
    {
        private Dictionary<string, string> typesObject;
        private StringBuilder sb;
        private string[] headers;

        public JSONBuilder()
        {
            sb = new StringBuilder();

        }
        public string GetData()
        {
            return sb.ToString();
        }

        public void BuildFromCSV(CsvParsedObject csvParsedObject)
        {
            headers = csvParsedObject.GetHeaders();
            sb.Append("[\n");
            int count = csvParsedObject.GetRowsCount();
            for (int i = 1; i < count; ++i)
            {
                string[] row = csvParsedObject.GetRowAt(i);
                BuildFromRow(row);
                sb.Append(",\n");
            }
            sb.Length--;
            sb.Length--;
            sb.Append("\n]");
        }

        private void BuildFromRow(string[] row)
        {
            sb.Append("\t{\n");
            for (int i = 0; i < headers.Length; i++)
            {
                AddObject(headers[i], row[i]);
                sb.Append(",\n");
            }
            sb.Length--;
            sb.Length--;
            sb.Append("\n\t}");
        }

        private void AddObject(string header, string data)
        {
            if (typesObject[header].Equals("date"))
            {
                data = data.Replace('#', '\"');
                data = data.Trim();
            }
            else if (typesObject[header].Equals("String"))
            {
                data = "\"" + data + "\"";
            }
            sb.Append("\t\t\"" + header + "\": ");
            sb.Append(data);
        }

        public void SetTypes(Dictionary<string, string> typesObject)
        {
            this.typesObject = typesObject;
        }
    }
}
