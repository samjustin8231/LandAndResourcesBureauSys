using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View.Business.BatchMng
{
    /// <summary>
    /// BatchHandler 的摘要说明
    /// </summary>
    public class BatchHandler : IHttpHandler, IRequiresSessionState
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

        BLL.T_Verif bll_verif = new BLL.T_Verif();
        BLL.T_Verif_Batch bll_batch_verif = new BLL.T_Verif_Batch();
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
            if (method == "Lock")
            {
                Lock(context);
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
            else if (method == "getListByTypeId")
            {
                GetListByTypeId(context);
            }
            else if (method == "getPlanListByBatchId")
            {
                GetPlanListByBatchId(context);
            }
            else if (method == "GetBatchListByPlanId")//根据计划id获取使用该计划的批次信息
            {
                GetBatchListByPlanId(context);
            }
            else if (method == "GetBatchListByDSId")//根据计划id获取使用该计划的批次信息
            {
                GetBatchListByDSId(context);
            }
            else if (method == "GetBatchListByVirifId")//根据计划id获取使用该计划的批次信息
            {
                GetBatchListByVirifId(context);
            }
            else if (method == "getDemandSupplyBalanceListByBatchId")
            {
                GetDemandSupplyBalanceListByBatchId(context);
            }
            else if (method == "GetRemainPlanArea")
            {
                GetRemainPlanArea(context);
            }
            else if (method == "GetRemainDSArea")
            {
                GetRemainDSArea(context);
            }
            else if (method == "addPlanToBatch")//addPlanToBatch
            {
                AddPlanToBatch(context);
            }
            else if (method == "deletePlanOfBatch")//addPlanToBatch
            {
                DeletePlanOfBatch(context);
            }
            else if (method == "addDemandSupplyBalanceToBatch")//AddDemandSupplyBalanceToBatch
            {
                AddDemandSupplyBalanceToBatch(context);
            }
            else if (method == "deleteDemandSupplyBalanceOfBatch")//deleteDemandSupplyBalanceOfBatch
            {
                DeleteDemandSupplyBalanceOfBatch(context);
            }
            else if (method == "IsUsedLevy")
            {
                IsUsedLevy(context);
            }
        }

        private void IsUsedLevy(HttpContext context)
        {
            string id = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }

            if (BusinessHelper.IsBatchHasUsedByLevy(Convert.ToInt32(id)))
            {
                message.flag = true;
            }
            else
            {
                message.flag = false;
            }

            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }

        private void GetBatchListByVirifId(HttpContext context)
        {
            string name, year, administratorArea, batchTypeId, VirifId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea = batchTypeId = VirifId = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["VirifId"])
            {
                VirifId = context.Request.QueryString["VirifId"].ToString().Trim();
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
            if (batchTypeId != "")
            {
                strWhere.AppendFormat(" batchTypeId = '{0}' and ", batchTypeId);
            }
            if (VirifId != "")
            {
                strWhere.AppendFormat(" VirifId = '{0}' and ", VirifId);
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
            DataSet ds = bll_batch_verif.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchTypeName", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int batchTypeId1 = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["batchTypeId"].ToString() != "" && dr["batchTypeId"] != null)
                {
                    batchTypeId1 = Convert.ToInt32(dr["batchTypeId"].ToString());
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

                Model.T_BatchType t_batch_type = bll_batch_type.GetModel(batchTypeId1);
                if (t_batch_type != null)
                {
                    dr["batchTypeName"] = t_batch_type.name;
                }
                else
                {
                    dr["batchTypeName"] = "";
                }
            }

            int count = bll_batch_plan.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
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

            Model.T_Batch t_batch = new T_Batch();
            t_batch = bll_batch.GetModel(Convert.ToInt32(id));
            if (t_batch.isDeleted == 1)
            {
                t_batch.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_batch.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_batch.Update(t_batch);

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

        private void GetBatchListByDSId(HttpContext context)
        {
            string name, year, administratorArea, batchTypeId, balanceId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea = batchTypeId = balanceId = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["balanceId"])
            {
                balanceId = context.Request.QueryString["balanceId"].ToString().Trim();
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
                strWhere.AppendFormat(" B.name like '%{0}%' and ", name);
            }

            if (year != "")
            {
                strWhere.AppendFormat(" B.year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" B.administratorArea = '{0}' and ", administratorArea);
            }
            if (batchTypeId != "")
            {
                strWhere.AppendFormat(" batchTypeId = '{0}' and ", batchTypeId);
            }
            if (balanceId != "")
            {
                strWhere.AppendFormat(" dsBalanceId = '{0}' and ", balanceId);
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
            DataSet ds = bll_batch_ds_balance.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchTypeName", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int batchTypeId1 = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["batchTypeId"].ToString() != "" && dr["batchTypeId"] != null)
                {
                    batchTypeId1 = Convert.ToInt32(dr["batchTypeId"].ToString());
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

                Model.T_BatchType t_batch_type = bll_batch_type.GetModel(batchTypeId1);
                if (t_batch_type != null)
                {
                    dr["batchTypeName"] = t_batch_type.name;
                }
                else
                {
                    dr["batchTypeName"] = "";
                }
            }

            int count = bll_batch_ds_balance.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }

        private void GetRemainDSArea(HttpContext context)
        {
            string batchId = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            Model.T_Batch t_batch = bll_batch.GetModel(Convert.ToInt32(batchId));

            //获取该批次已经使用面积
            decimal usedConsArea, usedAgriArea, usedArabArea;
            usedConsArea = usedAgriArea = usedArabArea = 0;
            DataSet ds = bll_batch_ds_balance.GetList("T.batchId=" + batchId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
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

            RemainArea1 remainAreas = new RemainArea1();
            remainAreas.consArea = (Decimal)t_batch.consArea - usedConsArea;
            remainAreas.agriArea = (Decimal)t_batch.agriArea - usedAgriArea;
            remainAreas.arabArea = (Decimal)t_batch.arabArea - usedArabArea;
            context.Response.Write(JsonHelper.Object2Json<RemainArea1>(remainAreas));
        }

        private void GetBatchListByPlanId(HttpContext context)
        {
            string name, year, administratorArea, batchTypeId,planId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea = batchTypeId = planId = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["planId"])
            {
                planId = context.Request.QueryString["planId"].ToString().Trim();
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
                strWhere.AppendFormat(" B.name like '%{0}%' and ", name);
            }

            if (year != "")
            {
                strWhere.AppendFormat(" B.year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" B.administratorArea = '{0}' and ", administratorArea);
            }
            if (batchTypeId != "")
            {
                strWhere.AppendFormat(" batchTypeId = '{0}' and ", batchTypeId);
            }
            if (planId != "")
            {
                strWhere.AppendFormat(" planId = '{0}' and ", planId);
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
            DataSet ds = bll_batch_plan.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchTypeName", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int batchTypeId1 = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["batchTypeId"].ToString() != "" && dr["batchTypeId"] != null)
                {
                    batchTypeId1 = Convert.ToInt32(dr["batchTypeId"].ToString());
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

                Model.T_BatchType t_batch_type = bll_batch_type.GetModel(batchTypeId1);
                if (t_batch_type != null)
                {
                    dr["batchTypeName"] = t_batch_type.name;
                }
                else
                {
                    dr["batchTypeName"] = "";
                }
            }

            int count = bll_batch_plan.GetRecordCount(strWhere.ToString());//获取条数  
            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }

        private void GetListByTypeId(HttpContext context)
        {
            string typeId,isDeleted;
            typeId = isDeleted = "";
            if (null != context.Request.QueryString["typeId"]&&""!= context.Request.QueryString["typeId"])
            {
                typeId = context.Request.QueryString["typeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"] && "" != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
            }

            StringBuilder strWhere = new StringBuilder();
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" isDeleted = '{0}' and ", isDeleted);
            }
            if (typeId != "")
            {
                strWhere.AppendFormat(" batchTypeId = '{0}' and ", typeId);
            }

            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }

            DataSet ds = bll_batch.GetList(strWhere.ToString());
            DataTable dt = ds.Tables[0];
            DataRow row = dt.NewRow();
            row["id"] = 0;
            row["name"] = "-- 请选择 --";
            row["des"] = "";
            dt.Rows.InsertAt(row, 0);
             
            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
            
        }

        /// <summary>
        /// 获取该批次剩余面积
        /// </summary>
        /// <param name="context"></param>
        private void GetRemainPlanArea(HttpContext context)
        {
            string batchId = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            Model.T_Batch t_batch = bll_batch.GetModel(Convert.ToInt32(batchId));

            DataSet ds = bll_batch_plan.GetList("batchId=" + batchId);

            //获取该批次已经使用面积
            decimal usedConsArea, usedAgriArea, usedArabArea, usedIssuedQuota;
            usedConsArea = usedAgriArea = usedArabArea = usedIssuedQuota = 0;

            RemainArea2 remainAreas = new RemainArea2();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (t_batch.batchTypeId == 4)//增减挂钩批次
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

            if (t_batch.batchTypeId == 4)//增减挂钩批次
            {
                remainAreas.issuedQuota = (Decimal)t_batch.agriArea - usedIssuedQuota;
            }
            else {
                remainAreas.consArea = (Decimal)t_batch.consArea - usedConsArea;
                remainAreas.agriArea = (Decimal)t_batch.agriArea - usedAgriArea;
                remainAreas.arabArea = (Decimal)t_batch.arabArea - usedArabArea;
            }
            
            context.Response.Write(JsonHelper.Object2Json<RemainArea2>(remainAreas));
        }


        /// <summary>
        /// 根据批次id获取该批次占补平衡信息
        /// </summary>
        /// <param name="context"></param>
        private void GetDemandSupplyBalanceListByBatchId(HttpContext context)
        {
            string batchId = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }

            if (batchId == "")
            {
                batchId = "0";
            }
            DataSet ds = bll_batch_ds_balance.GetList("T.batchId=" + batchId);

            ds.Tables[0].Columns.Add("typeName", typeof(System.String));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //根据planTypeId获取name
                if (dr["typeId"] != null && dr["typeId"] != DBNull.Value)
                {
                    Model.T_DemandSupplyBalanceType t_ds_balance_type = bll_ds_balance_type.GetModel(Convert.ToInt32(dr["typeId"]));
                    if (t_ds_balance_type != null)
                    {
                        dr["typeName"] = t_ds_balance_type.name;
                    }
                }
            }

            context.Response.Write(JsonHelper.Dataset2Json(ds));
            
        }

        /// <summary>
        /// 添加计划到批次中
        /// </summary>
        /// <param name="context"></param>
        private void AddPlanToBatch(HttpContext context)
        {
            string batchId, planId, consArea, agriArea, arabArea, issuedQuota;
            batchId = planId = consArea = agriArea = arabArea = issuedQuota = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["planId"])
            {
                planId = context.Request.QueryString["planId"].ToString().Trim();
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

            Model.T_Batch_Plan model = new T_Batch_Plan();
            if (planId != "")
            {
                model.planId = Convert.ToInt32(planId);
            }
            else
            {
                model.planId = 0;
            }
            if (batchId != "")
            {
                model.batchId = Convert.ToInt32(batchId);
            }
            else
            {
                model.batchId = 0;
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
            model.createTime = System.DateTime.Now;
            int n = bll_batch_plan.Add(model);
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
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        /// <summary>
        /// 删除批次中的计划
        /// </summary>
        /// <param name="context"></param>
        private void DeletePlanOfBatch(HttpContext context) {
            String ids = context.Request["ids"];
            bool flag = bll_batch_plan.DeleteList(ids);
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

        /// <summary>
        /// 添加占补平衡到批次中
        /// </summary>
        /// <param name="context"></param>
        private void AddDemandSupplyBalanceToBatch(HttpContext context)
        {
            string batchId, dsBalanceId, consArea, agriArea, arabArea;
            batchId = dsBalanceId = consArea = agriArea = arabArea = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["dsBalanceId"])
            {
                dsBalanceId = context.Request.QueryString["dsBalanceId"].ToString().Trim();
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

            Model.T_Batch_Demand_Supply_Balance t_batch_balance = new T_Batch_Demand_Supply_Balance();
            if (dsBalanceId != "")
            {
                t_batch_balance.dsBalanceId = Convert.ToInt32(dsBalanceId);
            }
            else
            {
                t_batch_balance.dsBalanceId = 0; 
            }
            if (batchId != "")
            {
                t_batch_balance.batchId = Convert.ToInt32(batchId);
            }
            else
            {
                t_batch_balance.batchId = 0;
            }
            if (consArea != "")
            {
                t_batch_balance.consArea = Convert.ToDecimal(consArea);
            }
            else
            {
                t_batch_balance.consArea = 0;
            }
            if (agriArea != "")
            {
                t_batch_balance.agriArea = Convert.ToDecimal(agriArea);
            }
            else
            {
                t_batch_balance.agriArea = 0;
            }
            if (arabArea != "")
            {
                t_batch_balance.arabArea = Convert.ToDecimal(arabArea);
            }
            else
            {
                t_batch_balance.arabArea = 0;
            }
            t_batch_balance.createTime = System.DateTime.Now;
            int n = bll_batch_ds_balance.Add(t_batch_balance);

            bool flag = true;

            if (t_batch_balance.batchId == 4)
            { //增减挂钩拆旧
                //农用地面积=0，耕地面积>0时，补充至耕地面积=剩余的耕地面积
                RemainArea1 remainAera = BusinessHelper.GetRemainAreaByBalanceId(t_batch_balance.dsBalanceId);
                if (Convert.ToInt32(remainAera.agriArea) == 0&&Convert.ToInt32(remainAera.arabArea) > 0)
                {
                    flag = false;

                     Model.T_DemandSupplyBalance t_balance = bll_ds_balance.GetModel(t_batch_balance.dsBalanceId);

                    //1、修改拆旧信息
                    t_balance.adjustArea = remainAera.arabArea;
                    t_balance.des = "xxx";
                    bll_ds_balance.Update(t_balance);

                    //2、插入复垦信息
                    Model.T_DemandSupplyBalance t_balance_new = new T_DemandSupplyBalance();
                    t_balance_new.typeId = 1;
                    t_balance_new.year = t_balance.year;
                    t_balance_new.administratorArea = t_balance.administratorArea;
                    t_balance_new.name = t_balance.name + " 【转为复垦】";
                    t_balance_new.no = t_balance.no + " 【转为复垦】";

                    t_balance_new.agriArea = 0;
                    t_balance_new.arabArea = remainAera.arabArea;
                    t_balance_new.scale = remainAera.arabArea;
                    t_balance_new.adjustArea = 0;
                    t_balance_new.des = t_balance.no +"耕地"+remainAera.arabArea+ " 【转为复垦】";
                    t_balance_new.isDeleted = 0;
                    t_balance_new.isSubmited = 1;
                    t_balance_new.createTime = System.DateTime.Now;
                    t_balance_new.batchId = t_batch_balance.batchId;
                    bll_ds_balance.Add(t_balance_new);

                    message.flag = true;
                    message.msg = "添加成功,该拆旧剩余农用地=0,耕地>0,耕地自动转为复垦.";
                }
            }

            if (flag){
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

            
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        private void DeleteDemandSupplyBalanceOfBatch(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_batch_ds_balance.DeleteList(ids);
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

        private void GetPlanListByBatchId(HttpContext context)
        {
            string batchId = "";
            StringBuilder strWhere = new StringBuilder();
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            if (batchId == "")
            {
                batchId = "0";
            }

            if (batchId != "")
            {
                strWhere.AppendFormat(" batchId = '{0}' and ", batchId);
            }

            strWhere = utils.Util.removeLastAnd(strWhere);

            DataSet ds = bll_batch_plan.GetList(strWhere.ToString());

            ds.Tables[0].Columns.Add("typeName", typeof(System.String));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //根据planTypeId获取name
                if (dr["typeId"] != null && dr["typeId"] != DBNull.Value)
                {
                    Model.T_PlanType t_plan_type = bll_plan_type.GetModel(Convert.ToInt32(dr["typeId"]));
                    if (t_plan_type != null)
                    {
                        dr["typeName"] = t_plan_type.name;
                    }
                }
            }
            

            context.Response.Write(JsonHelper.Dataset2Json(ds));
        }

        

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, name, year, administratorArea, batchTypeId, totalArea, addConsArea, consArea, agriArea, unusedArea, arabArea, approvalAuthority, approvalNo, approvalTime, des;
            id = name = year = administratorArea = batchTypeId = totalArea = addConsArea = unusedArea = consArea = agriArea = arabArea = approvalAuthority = approvalNo = approvalTime = des = "";

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
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["totalArea"])
            {
                totalArea = context.Request.QueryString["totalArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["addConsArea"])
            {
                addConsArea = context.Request.QueryString["addConsArea"].ToString().Trim();
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
            if (null != context.Request.QueryString["unusedArea"])
            {
                unusedArea = context.Request.QueryString["unusedArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["approvalNo"])
            {
                approvalNo = context.Request.QueryString["approvalNo"].ToString().Trim();
            }
            if (null != context.Request.QueryString["approvalAuthority"])
            {
                approvalAuthority = context.Request.QueryString["approvalAuthority"].ToString().Trim();
            }
            if (null != context.Request.QueryString["approvalTime"])
            {
                approvalTime = context.Request.QueryString["approvalTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }

            if (id == "")
            {
                Model.T_Batch model = new Model.T_Batch();

                model.name = name;
                model.year = year;
                model.administratorArea = administratorArea;
                if (batchTypeId != "")
                {
                    model.batchTypeId = Convert.ToInt32(batchTypeId);
                }
                else
                {
                    model.batchTypeId = 0;
                }
                if (totalArea != "")
                {
                    model.totalArea = Convert.ToDecimal(totalArea);
                }
                else
                {
                    model.totalArea = 0;
                }
                model.hasLevyArea = 0;
                if (addConsArea != "")
                {
                    model.addConsArea = Convert.ToDecimal(addConsArea);
                }
                else
                {
                    model.addConsArea = 0;
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
                if (consArea != "")
                {
                    model.consArea = Convert.ToDecimal(consArea);
                }
                else
                {
                    model.consArea = 0;
                }
                if (unusedArea != "")
                {
                    model.unusedArea = Convert.ToDecimal(unusedArea);
                }
                else
                {
                    model.unusedArea = 0;
                }

                model.approvalNo = approvalNo;
                model.approvalAuthority = approvalAuthority;
                if (approvalTime != "")
                {
                    model.approvalTime = Convert.ToDateTime(approvalTime);
                }
                else
                {
                    model.approvalTime = System.DateTime.Now;
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
                model.isSubmited = 0;
                model.isDeleted = 0;
                model.des = des;

                int n = bll_batch.Add(model);
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
                Model.T_Batch model = bll_batch.GetModel(Convert.ToInt32(id));

                model.name = name;
                model.year = year;
                model.administratorArea = administratorArea;
                if (batchTypeId != "")
                {
                    model.batchTypeId = Convert.ToInt32(batchTypeId);
                }
                if (totalArea != "")
                {
                    model.totalArea = Convert.ToDecimal(totalArea);
                }
                else
                {
                    model.totalArea = 0;
                }
                
                if (addConsArea != "")
                {
                    model.addConsArea = Convert.ToDecimal(addConsArea);
                }
                else
                {
                    model.addConsArea = 0;
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
                if (consArea != "")
                {
                    model.consArea = Convert.ToDecimal(consArea);
                }
                else
                {
                    model.consArea = 0;
                }
                if (unusedArea != "")
                {
                    model.unusedArea = Convert.ToDecimal(unusedArea);
                }
                else
                {
                    model.unusedArea = 0;
                }

                model.approvalNo = approvalNo;
                model.approvalAuthority = approvalAuthority;
                if (approvalTime != "")
                {
                    model.approvalTime = Convert.ToDateTime(approvalTime);
                }
                else
                {
                    model.approvalTime = System.DateTime.Now;
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
                model.isDeleted = 0;
                model.des = des;

                bool flag = bll_batch.Update(model);
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
            //获取id
            string id = "";
            Model.T_Batch t_batch = new T_Batch();
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
                t_batch = bll_batch.GetModel(Convert.ToInt32(id));
            }
            context.Response.Write(JsonHelper.Object2Json<T_Batch>(t_batch));
        }

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            

            //删除批次对应的计划、占补平衡、地块 todo:
            List<Model.T_Batch_Plan> list_batch_plan = bll_batch_plan.GetModelList(" T.batchId = '"+ids+"'");
            foreach (Model.T_Batch_Plan t_batch_plan in list_batch_plan) {
                bll_batch_plan.Delete(t_batch_plan.id);
            }

            List<Model.T_Batch_Demand_Supply_Balance> list_batch_balance = bll_batch_ds_balance.GetModelList(" T.batchId = '" + ids + "'");
            foreach (Model.T_Batch_Demand_Supply_Balance t_batch_balance in list_batch_balance)
            {
                bll_batch_ds_balance.Delete(t_batch_balance.id);
            }

            List<Model.T_LandBlock> list_land_block = bll_land_block.GetModelList(" batchId = '" + ids + "'");
            foreach (Model.T_LandBlock t_land_block in list_land_block)
            {
                bll_land_block.Delete(t_land_block.id);
            }

            //删除批次
            bool flag = bll_batch.DeleteList(ids);

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
            string name, year, administratorArea, batchTypeId,isDeleted, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea = batchTypeId = isDeleted = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["batchTypeId"])
            {
                batchTypeId = context.Request.QueryString["batchTypeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
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
            if (batchTypeId != "" && batchTypeId != "0")
            {
                strWhere.AppendFormat(" batchTypeId = '{0}' and ", batchTypeId);
            }

            if (startTime != "")
            {
                strWhere.AppendFormat(" create_time >= '{0}' and ", startTime);
            }
            if (endTime != "")
            {
                strWhere.AppendFormat(" create_time <= '{0}' and ", endTime);
            }
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" isDeleted = '{0}' and ", isDeleted);
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
            DataSet ds = bll_batch.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、批次类型batchTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchTypeName", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));

            ds.Tables[0].Columns.Add("totalUsed", typeof(System.String));
            ds.Tables[0].Columns.Add("agriUsed", typeof(System.String));
            ds.Tables[0].Columns.Add("arabUsed", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int administratorAreaId = 0;
                int planTypeId = 0;
                int userId = 0;
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["batchTypeId"].ToString() != "" && dr["batchTypeId"] != null)
                {
                    planTypeId = Convert.ToInt32(dr["batchTypeId"].ToString());
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

                Model.T_BatchType t_batch_type = bll_batch_type.GetModel(planTypeId);
                if (t_batch_type != null)
                {
                    dr["batchTypeName"] = t_batch_type.name;
                }
                else
                {
                    dr["batchTypeName"] = "";
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

                //获取已经添加的农用地、耕地、地块

                List<Model.T_Batch_Plan> list_batch_plan = bll_batch_plan.GetModelList(" T.batchId=" + dr["id"].ToString());

                if (dr["batchTypeId"].ToString() == "4") //增减挂钩
                {
                    decimal usedAgriArea = 0;
                    foreach (Model.T_Batch_Plan t_batch_plan in list_batch_plan)
                    {
                        usedAgriArea += (decimal)t_batch_plan.issuedQuota;
                    }
                    dr["agriUsed"] = usedAgriArea;
                }
                else {
                    
                    decimal usedAgriArea = 0;
                    foreach (Model.T_Batch_Plan t_batch_plan in list_batch_plan)
                    {
                        usedAgriArea += (decimal)t_batch_plan.agriArea;
                    }
                    dr["agriUsed"] = usedAgriArea;

                    
                }


                List<Model.T_Batch_Demand_Supply_Balance> list_batch_ds = bll_batch_ds_balance.GetModelList(" T.batchId=" + dr["id"].ToString());
                decimal usedagrableArea = 0;
                foreach (Model.T_Batch_Demand_Supply_Balance t_batch_ds in list_batch_ds)
                {
                    usedagrableArea += (decimal)t_batch_ds.arabArea;
                }
                dr["arabUsed"] = usedagrableArea;
                

                List<Model.T_LandBlock> list_land_block = bll_land_block.GetModelList(" batchId=" + dr["id"].ToString());
                decimal usedArea = 0;
                foreach (Model.T_LandBlock t_land_block in list_land_block)
                {
                    usedArea += (decimal)t_land_block.area;
                }
                dr["totalUsed"] = usedArea;
            }

            int count = bll_batch.GetRecordCount(strWhere.ToString());//获取条数  
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