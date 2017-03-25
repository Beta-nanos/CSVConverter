using System.Collections.Generic;

namespace CSVConverterLogic
{
    public class TypeObjectsFactory
    {
        private readonly List<Types> _typesList;

        public TypeObjectsFactory()
        {
            _typesList = new List<Types>();
            InitFactory();
        }

        private void InitFactory()
        {
            _typesList.Add(new TypeUnparsedDate());
            _typesList.Add(new TypeDate());
            _typesList.Add(new TypeInt());
        }

        public Dictionary<string, string> GetTypeObjectsCollection(CsvParsedObject csvParsedObject)
        {
            var headers = csvParsedObject.GetHeaders();
            var firstDataRow = csvParsedObject.GetRowAt(1);
            var typesTuple = new Dictionary<string, string>();
            for(int i = 0; i < firstDataRow.Length; ++i)
            {
                typesTuple.Add(headers[i], GetType(firstDataRow[i]));
            }

            return typesTuple;
        }

        private string GetType(string column)
        {
            foreach (var types in _typesList)
            {
                var success = types.Match(column);

                if (success)
                {
                    return types.Name;
                }
            }

            return "String";
        }
    }
}