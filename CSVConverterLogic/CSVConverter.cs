using System.IO;

namespace CSVConverterLogic
{
    public class CsvConverter
    {
        private readonly IFileParser _fileParser;
        private readonly TextWriter _fileWriter;
        private readonly ICsvConverter _converter;
        private readonly TypeObjectsFactory _typeFactory;
        public CsvConverter(IFileParser fileParser, TextWriter fileWriter, 
            ICsvConverter converter)
        {
            _fileParser = fileParser;
            _fileWriter = fileWriter;
            _converter = converter;
            _typeFactory = new TypeObjectsFactory();
        }

        /// <exception cref="UnparseableCsvException">CSV contains an invalid format.</exception>
        public CsvParsedObject ParseFile() 
        {
            CsvParsedObject csvParsedObject = _fileParser.Parse();
            
            return csvParsedObject;
        }

        public string MakeObject(CsvParsedObject csvParsedObject)
        {
            var typeObjectsCollection = _typeFactory.GetTypeObjectsCollection(csvParsedObject);
            _converter.SetTypes(typeObjectsCollection);
            _converter.BuildFromCsv(csvParsedObject);
            return _converter.GetData();
        }

        public void WriteConvertedCsv(string textObject)
        {   
            _fileWriter.WriteLine(textObject);
            _fileWriter.Close();
        }

    }
}