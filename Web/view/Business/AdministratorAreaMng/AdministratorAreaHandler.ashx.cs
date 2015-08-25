using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View.Business.AdministratorAreaMng
{
    /// <summary>
    /// AdministratorAreaHandler 的摘要说明
    /// </summary>
    public class AdministratorAreaHandler : IHttpHandler
    {

        BLL.T_AdministratorArea bll_plan_type = new BLL.T_AdministratorArea();
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
            else if (method == "getModelById")
            {
                //GetModelById(context);
            }
            else if (method == "IsUsed")
            {
                IsUsed(context);
            }
        }

        private void IsUsed(HttpContext context)
        {
            string id = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }

            if (BusinessHelper.IsAdministratorAreaHasUsed(Convert.ToInt32(id)))
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

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, name, sort, des;
            id = name = sort = des = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["sort"])
            {
                sort = context.Request.QueryString["sort"].ToString().Trim();
            }
            if (null != context.Request.QueryString["des"])
            {
                des = context.Request.QueryString["des"].ToString().Trim();
            }

            if (id == "")
            {
                Model.T_AdministratorArea model = new Model.T_AdministratorArea();

                model.name = name;
                model.pingyin = utils.PinYinConverter.Get(name);
                model.initial = utils.PinYinConverter.GetFirst(name);
                if (sort != "")
                {
                    model.sort = Convert.ToInt32(sort);
                }
                else {
                    model.sort = 1;
                }
                
                model.des = des;

                int n = bll_plan_type.Add(model);
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
                T_AdministratorArea model = bll_plan_type.GetModel(Convert.ToInt32(id));

                model.name = name;
                model.pingyin = utils.PinYinConverter.Get(name);
                model.initial = utils.PinYinConverter.GetFirst(name);
                if (sort != "")
                {
                    model.sort = Convert.ToInt32(sort);
                }
                else
                {
                    model.sort = 1;
                }
                model.des = des;

                bool flag = bll_plan_type.Update(model);
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
            bool flag = bll_plan_type.DeleteList(ids);
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
            string name, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名】  
            name = order = sort = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["name"])
            {//获取前台传来的值  
                name = context.Request.QueryString["name"].ToString().Trim();
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
                //strWhere.AppendFormat(" name like '%{0}%' and ", name);
                strWhere.AppendFormat(" initial like '%{0}%' or name like '%{1}%' or [pingyin] like '%{2}%' and ", name, name, name);
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
            DataSet ds = bll_plan_type.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);


            int count = bll_plan_type.GetRecordCount(strWhere.ToString());//获取条数  

            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }


        private void GetList(HttpContext context)
        {
            string keyword = "";

            if (null != context.Request.Form["keyword"])
            {
                keyword = utils.PinYinConverter.GetFirst(context.Request.Form["keyword"].ToString().Trim());//get方式获取参数
            }
            StringBuilder strWhere = new StringBuilder();
            string sort, order, oderby;
            sort = order = oderby = "";

            strWhere.Append(" 1=1 ");

            if (keyword != "")
            {
                strWhere.AppendFormat(" and initial like '%{0}%' or name like '%{1}%' or [pingyin] like '%{2}%'", keyword, keyword,keyword);
            }

            oderby = "sort asc";

            DataSet ds = bll_plan_type.GetList(strWhere.ToString());

            string strJson = JsonHelper.DataTable2JsonArray(ds);
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