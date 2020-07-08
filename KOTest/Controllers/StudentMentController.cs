using KOTest.logic;
using KOTest.Service.Interface;
using KOTest.Service.Method;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KOTest.Controllers
{
    public class StudentMentController : Controller
    {
        //
        // GET: /StudentMent/

        //学生列表
        [HttpGet]
        public ActionResult StudentInfoMentList(String UsersName, String UsersSex, int? page = 1)
        {
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();
            return Json(StudentInfoMethod.StudentInfoMentList(UsersName, UsersSex, page), JsonRequestBehavior.AllowGet);

        }

        //删除学生
        [HttpPost]
        public ActionResult StudentInfoMentDelete(Array arrs)
        {
            var str = arrs.GetValue(0).ToString();
            var getArr = str.Split(',');
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();
            return Content(StudentInfoMethod.StudentInfoMentDelete(getArr));
        }

        //学生修改的Get方法
        [HttpGet]
        public ActionResult StudentInfoEditGet(String UsersID)
        {
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();
            if (StudentInfoMethod.StudentInfoEditGet(UsersID) == null)
            {
                return Content(null);
            }
            return Json(StudentInfoMethod.StudentInfoEditGet(UsersID), JsonRequestBehavior.AllowGet);
        }

        //学生修改的Post方法
        [HttpPost]
        public ActionResult StudentInfoEditPost(String UsersID, String UsersName)
        {
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();
            return Content(StudentInfoMethod.StudentInfoEditPost(UsersID, UsersName));
        }

        //添加学生
        [HttpPost]
        public ActionResult StudentInfoAdd(String UsersWorkID, String UsersName, String Password,String UsersSex, String UsersAge)
        {
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();
            return Content(StudentInfoMethod.StudentInfoAdd(UsersWorkID, UsersName, Password,UsersSex, UsersAge));
        }

        //预览
        [HttpPost]
        public ActionResult Preview(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();
            excelTable = ImportExcel.GetExcelDataTable(path);

            DataTable dbdata = new DataTable();
            dbdata.Columns.Add("UsersID");
            dbdata.Columns.Add("UsersWorkID");
            dbdata.Columns.Add("UsersName");
            dbdata.Columns.Add("UsersSex");
            dbdata.Columns.Add("UsersAge");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["UsersID"] = dr["学生编号"];
                dr_["UsersWorkID"] = dr["工号"];
                dr_["UsersName"] = dr["姓名"];
                dr_["UsersSex"] = dr["性别"];
                dr_["UsersAge"] = dr["年龄"];
                dr_["CreationTime"] = dr["加入时间"];
                dbdata.Rows.Add(dr_);
            }
            RemoveEmpty(dbdata);

            var dataa = JsonConvert.SerializeObject(dbdata);
            return Json(dataa, JsonRequestBehavior.AllowGet);
        }

        //导入
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
            string path = Path.Combine(filePath, fileName);
            file.SaveAs(path);

            DataTable excelTable = new DataTable();
            excelTable = ImportExcel.GetExcelDataTable(path);

            DataTable dbdata = new DataTable();
            dbdata.Columns.Add("UsersID");
            dbdata.Columns.Add("UsersWorkID");
            dbdata.Columns.Add("UsersName");
            dbdata.Columns.Add("UsersSex");
            dbdata.Columns.Add("UsersAge");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["UsersID"] = dr["学生编号"];
                dr_["UsersWorkID"] = dr["工号"];
                dr_["UsersName"] = dr["姓名"];
                dr_["UsersSex"] = dr["性别"];
                dr_["UsersAge"] = dr["年龄"];
                dr_["CreationTime"] = dr["加入时间"];
                dbdata.Rows.Add(dr_);
            }
            RemoveEmpty(dbdata);

            string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];

            SqlBulkCopyByDatatable(constr, "Users", dbdata);

            return Content("导入成功！");
        }
        /// <summary>
        /// 大数据插入
        /// </summary>
        /// <param name="connectionString">目标库连接</param>
        /// <param name="TableName">目标表</param>
        /// <param name="dtSelect">来源数据</param>
        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dtSelect)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.BatchSize = 20000;
                        sqlbulkcopy.BulkCopyTimeout = 0;//不限时间
                        for (int i = 0; i < dtSelect.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dtSelect);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        protected void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }

        //导出数据
        public ActionResult StudentExportList()
        {
            IStudentInfoMent StudentInfoMethod = new StudentInfoMent();

            var Result = StudentInfoMethod.StudentExportList().UsersList.ToList();

            //var Result = model1.ExhibitionList;
            HSSFWorkbook ExcelBook = new HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = ExcelBook.CreateSheet("学生信息");
            // 第一列
            NPOI.SS.UserModel.IRow row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("学生编号");
            row.CreateCell(1).SetCellValue("学生姓名");
            row.CreateCell(2).SetCellValue("学生性别");
            row.CreateCell(3).SetCellValue("学生年龄");
            row.CreateCell(4).SetCellValue("加入时间");
            //IDataFormat dataformat = ExcelBook.CreateDataFormat();  
            //ICellStyle style1 = ExcelBook.CreateCellStyle();
            //style1.DataFormat = dataformat.GetFormat("yyyy年MM月dd日 hh:mm"); 

            for (int i = 0; i < Result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(Result[i].UsersWorkID);
                rowtemp.CreateCell(1).SetCellValue(Result[i].UsersName);
                rowtemp.CreateCell(2).SetCellValue(Result[i].UsersSex);
                rowtemp.CreateCell(3).SetCellValue(Result[i].UsersAge);
                rowtemp.CreateCell(6).SetCellValue(Result[i].CreationTime.ToString());
            }
            var fileName = "学生信息表.xls";
            MemoryStream ExcelStream = new MemoryStream();
            //string urlPath = "/Execl/" + fileName; // 文件下载的URL地址，供给前台下载
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("\\" + urlPath); // 文件路径
            //FileStream file = new FileStream(filePath, FileMode.Create);
            //ExcelBook.Write(file);
            ExcelBook.Write(ExcelStream);
            ExcelStream.Seek(0, SeekOrigin.Begin);
            return File(ExcelStream, "application/vnd.ms-excel", fileName);            //return  Content(TF.CreateExcel());

        }
    }
}
