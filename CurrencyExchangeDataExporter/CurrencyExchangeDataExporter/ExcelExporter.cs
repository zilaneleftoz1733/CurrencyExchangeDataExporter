using ClosedXML.Excel;
using CurrencyExchangeDataExporter.Constans;
using CurrencyExchangeDataExporter.Model;

namespace CurrencyExchangeDataExporter
{
    public class ExcelExporter
    {
        public static string ExportToExcel(List<ResponseDataExchangRate> data)
        {
            // Excel dosyasının oluşturulacağı konumu belirle
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ExcelConstants.EXCEL_NAME);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(ExcelConstants.EXCEL_PAGE_NAME);

                    // Başlık satırını ekle
                    var titleRow = worksheet.Row(1);
                   titleRow.Style.Font.Bold = true;

                    worksheet.Cell(1, 1).Value = ExcelConstants.EXCEL_COLUMN_HEADER_CURRENCY_CODE;
                    worksheet.Cell(1, 2).Value = ExcelConstants.EXCEL_COLUMN_HEADER_CURRENCY_NAME;
                    worksheet.Cell(1, 3).Value = ExcelConstants.EXCEL_COLUMN_HEADER_UNIT;
                    worksheet.Cell(1, 4).Value = ExcelConstants.EXCEL_COLUMN_HEADER_CURRENCY_TYPE;
                    worksheet.Cell(1, 5).Value = ExcelConstants.EXCEL_COLUMN_HEADER_FOREX_BUYING;
                    worksheet.Cell(1, 6).Value = ExcelConstants.EXCEL_COLUMN_HEADER_FOREX_SELLING;
                    worksheet.Cell(1, 7).Value = ExcelConstants.EXCEL_COLUMN_HEADER_BANKNOTE_BUYING;
                    worksheet.Cell(1, 8).Value = ExcelConstants.EXCEL_COLUMN_HEADER_BANKNOTE_SELLING;

                    // Verileri satırlara ekle
                    for (int i = 0; i < data.Count; i++)
                    {
                        var rate = data[i];
                        worksheet.Cell(i + 2, 1).Value = rate.CurrencyCode;
                        worksheet.Cell(i + 2, 2).Value = rate.CurrencyName;
                        worksheet.Cell(i + 2, 3).Value = rate.Unit;
                        worksheet.Cell(i + 2, 4).Value = rate.CurrencyType;
                        worksheet.Cell(i + 2, 5).Value = rate.ForexBuying;
                        worksheet.Cell(i + 2, 6).Value = rate.ForexSelling;
                        worksheet.Cell(i + 2, 7).Value = rate.BanknoteBuying;
                        worksheet.Cell(i + 2, 8).Value = rate.BanknoteSelling;
                    }

                    // Excel dosyasını kaydet
                    workbook.SaveAs(filePath);
                    Console.WriteLine(AppMessages.EXCEL_OPERATION_SUCCESS_MESSAGE);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(AppMessages.API_OPERATION_FAIL_MESSAGE, ex.Message));

                return null;
            }
        }
    }
}
