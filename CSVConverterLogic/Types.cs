namespace CSVConverterLogic
{
    public abstract class Types
    {
        public abstract bool Match(string column);
        public string name { get; private set; }
    }
}