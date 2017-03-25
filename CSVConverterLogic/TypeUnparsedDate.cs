using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeUnparsedDate : Types
    {
        public TypeUnparsedDate()
        {
            string datePattern = @"#\d{2}/\d{2}/\d{4}#";
            regex = new Regex(datePattern);
            name = "UnparsedDate";
        }
        public override bool Match(string column)
        {
            return regex.Match(column).Success;
        }
    }
}
