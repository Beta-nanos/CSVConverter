using Autofac;
using CSVConverterLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IContainer = Autofac.IContainer;

namespace CSVConverter
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            if(args.Length > 2)
            {
                var csvPath = args[0];
                var conversionType = args[1];
                var outputPath = args[2];

                var builder = new ContainerBuilder();
                
                var fileReader = new StreamReader(csvPath);
                var fileParser = new FileParser(fileReader);
                var fileWriter = new StreamWriter(outputPath);

                builder.RegisterInstance(fileReader).As<TextReader>();
                builder.RegisterInstance(fileParser).As<IFileParser>();
                builder.RegisterInstance(fileWriter).As<TextWriter>();
                builder.RegisterType<XMLBuilder>().As<ICsvConverter>();
                builder.
                    RegisterType<CSVConverterLogic.CSVConverter>().
                    As<CSVConverterLogic.CSVConverter>();
                Container = builder.Build();

                BuildFileFromCsv();
            }
        }
        
        public static void BuildFileFromCsv()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var converter = scope.Resolve<CSVConverterLogic.CSVConverter>();

                var csvParsedObject = converter.ParseFile();
                converter.WriteConvertedCSV(converter.MakeObject(csvParsedObject));

            }
        }
    }
}
