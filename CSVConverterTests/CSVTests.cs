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
            var csvParsedObject = csvConverter.parseFile();
            Assert.IsNotNull(csvParsedObject);
        }

        [TestMethod]
        [ExpectedException(typeof (UnparseableCsvException))]
        public void InvalidCSVFormatTest()
        {
            var fileReader = new StreamReader("Per.csv");
            var fileParser = new FileParser(fileReader);
            CSVConverter csvConverter = new CSVConverter(fileParser,
                It.IsAny<TextWriter>(), It.IsAny<ICsvConverter>());
            csvConverter.parseFile();
        }
    }
}
