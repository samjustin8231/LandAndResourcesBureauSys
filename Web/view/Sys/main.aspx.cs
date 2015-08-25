using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Web.View;
using System.Configuration;

namespace Maticsoft.Web.view
{
    public partial class main : BasePage
    {
        public String sysName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取配置信息
            sysName = ConfigurationManager.AppSettings["sysName"];

            //检查用户是否登录
            if (Session["user_id"] != null)
            {

            }
            else {//未登录 
                Response.Redirect("login.aspx");
            }

        }
    }
}