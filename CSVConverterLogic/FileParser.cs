using System;
using System.IO;
using System.Linq;

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
            var result = lineRead.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ',' })  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToArray();
            csvParsedObject.AddDataRow(result);
        }
    }
}