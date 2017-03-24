using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public abstract class Types
    {
        public abstract bool Match(string column);
        public string name { get; protected set; }
        protected Regex regex{ get;set;}
    }
}