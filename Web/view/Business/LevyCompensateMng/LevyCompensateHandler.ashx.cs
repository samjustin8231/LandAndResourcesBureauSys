using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Maticsoft.Model;
using System.Data;
using Maticsoft.Common;
using System.Text;

namespace Maticsoft.Web.View.Business.LevyCompensateMng
{
    /// <summary>
    /// LevyCompensateHandler 的摘要说明
    /// </summary>
    public class LevyCompensateHandler : IHttpHandler, IRequiresSessionState
    {

        BLL.T_LevyCompensate bll_levy = new BLL.T_LevyCompensate();
    
        BLL.T_AdministratorArea bll_administrator_area = new BLL.T_AdministratorArea();
        BLL.T_Batch bll_batch = new BLL.T_Batch();
        BLL.T_BatchType bll_batch_type = new BLL.T_BatchType();
        BLL.T_LandBlock bll_land_block = new BLL.T_LandBlock();
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
            if (method == "Lock")
            {
                Lock(context);
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
            else if (method == "EditHasLevyArea")
            {
                EditHasLevyArea(context);
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

            Model.T_LevyCompensate t_levy = new T_LevyCompensate();
            t_levy = bll_levy.GetModel(Convert.ToInt32(id));
            if (t_levy.isDeleted == 1)
            {
                t_levy.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_levy.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_levy.Update(t_levy);

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

        private void EditHasLevyArea(HttpContext context)
        {
            //获取字段:【用户name】  
            string id, hasLevyArea;
            id = hasLevyArea = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["hasLevyArea"])
            {
                hasLevyArea = context.Request.QueryString["hasLevyArea"].ToString().Trim();
            }

            Model.T_LevyCompensate model = bll_levy.GetModel(Convert.ToInt32(id));

            if(hasLevyArea!=""){
                model.hasLevyArea = Convert.ToDecimal(hasLevyArea);
            }

            bool flag = bll_levy.Update(model);
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

            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }

        /// <summary>
        /// 获取该批次剩余面积
        /// </summary>
        /// <param name="context"></param>
        private void GetRemainArea(HttpContext context)
        {
            
        }

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, year, administratorArea, batchId, landblockId, batchTypeId, town, village, _group, totalPeopleNumber, planLevyArea, hasLevyArea, levyNationalLandArea, levyColletLandArea, levyPreDeposit, countrySocialSecurityFund, countryCompensate, areaWaterFacilitiesCompensate, provHeavyAgriculturalFunds, provAdditionalFee, provReclamationFee, provArableLandTax, provSurveyFee, provServiceFee, des;
            id = year = administratorArea = batchId = landblockId = batchTypeId = town = village = _group = totalPeopleNumber = planLevyArea = hasLevyArea = levyNationalLandArea = levyColletLandArea = levyPreDeposit = countrySocialSecurityFund = countryCompensate = areaWaterFacilitiesCompensate = provHeavyAgriculturalFunds = provAdditionalFee = provReclamationFee = provArableLandTax = provSurveyFee = provServiceFee = des = "";
            
            //获取参数
            if (context.Request["id"] != null && context.Request["id"].ToString().Trim() != "")
            {
                id = context.Request["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["year"])
            {
                year = context.Request.QueryString["year"].ToString().Trim();
            }
            if (null != context.Request.QueryString["administratorArea"])
            {
                administratorArea = context.Request.QueryString["administratorArea"].ToString().Trim();
            }
            if (context.Request["batchId"] != null && context.Request["batchId"].ToString().Trim() != "")
            {
                batchId = context.Request["batchId"].ToString().Trim();
            }
            if (context.Request["landblockId"] != null && context.Request["landblockId"].ToString().Trim() != "")
            {
                landblockId = context.Request["landblockId"].ToString().Trim();
            }
            if (context.Request["batchTypeId"] != null && context.Request["batchTypeId"].ToString().Trim() != "")
            {
                batchTypeId = context.Request["batchTypeId"].ToString().Trim();
            }
            if (context.Request["town"] != null && context.Request["town"].ToString().Trim() != "")
            {
                town = context.Request["town"].ToString().Trim();
            }
            if (context.Request["village"] != null && context.Request["village"].ToString().Trim() != "")
            {
                village = context.Request["village"].ToString().Trim();
            }
            if (context.Request["_group"] != null && context.Request["_group"].ToString().Trim() != "")
            {
                _group = context.Request["_group"].ToString().Trim();
            }
            if (context.Request["totalPeopleNumber"] != null && context.Request["totalPeopleNumber"].ToString().Trim() != "")
            {
                totalPeopleNumber = context.Request["totalPeopleNumber"].ToString().Trim();
            }
            if (context.Request["hasLevyArea"] != null && context.Request["hasLevyArea"].ToString().Trim() != "")
            {
                hasLevyArea = context.Request["hasLevyArea"].ToString().Trim();
            }
            if (context.Request.QueryString["planLevyArea"] != null && context.Request["planLevyArea"].ToString().Trim() != "")
            {
                planLevyArea = context.Request.QueryString["planLevyArea"].ToString();
            }
            if (context.Request.QueryString["levyNationalLandArea"] != null && context.Request["levyNationalLandArea"].ToString().Trim() != "")
            {
                levyNationalLandArea = context.Request.QueryString["levyNationalLandArea"].ToString();
            }
            if (context.Request.QueryString["levyColletLandArea"] != null && context.Request["levyColletLandArea"].ToString().Trim() != "")
            {
                levyColletLandArea = context.Request.QueryString["levyColletLandArea"].ToString();
            }

            if (context.Request.QueryString["levyPreDeposit"] != null && context.Request["levyPreDeposit"].ToString().Trim() != "")
            {
                levyPreDeposit = context.Request.QueryString["levyPreDeposit"].ToString();
            }
            if (context.Request.QueryString["countrySocialSecurityFund"] != null && context.Request["countrySocialSecurityFund"].ToString().Trim() != "")
            {
                countrySocialSecurityFund = context.Request.QueryString["countrySocialSecurityFund"].ToString();
            }
            if (context.Request.QueryString["countryCompensate"] != null && context.Request["countryCompensate"].ToString().Trim() != "")
            {
                countryCompensate = context.Request.QueryString["countryCompensate"].ToString();
            }
            if (context.Request.QueryString["areaWaterFacilitiesCompensate"] != null && context.Request["areaWaterFacilitiesCompensate"].ToString().Trim() != "")
            {
                areaWaterFacilitiesCompensate = context.Request.QueryString["areaWaterFacilitiesCompensate"].ToString();
            }
            if (context.Request.QueryString["provHeavyAgriculturalFunds"] != null && context.Request["provHeavyAgriculturalFunds"].ToString().Trim() != "")
            {
                provHeavyAgriculturalFunds = context.Request.QueryString["provHeavyAgriculturalFunds"].ToString();
            }
            if (context.Request.QueryString["provAdditionalFee"] != null && context.Request["provAdditionalFee"].ToString().Trim() != "")
            {
                provAdditionalFee = context.Request.QueryString["provAdditionalFee"].ToString();
            }
            if (context.Request.QueryString["provReclamationFee"] != null && context.Request["provReclamationFee"].ToString().Trim() != "")
            {
                provReclamationFee = context.Request.QueryString["provReclamationFee"].ToString();
            }
            if (context.Request.QueryString["provArableLandTax"] != null && context.Request["provArableLandTax"].ToString().Trim() != "")
            {
                provArableLandTax = context.Request.QueryString["provArableLandTax"].ToString();
            }
            if (context.Request.QueryString["provSurveyFee"] != null && context.Request["provSurveyFee"].ToString().Trim() != "")
            {
                provSurveyFee = context.Request.QueryString["provSurveyFee"].ToString();
            }
            if (context.Request.QueryString["provServiceFee"] != null && context.Request["provServiceFee"].ToString().Trim() != "")
            {
                provServiceFee = context.Request.QueryString["provServiceFee"].ToString();
            }
            if (context.Request.QueryString["des"] != null)
            {
                des = context.Request.QueryString["des"];
            }



            if (id == "")
            {
                Model.T_LevyCompensate model = new Model.T_LevyCompensate();

                if (batchTypeId != ""){model.batchTypeId = Convert.ToInt32(batchTypeId);}else{model.batchTypeId = 0;}
                model.year = year;
                model.administratorArea = administratorArea;
                if (batchId != "")
                {
                    model.batchId = Convert.ToInt32(batchId);
                }
                else
                {
                    model.batchId = 0;
                }
                if (landblockId != "")
                {
                    model.landblockId = Convert.ToInt32(landblockId);
                }
                else
                {
                    model.landblockId = 0;
                }
                model.town = town;
                model.village = village;
                model._group = _group;
                if (totalPeopleNumber != ""){model.totalPeopleNumber = Convert.ToDecimal(totalPeopleNumber);}else{model.totalPeopleNumber = 0;}
                if (planLevyArea != ""){model.planLevyArea = Convert.ToDecimal(planLevyArea);}else{model.planLevyArea = 0;}
                if (hasLevyArea != ""){model.hasLevyArea = Convert.ToDecimal(hasLevyArea);}else{model.hasLevyArea = 0;}
                if (levyNationalLandArea != ""){model.levyNationalLandArea = Convert.ToDecimal(levyNationalLandArea);}else{model.levyNationalLandArea = 0;}
                if (levyColletLandArea != ""){model.levyColletLandArea = Convert.ToDecimal(levyColletLandArea);}else{model.levyColletLandArea = 0;}
                if (levyPreDeposit != ""){model.levyPreDeposit = Convert.ToDecimal(levyPreDeposit);}else{model.levyPreDeposit = 0;}
                if (countrySocialSecurityFund != ""){model.countrySocialSecurityFund = Convert.ToDecimal(countrySocialSecurityFund);}else{model.countrySocialSecurityFund = 0;}
                if (countryCompensate != ""){model.countryCompensate = Convert.ToDecimal(countryCompensate);}else{model.countryCompensate = 0;}
                if (areaWaterFacilitiesCompensate != "") { model.areaWaterFacilitiesCompensate = Convert.ToDecimal(areaWaterFacilitiesCompensate); } else { model.areaWaterFacilitiesCompensate = 0; }
                if (provHeavyAgriculturalFunds != "") { model.provHeavyAgriculturalFunds = Convert.ToDecimal(provHeavyAgriculturalFunds); } else { model.provHeavyAgriculturalFunds = 0; }
                if (provAdditionalFee != "") { model.provAdditionalFee = Convert.ToDecimal(provAdditionalFee); } else { model.provAdditionalFee = 0; }
                if (provReclamationFee != "") { model.provReclamationFee = Convert.ToDecimal(provReclamationFee); } else { model.provReclamationFee = 0; }
                if (provArableLandTax != "") { model.provArableLandTax = Convert.ToDecimal(provArableLandTax); } else { model.provArableLandTax = 0; }
                if (provSurveyFee != "") { model.provSurveyFee = Convert.ToDecimal(provSurveyFee); } else { model.provSurveyFee = 0; }
                if (provServiceFee != "") { model.provServiceFee = Convert.ToDecimal(provServiceFee); } else { model.provServiceFee = 0; }
                if (context.Session["user_id"].ToString() != ""){model.userId = Convert.ToInt32(context.Session["user_id"]);}else{model.userId = 0;}
                model.createTime = System.DateTime.Now;
                model.isSubmited = 0;
                model.isDeleted = 0;
                model.des = des;

                int n = bll_levy.Add(model);
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
                Model.T_LevyCompensate model = bll_levy.GetModel(Convert.ToInt32(id));
                model.year = year;
                model.administratorArea = administratorArea;
                if (batchTypeId != "") { model.batchTypeId = Convert.ToInt32(batchTypeId); } else { model.batchTypeId = 0; }
                if (batchId != "")
                {
                    model.batchId = Convert.ToInt32(batchId);
                }
                else
                {
                    model.batchId = 0;
                }
                if (landblockId != "")
                {
                    model.landblockId = Convert.ToInt32(landblockId);
                }
                else
                {
                    model.landblockId = 0;
                }
                model.town = town;
                model.village = village;
                model._group = _group;
                if (totalPeopleNumber != "") { model.totalPeopleNumber = Convert.ToDecimal(totalPeopleNumber); } else { model.totalPeopleNumber = 0; }
                if (planLevyArea != "") { model.planLevyArea = Convert.ToDecimal(planLevyArea); } else { model.planLevyArea = 0; }
                if (hasLevyArea != "") { model.hasLevyArea = Convert.ToDecimal(hasLevyArea); } else { model.hasLevyArea = 0; }
                if (levyNationalLandArea != "") { model.levyNationalLandArea = Convert.ToDecimal(levyNationalLandArea); } else { model.levyNationalLandArea = 0; }
                if (levyColletLandArea != "") { model.levyColletLandArea = Convert.ToDecimal(levyColletLandArea); } else { model.levyColletLandArea = 0; }
                if (levyPreDeposit != "") { model.levyPreDeposit = Convert.ToDecimal(levyPreDeposit); } else { model.levyPreDeposit = 0; }
                if (countrySocialSecurityFund != "") { model.countrySocialSecurityFund = Convert.ToDecimal(countrySocialSecurityFund); } else { model.countrySocialSecurityFund = 0; }
                if (countryCompensate != "") { model.countryCompensate = Convert.ToDecimal(countryCompensate); } else { model.countryCompensate = 0; }
                if (areaWaterFacilitiesCompensate != "") { model.areaWaterFacilitiesCompensate = Convert.ToDecimal(areaWaterFacilitiesCompensate); } else { model.areaWaterFacilitiesCompensate = 0; }
                if (provHeavyAgriculturalFunds != "") { model.provHeavyAgriculturalFunds = Convert.ToDecimal(provHeavyAgriculturalFunds); } else { model.provHeavyAgriculturalFunds = 0; }
                if (provAdditionalFee != "") { model.provAdditionalFee = Convert.ToDecimal(provAdditionalFee); } else { model.provAdditionalFee = 0; }
                if (provReclamationFee != "") { model.provReclamationFee = Convert.ToDecimal(provReclamationFee); } else { model.provReclamationFee = 0; }
                if (provArableLandTax != "") { model.provArableLandTax = Convert.ToDecimal(provArableLandTax); } else { model.provArableLandTax = 0; }
                if (provSurveyFee != "") { model.provSurveyFee = Convert.ToDecimal(provSurveyFee); } else { model.provSurveyFee = 0; }
                if (provServiceFee != "") { model.provServiceFee = Convert.ToDecimal(provServiceFee); } else { model.provServiceFee = 0; }
                if (context.Session["user_id"].ToString() != "") { model.userId = Convert.ToInt32(context.Session["user_id"]); } else { model.userId = 0; }
                model.createTime = System.DateTime.Now;
                model.isSubmited = 0;
                model.isDeleted = 0;
                model.des = des;

                bool flag = bll_levy.Update(model);
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
            string id = "";
            if (context.Request["id"] != null && context.Request["id"].ToString().Trim() != "")
            {
                id = context.Request["id"].ToString();
            }
            Model.T_LevyCompensate t_levy = bll_levy.GetModel(Convert.ToInt32(id));
            context.Response.Write(JsonHelper.Object2Json<Model.T_LevyCompensate>(t_levy));
        }

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_levy.DeleteList(ids);
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
            string name, year, administratorArea,isDeleted, planTypeId, startTime, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = year = administratorArea =isDeleted= planTypeId = startTime = endTime = order = sort = oderby = "";

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
            if (null != context.Request.QueryString["planTypeId"])
            {
                planTypeId = context.Request.QueryString["planTypeId"].ToString().Trim();
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
                strWhere.AppendFormat(" L.name like '%{0}%' and ", name);
            }

            if (year != "")
            {
                strWhere.AppendFormat(" T.year = '{0}' and ", year);
            }
            if (administratorArea != "")
            {
                strWhere.AppendFormat(" T.administratorArea = '{0}' and ", administratorArea);
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
            DataSet ds = bll_levy.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //插入其他字段，行政区administratorArea、批次类型batchTypeName、创建人createUserName
            ds.Tables[0].Columns.Add("administratorAreaName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchTypeName", typeof(System.String));
            ds.Tables[0].Columns.Add("batchName", typeof(System.String));
            ds.Tables[0].Columns.Add("createUserName", typeof(System.String));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int batchTypeId = 0; int administratorAreaId = 0;
                int batchId = 0;
                int userId = 0;
                
                if (dr["administratorArea"].ToString() != "" && dr["administratorArea"] != null)
                {
                    administratorAreaId = Convert.ToInt32(dr["administratorArea"].ToString());
                }
                if (dr["batchTypeId"].ToString() != "" && dr["batchTypeId"] != null)
                {
                    batchTypeId = Convert.ToInt32(dr["batchTypeId"].ToString());
                }
                if (dr["batchId"].ToString() != "" && dr["batchId"] != null)
                {
                    batchId = Convert.ToInt32(dr["batchId"].ToString());
                }
                //if (dr["landblockId"].ToString() != "" && dr["landblockId"] != null)
                //{
                //    landblockId = Convert.ToInt32(dr["landblockId"].ToString());
                //}
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
                Model.T_BatchType t_batch_type = bll_batch_type.GetModel(batchTypeId);
                if (t_batch_type != null){dr["batchTypeName"] = t_batch_type.name;}else{dr["batchTypeName"] = "";}

                Model.T_Batch t_batch = bll_batch.GetModel(batchId);
                if (t_batch != null) { dr["batchName"] = t_batch.name; } else { dr["batchName"] = ""; }

                //Model.T_LandBlock t_land_block = bll_land_block.GetModel(landblockId);
                //if (t_land_block != null) { dr["landBlockName"] = t_land_block.name; } else { dr["landBlockName"] = ""; }

                Model.T_User t_user = bll_user.GetModel(userId);
                if (t_user != null){dr["createUserName"] = t_user.name;}else{dr["createUserName"] = "";}
            }

            int count = bll_levy.GetRecordCount(strWhere.ToString());//获取条数  
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