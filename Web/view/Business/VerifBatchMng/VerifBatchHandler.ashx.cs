using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Data;
using System.Text;

namespace Maticsoft.Web.View.Business.VerifBatchMng
{
    /// <summary>
    /// VerifBatchHandler 的摘要说明
    /// </summary>
    public class VerifBatchHandler : IHttpHandler
    {
        BLL.T_Verif_Batch bll_verif_batch = new BLL.T_Verif_Batch();
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
            else if (method == "getModelByBatchId")
            {
                GetModelByBatchId(context);
            }
        }

        private void GetModelByBatchId(HttpContext context)
        {
            string batchId = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            BLL.T_Verif_Batch bll_verif_batch = new BLL.T_Verif_Batch();
            List<Model.T_Verif_Batch> list = bll_verif_batch.GetModelList(" 1=1 and batchId="+batchId);
            Model.T_Verif_Batch model;
            if (list.Count > 0)
            {
                model = list[0];
                context.Response.Write(JsonHelper.Object2Json<Model.T_Verif_Batch>(model));
            }
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
            DataSet ds = bll_verif.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、计划类型planTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
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

            int count = bll_verif.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, year, administratorArea, verifId, batchId, verifProvArea, verifProvArableArea, verifSelfArea, verifSelfArableArea;
            id = year = administratorArea = verifId = batchId = verifProvArea = verifProvArableArea = verifSelfArea = verifSelfArableArea = "";

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
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["verifProvArea"])
            {
                verifProvArea = context.Request.QueryString["verifProvArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["verifProvArableArea"])
            {
                verifProvArableArea = context.Request.QueryString["verifProvArableArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["verifSelfArea"])
            {
                verifSelfArea = context.Request.QueryString["verifSelfArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["verifSelfArableArea"])
            {
                verifSelfArableArea = context.Request.QueryString["verifSelfArableArea"].ToString().Trim();
            }

            if (id == "")
            {
                Model.T_Verif_Batch model = new Model.T_Verif_Batch();
                model.year = year;
                model.administratorArea = administratorArea;

                if (verifId != "") { model.verifId = Convert.ToInt32(verifId); } else { model.verifId = 0; }
                if (batchId != "") { model.batchId = Convert.ToInt32(batchId); } else { model.batchId = 0; }
                if (verifProvArea != "") { model.verifProvArea = Convert.ToDecimal(verifProvArea); } else { model.verifProvArea = 0; }
                if (verifProvArableArea != "") { model.verifProvArableArea = Convert.ToDecimal(verifProvArableArea); } else { model.verifProvArableArea = 0; }
                if (verifSelfArea != "") { model.verifSelfArea = Convert.ToDecimal(verifSelfArea); } else { model.verifSelfArea = 0; }
                if (verifSelfArableArea != "") { model.verifSelfArableArea = Convert.ToDecimal(verifSelfArableArea); } else { model.verifSelfArableArea = 0; }
                model.createTime = System.DateTime.Now;
                model.isDeleted = 0; model.isSubmited = 0; model.des = "";

                int n = bll_verif_batch.Add(model);
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
                Model.T_Verif_Batch model = bll_verif_batch.GetModel(Convert.ToInt32(id));
                model.year = year;
                model.administratorArea = administratorArea;

                if (verifId != "") { model.verifId = Convert.ToInt32(verifId); } else { model.verifId = 0; }
                if (batchId != "") { model.batchId = Convert.ToInt32(batchId); } else { model.batchId = 0; }
                if (verifProvArea != "") { model.verifProvArea = Convert.ToDecimal(verifProvArea); } else { model.verifProvArea = 0; }
                if (verifProvArableArea != "") { model.verifProvArableArea = Convert.ToDecimal(verifProvArableArea); } else { model.verifProvArableArea = 0; }
                if (verifSelfArea != "") { model.verifSelfArea = Convert.ToDecimal(verifSelfArea); } else { model.verifSelfArea = 0; }
                if (verifSelfArableArea != "") { model.verifSelfArableArea = Convert.ToDecimal(verifSelfArableArea); } else { model.verifSelfArableArea = 0; }
                model.createTime = System.DateTime.Now;
                model.isDeleted = 0; model.isSubmited = 0; model.des = "";

                bool flag = bll_verif_batch.Update(model);
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

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_verif_batch.DeleteList(ids);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}