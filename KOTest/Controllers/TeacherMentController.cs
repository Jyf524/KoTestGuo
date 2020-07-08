using KOTest.logic;
using KOTest.Service.Interface;
using KOTest.Service.Method;
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
    public class TeacherMentController : Controller
    {
        //
        // GET: /TeacherMent/

        //教师列表
        [HttpGet]
        public ActionResult TeacherInfoMentList(String TeacherName, String TeacherSex, int? page = 1)
        {
            ITeacherInfoMent TeacherInfoMethod = new TeacherInfoMent();
            return Json(TeacherInfoMethod.TeacherInfoMentList(TeacherName, TeacherSex, page), JsonRequestBehavior.AllowGet);
        }

        //删除教师
        [HttpPost]
        public ActionResult TeacherInfoMentDelete(String TeacherID)
        {
            ITeacherInfoMent TeacherInfoMethod = new TeacherInfoMent();
            return Content(TeacherInfoMethod.TeacherInfoMentDelete(TeacherID));
        }

        //教师修改的Get方法
        [HttpGet]
        public ActionResult TeacherInfoEditGet(String TeacherID)
        {
            ITeacherInfoMent TeacherInfoMethod = new TeacherInfoMent();
            if (TeacherInfoMethod.TeacherInfoEditGet(TeacherID) == null)
            {
                return Content(null);
            }
            return Json(TeacherInfoMethod.TeacherInfoEditGet(TeacherID), JsonRequestBehavior.AllowGet);
        }

        //教师修改的Post方法
        [HttpPost]
        public ActionResult TeacherInfoEditPost(String TeacherID, String TeacherName, String TeacherSex, String IdentityNumber)
        {
            ITeacherInfoMent TeacherInfoMethod = new TeacherInfoMent();
            return Content(TeacherInfoMethod.TeacherInfoEditPost(TeacherID, TeacherName, TeacherSex, IdentityNumber));
        }

        //添加教师
        [HttpPost]
        public ActionResult TeacherInfoAdd(String TeacherName, String TeacherSex, String IdentityNumber)
        {
            ITeacherInfoMent TeacherInfoMethod = new TeacherInfoMent();
            return Content(TeacherInfoMethod.TeacherInfoAdd(TeacherName, TeacherSex, IdentityNumber));
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
            dbdata.Columns.Add("TeacherID");
            dbdata.Columns.Add("TeacherName");
            dbdata.Columns.Add("TeacherSex");
            dbdata.Columns.Add("IdentityNumber");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["TeacherID"] = dr["教工号"];
                dr_["TeacherName"] = dr["教师姓名"];
                dr_["TeacherSex"] = dr["教师性别"];
                dr_["IdentityNumber"] = dr["身份证号"];
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
            dbdata.Columns.Add("TeacherID");
            dbdata.Columns.Add("TeacherName");
            dbdata.Columns.Add("TeacherSex");
            dbdata.Columns.Add("IdentityNumber");
            dbdata.Columns.Add("CreationTime");

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                DataRow dr = excelTable.Rows[i];
                DataRow dr_ = dbdata.NewRow();
                dr_["TeacherID"] = dr["教工号"];
                dr_["TeacherName"] = dr["教师姓名"];
                dr_["TeacherSex"] = dr["教师性别"];
                dr_["IdentityNumber"] = dr["身份证号"];
                dr_["CreationTime"] = dr["加入时间"];
                dbdata.Rows.Add(dr_);
            }

            RemoveEmpty(dbdata);

            string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];

            SqlBulkCopyByDatatable(constr, "Teacher", dbdata);

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
