using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;

namespace Maticsoft.Web.View.Business.FarmlandAdjustmentMng
{
    /// <summary>
    /// FarmlandAdjustmentHandler 的摘要说明
    /// </summary>
    public class FarmlandAdjustmentHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.T_FarmlandAdjustment bll_adjustment = new BLL.T_FarmlandAdjustment();
        BLL.T_Verif bll_verif = new BLL.T_Verif();
        BLL.T_AdministratorArea bll_administrator_area = new BLL.T_AdministratorArea();
        BLL.T_User bll_user = new BLL.T_User();

        Message message = new Message();
        string method;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            method = "";

            if (null != context.Request.QueryString["method"])
            {
                method = context.Request.QueryString["method"].ToString().Trim();
            }

            if (method == "add" || method == "modify")
            {
                AddOrModify(context);
            }
            else if (method == "delete")
            {
                Delete(context);
            }
            else if (method == "query")
            {
                //调用查询方法  
                Query(context);
            }
            else if (method == "IfHasAddSameCondition")
            {
                IfHasAddSameCondition(context);
            }
            else if (method == "getModelById")
            {
                GetModelById(context);
            }
            else if (method == "getList")
            {
                GetList(context);
            }
            else if (method == "getModelByYearAndArea")
            {
                GetModelByYearAndArea(context);
            }
        }

        private void IfHasAddSameCondition(HttpContext context)
        {
            string year, administratorArea, id;
            id = year = administratorArea = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorArea = context.Request.QueryString["administratorArea"].ToString().Trim();
            }

            List<Model.T_FarmlandAdjustment> list = new List<T_FarmlandAdjustment>();

            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and");
            if (id != "")
            {
                strWhere.AppendFormat(" id != {0} and ", id);
            }

            if (year != "")
            {
                strWhere.AppendFormat(" year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" administratorArea = '{0}' and ", administratorArea);
            }

            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }

            list = bll_adjustment.GetModelList(strWhere.ToString());
            if (list.Count > 0)
            {
                message.flag = true;
            }
            else
            {
                message.flag = false;
            }

            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        private void GetList(HttpContext context)
        {
            DataSet ds = bll_adjustment.GetList("");
            ds.Tables[0].Columns.Add("name", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string name = "";
                if (dr["year"].ToString() != "" && dr["administratorArea"] != null)
                {
                    name = dr["year"].ToString() + dr["administratorArea"].ToString();
                }
                dr["name"] = name;
            }
            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
        }

        private void GetModelByYearAndArea(HttpContext context)
        {
            string year, administratorArea;
            year = administratorArea = "";
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorArea = context.Request.QueryString["administratorArea"].ToString().Trim();
            }
            BLL.T_Verif bll_verif = new BLL.T_Verif();
            List<Model.T_Verif> list = bll_verif.GetModelList(" 1=1 and year = " + year + " and administratorArea=" + administratorArea);
            Model.T_Verif model = list[0];
            context.Response.Write(JsonHelper.Object2Json<Model.T_Verif>(model));
        }



        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, verifId, year, administratorArea, taskArea, adjustBeforeArea, adjustBeforeArableArea, adjustOutArea, adjustOutArableArea, adjustInArea, adjustInArableArea, des;
            id = verifId = year = administratorArea = taskArea = adjustBeforeArea = adjustBeforeArableArea = adjustOutArea = adjustOutArableArea = adjustInArea = adjustInArableArea = des = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorArea = context.Request.QueryString["administratorArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["verifId"])
            {
                verifId = context.Request.QueryString["verifId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["adjustBeforeArea"]) { adjustBeforeArea = context.Request.QueryString["adjustBeforeArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["adjustBeforeArableArea"]) { adjustBeforeArableArea = context.Request.QueryString["adjustBeforeArableArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["adjustOutArea"]) { adjustOutArea = context.Request.QueryString["adjustOutArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["adjustOutArableArea"]) { adjustOutArableArea = context.Request.QueryString["adjustOutArableArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["adjustInArea"]) { adjustInArea = context.Request.QueryString["adjustInArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["adjustInArableArea"]) { adjustInArableArea = context.Request.QueryString["adjustInArableArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["taskArea"]) { taskArea = context.Request.QueryString["taskArea"].ToString().Trim(); }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }



            if (id == "")
            {
                Model.T_FarmlandAdjustment model = new Model.T_FarmlandAdjustment();
                model.year = year;
                model.administratorArea = administratorArea;
                if (verifId != "") { model.verifId = Convert.ToInt32(verifId); } else { model.verifId = 0; }
                if (taskArea != "") { model.taskArea = Convert.ToDecimal(taskArea); } else { model.taskArea = 0; }
                if (adjustBeforeArea != "") { model.adjustBeforeArea = Convert.ToDecimal(adjustBeforeArea); } else { model.adjustBeforeArea = 0; }
                if (adjustBeforeArableArea != "") { model.adjustBeforeArableArea = Convert.ToDecimal(adjustBeforeArableArea); } else { model.adjustBeforeArableArea = 0; }
                if (adjustInArea != "") { model.adjustInArea = Convert.ToDecimal(adjustInArea); } else { model.adjustInArea = 0; }
                if (adjustInArableArea != "") { model.adjustInArableArea = Convert.ToDecimal(adjustInArableArea); } else { model.adjustInArableArea = 0; }
                if (adjustOutArea != "") { model.adjustOutArea = Convert.ToDecimal(adjustOutArea); } else { model.adjustOutArea = 0; }
                if (adjustOutArableArea != "") { model.adjustOutArableArea = Convert.ToDecimal(adjustOutArableArea); } else { model.adjustOutArableArea = 0; }
                if (context.Session["user_id"].ToString() != "")
                {
                    model.userId = Convert.ToInt32(context.Session["user_id"]);
                }
                else
                {
                    model.userId = 0;
                }
                model.createTime = System.DateTime.Now;
                model.isSubmited = 0;
                model.isDeleted = 0;
                model.des = des;

                int n = bll_adjustment.Add(model);
                if (n > 0)
                {
                    message.flag = true;
                    message.msg = "添加成功";
                }
                else
                {
                    message.flag = false;
                    message.msg = "添加失败";
                }
            }
            else
            {
                Model.T_FarmlandAdjustment model = bll_adjustment.GetModel(Convert.ToInt32(id));
                model.year = year;
                model.administratorArea = administratorArea;
                if (verifId != "") { model.verifId = Convert.ToInt32(verifId); } else { model.verifId = 0; }
                if (taskArea != "") { model.taskArea = Convert.ToDecimal(taskArea); } else { model.taskArea = 0; }
                if (adjustBeforeArea != "") { model.adjustBeforeArea = Convert.ToDecimal(adjustBeforeArea); } else { model.adjustBeforeArea = 0; }
                if (adjustBeforeArableArea != "") { model.adjustBeforeArableArea = Convert.ToDecimal(adjustBeforeArableArea); } else { model.adjustBeforeArableArea = 0; }
                if (adjustInArea != "") { model.adjustInArea = Convert.ToDecimal(adjustInArea); } else { model.adjustInArea = 0; }
                if (adjustInArableArea != "") { model.adjustInArableArea = Convert.ToDecimal(adjustInArableArea); } else { model.adjustInArableArea = 0; }
                if (adjustOutArea != "") { model.adjustOutArea = Convert.ToDecimal(adjustOutArea); } else { model.adjustOutArea = 0; }
                if (adjustOutArableArea != "") { model.adjustOutArableArea = Convert.ToDecimal(adjustOutArableArea); } else { model.adjustOutArableArea = 0; }
                if (context.Session["user_id"].ToString() != "")
                {
                    model.userId = Convert.ToInt32(context.Session["user_id"]);
                }
                else
                {
                    model.userId = 0;
                }
                model.createTime = System.DateTime.Now;
                model.isSubmited = 0;
                model.isDeleted = 0;
                model.des = des;

                bool flag = bll_adjustment.Update(model);
                if (flag)
                {
                    message.flag = true;
                    message.msg = "修改成功";
                }
                else
                {
                    message.flag = false;
                    message.msg = "修改失败";
                }
            }


            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }

        private void GetModelById(HttpContext context)
        {
            //throw new NotImplementedException();
        }

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_adjustment.DeleteList(ids);
            if (flag)
            {
                message.flag = true;
                message.msg = "删除成功";
            }
            else
            {
                message.flag = false;
                message.msg = "删除失败";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        private void Query(HttpContext context)
        {
            string name, year, administratorArea, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea = startTime = endTime = order = sort = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorArea = context.Request.QueryString["administratorArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["startTime"])
            {
                startTime = context.Request.QueryString["startTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["endTime"])
            {
                endTime = context.Request.QueryString["endTime"].ToString().Trim();
            }

            //================================================================  
            //获取分页和排序信息：页大小，页码，排序方式，排序字段  
            int pageRows, page;
            pageRows = 10;
            page = 1;

            if (null != context.Request.QueryString["rows"])
            {
                pageRows = int.Parse(context.Request.QueryString["rows"].ToString().Trim());

            }
            if (null != context.Request.QueryString["page"])
            {

                page = int.Parse(context.Request.QueryString["page"].ToString().Trim());

            }
            if (null != context.Request.QueryString["sort"])
            {

                order = context.Request.QueryString["sort"].ToString().Trim();

            }
            if (null != context.Request.QueryString["order"])
            {

                sort = context.Request.QueryString["order"].ToString().Trim();

            }

            //===================================================================  
            //组合查询语句：条件+排序  
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and");
            if (name != "")
            {
                strWhere.AppendFormat(" name like '%{0}%' and ", name);
            }

            if (year != "")
            {
                strWhere.AppendFormat(" year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" administratorArea = '{0}' and ", administratorArea);
            }
            if (startTime != "")
            {
                strWhere.AppendFormat(" create_time >= '{0}' and ", startTime);
            }
            if (endTime != "")
            {
                strWhere.AppendFormat(" create_time <= '{0}' and ", endTime);
            }

            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }
            if (sort != "" && order != "")
            {
                //strWhere.AppendFormat(" order by {0} {1}", sort, order);//添加排序  
                oderby = order + " " + sort;
            }

            //调用分页的GetList方法  
            DataSet ds = bll_adjustment.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、计划类型planTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("divisionArea", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int userId = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["userId"].ToString() != "" && dr["userId"] != null)
                {
                    userId = Convert.ToInt32(dr["userId"].ToString());
                }
                if (dr["verifId"].ToString() != "" && dr["verifId"] != null)
                {
                    dr["divisionArea"] = bll_verif.GetModel(Convert.ToInt32(dr["verifId"].ToString())).divisionArea;
                }

                dr["adjustAfterArea"] = Convert.ToDecimal(dr["adjustBeforeArea"]) - Convert.ToDecimal(dr["adjustOutArea"]) + Convert.ToDecimal(dr["adjustInArea"]);
                dr["adjustAfterArableArea"] = Convert.ToDecimal(dr["adjustBeforeArableArea"]) - Convert.ToDecimal(dr["adjustOutArableArea"]) + Convert.ToDecimal(dr["adjustInArableArea"]);

                Model.T_AdministratorArea t_administrator_area = bll_administrator_area.GetModel(administratorAreaId);
                if (t_administrator_area != null)
                {
                    dr["administratorAreaName"] = t_administrator_area.name;
                }
                else
                {
                    dr["administratorAreaName"] = "";
                }

                Model.T_User t_user = bll_user.GetModel(userId);
                if (t_user != null)
                {
                    dr["createUserName"] = t_user.name;
                }
                else
                {
                    dr["createUserName"] = "";
                }
            }

            int count = bll_adjustment.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
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