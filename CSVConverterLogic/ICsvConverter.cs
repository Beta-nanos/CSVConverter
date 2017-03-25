using System.Collections.Generic;

namespace CSVConverterLogic
{
    public interface ICsvConverter
    {
        string GetData();
        void BuildFromCSV(CsvParsedObject csvParsedObject);
        void SetTypes(Dictionary<string, string> typesObject);
    }
}