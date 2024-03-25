namespace CurrencyExchangeDataExporter.Constans
{
    public class ApiConstants
    {
        //API Constants
        public static string API_URL = "https://www.tcmb.gov.tr/kurlar/{0}.xml";
        public static string TODAY_STRING = "today";
        public static string API_REQUEST_STRING_FORMAT = "{2}{1}/{0}{1}{2}";
        public static string API_REQUEST_SELECTED_NODE = "Tarih_Date";
        public static string COMMA_STRING = ",";
        public static string POINT_STRING = ".";
        public static string ZERO_STRING = "0";

        //API Response
        public static string API_RESPONSE_NODE_KOD = "Kod";
        public static string API_RESPONSE_NODE_ISIM = "Isim";
        public static string API_RESPONSE_NODE_UNIT = "Unit";
        public static string API_RESPONSE_NODE_FOREX_BUYING = "ForexBuying";
        public static string API_RESPONSE_NODE_FOREX_SELLING = "ForexSelling";
        public static string API_RESPONSE_NODE_BANKNOTE_BUYING = "BanknoteBuying";
        public static string API_RESPONSE_NODE_BANKNOTE_SELLING = "BanknoteSelling";
    }
}
