using System;
using System.IO;

namespace CSVConverterLogic
{
    public class FileParser : IFileParser
    {
        private TextReader fileReader;

        public FileParser(TextReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public CsvParsedObject Parse()
        {
            var csvParsedObject = new CsvParsedObject();
            string lineRead = "";
            while (!String.IsNullOrEmpty(lineRead = this.fileReader.ReadLine()))
            {
                AddDataRow(lineRead, csvParsedObject);
            }

            ValidateCsvObjectStructure(csvParsedObject);
            
            return csvParsedObject;
        }

        private void ValidateCsvObjectStructure(CsvParsedObject csvParsedObject)
        {
            int headersCount = csvParsedObject.GetHeadersCount();
            int rowsCount = csvParsedObject.GetRowsCount();

            for (int i = 1; i < rowsCount; ++i)
            {
                int dataColumnsCount = csvParsedObject.GetRowAt(i).Length;

                if (dataColumnsCount != headersCount)
                {
                    throw new UnparseableCsvException
                        (
                        "CSV contains an invalid format. Check data columns match headers count at row " + i
                        );
                }
            }
        }

        private void AddDataRow(string lineRead, CsvParsedObject csvParsedObject)
        {
            var rowData = lineRead.Split(',');
            csvParsedObject.AddDataRow(rowData);
        }
    }
}