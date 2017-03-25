using System.Collections.Generic;

namespace CSVConverterLogic
{
    public sealed class CsvParsedObject
    {
        private readonly List<string[]> _dataRows;

        public CsvParsedObject()
        {
            _dataRows = new List<string[]>();
        }

        public void AddDataRow(string[] rowData)
        {
            _dataRows.Add(rowData);
        }

        public int GetHeadersCount()
        {
            return GetRowAt(0).Length;
        }

        public string[] GetRowAt(int i)
        {
            return _dataRows[i];
        }

        public int GetRowsCount()
        {
            return _dataRows.Count;
        }

        public string[] GetHeaders()
        {
            return GetRowAt(0);
        }
    }
}