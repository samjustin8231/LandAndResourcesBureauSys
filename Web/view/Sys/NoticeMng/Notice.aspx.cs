using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.View.NoticeMng
{
    public partial class Notice : BasePage
    {
        public BLL.T_Notice bll_notice = new BLL.T_Notice();
        public Model.T_Notice t_notice = new Model.T_Notice();

        protected void Page_Load(object sender, EventArgs e)
        {
            //获取最新的notice

            string id_notice;
            id_notice = Request["id_notice"];

            //List<Model.T_Notice> list_notice = bll_notice.GetModelList("");
            if (id_notice != "")
            {
                t_notice = bll_notice.GetModel(Convert.ToInt32(id_notice));
            }

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //比当前小的最后一篇

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //比当前大的第一篇

        }
    }
}