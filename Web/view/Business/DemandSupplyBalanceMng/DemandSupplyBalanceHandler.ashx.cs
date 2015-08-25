using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using System.Web.SessionState;

namespace Maticsoft.Web.View.Business.DemandSupplyBalanceMng
{
    /// <summary>
    /// DemandSupplyBalanceHandler 的摘要说明
    /// </summary>
    public class DemandSupplyBalanceHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.T_Batch_Demand_Supply_Balance bll_batch_balance = new BLL.T_Batch_Demand_Supply_Balance();
        BLL.T_DemandSupplyBalance bll_demand_supply_balance = new BLL.T_DemandSupplyBalance();
        BLL.T_AdministratorArea bll_administrator_area = new BLL.T_AdministratorArea();
        BLL.T_DemandSupplyBalanceType bll_demand_supplay_balance_type = new BLL.T_DemandSupplyBalanceType();
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
            if (method == "Lock")
            {
                Lock(context);
            }
            if (method == "submit")
            {
                Submit(context);
            }
            if (method == "add"||method=="modify")
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
            else if (method == "getModelById")
            {
                GetModelById(context);
            }
            else if (method == "getRemainArea")
            {
                GetRemainArea(context);
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

            Model.T_DemandSupplyBalance t_balance = new T_DemandSupplyBalance();
            t_balance = bll_demand_supply_balance.GetModel(Convert.ToInt32(id));
            if (t_balance.isDeleted == 1)
            {
                t_balance.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_balance.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_demand_supply_balance.Update(t_balance);

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
            List<Model.T_DemandSupplyBalance> list = new List<Model.T_DemandSupplyBalance>();

            if (ids != "") { strWhere += " and id in(" + ids + ")"; }

            list = bll_demand_supply_balance.GetModelList(strWhere);

            //是否需要提交
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].isSubmited = 1;
                    bll_demand_supply_balance.Update(list[i]);
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
            string dsBalanceId = "";
            if (null != context.Request.QueryString["dsBalanceId"])
            {
                dsBalanceId = context.Request.QueryString["dsBalanceId"].ToString().Trim();
            }
            Model.T_DemandSupplyBalance model = bll_demand_supply_balance.GetModel(Convert.ToInt32(dsBalanceId));

            //获取该批次已经使用面积
            decimal usedConsArea, usedAgriArea, usedArabArea;
            usedConsArea = usedAgriArea = usedArabArea = 0;
            DataSet ds = bll_batch_balance.GetList(" dsBalanceId=" + dsBalanceId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
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
            remainAreas.consArea = 0;
            remainAreas.agriArea = (Decimal)model.agriArea - usedAgriArea;
            remainAreas.arabArea = (Decimal)model.arabArea - usedArabArea;
            context.Response.Write(JsonHelper.Object2Json<RemainArea1>(remainAreas));
        }

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id,no ,name, year, administratorAreaId, isSubmited, typeId, scale, agriArea, arabArea, occupyArea, position, acceptUnit, acceptNo, acceptTime, des;
            id =no= name = year = administratorAreaId =isSubmited= typeId = scale = agriArea = arabArea = occupyArea = position = acceptUnit = acceptNo = acceptTime = des = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["no"])
            {
                no = context.Request.QueryString["no"].ToString().Trim();
            }
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorAreaId = context.Request.QueryString["administratorArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isSubmited"])
            {
                isSubmited = context.Request.QueryString["isSubmited"].ToString().Trim();
            }
            if (null != context.Request.QueryString["typeId"])
            {
                typeId = context.Request.QueryString["typeId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["scale"])
            {
                scale = context.Request.QueryString["scale"].ToString().Trim();
            }
            if (null != context.Request.QueryString["agriArea"])
            {
                agriArea = context.Request.QueryString["agriArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["arabArea"])
            {
                arabArea = context.Request.QueryString["arabArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["occupyArea"])
            {
                occupyArea = context.Request.QueryString["occupyArea"].ToString().Trim();
            }
            if (null != context.Request.QueryString["position"])
            {
                position = context.Request.QueryString["position"].ToString().Trim();
            }
            if (null != context.Request.QueryString["acceptUnit"])
            {
                acceptUnit = context.Request.QueryString["acceptUnit"].ToString().Trim();
            }
            if (null != context.Request.QueryString["acceptNo"])
            {
                acceptNo = context.Request.QueryString["acceptNo"].ToString().Trim();
            }
            if (null != context.Request.QueryString["acceptTime"])
            {
                acceptTime = context.Request.QueryString["acceptTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }



            if (id == "")
            {
                Model.T_DemandSupplyBalance model = new Model.T_DemandSupplyBalance();

                model.name = name;
                model.no = no;
                model.year = year;
                model.administratorArea = administratorAreaId;
                if (typeId != "")
                {
                    model.typeId = Convert.ToInt32(typeId);
                }
                else {
                    model.typeId = 0;
                }
                if (scale != "")
                {
                    model.scale = Convert.ToDecimal(scale);
                }
                else {
                    model.scale = 0;
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
                if (occupyArea != "")
                {
                    model.occupyArea = Convert.ToDecimal(occupyArea);
                }
                else
                {
                    model.occupyArea = 0;
                }
                model.position = position;
                model.acceptUnit = acceptUnit;
                model.acceptNo = acceptNo;
                if (acceptTime != "")
                {
                    model.acceptTime = Convert.ToDateTime(acceptTime);
                }
                else
                {
                    model.acceptTime = System.DateTime.Now;
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
                if (isSubmited != "") { model.isSubmited = Convert.ToInt32(isSubmited); } else { model.isSubmited = 0; }
                model.isDeleted = 0;
                model.des = des;

                int n = bll_demand_supply_balance.Add(model);
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
                Model.T_DemandSupplyBalance model = bll_demand_supply_balance.GetModel(Convert.ToInt32(id));
                model.no = no;
                model.name = name;
                model.year = year;
                model.administratorArea = administratorAreaId;
                if (typeId != "")
                {
                    model.typeId = Convert.ToInt32(typeId);
                }
                if (scale != "")
                {
                    model.scale = Convert.ToDecimal(scale);
                }
                else
                {
                    model.scale = 0;
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
                if (occupyArea != "")
                {
                    model.occupyArea = Convert.ToDecimal(occupyArea);
                }
                else
                {
                    model.occupyArea = 0;
                }
                model.position = position;
                model.acceptUnit = acceptUnit;
                model.acceptNo = acceptNo;
                if (acceptTime != "")
                {
                    model.acceptTime = Convert.ToDateTime(acceptTime);
                }
                else
                {
                    model.acceptTime = System.DateTime.Now;
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

                bool flag = bll_demand_supply_balance.Update(model);
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
            bool flag = bll_demand_supply_balance.DeleteList(ids);
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
            string batchTypeId,name, year, administratorArea, isSubmited,isDeleted, typeId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            batchTypeId = name = year = administratorArea = isDeleted = typeId = isSubmited = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["isSubmited"])
            {
                isSubmited = context.Request.QueryString["isSubmited"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
            }
            if (null != context.Request.QueryString["typeId"])
            {
                typeId = context.Request.QueryString["typeId"].ToString().Trim();
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
            if (isSubmited != "")
            {
                strWhere.AppendFormat(" isSubmited = '{0}' and ", isSubmited);
            }
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" isDeleted = '{0}' and ", isDeleted);
            }
            if (typeId != "")
            {
                strWhere.AppendFormat(" typeId = '{0}' and ", typeId);
            }

            if (startTime != "")
            {
                strWhere.AppendFormat(" create_time >= '{0}' and ", startTime);
            }
            if (endTime != "")
            {
                strWhere.AppendFormat(" create_time <= '{0}' and ", endTime);
            }

            if (batchTypeId != "")
            { //获取该批次应该获取哪几种类型的计划
                string dsTypeIds = "";
                if (batchTypeId == "1" || batchTypeId == "2" || batchTypeId == "3")//可以添加土地复垦、异地购买
                {
                    dsTypeIds = "1,2";
                }
                else if (batchTypeId == "4")//只能添加增减挂钩拆旧
                {//增减挂钩批次
                    dsTypeIds = "3";
                }

                if (batchTypeId != "")
                {
                    strWhere.AppendFormat(" typeId in ({0}) and ", dsTypeIds);
                }
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
            DataSet ds = bll_demand_supply_balance.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、计划类型planTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("typeName", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));

            ds.Tables[0].Columns.Add("usedAgriArea", typeof(System.String));
            ds.Tables[0].Columns.Add("remainAgriArea", typeof(System.String));
            ds.Tables[0].Columns.Add("usedArabArea", typeof(System.String));
            ds.Tables[0].Columns.Add("remainArabArea", typeof(System.String));
            ds.Tables[0].Columns.Add("isUsed", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows) {
                int administratorAreaId = 0;
                int demandSupplyBalanceTypeId = 0;
                decimal usedAgriArea = 0;
                decimal usedArabArea = 0;
                int userId = 0;

                if (utils.BusinessHelper.IsBalanceHasUsed(Convert.ToInt32(dr["id"])))
                {
                    dr["isUsed"] = true;
                }
                else
                {
                    dr["isUsed"] = false;
                }

                List<Model.T_Batch_Demand_Supply_Balance> list_batch_balance = bll_batch_balance.GetModelList(" dsBalanceId = " + dr["id"].ToString());
                if (list_batch_balance.Count > 0)
                {
                    foreach (Model.T_Batch_Demand_Supply_Balance t_batch_balance in list_batch_balance)
                    {
                        usedAgriArea += (decimal)t_batch_balance.agriArea;
                        usedArabArea += (decimal)t_batch_balance.arabArea;

                    }
                    dr["usedAgriArea"] = usedAgriArea;
                    dr["remainAgriArea"] = Convert.ToDecimal(dr["agriArea"]) - usedAgriArea;

                    dr["usedArabArea"] = usedArabArea;
                    dr["remainArabArea"] = Convert.ToDecimal(dr["arabArea"]) - usedArabArea;
                }
                else
                {
                    dr["usedAgriArea"] = 0;
                    dr["remainAgriArea"] = Convert.ToDecimal(dr["agriArea"]);
                    dr["usedArabArea"] = 0;
                    dr["remainArabArea"] = Convert.ToDecimal(dr["arabArea"]);
                }


                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"]!=null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["typeId"].ToString() != "" && dr["typeId"] != null)
                {
                    demandSupplyBalanceTypeId = Convert.ToInt32(dr["typeId"].ToString());
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

                Model.T_DemandSupplyBalanceType t_demand_supply_balance_type = bll_demand_supplay_balance_type.GetModel(demandSupplyBalanceTypeId);
                if (t_demand_supply_balance_type != null)
                {
                    dr["typeName"] = t_demand_supply_balance_type.name;
                }
                else
                {
                    dr["typeName"] = "";
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

            int count = bll_demand_supply_balance.GetRecordCount(strWhere.ToString());//获取条数  
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