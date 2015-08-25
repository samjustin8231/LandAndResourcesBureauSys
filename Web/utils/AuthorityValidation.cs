using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maticsoft.Web.utils
{
    public static class AuthorityValidation
    {
        public static BLL.T_User_Role bll_user_role = new BLL.T_User_Role();
        public static BLL.T_Menu bll_menu = new BLL.T_Menu();
        public static BLL.T_Role bll_role = new BLL.T_Role();
        public static BLL.T_Privilege bll_privilege = new BLL.T_Privilege();

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static List<Model.T_User_Role> GetRoleByUserId(int user_id)
        {
            List<Model.T_User_Role> list_role = bll_user_role.GetModelList(" id_user = "+user_id);

            return list_role;
        }

        /// <summary>
        /// 获取角色的一级菜单权限
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        internal static List<Model.T_Menu> GetOneLevelMenuListByRoleId(string role_id)
        {
            List<Model.T_Menu> list_menu = bll_menu.GetModelList(" ");

            return list_menu;
        }

        /// <summary>
        /// 获取角色pid下面的菜单权限
        /// </summary>
        /// <param name="role_id"></param>
        /// <returns></returns>
        internal static List<Model.T_Menu> GetMenuListByPid(string role_id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取role的权限content
        /// </summary>
        /// <param name="nullable"></param>
        /// <returns></returns>
        internal static string GetMenusByRoleId(int? id_role)
        {
            Model.T_Role t_role = bll_role.GetModel((int)id_role);
            if (t_role != null)
            {
                return t_role.content;
            }
            else {
                return "";
            }
        }

        /// <summary>
        /// 根据菜单Id、操作code获取菜单操作全称  如：添加计划
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetMethodNameByMenuIdAndCode(int menuId, int i)
        {
            String name = "";
            List<Model.T_Menu> list_menu = bll_menu.GetModelList("");
            foreach (Model.T_Menu t_menu in list_menu)
            {
                if (t_menu.id == menuId)
                {
                    name = utils.Util.GetNameByPrivilegeId(i + "") + t_menu.name;
                }
            }
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="menuContent"></param>
        /// <returns></returns>
        public static List<String> GetMethodByMenuId(int menuId,String menuContent)
        {
            List<String> list = new List<string>();
            Model.T_Menu menu = bll_menu.GetModel(menuId);
            for (int i = 100; i <= 1300; i += 100)
            {
                int _id = menuId + i;
                if (menuContent.Contains(_id + ""))
                {
                    list.Add(i+"");
                }
            }
            return list;
        }

        /// <summary>
        /// 判断变换后的操作code是否在对应的CRUD操作中
        /// </summary>
        /// <param name="id_int"></param>
        /// <returns></returns>
        public static bool IsInCRUD(int id_int)
        {
            List<Model.T_Menu> list_menu = bll_menu.GetModelList("");
            foreach (Model.T_Menu t_menu in list_menu)
            {
                for (int i = 100; i <= 1300; i += 100)
                {
                    int _id = t_menu.id + i;
                    if (id_int == _id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取CRUD全称名称
        /// </summary>
        /// <param name="id_int"></param>
        /// <returns></returns>
        public static string GetMethodCRUD(int id_int)
        {
            String methodName = "";
            List<Model.T_Menu> list_menu = bll_menu.GetModelList("");
            foreach (Model.T_Menu t_menu in list_menu)
            {
                for (int i = 100; i <= 1300; i += 100)
                {
                    int _id = t_menu.id + i;
                    if (id_int == _id)
                    {
                        methodName = GetMethodNameByMenuIdAndCode(t_menu.id, i);
                    }
                }
            }
            return methodName;
        }

        /// <summary>
        /// 根据CRUD id 获取菜单
        /// </summary>
        /// <param name="id_int"></param>
        /// <returns></returns>
        public static Model.T_Menu GetMenuByCRUDId(int id_int)
        {
            List<Model.T_Menu> list_menu = bll_menu.GetModelList("");
            foreach (Model.T_Menu t_menu in list_menu)
            {
                for (int i = 100; i <= 1300; i += 100)
                {
                    int _id = t_menu.id + i;
                    if (id_int == _id)
                    {
                        return t_menu;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取用户权限里的一级菜单
        /// 如果没有一级，则获取二级菜单的上级
        /// 如果没有二级，则获取crud对应的二级
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="menuContent"></param>
        /// <returns></returns>
        internal static List<Model.T_Menu> GetMenuListOfMenusByPid(int pid, string menuContent)
        {
            List<Model.T_Menu> list_menu = bll_menu.GetModelList("1=1 and pid = " + pid + " and isDeleted = 0 and id in(" + menuContent + ")");

            //如果没有一级菜单，则看这些二级菜单的的上级

            //16,25

            string[] ids = menuContent.Split(',');
            //获取每一个上级放在list_menu中
            foreach (string id in ids)
            {
                //获取所有的一级菜单
                //Model.T_Menu t_menu = GetOneLevelById(Convert.ToInt32(id));

                //if (!list_menu.Contains(t_menu, MenuComparer.Default)) {
                //    list_menu.Add(t_menu);
                //}

                //没有二级菜单,则获取crud对应的二级
                Model.T_Menu t_menu = bll_menu.GetModel(Convert.ToInt32(id));

                if (t_menu == null)
                {
                    //是否是增删改查
                    if (IsInCRUD(Convert.ToInt32(id)))
                    {
                        t_menu = GetMenuByCRUDId(Convert.ToInt32(id));
                    }

                    if (pid == 0)
                    {
                        //判断菜单是一级还是二级
                        if (t_menu.pid == 0)//一级，扔掉
                        {

                        }
                        else
                        {
                            //有二级菜单,则获取二级对应的上级
                            Model.T_Menu p_menu = bll_menu.GetModel((int)t_menu.pid);

                            if (!list_menu.Contains(p_menu, MenuComparer.Default))
                            {
                                list_menu.Add(p_menu);
                            }
                        }


                    }
                    else
                    {
                        // pid t_menu 有关系
                        if (t_menu.id == pid || pid != t_menu.pid)
                        {

                        }
                        else
                        {
                            if (!list_menu.Contains(t_menu, MenuComparer.Default))//二级菜单中是否已经添加
                            {
                                //是否属于一个一级

                                list_menu.Add(t_menu);
                            }
                        }
                    }
                }

                else
                {
                    if (pid == 0)
                    {
                        //判断菜单是一级还是二级
                        if (t_menu.pid == 0)//一级，扔掉
                        {

                        }
                        else
                        {
                            //有二级菜单,则获取二级对应的上级
                            Model.T_Menu p_menu = bll_menu.GetModel((int)t_menu.pid);

                            if (!list_menu.Contains(p_menu, MenuComparer.Default))
                            {
                                list_menu.Add(p_menu);
                            }
                        }


                    }
                    else
                    {
                        // pid t_menu 有关系
                        if (t_menu.id == pid || pid != t_menu.pid)
                        {

                        }
                        else
                        {
                            if (!list_menu.Contains(t_menu, MenuComparer.Default))//二级菜单中是否已经添加
                            {
                                //是否属于一个一级

                                list_menu.Add(t_menu);
                            }
                        }
                    }




                }
            }
            return list_menu;
        }


        
    }
}