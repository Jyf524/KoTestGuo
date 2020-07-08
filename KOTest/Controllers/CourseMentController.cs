using KOTest.logic;
using KOTest.Service.Interface;
using KOTest.Service.Method;
using KOTest.Service.Model;
using Newtonsoft.Json;
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
    public class CourseMentController : Controller
    {
        //
        // GET: /CourseMent/

        //图片上传
        public bool IsImageUp = false;
      

        [HttpPost]
        public ActionResult ImageSave()
        {
            string imgurl = null;
            if (Request.Files.Count > 0)
            {
                string paths = null;
                HttpPostedFileBase f = Request.Files["file"];
                string path = imgurl = "/Files/" + f.FileName;
                CourseInfoAddDM.Url = path;
                paths = Server.MapPath(path);
                f.SaveAs(paths);
                IsImageUp = true;
            }
            return Json(imgurl);
        }

        //课程列表
        [HttpGet]
        public ActionResult CourseInfoMentList(String SearchIngTxt, String CourseKind, String DepartmentName, int? page = 1)
        {
            ICourseInfoMent CourseInfoMethod = new CourseInfoMent();
            return Json(CourseInfoMethod.CourseInfoMentList(SearchIngTxt, CourseKind, DepartmentName,page), JsonRequestBehavior.AllowGet);

        }

        //删除课程
        [HttpPost]
        public ActionResult CourseInfoMentDelete(String CourseID)
        {
            ICourseInfoMent CourseInfoMethod = new CourseInfoMent();
            return Content(CourseInfoMethod.CourseInfoMentDelete(CourseID));
        }

        //课程修改的Get方法
        [HttpGet]
        public ActionResult CourseInfoEditGet(String CourseID)
        {
            ICourseInfoMent CourseInfoMethod = new CourseInfoMent();
            if (CourseInfoMethod.CourseInfoEditGet(CourseID) == null)
            {
                return Content(null);
            }
            return Json(CourseInfoMethod.CourseInfoEditGet(CourseID), JsonRequestBehavior.AllowGet);
        }

        //课程修改的Post方法
        [HttpPost]
        public ActionResult CourseInfoEditPost(String CourseID,String CourseNumber, String CourseName, String FullName, String FirstSpelling, String AllSpelling, String CourseKind, String DepartmentName, String Introduction)
        {
            ICourseInfoMent CourseInfoMethod = new CourseInfoMent();
            return Content(CourseInfoMethod.CourseInfoEditPost(CourseID,CourseNumber, CourseName, FullName, FirstSpelling, AllSpelling, CourseKind, DepartmentName, Introduction));
        }


        //添加课程
        [HttpPost]
        public ActionResult CourseInfoAdd(String CourseNumber, String CourseName, String FullName, String FirstSpelling,String AllSpelling, String CourseKind, String DepartmentName, String Introduction)
        {
          
            ICourseInfoMent CourseInfoMethod = new CourseInfoMent();

            //string fileSave = Server.MapPath("~/Files/");
            //string path1 = "";
            //try
            //{
            //    HttpFileCollectionBase file = Request.Files;
            //    if (file.Count != 0)
            //    {
            //        for (int i = 0; i < file.Count; i++)
            //        {
            //                HttpPostedFileBase file1 = file[i];
            //                string extName = Path.GetExtension(file1.FileName);
            //                string newName = Guid.NewGuid().ToString() + extName;
            //                file1.SaveAs(Path.Combine(fileSave, newName));
            //                path1 = "/Files/" + newName;
                        
            //        }
            //    }
            //}
            //catch
            //{
            //    return Content("抱歉，您选择文件过大请重新选择");
            //}

            return Content(CourseInfoMethod.CourseInfoAdd(CourseInfoAddDM.Url, CourseNumber, CourseName, FullName, FirstSpelling, AllSpelling, CourseKind, DepartmentName, Introduction));
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
            dbdata.Columns.Add("CourseID");
            dbdata.Columns.Add("CourseNumber");
            dbdata.Columns.Add("CourseName");
            dbdata.Columns.Add("FullName");
            dbdata.Columns.Add("FirstSpelling");
            dbdata.Columns.Add("AllSpelling");
            dbdata.Columns.Add("CourseKind");
            dbdata.Columns.Add("DepartmentName");
            dbdata.Columns.Add("Introduction");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["CourseID"] = dr["课程号"];
                dr_["CourseNumber"] = dr["编号"];
                dr_["CourseName"] = dr["名称"];
                dr_["FullName"] = dr["全称"];
                dr_["FirstSpelling"] = dr["首拼"];
                dr_["AllSpelling"] = dr["全拼"];
                dr_["CourseKind"] = dr["类型"];
                dr_["DepartmentName"] = dr["系部"];
                dr_["Introduction"] = dr["简介"];
                dr_["CreationTime"] = dr["创建时间"];
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
            dbdata.Columns.Add("CourseID");
            dbdata.Columns.Add("CourseNumber");
            dbdata.Columns.Add("CourseName");
            dbdata.Columns.Add("FullName");
            dbdata.Columns.Add("FirstSpelling");
            dbdata.Columns.Add("AllSpelling");
            dbdata.Columns.Add("CourseKind");
            dbdata.Columns.Add("DepartmentName");
            dbdata.Columns.Add("Introduction");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["CourseID"] = dr["课程号"];
                dr_["CourseNumber"] = dr["编号"];
                dr_["CourseName"] = dr["名称"];
                dr_["FullName"] = dr["全称"];
                dr_["FirstSpelling"] = dr["首拼"];
                dr_["AllSpelling"] = dr["全拼"];
                dr_["CourseKind"] = dr["类型"];
                dr_["DepartmentName"] = dr["系部"];
                dr_["Introduction"] = dr["简介"];
                dr_["CreationTime"] = dr["创建时间"];
                dbdata.Rows.Add(dr_);
            }
            RemoveEmpty(dbdata);

            string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];

            SqlBulkCopyByDatatable(constr, "Course", dbdata);

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
    }
}
