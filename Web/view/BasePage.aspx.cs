using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Web.utils;

namespace Maticsoft.Web.View
{
    public partial class BasePage : System.Web.UI.Page
    {
        public List<String> list_privilege_cur_page = new List<string>();
        private BLL.T_Role bll_role = new BLL.T_Role();
        private BLL.T_Privilege bll_privilege = new BLL.T_Privilege();
        public String sysName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 构造函数
        public BasePage()
        {
        }

        #endregion 构造函数

        protected override void OnInit(EventArgs e) {
            //Session失效，跳到登录页面
            if (Session["user_id"] == null)
            {
                Response.Write("<script>top.location='/View/Sys/login.aspx';</script>");
                //Response.Redirect("/View/Sys/login.html");
            }
            else { 
                //获取用户的角色
                if (Session["user_name"].ToString() == "admin")
                {
                    for (int i = 100; i <= 1300; i += 100)
                    {
                        list_privilege_cur_page.Add(i+"");
                    }
                }
                else {
                    //获取用户的权限
                    if (Session["role_id"] != null) {
                        Model.T_Role t_role = bll_role.GetModel(Convert.ToInt32(Session["role_id"].ToString()));
                        //获取role的权限content
                        String menuContent = AuthorityValidation.GetMenusByRoleId(Convert.ToInt32(t_role.id));

                        String curPageName = this.GetType().Name;
                        //view_business_planmng_planlist_aspx  view_sys_noticemng_noticelist_aspx

                        if (curPageName.Contains("main") || curPageName.Contains("west") || curPageName.Contains("center") || curPageName.Contains("north") || curPageName.Contains("south"))
                        {
                            
                        }else{
                            //获取角色的一级菜单权限
                            List<Model.T_Menu> list_menu_one = new List<Model.T_Menu>();
                            List<Model.T_Menu> list_menu_two = new List<Model.T_Menu>();
                            list_menu_one = AuthorityValidation.GetMenuListOfMenusByPid(0, menuContent);

                            if (list_menu_one.Count > 0)
                            {
                                bool flag = false;
                                foreach (Model.T_Menu t_menu in list_menu_one)
                                {
                                    if (flag)
                                    {
                                        break;
                                    }
                                    else {
                                        //获取角色pid下面的菜单权限  "view_business_planmng_planlist_aspx"
                                        list_menu_two = AuthorityValidation.GetMenuListOfMenusByPid(t_menu.id, menuContent);
                                        //判断当前页是否在权限范围内
                                        foreach (Model.T_Menu menuTwo in list_menu_two)
                                        {
                                            if (menuTwo.url != "") {
                                                String pageName = menuTwo.url.Substring(menuTwo.url.LastIndexOf('/') + 1);
                                                if (curPageName.Contains(pageName.Substring(0, pageName.IndexOf('.')).ToLower()))
                                                {
                                                    //获取该页面对应的几个权限
                                                    list_privilege_cur_page = AuthorityValidation.GetMethodByMenuId(menuTwo.id, menuContent);
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                            
                                        }
                                    }

                                
                                }
                            }
                        }

                        
                    }
                    

                }

                

            }

            base.OnInit(e);
        }


    }
}