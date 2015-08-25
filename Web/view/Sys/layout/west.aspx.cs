using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Web.utils;
using BLL;

namespace Maticsoft.Web.View.layout
{
    public partial class west : BasePage
    {
        BLL.T_Menu bll_menu = new BLL.T_Menu();
        BLL.T_Role bll_role = new BLL.T_Role();
        BLL.T_User bll_user = new BLL.T_User();
        BLL.T_User_Role bll_user_role = new BLL.T_User_Role();

        public List<Model.T_Menu> list_menu_one = new List<Model.T_Menu>();
        public List<Model.T_Menu> list_menu_two = new List<Model.T_Menu>();
        public Dictionary<String, List<Model.T_Menu>> map_menu = new Dictionary<string, List<Model.T_Menu>>();
        public Dictionary<String, Model.T_Menu> map_one = new Dictionary<string, Model.T_Menu>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.OnInit(e);

            //获取用户的权限
            if (Session["user_name"] != null)
            {
                //admin不进行权限检查，拥有所哟权限
                if (Session["user_name"].ToString() == "admin")
                {
                    //管理员拥有所有权限，不进行权限判断

                    //获取所有菜单信息
                    list_menu_one = bll_menu.GetModelList("1=1 and pid=0 and isDeleted = 0");

                    if (list_menu_one.Count > 0)
                    {

                        foreach (Model.T_Menu t_menu in list_menu_one)
                        {
                            //
                            map_one.Add(t_menu.name,t_menu);

                            list_menu_two = bll_menu.GetModelList("1=1 and pid=" + t_menu.id + " and isDeleted = 0");
                            map_menu.Add(t_menu.name, list_menu_two);
                        }
                    }
                }
                else
                {
                    //非admin用户进行权限检查，根据权限动态生成菜单

                    if (Session["user_id"] != null)
                    {
                        //获取用户所有角色
                        List < Model.T_User_Role> list_user_role = bll_user_role.GetModelList(" id_user='" + Convert.ToInt32(Session["user_id"]) + "'");


                        foreach (Model.T_User_Role t_user_role in list_user_role) {

                            Model.T_Role t_role = bll_role.GetModel(Convert.ToInt32(t_user_role.id_role));

                            //获取role的权限content
                            String menuContent = AuthorityValidation.GetMenusByRoleId(Convert.ToInt32(t_role.id));

                            //获取角色的一级菜单权限
                            list_menu_one = AuthorityValidation.GetMenuListOfMenusByPid(0, menuContent);

                            if (list_menu_one.Count > 0)
                            {

                                foreach (Model.T_Menu t_menu in list_menu_one)
                                {
                                    map_one.Add(t_menu.name, t_menu);

                                    //获取角色pid下面的菜单权限
                                    list_menu_two = AuthorityValidation.GetMenuListOfMenusByPid(t_menu.id, menuContent);
                                    map_menu.Add(t_menu.name, list_menu_two);
                                }
                            }
                        }

                        
                        
                    }
                }

            }


        }
    }
}