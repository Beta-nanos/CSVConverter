﻿using Autofac;
using CSVConverterLogic;
using System.IO;
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
                if (conversionType.Equals("XML"))
                {
                    builder.RegisterType<XmlBuilder>().As<ICsvConverter>();
                }else if (conversionType.Equals("JSON"))
                {
                    builder.RegisterType<JsonBuilder>().As<ICsvConverter>();
                }
                builder.
                    RegisterType<CSVConverterLogic.CsvConverter>().
                    As<CSVConverterLogic.CsvConverter>();
                Container = builder.Build();

                BuildFileFromCsv();
            }
        }

        public static void BuildFileFromCsv()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var converter = scope.Resolve<CSVConverterLogic.CsvConverter>();

                var csvParsedObject = converter.ParseFile();
                converter.WriteConvertedCsv(converter.MakeObject(csvParsedObject));

            }
        }
    }
}
