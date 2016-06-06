﻿using CMS.BLL.Services;
using Microsoft.Reporting.WebForms;
using Novacode;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var models = OrderSevice.GetCustomerOrders();
            return View(models);
        }

        public ActionResult Export(string type)
        {
            // 讀取.rdlc檔
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Rdlc"), "rdlcOrders.rdlc");
            lr.ReportPath = path;

            //取得訂單資料
            var list = OrderSevice.GetCustomerOrders();

            //設定資料庫
            ReportDataSource rd = new ReportDataSource("DataSet1", list);
            lr.DataSources.Add(rd);

            //檔案類型 type
            string reportType = type;
            string mineType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
             "<DeviceInfo>" +
            "  <OutputFormat>" + type + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mineType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warings
                );

            string fileType;
            switch (type)
            {
                case "Excel":
                    fileType = "xls";
                    break;

                case "word":
                    fileType = "doc";
                    break;
                case "pdf":
                    fileType = "pdf";
                    break;
                default:
                    fileType = "";
                    break;
            }

            // 回傳檔案 (Excel xls/ pdf pdf/word doc)
            return File(renderedBytes, mineType, "Report." + fileType);
        }

        //製作Word客製化報表, 使用DocX
        public ActionResult ExportByDocx()
        {
            //樣板路徑
            string TemplatePath = Server.MapPath("~/Templates/TestTemplate.docx");
            //儲存路徑
            string SavePath = @"D:/test.docx";
            DocX document = DocX.Load(TemplatePath);
            document.ReplaceText("{{OrderNumber}}", "555555");
            document.ReplaceText("{{Name}}", "Kyle");
            document.ReplaceText("{{CurrTime}}", DateTime.Now.ToShortTimeString());
            document.SaveAs(SavePath);
            return File(SavePath, "application/docx", "Report.docx");
        }

        public ActionResult UploadOrder()
        {
            return View();
        }

        //使用NPOI讀取Excel檔案中的Row Data
        [HttpPost]
        public ActionResult UploadOrder(IEnumerable<HttpPostedFileBase> UploadFile)
        {
            ViewBag.PageName = "請選擇檔案!";

            //儲存檔案
            foreach (var file in UploadFile)
            {
                if (file == null)
                {
                    ViewBag.Error = "副檔名錯誤，請下載樣板並上傳資料!";
                    return View();
                }

                if (Path.GetExtension(file.FileName) != ".xlsx")
                {
                    return View();
                }

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);
                string FilePath = @"D:\";
                file.InputStream.Position = 0;
                string FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(file.FileName);
                file.SaveAs(FilePath + FileName);
                ms.Dispose();
                ms.Close();

                //NPOI讀取
                XSSFWorkbook wb;
                using (FileStream fs = new FileStream(FilePath + FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    wb = new XSSFWorkbook(fs);
                    XSSFSheet MySheet;
                    MySheet = (XSSFSheet)wb.GetSheetAt(0);

                    //讀每筆資料 從1開始(跳過標提列)
                    for (int i = 1; i <= MySheet.LastRowNum; i++)
                    {
                        XSSFRow Row = (XSSFRow)MySheet.GetRow(i);

                        //讀取每欄資料
                        for (int k = 0; k < Row.Cells.Count; k++)
                        {
                            string MyTemp = Row.GetCell(k).ToString();
                        }
                    }
                }
            }
            return View();
        }

        //使用NPOI將資料庫資料匯出成自訂樣式的Excel檔案
        public ActionResult ExportExcelByNPOI()
        {
            //資料來源 訂單資料
            var models = OrderSevice.GetCustomerOrders();

            //讀取樣版
            string ExcelPath = Server.MapPath("~/Templates/ExportTemplete.xlsx");
            FileStream Template = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(Template);
            Template.Close();

            ISheet _sheet = workbook.GetSheetAt(0);
            //讀取Excel設定的字型樣式(第二列首欄)
            ICellStyle cellStyle = _sheet.GetRow(1).Cells[0].CellStyle;

            int CurrRow = 1; //起始列(跳過標題列)
            foreach (var item in models)
            {
                IRow MyRow = _sheet.CreateRow(CurrRow);
                CreateCell(item.OrderID.ToString(), MyRow, 0, cellStyle);
                CreateCell(item.OrderDate.ToString(), MyRow, 1, cellStyle);
                CreateCell(item.CustomerID.ToString(), MyRow, 2, cellStyle);
                CreateCell(item.ContactName.ToString(), MyRow, 3, cellStyle);
                CurrRow++;
            }

            string SavePath = @"D:/text.xlsx";
            FileStream file = new FileStream(SavePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return File(SavePath, "application/excel", "Report.xlsx");
        }

        private static void CreateCell(string Word, IRow ContentRow, int CellIndex, ICellStyle cellStyleBorder)
        {
            ICell _cell = ContentRow.CreateCell(CellIndex);
            _cell.SetCellValue(Word);
            _cell.CellStyle = cellStyleBorder;
        }
    }
}