using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Maticsoft.Web.utils
{
    public class MenuComparer:IEqualityComparer<Model.T_Menu>
    {
        public static MenuComparer Default = new MenuComparer();
        #region  成员
        public bool Equals(Model.T_Menu x, Model.T_Menu y)
        {
            return x.id == y.id;
        }
        public int GetHashCode(Model.T_Menu obj)
        {
            return obj.GetHashCode();
        }
        #endregion
    }
}