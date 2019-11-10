using System.Collections.Generic;

namespace ArenaBrawl.Payments
{
    public static class Currencies
    {
        public static List<Currency> Supported = new List<Currency>
        {
            new Currency("gbp","Pound Sterling", "£"),
            new Currency("usd","United States Dollar","$"),
            new Currency("aud","Australian Dollar","$"),
            new Currency("chf","Swiss Franc", "CHF"),
            new Currency("eur","Euro", "€"),
            new Currency("dkk","Denmark Krone" ,"kr"),
            new Currency("cad","Canada Dollar", "$"),
            new Currency("nzd","New Zealand Dollar", "$"),
            new Currency("pln","Poland Zloty", "zł")
        };
    }
}