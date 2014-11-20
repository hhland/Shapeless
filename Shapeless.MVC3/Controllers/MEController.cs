using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace Shapeless.MVC3.Controllers
{
    public abstract class MEController<M> : Controller where M : class, new()
    {

        protected virtual int start
        {
            get { return 2; }
        }

        protected virtual int limit
        {
            get { return Int32.MaxValue; }
        }

        protected virtual void checkUpload(HttpPostedFileBase file)
        {

        }

        protected virtual IXLWorksheet loadIXLWorksheetFrom(XLWorkbook xlWorkbook)
        {
            return xlWorkbook.Worksheet(1);
        }

        protected bool isLastRow(int rowindex, IXLRow row)
        {
            return rowindex > limit || string.IsNullOrWhiteSpace(row.Cell(1).GetValue<string>());
        }

        protected abstract M toM(IXLRow row);

        protected abstract void eachRow(int rowindex, IXLRow row, M m);

        protected abstract void onImported();

        public ActionResult import()
        {

            HttpPostedFileBase file = Request.Files[0];
            checkUpload(file);
            using (
                XLWorkbook xlWorkbook = new XLWorkbook(file.InputStream))
            {
                IXLWorksheet worksheet = xlWorkbook.Worksheet(1);
                try
                {

                    worksheet = loadIXLWorksheetFrom(xlWorkbook);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("文件格式错误:{0}", ex.Message));
                }
                int ri = start;
                IXLRow row = null;
                do
                {
                    row = worksheet.Row(ri);
                    if (isLastRow(ri, row)) break;
                    eachRow(ri, row, toM(row));
                    ri++;
                } while (true);
                onImported();
            }
            return Content("{success:true}");
        }


        

    }
}
