using System;
using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeInt : Types
    {
        public TypeInt()
        {
            string integerPattern = @"^\d+";
            regex = new Regex(integerPattern);
            name = "int";    
        }
        public override bool Match(string column)
        {
            return regex.Match(column).Success;
        }
    }
}