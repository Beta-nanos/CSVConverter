using System;
using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeDate : Types
    {
        public TypeDate()
        {
            string datePattern = @"#\d{2}/\d{2}/\d{4}#";
            regex = new Regex(datePattern);
            name = "date";
        }
        public override bool Match(string column)
        {
            return regex.Match(column).Success;
        }
    }
}