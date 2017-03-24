using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public static class TypeObjectsFactory
    {
        public static object GetTypeObjectsCollection(CsvParsedObject csvParsedObject)
        {
            var headers = csvParsedObject.GetHeaders();
            var firstDataRow = csvParsedObject.GetRowAt(1);
            string integerPattern = @"/\d+/";
            string datePattern = @"/#\d{2}/";//not finished
            foreach (var column in firstDataRow)
            {
   
            }
            return null;
        }
    }
}