using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public abstract class Types
    {
        public abstract bool Match(string column);
        public string Name { get; protected set; }
        protected Regex Regex{ get;set;}
    }
}