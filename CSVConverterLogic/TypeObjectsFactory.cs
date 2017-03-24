using System.Collections.Generic;

namespace CSVConverterLogic
{
    public class TypeObjectsFactory
    {
        private readonly List<Types> typesList;

        public TypeObjectsFactory()
        {
            this.typesList = new List<Types>();
            InitFactory();
        }

        private void InitFactory()
        {
            typesList.Add(new TypeInt());
            typesList.Add(new TypeDate());
        }

        public Dictionary<string, string> GetTypeObjectsCollection(CsvParsedObject csvParsedObject)
        {
            var headers = csvParsedObject.GetHeaders();
            var firstDataRow = csvParsedObject.GetRowAt(1);
            var typesTuple = new Dictionary<string, string>();
            for(int i = 0; i < firstDataRow.Length; ++i)
            {
                typesTuple.Add(headers[i], this.GetType(firstDataRow[i]));
            }

            return typesTuple;
        }

        private string GetType(string column)
        {
            foreach (var types in this.typesList)
            {
                var success = types.Match(column);

                if (success)
                {
                    return types.name;
                }
            }

            return "String";
        }
    }
}