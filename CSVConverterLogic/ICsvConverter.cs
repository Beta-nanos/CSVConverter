using System.Collections.Generic;

namespace CSVConverterLogic
{
    public interface ICsvConverter
    {
        string GetData();
        void BuildFromCsv(CsvParsedObject csvParsedObject);
        void SetTypes(Dictionary<string, string> typesObject);
    }
}