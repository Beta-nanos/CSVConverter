using System.CodeDom;
using System.IO;

namespace CSVConverterLogic
{
    public class CSVConverter
    {
        private IFileParser fileParser;
        private TextWriter fileWriter;
        private ICsvConverter converter;

        public CSVConverter(IFileParser fileParser, TextWriter fileWriter, 
            ICsvConverter converter)
        {
            this.fileParser = fileParser;
            this.fileWriter = fileWriter;
            this.converter = converter;
        }

        /// <exception cref="UnparseableCsvException">CSV contains an invalid format.</exception>
        public CsvParsedObject ParseFile() 
        {
            CsvParsedObject csvParsedObject = this.fileParser.Parse();
            
            return csvParsedObject;
        }
    }
}