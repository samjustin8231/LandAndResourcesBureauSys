using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using System.Text;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.View.Business.BatchPlanMng
{
    /// <summary>
    /// BatchPlanHandler 的摘要说明
    /// </summary>
    public class BatchPlanHandler : IHttpHandler
    {

        BLL.T_Batch bll_batch = new BLL.T_Batch();
        BLL.T_BatchType bll_batch_type = new BLL.T_BatchType();

        BLL.T_Plan bll_plan = new BLL.T_Plan();
        BLL.T_PlanType bll_plan_type = new BLL.T_PlanType();

        BLL.T_DemandSupplyBalanceType bll_ds_balance_type = new BLL.T_DemandSupplyBalanceType();
        BLL.T_DemandSupplyBalance bll_ds_balance = new BLL.T_DemandSupplyBalance();

        BLL.T_Batch_Plan bll_batch_plan = new BLL.T_Batch_Plan();
        BLL.T_Batch_Demand_Supply_Balance bll_batch_ds_balance = new BLL.T_Batch_Demand_Supply_Balance();

        BLL.T_LandBlock bll_land_block = new BLL.T_LandBlock();

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

            if (method == "GetAllKindPlansByBatchId")
            {
                GetAllKindPlansByBatchId(context);//根据批次id获取可以选择类型的计划
            }
        }

        private void GetAllKindPlansByBatchId(HttpContext context)
        {
            string batchId, name, year, administratorArea, isSubmited, planTypeId, order, sort, oderby;

            name = year = administratorArea = isSubmited = batchId = planTypeId = order = sort = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
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

            if (name != "")
            {
                strWhere.AppendFormat(" name like '%{0}%' and ", name);
            }
            if (isSubmited != "")
            {
                strWhere.AppendFormat(" P.isSubmited = '{0}' and ", isSubmited);//改为P and batch_paln
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

            if (batchId != "")
            {
                strWhere.AppendFormat(" batchId = '{0}' and ", batchId);
                if (batchId == "1" || batchId == "2")
                { //城镇批次、中心城区批次：可以添加国家、点供、奖励、其他计划
                    strWhere.Append(" (planId = '1' or planId = '2' or planId = '3' or planId = '4') and ");
                }
                else if (batchId == "3")//独立选址批次：只能添加该独立选址的计划
                {
                    strWhere.Append(" planId = '5' and ");
                }
                else if (batchId == "4")//增减挂钩批次：只能添加增减挂钩计划
                {
                    strWhere.Append(" planId = '6' and ");
                }
            }

            strWhere = utils.Util.removeLastAnd(strWhere);

            if (sort != "" && order != "")
            {
                //strWhere.AppendFormat(" order by {0} {1}", sort, order);//添加排序  
                oderby = order + " " + sort;
            }

            //调用分页的GetList方法  
            DataSet ds = bll_batch_plan.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            int count = bll_batch_plan.GetRecordCount(strWhere.ToString());//获取条数  
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