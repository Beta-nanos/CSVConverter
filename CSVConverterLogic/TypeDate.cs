using System.Text.RegularExpressions;

namespace CSVConverterLogic
{
    public class TypeDate : Types
    {
        public TypeDate()
        {
            string datePattern = @"^(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})$";
            Regex = new Regex(datePattern);
            Name = "date";
        }
        public override bool Match(string column)
        {
            return Regex.Match(column).Success;
        }
    }
}