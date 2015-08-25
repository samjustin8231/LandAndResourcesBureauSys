using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Maticsoft.Model;

namespace Maticsoft.Web.utils
{
    public static class BusinessHelper
    {
        public static BLL.T_Batch bll_batch = new BLL.T_Batch();
        public static BLL.T_Plan bll_plan = new BLL.T_Plan();
        public static BLL.T_DemandSupplyBalance bll_demand_supply_balance = new BLL.T_DemandSupplyBalance();
        public static BLL.T_Batch_Demand_Supply_Balance bll_batch_balance = new BLL.T_Batch_Demand_Supply_Balance();
        public static BLL.T_LevyCompensate bll_levy = new BLL.T_LevyCompensate();
        public static BLL.T_Batch_Plan bll_batch_plan = new BLL.T_Batch_Plan();
        public static BLL.T_Role bll_role = new BLL.T_Role();
        public static BLL.T_Menu bll_menu = new BLL.T_Menu();
        public static BLL.T_Privilege bll_privilege = new BLL.T_Privilege();
        public static BLL.T_User bll_user = new BLL.T_User();
        public static BLL.T_User_Role bll_user_role = new BLL.T_User_Role();
        public static BLL.T_PlanType bll_plan_type = new BLL.T_PlanType();
        public static BLL.T_DemandSupplyBalanceType bll_ds_balance_type = new BLL.T_DemandSupplyBalanceType();
        public static BLL.T_BatchType bll_batch_type = new BLL.T_BatchType();

        /// <summary>
        /// 角色是否添加了用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsRoleHasUsed(int roleId) {
            List<Model.T_User_Role> list_user_role = bll_user_role.GetModelList(" id_role = "+roleId);
            if (list_user_role.Count > 0) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 行政区是否添加了（主要看计划、占补平衡、批次、user）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsAdministratorAreaHasUsed(int id)
        {
            List<Model.T_Plan> list_plan = bll_plan.GetModelList(" administratorArea = " + id);
            List<Model.T_DemandSupplyBalance> list_ds_balance = bll_demand_supply_balance.GetModelList(" administratorArea = " + id);
            List<Model.T_Batch> list_batch = bll_batch.GetModelList(" administratorArea = " + id);

            if (list_batch.Count > 0 || list_ds_balance.Count > 0 || list_plan.Count > 0) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 权限是否添加了角色（有点复杂）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsPrivilegeHasUsed(int menuId,int code)
        {
            int n = menuId + code;
            bool flag = false;
            List<Model.T_Role> list_batch = bll_role.GetModelList("");

            foreach (Model.T_Role t_role in list_batch) {
                if (t_role.content.Contains(n+"")) {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        /// <summary>
        /// 菜单是否添加了小权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsMenuHasUsed(int menuId)
        {
            List<Model.T_Privilege> list_privilege = bll_privilege.GetModelList(" menuId = " + menuId);
            if (list_privilege.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计划类型是否添加了计划
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsPlanTypeHasUsed(int planId)
        {
            List<Model.T_Plan> list_plan = bll_plan.GetModelList(" planTypeId = " + planId);
            if (list_plan.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 占补平衡类型是否添加了
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsDSBalanceTypeHasUsed(int typeId)
        {
            List<Model.T_DemandSupplyBalance> list_ds_balance = bll_demand_supply_balance.GetModelList(" typeId = " + typeId);
            if (list_ds_balance.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批次类型是否添加了
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsBatchTypeHasUsed(int typeId)
        {
            List<Model.T_Batch> list_batch = bll_batch.GetModelList(" batchTypeId = " + typeId);
            if (list_batch.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计划是否被批次使用了
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public static bool IsPlanHasUsed(int planId)
        {
            List<Model.T_Batch_Plan> list_batch_plan = bll_batch_plan.GetModelList(" planId = "+planId);
            if (list_batch_plan.Count > 0) {
                return true;
            }else{
                return false;
            }
        }

        /// <summary>
        /// 占补平衡是否被批次使用了
        /// </summary>
        /// <param name="balanceId"></param>
        /// <returns></returns>
        public static bool IsBalanceHasUsed(int balanceId)
        {
            List<Model.T_Batch_Demand_Supply_Balance> list_batch_balance = bll_batch_balance.GetModelList(" dsBalanceId=" + balanceId);
            if (list_batch_balance.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 地块是否被征收补偿使用了
        /// </summary>
        /// <param name="landblockId"></param>
        /// <returns></returns>
        public static bool IsLandBlockHasUsed(int landblockId)
        {
            List<Model.T_LevyCompensate> list_levy = bll_levy.GetModelList(" landblockId=" + landblockId);
            if (list_levy.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批次是否用了计划
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static bool IsBatchHasUsePlan(int batchId)
        {
            List<Model.T_Batch_Plan> list_batch_plan = bll_batch_plan.GetModelList(" batchId = " + batchId);
            if (list_batch_plan.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批次是否使用了占卜平衡
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static bool IsBatchHasUseBalance(int batchId)
        {
            List<Model.T_Batch_Demand_Supply_Balance> list_batch_balance = bll_batch_balance.GetModelList(" batchId=" + batchId);
            if (list_batch_balance.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批次是否被征收补偿使用了
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static bool IsBatchHasUsedByLevy(int batchId)
        {
            List<Model.T_LevyCompensate> list_levy = bll_levy.GetModelList(" batchId=" + batchId);
            if (list_levy.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批次是否使用了：批次是否使用了计划和占补平衡
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static bool IsBatchHasUse(int batchId)
        {
            if (IsBatchHasUsePlan(batchId) || IsBatchHasUseBalance(batchId) || IsBatchHasUsedByLevy(batchId))
            {
                return true;
            }
            else {
                return false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 获取计划剩余面积
        /// </summary>
        /// <param name="planId"></param>
        public static void GetRemainAreaByPlanId(int planId) { 
            
        }

        /// <summary>
        /// 根据占补平衡id获取剩余面积
        /// </summary>
        /// <param name="balanceId"></param>
        /// <returns></returns>
        public static RemainArea1 GetRemainAreaByBalanceId(int balanceId)
        {
            Model.T_DemandSupplyBalance model = bll_demand_supply_balance.GetModel(Convert.ToInt32(balanceId));

            //获取该批次已经使用面积
            decimal usedConsArea, usedAgriArea, usedArabArea;
            usedConsArea = usedAgriArea = usedArabArea = 0;
            DataSet ds = bll_batch_balance.GetList(" dsBalanceId=" + balanceId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["agriArea"] != null)
                    {
                        usedAgriArea += Convert.ToDecimal(dr["agriArea"]);
                    }
                    if (dr["arabArea"] != null)
                    {
                        usedArabArea += Convert.ToDecimal(dr["arabArea"]);
                    }
                }
            }

            RemainArea1 remainAreas = new RemainArea1();
            remainAreas.consArea = 0;
            remainAreas.agriArea = (Decimal)model.agriArea - usedAgriArea;
            remainAreas.arabArea = (Decimal)model.arabArea - usedArabArea;
            return remainAreas;
        }

        public static void GetRemainAreaByBatchId(int planId)
        {

        }
    }
}