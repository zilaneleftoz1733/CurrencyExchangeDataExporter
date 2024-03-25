namespace CurrencyExchangeDataExporter.Model
{
    public class ResponseDataExchangRate
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public int Unit { get; set; }
        public string CurrencyType { get; set; }
        public decimal ForexBuying { get; set; }
        public decimal ForexSelling { get; set; }
        public decimal BanknoteBuying { get; set; }
        public decimal BanknoteSelling { get; set; }
    }
}
