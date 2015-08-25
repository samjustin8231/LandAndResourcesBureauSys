using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using Maticsoft.Common;
using System.Text;
using System.Data;
using System.Web.SessionState;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View.MenuMng
{
    /// <summary>
    /// MenuHandler 的摘要说明
    /// </summary>
    public class MenuHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.T_Menu bll_menu = new BLL.T_Menu();
        BLL.T_Privilege bll_privilege = new BLL.T_Privilege();
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

            if (method == "modify" || method == "add")
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
            else if (method == "GetMenuPrivilege")
            {
                //调用查询方法  
                GetMenuPrivilege(context);
            }
            else if (method == "getTreeList")
            {
                GetTreeList(context);
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

            if (BusinessHelper.IsMenuHasUsed(Convert.ToInt32(id)))
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

        private void GetMenuPrivilege(HttpContext context)
        {
            string order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            sort = order = oderby = "";

            if (sort != "" && order != "")
            {
                //strWhere.AppendFormat(" order by {0} {1}", sort, order);//添加排序  
                oderby = order + " " + sort;
            }

            //调用分页的GetList方法  
            DataSet ds = bll_menu.GetLevelListByPId(0, false, "", oderby);

            context.Response.Write(GetJsonForTreeByDataset_Menu(ds, "", 1, oderby));
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

            Model.T_Menu t_menu = new T_Menu();
            t_menu = bll_menu.GetModel(Convert.ToInt32(id));
            if (t_menu.isDeleted == 1)
            {
                t_menu.isDeleted = 0;
                opt = "开启";
            }
            else
            {
                t_menu.isDeleted = 1;
                opt = "关闭";
            }
            flag = bll_menu.Update(t_menu);

            /*
               //添加登录日志
            Model.T_Log t_log = new T_Log();
            t_log.type = "【开启】";
            t_log.user_id = Convert.ToInt32(context.Session["user_id"].ToString());
            t_log.user_name = context.Session["user_name"].ToString();
            t_log.ip = Web.utils.Util.GetIP();
            t_log.create_time = DateTime.Now;
            t_log.id_role = context.Session["role_id"].ToString() + "";
            t_log.isDeleted = 0;
            t_log.des = "IP为 " + t_log.ip + " 的用户【" + context.Session["user_name"].ToString() + "】 身份【" + list_role[0].name + "】在 " + DateTime.Now + " 【启用】";
            bll_log.Add(t_log);
             */

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

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, name, url, icon, description, pid,_sort;
            id = name = url = icon = pid = description = _sort = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["_sort"])
            {
                _sort = context.Request.QueryString["_sort"].ToString().Trim();
            }
            if (null != context.Request.QueryString["url"])
            {
                url = context.Request.QueryString["url"].ToString().Trim();
            }
            if (null != context.Request.QueryString["icon"])
            {
                icon = context.Request.QueryString["icon"].ToString().Trim();
            }
            if (null != context.Request.QueryString["description"])
            {
                description = context.Request.QueryString["description"].ToString().Trim();
            }
            if (null != context.Request.QueryString["pid"])
            {
                pid = context.Request.QueryString["pid"].ToString().Trim();
            }
            Model.T_Menu model = new Model.T_Menu();
            message = new Message();
            if (id == "") {
                model.name = name;
                model.url = url;
                model.icon = icon;
                model.description = description;
                if (pid != null && pid != "")
                {
                    model.pid = Convert.ToInt32(pid);
                }
                else
                {
                    model.pid = 0;
                }
                if (_sort != null && _sort != "")
                {
                    model._sort = Convert.ToInt32(_sort);
                }
                else
                {
                    model._sort = 1;
                }

                int n = bll_menu.Add(model);
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
            }else
            {
                model = bll_menu.GetModel(Convert.ToInt32(id));

                model.name = name;
                model.url = url;
                model.icon = icon;
                model.description = description;
                if (pid != null && pid != "")
                {
                    model.pid = Convert.ToInt32(pid);
                }
                else
                {
                    model.pid = 0;
                }

                bool flag = bll_menu.Update(model);
                
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

            if (ids == "" || ids == "")
            {
                message.flag = false;
                message.msg = "删除请至少选择一项删除";
            }
            else {
                String[] idArr = ids.Split(',');
                if (idArr.Length > 0) {
                    foreach (String pid in idArr) {
                        bll_menu.DeleteCascode(Convert.ToInt32(pid));

                    }
                    message.flag = true;
                    message.msg = "删除成功";
                }
            }

            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        private void Query(HttpContext context)
        {
            string name, order, sort, oderby;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = sort = order = oderby = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["name"])
            {//获取前台传来的值  
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["sort"]) { order = context.Request.QueryString["sort"].ToString().Trim(); }
            if (null != context.Request.QueryString["order"]) { sort = context.Request.QueryString["order"].ToString().Trim(); }
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
            DataSet ds = bll_menu.GetLevelListByPId(0, false, "", oderby);

            context.Response.Write(JsonHelper.GetJsonForTreeByDataset_Menu(ds, "", 1, oderby));
        }

        /// <summary>
        /// 获取AccountsList
        /// </summary>
        /// <param name="context"></param>
        public void GetTreeList(HttpContext context)
        {
            StringBuilder strWhere = new StringBuilder();
            string id = "";
            string sort, order, oderby;
            sort = order = oderby = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
                strWhere.AppendFormat(" id != '{0}'", id);
            }

            oderby = "_sort asc";

            DataSet ds = bll_menu.GetLevelListByPId(0, false, "", oderby);

            string strJson = GetJsonForTreeByDataset(ds, "", 1, oderby);
            context.Response.Write(strJson);//返回给前台页面  
        }

        private string GetJsonForTreeByDataset(DataSet ds, string where, int type,string orderby)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder json = new StringBuilder();

            json.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                BLL.T_Menu bll_menu = new BLL.T_Menu();

                json.Append("{\"id\":" + dr["id"].ToString());
                json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
                json.Append(",\"iconCls\":\"" + "" + "\"");
                json.Append(",\"pid\":\"" + dr["pid"].ToString() + "\"");
                //获取下一级
                DataSet dsChilds = bll_menu.GetLevelListByPId(Convert.ToInt32(dr["id"].ToString()), false, where, orderby);
                DataTable dtChilds = dsChilds.Tables[0];
                //下一级含有在权限范围之内的
                if (dtChilds.Rows.Count > 0)
                {
                    //根据id获取子菜单 
                    if (dtChilds != null && dtChilds.Rows.Count > 0)
                    {
                        json.Append(",\"children\":");
                        //子菜单递归拼接
                        json.Append(GetJsonForTreeByDataset(dsChilds, where, type,orderby));
                    }
                }

                json.Append("},");

            }
            if (dt.Rows.Count > 0 && json.ToString().Substring(json.Length - 1, 1) == ",")
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// 递归方式，获取子菜单，以json的格式
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string GetJsonForTreeByDataset_Menu(DataSet ds, string where, int type, string oderby)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder json = new StringBuilder();

            json.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                BLL.T_Menu bll_menu = new BLL.T_Menu();

                json.Append("{\"id\":" + dr["id"].ToString());

                json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
                json.Append(",\"_sort\":\"" + dr["_sort"].ToString() + "\"");
                json.Append(",\"isDeleted\":\"" + dr["isDeleted"].ToString() + "\"");
                json.Append(",\"icon\":\"" + "icon-man" + "\"");
                json.Append(",\"state\":\"true\"");
                json.Append(",\"url\":\"" + dr["url"].ToString() + "\"");
                json.Append(",\"description\":\"" + dr["description"].ToString() + "\"");
                json.Append(",\"pid\":\"" + dr["pid"].ToString() + "\"");
                json.Append(",\"attributes\":{\"iconCls\":\"" + "icon-man" + "\"" + ",\"url\":\"" + dr["url"].ToString() + "\"" + "}");

                //获取下一级
                DataSet dsChilds = bll_menu.GetLevelListByPId(Convert.ToInt32(dr["id"].ToString()), false, where, oderby);
                DataTable dtChilds = dsChilds.Tables[0];
                //下一级含有在权限范围之内的
                if (dtChilds.Rows.Count > 0)
                {
                    //根据id获取子菜单 
                    if (dtChilds != null && dtChilds.Rows.Count > 0)
                    {
                        json.Append(",\"children\":");
                        //子菜单递归拼接
                        json.Append(GetJsonForTreeByDataset_Menu(dsChilds, where, type, oderby));
                    }

                }
                else
                { //拼接小权限 
                    List<Model.T_Privilege> list_privilege = bll_privilege.GetModelList(" 1=1 and menuId = " + Convert.ToInt32(dr["id"].ToString()));
                    if (list_privilege.Count > 0) {
                        json.Append(",\"children\":");
                        json.Append("[");

                        foreach (Model.T_Privilege model in list_privilege)
                        {
                            int ids = 0;
                            ids = model.menuId+Convert.ToInt32(model.name);
                            json.Append("{\"id\":\"" + ids + "\"");
                            String privilegeName = "";
                            privilegeName = utils.Util.GetNameByPrivilegeId(model.name);
                            json.Append(",\"text\":\""+privilegeName+"\"");
                            json.Append(",\"iconCls\":\"" + model.icon + "\"");
                            json.Append(",\"state\":\"true\"");
                            json.Append(",\"pid\":\"" + model.menuId + "\"");
                            json.Append(",\"attributes\":{\"iconCls\":\"" + model.icon + "\"" + "}},");
                        }

                        if (dt.Rows.Count > 0 && json.ToString().Substring(json.Length - 1, 1) == ",")
                        {
                            json.Remove(json.Length - 1, 1);
                        }
                        json.Append("]");
                    }
                    
                }

                json.Append("},");

            }
            if (dt.Rows.Count > 0 && json.ToString().Substring(json.Length - 1, 1) == ",")
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
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