using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeUnparsedDate : Types
    {
        public TypeUnparsedDate()
        {
            string datePattern = @"#\d{2}/\d{2}/\d{4}#";
            Regex = new Regex(datePattern);
            Name = "UnparsedDate";
        }
        public override bool Match(string column)
        {
            return Regex.Match(column).Success;
        }
    }
}
