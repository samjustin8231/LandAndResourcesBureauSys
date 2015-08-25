using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Web.SessionState;

namespace Maticsoft.Web.View.Business.LandBlockMng
{
    /// <summary>
    /// LandBlockHandler 的摘要说明
    /// </summary>
    public class LandBlockHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.T_LandBlock bll_land_block = new BLL.T_LandBlock();
        BLL.T_LevyCompensate bll_levy = new BLL.T_LevyCompensate();
        BLL.T_Batch bll_batch = new BLL.T_Batch();
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
            else if (method == "getList")
            {
                GetList(context);
            }
            else if (method == "GetListForCombo")
            {
                GetListForCombo(context);
            }
            else if (method == "getModelById")
            {
                GetModelById(context);
            }
            else if (method == "getNextLandBlock")
            {
                GetNextLandBlock(context);
            }
            else if (method == "getRemainLandBlock")
            {
                GetRemainLandBlock(context);
            }
        }

        private void GetListForCombo(HttpContext context)
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
            DataSet ds = bll_land_block.GetList(" batchId=" + batchId);
            DataTable dt = ds.Tables[0];
            DataRow row = dt.NewRow();
            row["id"] = 0;
            row["name"] = "-- 请选择 --";
            row["des"] = "";
            dt.Rows.InsertAt(row, 0);

            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
        }

        private void GetList(HttpContext context)
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
            DataSet ds = bll_land_block.GetList(" batchId=" + batchId);
            ds.Tables[0].Columns.Add("isUsed", typeof(System.String));

            //获取征收补偿信息
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (utils.BusinessHelper.IsLandBlockHasUsed(Convert.ToInt32(dr["id"])))
                {
                    dr["isUsed"] = true;
                }
                else
                {
                    dr["isUsed"] = false;
                }
            }

            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
        }

        /// <summary>
        /// 获取该地块被征收补偿用了还剩多少
        /// </summary>
        /// <param name="context"></param>
        private void GetRemainLandBlock(HttpContext context)
        {
            string landblockId = "";
            if (null != context.Request.QueryString["landblockId"])
            {
                landblockId = context.Request.QueryString["landblockId"].ToString().Trim();
            }

            //组合查询语句：条件+排序  
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and");
            if (landblockId != "")
            {
                strWhere.AppendFormat(" landblockId = '{0}' and ", landblockId);
            }

            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }
            //地块信息
            Model.T_LandBlock t_landblock = bll_land_block.GetModel(Convert.ToInt32(landblockId));

            //用了该地块的征收补偿
            List<Model.T_LevyCompensate> list_levy = bll_levy.GetModelList(strWhere.ToString());

            if (list_levy.Count > 0)
            {
                decimal usedArea = 0;
                foreach (Model.T_LevyCompensate t_levy in list_levy) {
                    usedArea += (decimal)t_levy.planLevyArea;
                }
                message.msg = t_landblock.area -usedArea+ ""; 
            }
            else {
                message.msg = t_landblock.area+""; 
            }
            
            
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        /// <summary>
        /// 获取该批次下的下一个地块名称
        /// </summary>
        /// <param name="context"></param>
        private void GetNextLandBlock(HttpContext context)
        {
            string batchId = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }

            //组合查询语句：条件+排序  
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and");
            if (batchId != "")
            {
                strWhere.AppendFormat(" batchId = '{0}' and ", batchId);
            }

            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }

            DataSet ds = bll_land_block.GetList(1, strWhere.ToString(), " id desc");
            if (ds.Tables[0].Rows.Count == 0)
            {
                message.msg = "地块1";
            }
            else
            {
                Model.T_LandBlock model = bll_land_block.GetModel(Convert.ToInt32(ds.Tables[0].Rows[0]["id"]));
                string name = model.name;

                int nextId = Convert.ToInt32(name.Substring(2)) + 1;
                message.msg = "地块" + nextId;
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        private void GetModelById(HttpContext context)
        {
            //获取id
            string id = "";
            Model.T_LandBlock t_landblock = new T_LandBlock();
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
                t_landblock = bll_land_block.GetModel(Convert.ToInt32(id));
            }
            context.Response.Write(JsonHelper.Object2Json<T_LandBlock>(t_landblock));
        }

        private void Query(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_land_block.DeleteList(ids);
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

        private void AddOrModify(HttpContext context)
        {
            string batchId, name, area, des;
            batchId = name = area  = des = "";
            if (null != context.Request.QueryString["batchId"])
            {
                batchId = context.Request.QueryString["batchId"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["area"])
            {
                area = context.Request.QueryString["area"].ToString().Trim();
            }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }

            Model.T_LandBlock model = new T_LandBlock();
            model.name = name;
            if (batchId != "")
            {
                model.batchId = Convert.ToInt32(batchId);
            }
            else
            {
                model.batchId = 0;
            }
            if (area != "")
            {
                model.area = Convert.ToDecimal(area);
            }
            else
            {
                model.area = 0;
            }
            if (context.Session["user_id"] != null && context.Session["user_id"].ToString() != "")
            {
                model.userId = Convert.ToInt32(context.Session["user_id"].ToString());
            }
            model.createTime = System.DateTime.Now;
            model.isDeleted = 0; model.isSubmited = 0;
            model.des = des;

            int n = bll_land_block.Add(model);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}