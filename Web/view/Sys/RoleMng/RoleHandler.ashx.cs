using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View.RoleMng
{
    /// <summary>
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : IHttpHandler
    {

        BLL.T_Role bll_role = new BLL.T_Role();
        BLL.T_Menu bll_menu = new BLL.T_Menu();
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

            if (method == "add")
            {
                Add(context);
            }
            else if (method == "modify")
            {
                Modify(context);
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

            if (BusinessHelper.IsRoleHasUsed(Convert.ToInt32(id)))
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

        private void Add(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string name, content, description;
            name = content = description = "";

            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["content"])
            {
                content = context.Request.QueryString["content"].ToString().Trim();
            }
            if (null != context.Request.QueryString["description"])
            {
                description = context.Request.QueryString["description"].ToString().Trim();
            }

            Model.T_Role model = new Model.T_Role();

            model.name = name;
            model.content = content;
            model.description = description;

            int n = bll_role.Add(model);
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
            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }

        private void Modify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, name, content, description;
            id = name = content = description = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["content"])
            {
                content = context.Request.QueryString["content"].ToString().Trim();
            }
            if (null != context.Request.QueryString["description"])
            {
                description = context.Request.QueryString["description"].ToString().Trim();
            }

            T_Role model = bll_role.GetModel(Convert.ToInt32(id));

            model.name = name;
            model.content = content;
            model.description = description;

            bool flag = bll_role.Update(model);
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

        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];
            bool flag = bll_role.DeleteList(ids);
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
                strWhere.AppendFormat(" name like '%{0}%' and ", name);
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
            DataSet ds = bll_role.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            //向ds中插入contentname（上级名称）
            ds.Tables[0].Columns.Add("contentname", typeof(System.String));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StringBuilder sbconent = new StringBuilder();

                //根据Menu id获取name
                if (dr["content"] != null && dr["content"] != DBNull.Value && dr["content"].ToString()!="")
                {
                    //1,2,3,5 分割
                    String[] ids = dr["content"].ToString().Split(',');


                    if (ids.Length > 0)
                    {
                        foreach (String id in ids) {
                            int id_int = Convert.ToInt32(id);
                            T_Menu t_menu = bll_menu.GetModel(id_int);
                            if (t_menu != null)
                            {
                                sbconent.Append(t_menu.name + ",");
                            }
                            else {
                                //是否是增删改查
                                if (AuthorityValidation.IsInCRUD(id_int))
                                {
                                    String _name = AuthorityValidation.GetMethodCRUD(id_int);
                                    sbconent.Append(_name + ",");
                                }
                                else {
                                    sbconent.Append(id + ",");
                                }

                                
                            }
                        }
                        dr["contentname"] = sbconent.ToString().Substring(0,sbconent.ToString().Length-1);
                    }
                    else {
                        dr["contentname"] = "";
                    }

                    
                }
                else {
                    dr["contentname"] = "";
                }
            }

            int count = bll_role.GetRecordCount(strWhere.ToString());//获取条数  

            string strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            context.Response.Write(strJson);//返回给前台页面  
        }

        

        


        private void GetList(HttpContext context)
        {

            DataSet ds = bll_role.GetList("");

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