using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using System.Web.SessionState;

namespace Maticsoft.Web.View.Business.PlanMng
{
    /// <summary>
    /// PlanHandler administratorArea
    /// </summary>
    public class PlanHandler : IHttpHandler, IRequiresSessionState
    {

        BLL.T_Plan bll_plan = new BLL.T_Plan();
        BLL.T_Batch_Plan bll_batch_plan = new BLL.T_Batch_Plan();
        BLL.T_AdministratorArea bll_administrator_area = new BLL.T_AdministratorArea();
        BLL.T_PlanType bll_plan_type = new BLL.T_PlanType();
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

            if (method == "add"||method=="modify")
            {
                AddOrModify(context);
            }
            if (method == "Lock")
            {
                Lock(context);
            }
            if (method == "submit")
            {
                Submit(context);
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
            else if (method == "getModelById")
            {
                GetModelById(context);
            }
            else if (method == "getRemainArea")
            {
                GetRemainArea(context);
            }
            else if (method == "GetSummaryList")
            {
                GetSummaryList(context);
            }
        }

        private void Lock(HttpContext context)
        {
            bool flag = false;
            string id = "";
            string opt = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }

            Model.T_Plan t_plan = new T_Plan();
            t_plan = bll_plan.GetModel(Convert.ToInt32(id));
            if (t_plan.isDeleted == 1)
            {
                t_plan.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_plan.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_plan.Update(t_plan);

            message = new Message();
            if (flag)
            {
                message.flag = true;
                message.msg = opt + "成功";
            }
            else
            {
                message.flag = false;
                message.msg = opt + "失败";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));//返回给前台页面  
        }

        private void GetSummaryList(HttpContext context)
        {
            string name, year, administratorArea,isDeleted, isSubmited, planTypeId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea =isDeleted= isSubmited  = planTypeId = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["isSubmited"])
            {
                isSubmited = context.Request.QueryString["isSubmited"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
            }
            if (null != context.Request.QueryString["planTypeId"])
            {
                planTypeId = context.Request.QueryString["planTypeId"].ToString().Trim();
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
            if (isSubmited != "")
            {
                strWhere.AppendFormat(" isSubmited = '{0}' and ", isSubmited);
            }
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" isDeleted = '{0}' and ", isDeleted);
            }
            if (year != "")
            {
                strWhere.AppendFormat(" year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" administratorArea = '{0}' and ", administratorArea);
            }
            if (planTypeId != "")
            {
                strWhere.AppendFormat(" planTypeId = '{0}' and ", planTypeId);
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
            DataSet ds = bll_plan.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、计划类型planTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("isUsed", typeof(System.String));

            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("planTypeName", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));

            ds.Tables[0].Columns.Add("usedConsArea", typeof(System.String));
            ds.Tables[0].Columns.Add("remainConsArea", typeof(System.String));
            ds.Tables[0].Columns.Add("usedAgriArea", typeof(System.String));
            ds.Tables[0].Columns.Add("remainAgriArea", typeof(System.String));
            ds.Tables[0].Columns.Add("usedArabArea", typeof(System.String));
            ds.Tables[0].Columns.Add("remainArabArea", typeof(System.String));

            ds.Tables[0].Columns.Add("usedIssuedQuota", typeof(System.String));
            ds.Tables[0].Columns.Add("remainIssuedQuota", typeof(System.String));
            //添加其他字段
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int planTypeId1 = 0;
                decimal usedConsArea = 0;
                decimal usedAgriArea = 0;
                decimal usedArabArea = 0;
                decimal usedIssuedQuota = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["planTypeId"].ToString() != "" && dr["planTypeId"] != null)
                {
                    planTypeId1 = Convert.ToInt32(dr["planTypeId"].ToString());
                }

                if (utils.BusinessHelper.IsPlanHasUsed(Convert.ToInt32(dr["id"])))
                {
                    dr["isUsed"] = true;
                }
                else
                {
                    dr["isUsed"] = false;
                }

                List<Model.T_Batch_Plan> list_batch_plan = bll_batch_plan.GetModelList(" planId = " + dr["id"].ToString());
                if (list_batch_plan.Count > 0)
                {
                    foreach (Model.T_Batch_Plan t_batch_plan in list_batch_plan)
                    {
                        if (t_batch_plan.planId == 6)
                        {//增减挂钩批次
                            usedIssuedQuota += (decimal)t_batch_plan.issuedQuota;
                        }
                        else {
                            usedConsArea += (decimal)t_batch_plan.consArea;
                            usedAgriArea += (decimal)t_batch_plan.agriArea;
                            usedArabArea += (decimal)t_batch_plan.arabArea;
                        }
                        
                    }
                    dr["usedConsArea"] = usedAgriArea;
                    dr["remainConsArea"] = Convert.ToDecimal(dr["consArea"]) - usedAgriArea;

                    dr["usedAgriArea"] = usedAgriArea;
                    dr["remainAgriArea"] = Convert.ToDecimal(dr["agriArea"]) - usedAgriArea;

                    dr["usedArabArea"] = usedAgriArea;
                    dr["remainArabArea"] = Convert.ToDecimal(dr["arabArea"]) - usedAgriArea;

                    dr["usedIssuedQuota"] = usedAgriArea;
                    dr["remainIssuedQuota"] = Convert.ToDecimal(dr["issuedQuota"]) - usedAgriArea;
                }
                else {
                    dr["usedConsArea"] = 0;
                    dr["remainConsArea"] = Convert.ToDecimal(dr["agriArea"]);
                    dr["usedAgriArea"] = 0;
                    dr["remainAgriArea"] = Convert.ToDecimal(dr["agriArea"]);
                    dr["usedArabArea"] = 0;
                    dr["remainArabArea"] = Convert.ToDecimal(dr["agriArea"]);
                    dr["usedIssuedQuota"] = 0;
                    dr["remainIssuedQuota"] = Convert.ToDecimal(dr["issuedQuota"]);
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

                Model.T_PlanType t_plan_type = bll_plan_type.GetModel(planTypeId1);
                if (t_plan_type != null)
                {
                    dr["planTypeName"] = t_plan_type.name;
                }
                else
                {
                    dr["planTypeName"] = "";
                }

            }

            int count = bll_plan.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }

        private void Submit(HttpContext context)
        {
            string ids = "";

            if (context.Request["ids"] != null)
            {
                ids = context.Request["ids"];
            }

            //where语句
            String strWhere = " 1=1 and isSubmited=0";
           
            //获取所有未提交的
            List<Model.T_Plan> list = new List<Model.T_Plan>();

            if (ids != "") { strWhere += " and id in(" + ids + ")"; }

            list = bll_plan.GetModelList(strWhere);

            //是否需要提交
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].isSubmited = 1;
                    bll_plan.Update(list[i]);
                }
                message.flag = true;
                message.msg = "提交成功";
            }
            else
            {
                message.flag = false;
                message.msg = "提交成功";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        /// <summary>
        /// 获取该批次剩余面积
        /// </summary>
        /// <param name="context"></param>
        private void GetRemainArea(HttpContext context)
        {
            string planId = "";
            if (null != context.Request.QueryString["planId"])
            {
                planId = context.Request.QueryString["planId"].ToString().Trim();
            }
            Model.T_Plan t_plan = bll_plan.GetModel(Convert.ToInt32(planId));

            //获取该批次已经使用面积
            decimal usedConsArea, usedAgriArea, usedArabArea, usedIssuedQuota;
            usedConsArea = usedAgriArea = usedArabArea = usedIssuedQuota = 0;
            RemainArea2 remainAreas = new RemainArea2();

            DataSet ds = bll_batch_plan.GetList(" planId=" + planId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (planId == "6")//增减挂钩批次
                    {
                        if (dr["issuedQuota"] != null)
                        {
                            usedIssuedQuota += Convert.ToDecimal(dr["issuedQuota"]);
                        }
                    }
                    else
                    {
                        if (dr["consArea"] != null)
                        {
                            usedConsArea += Convert.ToDecimal(dr["consArea"]);
                        }
                        if (dr["agriArea"] != null)
                        {
                            usedAgriArea += Convert.ToDecimal(dr["agriArea"]);
                        }
                        if (dr["arabArea"] != null)
                        {
                            usedArabArea += Convert.ToDecimal(dr["arabArea"]);
                        }
                    }
                }
            }
            
            remainAreas.consArea = (Decimal)t_plan.consArea - usedConsArea;
            remainAreas.agriArea = (Decimal)t_plan.agriArea - usedAgriArea;
            remainAreas.arabArea = (Decimal)t_plan.arabArea - usedArabArea;
            remainAreas.issuedQuota = (Decimal)t_plan.agriArea - usedIssuedQuota;

            context.Response.Write(JsonHelper.Object2Json<RemainArea2>(remainAreas));
        }

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, name, year, administratorArea,isSubmited, planTypeId, consArea, agriArea, arabArea, issuedQuota, remainQuota, releaseNo, releaseTime, des;
            id = name = year = administratorArea = planTypeId = isSubmited = consArea = agriArea = arabArea = issuedQuota = remainQuota = releaseNo = releaseTime = des = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
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
            if (null != context.Request.QueryString["isSubmited"])
            {
                isSubmited = context.Request.QueryString["isSubmited"].ToString().Trim();
            }
            if (null != context.Request.QueryString["planTypeId"])
            {
                planTypeId = context.Request.QueryString["planTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["consArea"])
            {
                consArea = context.Request.QueryString["consArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["agriArea"])
            {
                agriArea = context.Request.QueryString["agriArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["arabArea"])
            {
                arabArea = context.Request.QueryString["arabArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["issuedQuota"])
            {
                issuedQuota = context.Request.QueryString["issuedQuota"].ToString().Trim();
            }
            if (null != context.Request.QueryString["remainQuota"])
            {
                remainQuota = context.Request.QueryString["remainQuota"].ToString().Trim();
            }
            if (null != context.Request.QueryString["releaseNo"])
            {
                releaseNo = context.Request.QueryString["releaseNo"].ToString().Trim();
            }
            if (null != context.Request.QueryString["releaseTime"])
            {
                releaseTime = context.Request.QueryString["releaseTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }



            if (id == "")
            {
                Model.T_Plan model = new Model.T_Plan();

                model.name = name;
                model.year = year;
                model.administratorArea = administratorArea;
                if (planTypeId != "")
                {
                    model.planTypeId = Convert.ToInt32(planTypeId);
                }
                else {
                    model.planTypeId = 0;
                }
                if (consArea != "")
                {
                    model.consArea = Convert.ToDecimal(consArea);
                }
                else {
                    model.consArea = 0;
                }
                if (agriArea != "")
                {
                    model.agriArea = Convert.ToDecimal(agriArea);
                }
                else
                {
                    model.agriArea = 0;
                }
                if (arabArea != "")
                {
                    model.arabArea = Convert.ToDecimal(arabArea);
                }
                else
                {
                    model.arabArea = 0;
                }
                if (issuedQuota != "")
                {
                    model.issuedQuota = Convert.ToDecimal(issuedQuota);
                }
                else
                {
                    model.issuedQuota = 0;
                }
                if (remainQuota != "")
                {
                    model.remainQuota = Convert.ToDecimal(remainQuota);
                }
                else
                {
                    model.remainQuota = 0;
                }
                model.releaseNo = releaseNo;
                if (releaseTime != "")
                {
                    model.releaseTime = Convert.ToDateTime(releaseTime);
                }
                else
                {
                    model.releaseTime = System.DateTime.Now;
                }
                if (context.Session["user_id"].ToString() != "")
                {
                    model.userId = Convert.ToInt32(context.Session["user_id"]);
                }
                else
                {
                    model.userId = 0;
                }
                if (isSubmited != "") { model.isSubmited = Convert.ToInt32(isSubmited); } else { model.isSubmited = 0; }
                model.createTime = System.DateTime.Now;
                model.isDeleted = 0;
                model.des = des;

                int n = bll_plan.Add(model);
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
                Model.T_Plan model = bll_plan.GetModel(Convert.ToInt32(id));

                model.name = name;
                model.year = year;
                model.administratorArea = administratorArea;
                if (planTypeId != "")
                {
                    model.planTypeId = Convert.ToInt32(planTypeId);
                }
                if (consArea != "")
                {
                    model.consArea = Convert.ToDecimal(consArea);
                }
                else
                {
                    model.consArea = 0;
                }
                if (agriArea != "")
                {
                    model.agriArea = Convert.ToDecimal(agriArea);
                }
                else
                {
                    model.agriArea = 0;
                }
                if (arabArea != "")
                {
                    model.arabArea = Convert.ToDecimal(arabArea);
                }
                else
                {
                    model.arabArea = 0;
                }
                if (issuedQuota != "")
                {
                    model.issuedQuota = Convert.ToDecimal(issuedQuota);
                }
                else
                {
                    model.issuedQuota = 0;
                }
                if (remainQuota != "")
                {
                    model.remainQuota = Convert.ToDecimal(remainQuota);
                }
                else
                {
                    model.remainQuota = 0;
                }
                model.releaseNo = releaseNo;
                if (releaseTime != "")
                {
                    model.releaseTime = Convert.ToDateTime(releaseTime);
                }
                else
                {
                    model.releaseTime = System.DateTime.Now;
                }
                if (context.Session["user_id"].ToString() != "")
                {
                    model.userId = Convert.ToInt32(context.Session["user_id"]);
                }
                else
                {
                    model.userId = 0;
                }
                model.createTime = System.DateTime.Now;
                model.des = des;

                bool flag = bll_plan.Update(model);
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
            bool flag = bll_plan.DeleteList(ids);
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
            string name, year, administratorArea,isDeleted, isSubmited, batchTypeId,planTypeId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea =isDeleted= isSubmited =batchTypeId =planTypeId  = startTime = endTime = order = sort = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
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
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isSubmited"])
            {
                isSubmited = context.Request.QueryString["isSubmited"].ToString().Trim();
            }
            if (null != context.Request.QueryString["planTypeId"])
            {
                planTypeId = context.Request.QueryString["planTypeId"].ToString().Trim();
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
            if (isSubmited != "")
            {
                strWhere.AppendFormat(" isSubmited = '{0}' and ", isSubmited);
            }
            if (year != "" && year != "0")
            {
                strWhere.AppendFormat(" year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" administratorArea = '{0}' and ", administratorArea);
            }
            if (planTypeId != "" && planTypeId!="0")
            {
                strWhere.AppendFormat(" planTypeId = '{0}' and ", planTypeId);
            }
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" isDeleted = '{0}' and ", isDeleted);
            }

            if (batchTypeId != "") { //获取该批次应该获取哪几种类型的计划
                string planTypeIds = "";
                if (batchTypeId == "1" || batchTypeId == "2") {
                    planTypeIds = "1,2,3,4";
                }
                else if (batchTypeId == "3")
                {//独立选址批次
                    planTypeIds = "5";//
                }
                else if (batchTypeId == "4")
                {//增减挂钩批次
                    planTypeIds = "6";
                }

                if (batchTypeId != "")
                {
                    strWhere.AppendFormat(" planTypeId in ({0}) and ", planTypeIds);
                }
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
            DataSet ds = bll_plan.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、计划类型planTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("planTypeName", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));
            ds.Tables[0].Columns.Add("isUsed", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows) {
                int administratorAreaId = 0;
                int planTypeId1 = 0;
                int userId = 0;

                if (utils.BusinessHelper.IsPlanHasUsed(Convert.ToInt32(dr["id"])))
                {
                    dr["isUsed"] = true;
                }
                else {
                    dr["isUsed"] = false;
                }

                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"]!=null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["planTypeId"].ToString() != "" && dr["planTypeId"] != null)
                {
                    planTypeId1 = Convert.ToInt32(dr["planTypeId"].ToString());
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
                else {
                    dr["administratorAreaName"] = "";
                }

                Model.T_PlanType t_plan_type = bll_plan_type.GetModel(planTypeId1);
                if (t_plan_type != null)
                {
                    dr["planTypeName"] = t_plan_type.name;
                }
                else
                {
                    dr["planTypeName"] = "";
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

            int count = bll_plan.GetRecordCount(strWhere.ToString());//获取条数  
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