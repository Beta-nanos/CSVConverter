using Autofac;
using CSVConverterLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVConverter
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            /*if(args.Length > 2)
            {
                var csvPath = args[0];
                var conversionType = args[1];
                var outputPath = args[2];

                var builder = new ContainerBuilder();
                //builder.RegisterType<ConsoleOutput>().As<IOutput>();
                //builder.RegisterType<TodayWriter>().As<IDateWriter>();

                builder.RegisterType<FileParser>().As<IFileParser>();
                builder.RegisterType<StreamWriter>().As<TextWriter>();
                builder.RegisterType<XMLBuilder>().As<ICsvConverter>();

                Container = builder.Build();

                BuildFileFromCSV();
            }*/
            string myString = "\"Raim,Interiano\",21,#05/05/1995#";
            var result = myString.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToArray();
            
            foreach (var item in result)
            {
                Console.WriteLine("P"+item+"F");
            }
            Console.ReadLine();
        }

        public static void BuildFileFromCSV()
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
