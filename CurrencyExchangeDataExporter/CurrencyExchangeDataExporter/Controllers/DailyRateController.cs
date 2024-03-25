using CurrencyExchangeDataExporter.Constans;
using CurrencyExchangeDataExporter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml;

namespace CurrencyExchangeDataExporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyRateController : ControllerBase
    {
        [HttpPost]
        public ResponseData Run(RequestData request)
        {
            ResponseData result = new ResponseData();

            try
            {
                string tcmblink = string.Format(ApiConstants.API_URL, (request.IsDay) ? ApiConstants.TODAY_STRING :
                string.Format(ApiConstants.API_REQUEST_STRING_FORMAT, request.Day.ToString().PadLeft(2, '0'), request.Year));

                result.Data = new List<ResponseDataExchangRate>();
                XmlDocument doc = new XmlDocument();
                doc.Load(tcmblink);

                if (doc.SelectNodes(ApiConstants.API_REQUEST_SELECTED_NODE).Count < 1)
                {
                    result.Error = AppMessages.API_OPERATION_FAIL_MESSAGE;

                    return result;
                }

                foreach (XmlNode node in doc.SelectNodes(ApiConstants.API_REQUEST_SELECTED_NODE)[0].ChildNodes)
                {
                    ResponseDataExchangRate ExchangRate = new ResponseDataExchangRate();

                    ExchangRate.CurrencyCode = node.Attributes[ApiConstants.API_RESPONSE_NODE_KOD].Value;
                    ExchangRate.CurrencyName = node[ApiConstants.API_RESPONSE_NODE_ISIM].InnerText;
                    ExchangRate.Unit = int.Parse(node[ApiConstants.API_RESPONSE_NODE_UNIT].InnerText);
                    ExchangRate.ForexBuying = Convert.ToDecimal(ApiConstants.ZERO_STRING + node[ApiConstants.API_RESPONSE_NODE_FOREX_BUYING].InnerText.Replace(ApiConstants.POINT_STRING, ApiConstants.COMMA_STRING));
                    ExchangRate.ForexSelling = Convert.ToDecimal(ApiConstants.ZERO_STRING + node[ApiConstants.API_RESPONSE_NODE_FOREX_SELLING].InnerText.Replace(ApiConstants.POINT_STRING, ApiConstants.COMMA_STRING));
                    ExchangRate.BanknoteBuying = Convert.ToDecimal(ApiConstants.ZERO_STRING + node[ApiConstants.API_RESPONSE_NODE_BANKNOTE_BUYING].InnerText.Replace(ApiConstants.POINT_STRING, ApiConstants.COMMA_STRING));
                    ExchangRate.BanknoteSelling = Convert.ToDecimal(ApiConstants.ZERO_STRING + node[ApiConstants.API_RESPONSE_NODE_BANKNOTE_SELLING].InnerText.Replace(ApiConstants.POINT_STRING, ApiConstants.COMMA_STRING));
                    result.Data.Add(ExchangRate);
                }

                // Excel'e verileri aktar
                string excelFilePath = ExcelExporter.ExportToExcel(result.Data);

                if (excelFilePath != null)
                {
                    Console.WriteLine(String.Format(AppMessages.EXCEL_FILE_CREATED_SUCCESFULLY_MESSAGE, excelFilePath));
                }
                else
                {
                    Console.WriteLine(AppMessages.EXCEL_FILE_CREATE_FAILED_MESSAGE);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
