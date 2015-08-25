using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Web.SessionState;

namespace Maticsoft.Web.View.NoticeMng
{
    /// <summary>
    /// NoticeHandler 的摘要说明
    /// </summary>
    public class NoticeHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.T_Notice bll_notice = new BLL.T_Notice();
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

            if (method == "add" || method=="modify")
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
            else if (method == "getPage")
            {
                //调用查询方法  
                GetPage(context);
            }
            else if (method == "getList")
            {
                //GetList(context);
            }
            else if (method == "getContentById")
            {
                getContentById(context);
            }
            else if (method == "getModelById")
            {
                GetModelById(context);
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

            Model.T_Notice t_notice = new T_Notice();
            t_notice = bll_notice.GetModel(Convert.ToInt32(id));
            if (t_notice.isDeleted == 1)
            {
                t_notice.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_notice.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_notice.Update(t_notice);

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

        private void GetPage(HttpContext context)
        {
            int pageIndex = 1;
            int pageSize = 10;
            if (null != context.Request.QueryString["pageIndex"])
            {
                pageIndex = Convert.ToInt32(context.Request.QueryString["pageIndex"].ToString().Trim());
            }

            DataSet ds = bll_notice.GetListByPage("", "create_time desc", (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            context.Response.Write(JsonHelper.DataTable2JsonArray(ds));
        }

        private void getContentById(HttpContext context)
        {
            string id = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();//get方式获取参数
            }

            if (id != "")
            {
                T_Notice t_notice = bll_notice.GetModel(Convert.ToInt32(id));
                if (t_notice != null)
                {
                    //context.Response.Write(t_notice.content);
                    message.flag = true;
                    message.msg = t_notice.content;
                    context.Response.Write(JsonHelper.Object2Json<Message>(message));
                }
                else {
                    context.Response.Write("");
                }
            }
            else
            {
                context.Response.Write("");
            }

        }

        private void GetModelById(HttpContext context)
        {
            //throw new NotImplementedException();
        }



        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, title, isDeleted, content;
            id = title = isDeleted = content = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["title"])
            {
                title = context.Request.QueryString["title"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
            }
            if (null != context.Request.QueryString["content"])
            {
                content = context.Request.QueryString["content"].ToString().Trim();
            }

            if (id != "")
            {
                T_Notice model = bll_notice.GetModel(Convert.ToInt32(id));

                model.title = title;
                model.content = content;
                model.isDeleted = Convert.ToInt16(isDeleted);
                model.create_time = DateTime.Now;
                if (context.Session["user_id"] != null)
                {
                    model.admin_id = Convert.ToInt32(context.Session["user_id"]);
                }

                bool flag = bll_notice.Update(model);
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
            else {
                T_Notice model = new T_Notice();

                model.title = title;
                model.isDeleted = Convert.ToInt16(isDeleted);
                model.content = content;
                model.create_time = DateTime.Now;
                if (context.Session["user_id"] != null)
                {
                    model.admin_id = Convert.ToInt32(context.Session["user_id"]);
                }

                int n = bll_notice.Add(model);
                if (n>0)
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

            
            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_notice.DeleteList(ids);
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
            string title,startTime,isDeleted, endTime, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            title = startTime = endTime = isDeleted = order = sort = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["title"])
            {//获取前台传来的值  
                title = context.Request.QueryString["title"].ToString().Trim();
            }
            if (null != context.Request.QueryString["startTime"])
            {
                startTime = context.Request.QueryString["startTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["endTime"])
            {
                endTime = context.Request.QueryString["endTime"].ToString().Trim();
            }
            if (null != context.Request.QueryString["isDeleted"])
            {
                isDeleted = context.Request.QueryString["isDeleted"].ToString().Trim();
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
            if (null != context.Request.QueryString["sort"]){order = context.Request.QueryString["sort"].ToString().Trim();}
            if (null != context.Request.QueryString["order"]){sort = context.Request.QueryString["order"].ToString().Trim();}

            //===================================================================  
            //组合查询语句：条件+排序  
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 1=1 and");
            if (title != "")
            {
                strWhere.AppendFormat(" title like '%{0}%' and ", title);
            }
            if (isDeleted != "" && isDeleted != "-1")
            {
                strWhere.AppendFormat(" T.isDeleted = '{0}' and ", isDeleted);
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
            DataSet ds = bll_notice.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            int count = bll_notice.GetRecordCount(strWhere.ToString());//获取条数  
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