using System.Collections.Generic;

namespace CSVConverterLogic
{
    public sealed class CsvParsedObject
    {
        private List<string[]> dataRows;

        public CsvParsedObject()
        {
            dataRows = new List<string[]>();
        }

        public void AddDataRow(string[] rowData)
        {
            dataRows.Add(rowData);
        }

        public int GetHeadersCount()
        {
            return this.GetRowAt(0).Length;
        }

        public string[] GetRowAt(int i)
        {
            return this.dataRows[i];
        }

        public int GetRowsCount()
        {
            return this.dataRows.Count;
        }

        public string[] GetHeaders()
        {
            return this.GetRowAt(0);
        }
    }
}