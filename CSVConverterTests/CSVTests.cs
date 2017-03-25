using System.IO;
using CSVConverterLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSVConverterTests
{
    [TestClass]
    public class CsvTests
    {

        [TestMethod]
        public void ValidateCsvStructureTest()
        {
            var fileReader = new StreamReader(GetTestFilePath("Personas.csv"));
            var fileParser = new FileParser(fileReader);
            CsvConverter csvConverter = new CsvConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            var csvParsedObject = csvConverter.ParseFile();
            Assert.IsNotNull(csvParsedObject);
        }

        [TestMethod]
        [ExpectedException(typeof(UnparseableCsvException))]
        public void InvalidCsvFormatTest()
        {
            var fileReader = new StreamReader(GetTestFilePath("Per.csv"));
            var fileParser = new FileParser(fileReader);
            CsvConverter csvConverter = new CsvConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            csvConverter.ParseFile();
        }

        [TestMethod]
        public void ValidCsvParsedObjectDataTypes()
        {
            var fileReader = new StreamReader(GetTestFilePath("Personas.csv"));
            var fileParser = new FileParser(fileReader);
            CsvConverter csvConverter = new CsvConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            var csvParsedObject = csvConverter.ParseFile();
            var typeObjectsFactory = new TypeObjectsFactory();

            var typeObjectsCollection =
                typeObjectsFactory.GetTypeObjectsCollection(csvParsedObject);
            Assert.AreEqual(typeObjectsCollection["Nombre"], "String");
            Assert.AreEqual(typeObjectsCollection["Edad"], "int");
            Assert.AreEqual(typeObjectsCollection["FechaNacimiento"], "date");

        }

        [TestMethod]
        public void AddObjectToXmlTest()
        {
            var csvParsedObject = new CsvParsedObject();
            csvParsedObject.AddDataRow(new[] { "nombre","edad"});
            csvParsedObject.AddDataRow(new[] { "luis", "21" });
            var xmlBuilder = new XmlBuilder();

            var typeObjectsFactory = new TypeObjectsFactory();

            var typeObjectsCollection =
                typeObjectsFactory.GetTypeObjectsCollection(csvParsedObject);
            xmlBuilder.SetTypes(typeObjectsCollection);
            xmlBuilder.BuildFromCsv(csvParsedObject);
            string xmlData =
                "<Rows>\n" +
                    "\t<Row>\n" +
                        "\t\t<nombre>luis</nombre>\n" +
                        "\t\t<edad>21</edad>\n" +
                    "\t</Row>\n" +
                "</Rows>";

            Assert.AreEqual(xmlData, xmlBuilder.GetData());
        }

        [TestMethod]
        public void CsvtoXmlTest()
        {
            var fileReader = new StreamReader(GetTestFilePath("Personas.csv"));
            var fileParser = new FileParser(fileReader);
            var fileWriter = new StreamWriter(GetTestFilePath("personas.xml"));
            var xmlBuilder = new XmlBuilder();
            CsvConverter csvConverter = new CsvConverter(fileParser,
                fileWriter, xmlBuilder);
            var csvParsedObject = csvConverter.ParseFile();
            csvConverter.WriteConvertedCsv(csvConverter.MakeObject(csvParsedObject));

            Assert.IsTrue(File.Exists(GetTestFilePath("personas.xml")));
        }

        [TestMethod]
        public void AddObjectToJsonTest()
        {
            var csvParsedObject = new CsvParsedObject();
            csvParsedObject.AddDataRow(new[] { "nombre", "edad" });
            csvParsedObject.AddDataRow(new[] { "Chungo", "22" });
            var jsonBuilder = new JsonBuilder();

            var typeObjectsFactory = new TypeObjectsFactory();

            var typeObjectsCollection =
                typeObjectsFactory.GetTypeObjectsCollection(csvParsedObject);
            jsonBuilder.SetTypes(typeObjectsCollection);
            jsonBuilder.BuildFromCsv(csvParsedObject);
            string jsonData =
                "[\n" +
                    "\t{\n" +
                        "\t\t\"nombre\": \"Chungo\",\n" +
                        "\t\t\"edad\": 22\n" +
                    "\t}\n" +
                "]";

            Assert.AreEqual(jsonData, jsonBuilder.GetData());
        }

        [TestMethod]
        public void CsvtoJsonTest()
        {
            var fileReader = new StreamReader(GetTestFilePath("Personas.csv"));
            var fileParser = new FileParser(fileReader);
            var fileWriter = new StreamWriter(GetTestFilePath("personas.json"));
            var jsonBuilder = new JsonBuilder();
            CsvConverter csvConverter = new CsvConverter(fileParser,
                fileWriter, jsonBuilder);
            var csvParsedObject = csvConverter.ParseFile();
            csvConverter.WriteConvertedCsv(csvConverter.MakeObject(csvParsedObject));

            Assert.IsTrue(File.Exists(GetTestFilePath("personas.json")));
        }

        private static string GetTestFilePath(string fileName)
        {
            string solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            if (solutionPath != null)
            {
                DirectoryInfo parentDir = Directory.GetParent(solutionPath);
                string testFolderPath = Path.Combine(parentDir.FullName, "CSVTests");
                string filePath = Path.Combine(testFolderPath, fileName);
                return filePath;
            }
            return null;
        }
    }
}
