using System.IO;
using CSVConverterLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CSVConverterTests
{
    [TestClass]
    public class CSVTests
    {

        [TestMethod]
        public void ValidateCSVStructureTest()
        {
            var fileReader = new StreamReader("Personas.csv");
            var fileParser = new FileParser(fileReader);
            CSVConverter csvConverter = new CSVConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            var csvParsedObject = csvConverter.ParseFile();
            Assert.IsNotNull(csvParsedObject);
        }

        [TestMethod]
        [ExpectedException(typeof(UnparseableCsvException))]
        public void InvalidCSVFormatTest()
        {
            var fileReader = new StreamReader("Per.csv");
            var fileParser = new FileParser(fileReader);
            CSVConverter csvConverter = new CSVConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            csvConverter.ParseFile();
        }

        [TestMethod]
        public void ValidCSVParsedObjectDataTypes()
        {
            var fileReader = new StreamReader("Personas.csv");
            var fileParser = new FileParser(fileReader);
            CSVConverter csvConverter = new CSVConverter(fileParser,
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
        public void AddObjectToXMLTest()
        {
            var csvParsedObject = new CsvParsedObject();
            csvParsedObject.AddDataRow(new string[] { "nombre","edad"});
            csvParsedObject.AddDataRow(new string[] { "luis", "21" });
            var xmlBuilder = new XMLBuilder();

            var typeObjectsFactory = new TypeObjectsFactory();

            var typeObjectsCollection =
                typeObjectsFactory.GetTypeObjectsCollection(csvParsedObject);
            xmlBuilder.SetTypes(typeObjectsCollection);
            xmlBuilder.BuildFromCSV(csvParsedObject);
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
        public void CSVToXMLTest()
        {
            var fileReader = new StreamReader("Personas.csv");
            var fileParser = new FileParser(fileReader);
            var fileWriter = new StreamWriter("personas.xml");
            var xmlBuilder = new XMLBuilder();
            CSVConverter csvConverter = new CSVConverter(fileParser,
                fileWriter, xmlBuilder);
            var csvParsedObject = csvConverter.ParseFile();
            csvConverter.WriteConvertedCSV(csvConverter.MakeObject(csvParsedObject));

            Assert.IsTrue(File.Exists("personas.xml"));
        }

        [TestMethod]
        public void AddObjectToJSONTest()
        {
            var csvParsedObject = new CsvParsedObject();
            csvParsedObject.AddDataRow(new string[] { "nombre", "edad" });
            csvParsedObject.AddDataRow(new string[] { "Chungo", "22" });
            var jsonBuilder = new JSONBuilder();

            var typeObjectsFactory = new TypeObjectsFactory();

            var typeObjectsCollection =
                typeObjectsFactory.GetTypeObjectsCollection(csvParsedObject);
            jsonBuilder.SetTypes(typeObjectsCollection);
            jsonBuilder.BuildFromCSV(csvParsedObject);
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
        public void CSVToJSONTest()
        {
            var fileReader = new StreamReader("Personas.csv");
            var fileParser = new FileParser(fileReader);
            var fileWriter = new StreamWriter("personas.json");
            var jsonBuilder = new JSONBuilder();
            CSVConverter csvConverter = new CSVConverter(fileParser,
                fileWriter, jsonBuilder);
            var csvParsedObject = csvConverter.ParseFile();
            csvConverter.WriteConvertedCSV(csvConverter.MakeObject(csvParsedObject));

            Assert.IsTrue(File.Exists("personas.json"));
        }
    }
}
