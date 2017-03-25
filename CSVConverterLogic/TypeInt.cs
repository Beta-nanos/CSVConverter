using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeInt : Types
    {
        public TypeInt()
        {
            string integerPattern = @"^\d+$";
            Regex = new Regex(integerPattern);
            Name = "int";    
        }
        public override bool Match(string column)
        {
            return Regex.Match(column).Success;
        }
    }
}