using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Common;
using System.Data;
using System.Configuration;

namespace Maticsoft.Web.View.Sys.ajax
{
    /// <summary>
    /// UtilHandler 的摘要说明
    /// </summary>
    public class UtilHandler : IHttpHandler
    {
        string method;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            method = "";

            if (null != context.Request.QueryString["method"])
            {
                method = context.Request.QueryString["method"].ToString().Trim();
            }

            if (method == "getYearList")
            {
                GetYearList(context);
            }
            else if (method == "getAdministratorNameById")
            {
                GetAdministratorNameById(context);
            }
        }

        private void GetYearList(HttpContext context)
        {
            DataSet ds = new DataSet("ds_year");
            DataTable dt = ds.Tables.Add("dt_year");

            dt.Columns.Add("id", Type.GetType("System.String"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns[0].AutoIncrement = true;
            dt.Columns[0].AutoIncrementSeed = 1;
            dt.Columns[0].AutoIncrementStep = 1;

            int curYear = DateTime.Now.Year;
            int minYear = Convert.ToInt32(ConfigurationManager.AppSettings["startYear"].ToString());

            for (int i = curYear; i >= minYear; i--) {
                dt.Rows.Add(new object[]{i,i});
            }

            /*
             DataRow row = dt.NewRow();
            row["id"] = "0";
            row["name"] = "-- 请选择 --";
            dt.Rows.InsertAt(row, 0);
             */
            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
        }

        private void GetAdministratorNameById(HttpContext context)
        {
            string id = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            BLL.T_AdministratorArea bll_administrator_area = new BLL.T_AdministratorArea();
            Model.T_AdministratorArea t_administrator_area = bll_administrator_area.GetModel(Convert.ToInt32(id));
            context.Response.Write(JsonHelper.Object2Json<Model.T_AdministratorArea>(t_administrator_area));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}