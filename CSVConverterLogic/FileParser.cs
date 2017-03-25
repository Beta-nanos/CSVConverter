using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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
            string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
            var result = Regex.Split(lineRead, pattern);
            for(int i = 0; i < result.Length; ++i)
            {
                result[i] = result[i].Replace("\"", String.Empty);
                result[i] = FormatResultIfDate(result[i]);
            }
            csvParsedObject.AddDataRow(result);
        }

        private string FormatResultIfDate(string result)
        {
            var dateMatcher = new TypeUnparsedDate();
            if (dateMatcher.Match(result))
            {
                result = result.Replace("#", String.Empty);
                DateTime date = Convert.ToDateTime(result);
                var strDate = date.ToString("yyyy-MM-dd HH':'mm':'ss");
                return strDate;
            }
            return result;
        }
    }
}