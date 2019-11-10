namespace ArenaBrawl.Payments
{
    public class Currency
    {
        public string IsoCode { get; }
        public string HumanReadable { get; }
        public string Symbol { get; }

        public Currency(string isoCode, string humanReadable, string symbol)
        {
            IsoCode = isoCode;
            HumanReadable = humanReadable;
            Symbol = symbol;
        }  
    }
}