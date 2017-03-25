using System.CodeDom;
using System.IO;

namespace CSVConverterLogic
{
    public class CSVConverter
    {
        private IFileParser fileParser;
        private TextWriter fileWriter;
        private ICsvConverter converter;
        private TypeObjectsFactory typeFactory;
        public CSVConverter(IFileParser fileParser, TextWriter fileWriter, 
            ICsvConverter converter)
        {
            this.fileParser = fileParser;
            this.fileWriter = fileWriter;
            this.converter = converter;
            this.typeFactory = new TypeObjectsFactory();
        }

        /// <exception cref="UnparseableCsvException">CSV contains an invalid format.</exception>
        public CsvParsedObject ParseFile() 
        {
            CsvParsedObject csvParsedObject = this.fileParser.Parse();
            
            return csvParsedObject;
        }

        public string MakeObject(CsvParsedObject csvParsedObject)
        {
            var typeObjectsCollection = typeFactory.GetTypeObjectsCollection(csvParsedObject);
            converter.SetTypes(typeObjectsCollection);
            converter.BuildFromCSV(csvParsedObject);
            return converter.GetData();
        }

        public void WriteConvertedCSV(string textObject)
        {   
            fileWriter.WriteLine(textObject);
            fileWriter.Close();
        }

    }
}