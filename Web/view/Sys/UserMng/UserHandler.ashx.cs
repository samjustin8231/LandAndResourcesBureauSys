using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Model;
using System.Data;
using Maticsoft.Common;
using System.Web.SessionState;
using System.Text;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View.Sys.UserMng
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler, IRequiresSessionState
    {

        BLL.T_User bll_user = new BLL.T_User();
        BLL.T_Role bll_role = new BLL.T_Role();
        BLL.T_User_Role bll_user_role = new BLL.T_User_Role();
        BLL.T_Log bll_log = new BLL.T_Log();

        Message message = new Message();
        string method;

        public void ProcessRequest(HttpContext context)
        {
            //base.ProcessRequest(context);
            context.Response.ContentType = "text/plain";

            method = "";

            if (null != context.Request.QueryString["method"])
            {
                method = context.Request.QueryString["method"].ToString().Trim();
            }

            if (method == "login")
            {
                Login(context);
            }
            else if (method == "logout")
            {
                Logout(context);
            }
            if (method == "Lock")
            {
                Lock(context);
            }
            else if (method == "EditPassword")
            {
                EditPassword(context);
            }
            else if (method == "InitPassword")
            {
                InitPassword(context);
            }
            else if (method == "checkName")
            {
                CheckName(context);
            }
            else if (method == "add" || method == "modify")
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
        
        }

        private void InitPassword(HttpContext context)
        {
            bool flag = false;
            string id = "";

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }

            Model.T_User t_account = new T_User();
            t_account = bll_user.GetModel(Convert.ToInt32(id));
            t_account.password = Util.GetMD5("123");
            flag = bll_user.Update(t_account);

            message = new Message();
            if (flag)
            {
                message.flag = true;
                message.msg = "密码初始化成功";
            }
            else
            {
                message.flag = false;
                message.msg = "密码初始化失败";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));//返回给前台页面  
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

            Model.T_User t_account = new T_User();
            t_account = bll_user.GetModel(Convert.ToInt32(id));
            if (t_account.isDeleted == 1)
            {
                t_account.isDeleted = 0;
                opt = "开启";
            }
            else {
                t_account.isDeleted = 1;
                opt = "关闭"; 
            }
            flag = bll_user.Update(t_account);

            message = new Message();
            if (flag)
            {
                message.flag = true;
                message.msg = opt+"成功";
            }
            else
            {
                message.flag = false;
                message.msg = opt+"失败";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));//返回给前台页面  
        }

        private void EditPassword(HttpContext context)
        {
            string password = "";
            if (null != context.Request.QueryString["newpassword"])
            {
                password = context.Request.QueryString["newpassword"].ToString().Trim();
            }
            bool flag = false;

            Model.T_User t_account = new T_User();
            t_account = bll_user.GetModel(Convert.ToInt32(context.Session["user_id"].ToString()));
            t_account.password = Util.GetMD5(password);
            flag = bll_user.Update(t_account);

            message = new Message();
            if (flag)
            {
                message.flag = true;
                message.msg = "密码修改成功";
            }
            else
            {
                message.flag = false;
                message.msg = "密码修改失败";
            }
            context.Response.Write(JsonHelper.Object2Json<Message>(message));//返回给前台页面  
        }


        private void CheckName(HttpContext context)
        {
            string id, name;
            id = name = "";
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();//get方式获取参数
            }
            if (null != context.Request.Form["name"])
            {
                name = context.Request.Form["name"].ToString().Trim();//post方式获取参数
            }

            if (id == "")
            {//添加
                //检查用户名是否已经存在
                DataSet ds = bll_user.GetList(" name='" + name + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断是不是本身
                    //if(ds.Tables[0].Rows[0]["id"].ToString())

                    context.Response.Write("false");

                }
                else
                {
                    context.Response.Write("true");
                }
            }
            else
            { //修改
                //检查用户名除了自己是否存在其他
                DataSet ds = bll_user.GetList(" name='" + name + "' and id != " + id);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    context.Response.Write("false");
                }
                else
                {
                    context.Response.Write("true");
                }
            }


        }

        private void AddOrModify(HttpContext context)
        {
            //===============================================================  
            //获取字段:【用户name】  
            string id, id_role, name, birthday, telephone, address, create_time, remarks,password;
            id = id_role = name = birthday = telephone = address = create_time = remarks =password = "";
            //获取前台传来的值  

            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
            }
            if (null != context.Request.QueryString["id_role"])
            {
                id_role = context.Request.QueryString["id_role"].ToString().Trim();
            }
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["birthday"])
            {
                birthday = context.Request.QueryString["birthday"].ToString().Trim();
            }
            if (null != context.Request.QueryString["telephone"])
            {
                telephone = context.Request.QueryString["telephone"].ToString().Trim();
            }
            if (null != context.Request.QueryString["address"])
            {
                address = context.Request.QueryString["address"].ToString().Trim();
            }
            if (null != context.Request.QueryString["password"])
            {
                password = context.Request.QueryString["password"].ToString().Trim();
            }
            if (null != context.Request.QueryString["create_time"])
            {
                create_time = context.Request.QueryString["create_time"].ToString().Trim();
            }
            if (null != context.Request.QueryString["remarks"])
            {
                remarks = context.Request.QueryString["remarks"].ToString().Trim();
            }


            //获取角色信息
            T_Role t_role = new T_Role();
            if (id_role != "")
            {
                t_role = bll_role.GetModel(Convert.ToInt32(id_role));

            }

            Model.T_User t_user = new Model.T_User();
            if (method == "add")
            {
                //检查用户名是否已经存在
                DataSet ds = bll_user.GetList(" name='" + name + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    message.flag = false;
                    message.msg = "用户名已经存在";

                }
                else
                {

                    t_user.name = name;
                    t_user.password = Util.GetMD5(password);
                    if (birthday != "")
                    {
                        t_user.birthday = Convert.ToDateTime(birthday);
                    }
                    t_user.telephone = telephone;
                    t_user.address = address;
                    t_user.create_time = DateTime.Now;
                    t_user.remarks = remarks;

                    int n = bll_user.Add(t_user);

                    //插入用户角色信息
                    Model.T_User_Role t_user_role = new T_User_Role();
                    if (id_role != "")
                    {
                        t_user_role.id_role = Convert.ToInt32(id_role);
                    }

                    t_user_role.id_user = n;
                    bll_user_role.Add(t_user_role);

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
            }
            else
            {
                t_user = bll_user.GetModel(Convert.ToInt32(id));

                //检查用户名是否已经存在
                DataSet ds = bll_user.GetList(" name='" + name + "' and id != " + id);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    message.flag = false;
                    message.msg = "用户名已经存在";

                }
                else
                {

                    t_user.name = name;
                    t_user.password = Util.GetMD5(password);
                    if (birthday != "")
                    {
                        t_user.birthday = Convert.ToDateTime(birthday);
                    }
                    t_user.telephone = telephone;
                    t_user.address = address;
                    t_user.create_time = DateTime.Now;
                    t_user.remarks = remarks;

                    bool flag = bll_user.Update(t_user);

                    //删除旧的在插入新的
                    bll_user_role.DeleteByWhere(" id_user = " + id);

                    //插入用户角色信息
                    Model.T_User_Role t_user_role = new T_User_Role();
                    if (id_role != "")
                    {
                        t_user_role.id_role = Convert.ToInt32(id_role);
                    }

                    t_user_role.id_user = Convert.ToInt32(id);
                    bll_user_role.Add(t_user_role);

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


            }

            String jsonString = JsonHelper.Object2Json<Message>(message);
            context.Response.Write(jsonString);
        }




        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        public void Login(HttpContext context)
        {
            //获取用户名密码
            string name, password, identity, type;
            name = password = identity = type = "";
            if (null != context.Request.QueryString["name"])
            {
                name = context.Request.QueryString["name"].ToString().Trim();
            }
            if (null != context.Request.QueryString["password"])
            {
                password = context.Request.QueryString["password"].ToString().Trim();
            }
            if (null != context.Request.QueryString["identity"])
            {
                identity = context.Request.QueryString["identity"].ToString().Trim();
            }
            if (null != context.Request.QueryString["type"])
            {
                type = context.Request.QueryString["type"].ToString().Trim();
            }

            Model.T_User t_user;

            //一般用户
            string strWhere = " 1=1 and name='" + name + "'";
            DataSet ds = bll_user.GetList(strWhere);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //用户名不存在
                message.flag = false;
                message.msg = "用户名不存在！";
            }
            else
            {
                //用户名存在，判断密码是否正确
                t_user = bll_user.Login(name, Util.GetMD5(password));

                if (t_user != null)
                {//登录成功
                    //存入session

                    //判断是否被锁定账户
                    if (t_user.isDeleted == 1)
                    {
                        message.flag = false;
                        message.msg = "账户已经被关闭！";
                    }
                    else {

                        //修改登录状态
                        t_user.isOnline = 1;
                        bll_user.Update(t_user);

                        //获取用户角色信息
                        List<Model.T_User_Role> list_user_role = bll_user_role.GetModelList(" id_user='" + t_user.id + "'");
                        if (list_user_role.Count > 0)
                        {
                            List<Model.T_Role> list_role = bll_role.GetModelList(" id='" + list_user_role[0].id_role + "'");
                            if (t_user.name == "admin")
                            {
                                context.Session["role_id"] = "admin";
                                context.Session["role_name"] = "admin";
                            }
                            else
                            {
                                if (list_role.Count > 0)
                                {
                                    context.Session["role_id"] = list_role[0].id;
                                    context.Session["role_name"] = list_role[0].name;
                                }
                            }



                            //添加登录日志
                            Model.T_Log t_log = new T_Log();
                            t_log.type = "【登录】";
                            t_log.user_id = t_user.id;
                            t_log.user_name = name;
                            t_log.ip = Web.utils.Util.GetIP();
                            t_log.create_time = DateTime.Now;
                            t_log.id_role = list_role[0].id+"";
                            t_log.isDeleted = 0;
                            t_log.des = "IP为 " + t_log.ip + " 的用户【" + t_user.name + "】 身份【" + list_role[0].name+ "】在 " + DateTime.Now + " 【登录】";
                            bll_log.Add(t_log);
                        }

                        context.Session["user_name"] = t_user.name;
                        context.Session["user_id"] = t_user.id;

                        message.flag = true;
                        message.msg = "登录成功！";
                    }
                    
                }
                else
                {
                    message.flag = false;
                    message.msg = "密码错误！";
                }

            }

            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="context"></param>
        public void Logout(HttpContext context)
        {

            //修改登录状态
            if (context.Session["user_id"] != null) {
                Model.T_User t_user = bll_user.GetModel(Convert.ToInt32(context.Session["user_id"]));
                t_user.isOnline = 0;
                bll_user.Update(t_user);
            }

            context.Session["user_name"] = "";
            context.Session["user_id"] = "";



            //清空session    
            //context.Session.Clear();

            message.flag = true;
            message.msg = "成功退出系统！";
            message.msg = context.Session["user_id"].ToString();
            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private void Delete(HttpContext context)
        {
            String ids = context.Request["ids"];

            if (ids == "" || ids == "")
            {
                message.flag = false;
                message.msg = "删除请至少选择一项删除";
            }
            else
            {
                bool flag = bll_user.DeleteList(ids);
                if (flag)
                {
                    message.flag = true;
                    message.msg = "删除成功";
                }
                else {
                    message.flag = false;
                    message.msg = "删除失败";
                }
            }

            context.Response.Write(JsonHelper.Object2Json<Message>(message));
        }

        /// <summary>
        /// 获取model
        /// </summary>
        /// <param name="context"></param>
        public void GetModelById(HttpContext context)
        {
            //获取id
            string id = "";
            Model.T_User t_user = new T_User();
            if (null != context.Request.QueryString["id"])
            {
                id = context.Request.QueryString["id"].ToString().Trim();
                t_user = bll_user.GetModel(Convert.ToInt32(id));
            }
            else
            {
                t_user = bll_user.GetModel(Convert.ToInt32(context.Session["user_id"]));
            }
            context.Response.Write(JsonHelper.Object2Json<T_User>(t_user));
        }

        ///// <summary>
        ///// 获取AccountsList
        ///// </summary>
        ///// <param name="context"></param>
        //public void GetTreeList(HttpContext context)
        //{
        //    StringBuilder strWhere = new StringBuilder();
        //    string id = "";
        //    string sort, order, oderby;
        //    sort = order = oderby = "";
        //    if (null != context.Request.QueryString["id"])
        //    {
        //        id = context.Request.QueryString["id"].ToString().Trim();
        //        strWhere.AppendFormat(" id != '{0}'", id);
        //    }

        //    oderby = "name asc";

        //    DataSet ds = bll_account.GetLevelListByPId(0, false, " and a.[identity] = '2'");

        //    //向ds中插入pname（上级名称）
        //    /*
        //     ds.Tables[0].Columns.Add("pname", typeof(System.String));
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        //根据Account id获取name
        //        if (dr["pid"] != null && dr["pid"] != DBNull.Value)
        //        {
        //            T_Account t_account = bll_account.GetModel(Convert.ToInt32(dr["pid"]));
        //            if (t_account != null)
        //            {
        //                dr["pname"] = t_account.name;
        //            }
        //        }
        //    }
        //     */

        //    //在第一条插入"无"
        //    /*
        //     DataRow row = ds.Tables[0].NewRow();
        //    row["id"] = 0;
        //    row["name"] = "无";
        //    ds.Tables[0].Rows.InsertAt(row, 0);
        //     */

        //    string strJson = GetJsonForTreeByDataset(ds, "", 1);
        //    context.Response.Write(strJson);//返回给前台页面  
        //}

        //private string GetJsonForTreeByDataset(DataSet ds, string where, int type)
        //{
        //    DataTable dt = ds.Tables[0];
        //    StringBuilder json = new StringBuilder();

        //    json.Append("[");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        BLL.T_Menu bll_menu = new BLL.T_Menu();

        //        json.Append("{\"id\":" + dr["id"].ToString());
        //        json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
        //        json.Append(",\"name\":\"" + dr["name"].ToString() + "\"");
        //        json.Append(",\"score\":\"" + dr["name"].ToString() + "\"");
        //        json.Append(",\"able_score\":\"" + dr["name"].ToString() + "\"");
        //        json.Append(",\"pname\":\"" + dr["pname"].ToString() + "\"");
        //        json.Append(",\"register_time\":\"" + dr["register_time"].ToString() + "\"");

        //        json.Append(",\"iconCls\":\"" + "icon-man" + "\"");
        //        json.Append(",\"pid\":\"" + dr["pid"].ToString() + "\"");
        //        //获取下一级
        //        DataSet dsChilds = bll_account.GetLevelListByPId(Convert.ToInt32(dr["id"].ToString()), false, where);



        //        DataTable dtChilds = dsChilds.Tables[0];
        //        //下一级含有在权限范围之内的
        //        if (dtChilds.Rows.Count > 0)
        //        {
        //            //根据id获取子菜单 
        //            if (dtChilds != null && dtChilds.Rows.Count > 0)
        //            {
        //                json.Append(",\"children\":");
        //                //子菜单递归拼接
        //                json.Append(GetJsonForTreeByDataset(dsChilds, where, type));
        //            }
        //        }

        //        json.Append("},");

        //    }
        //    if (dt.Rows.Count > 0 && json.ToString().Substring(json.Length - 1, 1) == ",")
        //    {
        //        json.Remove(json.Length - 1, 1);
        //    }
        //    json.Append("]");
        //    return json.ToString();
        //}

        ///// <summary>
        ///// 获取AccountTreeData
        ///// </summary>
        ///// <param name="context"></param>
        //public void GetAccountTreeData(HttpContext context)
        //{
        //    StringBuilder strWhere = new StringBuilder();
        //    string id = "";
        //    string sort, order, oderby;
        //    sort = order = oderby = "";

        //    if (null != context.Request.QueryString["id"])
        //    {
        //        id = context.Request.QueryString["id"].ToString().Trim();
        //        strWhere.AppendFormat(" and a.id != '{0}'", id);
        //    }

        //    if (null != context.Request["id"])
        //    {
        //        id = context.Request["id"].ToString().Trim();
        //        strWhere.AppendFormat(" and a.id != '{0}'", id);
        //    }

        //    oderby = "name asc";
        //    strWhere.Append(" and a.[identity] = '2'");

        //    DataSet ds = bll_account.GetLevelListByPId(0, false, strWhere.ToString());

        //    /*
        //     //在第一条插入"无"
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["id"] = 0;
        //    row["name"] = "无";
        //    ds.Tables[0].Rows.InsertAt(row, 0);
        //     */


        //    string strJson = JsonHelper.GetJsonForTreeByDataset(ds, strWhere.ToString(), 1);
        //    context.Response.Write(strJson);//返回给前台页面  
        //}

        ///// <summary>
        ///// 获取 pid 的下级
        ///// </summary>
        ///// <param name="context"></param>
        //public void GetAccountTreeDataForAccount(HttpContext context)
        //{
        //    StringBuilder strWhere = new StringBuilder();
        //    string id = "";
        //    string sort, order, oderby;
        //    sort = order = oderby = "";


        //    if (null != context.Session["user_id"])
        //    {
        //        id = context.Session["user_id"].ToString().Trim();
        //        strWhere.AppendFormat(" and a.id != '{0}'", id);
        //    }

        //    oderby = "name asc";
        //    strWhere.Append(" and a.[identity] = '2'");



        //    DataSet ds = bll_account.GetLevelListByPId(Convert.ToInt32(id), false, strWhere.ToString());

        //    string strJson = JsonHelper.GetJsonForTreeByDataset(ds, strWhere.ToString(), 1);

        //    //将所有子集嵌入到子集的下面
        //    T_Account t_account = bll_account.GetModel(Convert.ToInt32(id));

        //    strJson = AppendToPid(t_account, strJson);
        //    context.Response.Write(strJson);//返回给前台页面  
        //}

        //private string AppendToPid(T_Account t_account, string strJson)
        //{
        //    StringBuilder json = new StringBuilder();
        //    json.Append("[");

        //    json.Append("{\"id\":" + t_account.id);

        //    json.Append(",\"text\":\"" + t_account.name + "\"");
        //    json.Append(",\"iconCls\":\"" + "icon-man" + "\"");
        //    json.Append(",\"state\":\"true\"");

        //    json.Append(",\"attributes\":{\"iconCls\":\"" + "icon-man" + "\"" + "}");

        //    json.Append(",\"children\":");

        //    json.Append(strJson.ToString());

        //    json.Append("}");
        //    json.Append("]");
        //    return json.ToString();
        //}

        //private void QueryTreeGrid(HttpContext context)
        //{
        //    string name, order, sort, startTime, endTime, oderby;

        //    //===============================================================  
        //    //获取查询条件:【用户名,开始时间，结束时间】  
        //    name = startTime = endTime = order = sort = oderby = "";

        //    //获取前台传来的值  
        //    if (null != context.Request.QueryString["name"])
        //    {//获取前台传来的值  
        //        name = context.Request.QueryString["name"].ToString().Trim();
        //    }
        //    if (null != context.Request.QueryString["startTime"])
        //    {
        //        startTime = context.Request.QueryString["startTime"].ToString().Trim();
        //    }
        //    if (null != context.Request.QueryString["endTime"])
        //    {
        //        endTime = context.Request.QueryString["endTime"].ToString().Trim();
        //    }

        //    //================================================================  

        //    if (null != context.Request.QueryString["sort"])
        //    {

        //        order = context.Request.QueryString["sort"].ToString().Trim();

        //    }
        //    if (null != context.Request.QueryString["order"])
        //    {

        //        sort = context.Request.QueryString["order"].ToString().Trim();

        //    }


        //    //===================================================================  
        //    //组合查询语句：条件+排序  
        //    StringBuilder strWhere = new StringBuilder();
        //    if (name != "")
        //    {
        //        strWhere.AppendFormat(" name like '%{0}%' and ", name);
        //    }
        //    if (startTime != "")
        //    {
        //        strWhere.AppendFormat(" register_time >= '{0}' and ", startTime);
        //    }
        //    if (endTime != "")
        //    {
        //        strWhere.AppendFormat(" register_time <= '{0}' and ", endTime);
        //    }


        //    //删除多余的and  
        //    int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
        //    if (startindex >= 0)
        //    {
        //        strWhere.Remove(startindex, 3);//删除多余的and关键字  
        //    }
        //    if (sort != "" && order != "")
        //    {
        //        //strWhere.AppendFormat(" order by {0} {1}", sort, order);//添加排序  
        //        oderby = order + " " + sort;
        //    }

        //    String where = " and a.[identity] = '2'";

        //    //调用分页的GetList方法  
        //    DataSet ds = bll_account.GetLevelListByPId(0, false, where);


        //    context.Response.Write(GetJsonForTreeByDataset(ds, where, 1));
        //    context.Response.End();
        //}

        /// <summary>  
        /// 查询记录  
        /// </summary>  
        /// <param name="context"></param>  
        public void Query(HttpContext context)
        {
            string name, order, sort, startTime, endTime, isDeleted, oderby,isExport;

            //===============================================================  
            //获取查询条件:【用户名,开始时间，结束时间】  
            name = startTime = endTime = isDeleted = order = sort = oderby = isExport = "";

            //获取前台传来的值  
            if (null != context.Request.QueryString["name"])
            {//获取前台传来的值  
                name = context.Request.QueryString["name"].ToString().Trim();
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
            if (null != context.Request.QueryString["isExport"])
            {
                isExport = context.Request.QueryString["isExport"].ToString().Trim();
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
            //DataSet ds = Bnotice.GetList(strWhere.ToString());  //调用不分页的getlist  



            //调用分页的GetList方法  
            DataSet ds = bll_user.GetListByPage(strWhere.ToString(), oderby, (page - 1) * pageRows + 1, page * pageRows);

            string strJson = "";
            if (isExport == "1")
            {
                DataTable thisTable = ds.Tables[0];
                //镇江市土地利用年度国家计划使用情况表
                string fileName = "镇江市土地利用年度国家计划使用情况表";
                strJson = JsonHelper.GetJsonByDataset_Export(ds, fileName);
            }
            else {
                int count = bll_user.GetRecordCount(strWhere.ToString());//获取条数  
                strJson = JsonHelper.Dataset2Json(ds, count);//DataSet数据转化为Json数据  
            }

            
            context.Response.Write(strJson);//返回给前台页面  
            context.Response.End();

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