using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Json;
using BLL;

namespace Maticsoft.Common
{
    public class JsonHelper
    {
        public JsonHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string Object2Json<T>(T obj)
        {
            //记住 添加引用 System.ServiceModel.Web 
            /**
             * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json是出不来的哦
             * */
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, obj);
                string szJson = Encoding.UTF8.GetString(ms.ToArray());
                return szJson;
            }
        }

        #region DataSet转换成Json格式
        /// <summary>  
        /// DataSet转换成Json格式    
        /// </summary>    
        /// <param name="ds">DataSet</param>   
        /// <returns></returns>    
        public static string Dataset2Json(DataSet ds, int total = -1)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                //{"total":5,"rows":[  
                json.Append("{\"total\":");
                if (total == -1)
                {
                    json.Append(dt.Rows.Count);
                }
                else
                {
                    json.Append(total);
                }
                json.Append(",\"rows\":[");
                json.Append(DataTable2Json(dt));
                json.Append("]}");
            } return json.ToString();
        }
        #endregion

        #region dataTable转换成Json格式
        /// <summary>    
        /// dataTable转换成Json格式    
        /// </summary>    
        /// <param name="dt"></param>    
        /// <returns></returns>    
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(cleanString(dt.Rows[i][j].ToString()));
                    jsonBuilder.Append("\",");
                }
                if (dt.Columns.Count > 0)
                {
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                }
                jsonBuilder.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            }

            return jsonBuilder.ToString();
        }
        #endregion dataTable转换成Json格式  

        public static string cleanString(string newStr)
        {
            string tempStr = newStr.Replace("\r\n", "\\\r\n");//转义换行符  
            newStr = newStr.Replace("\"", "'");  //替换双引号为单引号  
            return tempStr;
        }


        /// <summary>
        /// 把dataset数据转换成json的格式，和文件名  
        /// </summary> 
        /// <param name="ds"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetJsonByDataset_Export(DataSet ds, string fileName)
        {
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                //如果查询到的数据为空则返回标记ok:false
                return "{\"fileName\":\"" + fileName + "\",\"rows\":[]}";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"fileName\":\"" + fileName + "\",");
            int n = ds.Tables[0].Rows.Count;

            foreach (DataTable dt in ds.Tables)
            {
                sb.Append(string.Format("\"rows\":[", dt.TableName));

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", dr.Table.Columns[i].ColumnName.Replace("\"", "\\\"").Replace("\'", "\\\'"), ObjToStr(dr[i]).Replace("\"", "\\\"").Replace("\'", "\\\'")).Replace(Convert.ToString((char)13), "\\r\\n").Replace(Convert.ToString((char)10), "\\r\\n");
                    }
                    sb.Remove(sb.ToString().LastIndexOf(','), 1);
                    sb.Append("},");
                }

                sb.Remove(sb.ToString().LastIndexOf(','), 1);
                sb.Append("],");
            }
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 将object转换成为string
        /// </summary>
        /// <param name="ob">obj对象</param>
        /// <returns></returns>
        public static string ObjToStr(object ob)
        {
            if (ob == null)
            {
                return string.Empty;
            }
            else
                return ob.ToString();
        }

        /// <summary>
        /// 把dataset数据转换成treegrid 需要的json的格式
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DataSet2JsonForTree_Menu(DataSet ds, string where,string orderby)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder json = new StringBuilder();
            String pname = "";
            json.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                BLL.T_Menu bll_menu = new BLL.T_Menu();
                json.Append("{\"id\":" + dr["id"].ToString());
                json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
                json.Append(",\"state\":\"true\"");
                json.Append(",\"icon\":\"" + "" + "\"");
                json.Append(",\"url\":\"" + dr["url"].ToString() + "\"");
                json.Append(",\"description\":\"" + dr["description"].ToString() + "\"");
                json.Append(",\"pid\":\"" + dr["pid"] + "\"");

                //获取上一级菜单名称pname
                if (dr["pid"] != null && dr["pid"].ToString() != "")
                {
                    pname = bll_menu.GetModel(Convert.ToInt32(dr["pid"])).name;

                }
                json.Append(",\"pname\":\"" + pname + "\"");
                //json.Append(",\"url\":\"" + dr["url"] + "\"");
                DataTable dtChildren = new DataTable();

                //调用D层方法获取dataTable 
                DataSet dsChilds = bll_menu.GetLevelListByPId(Convert.ToInt32(dr["id"].ToString()), false, where,orderby);
                DataTable dtChilds = dsChilds.Tables[0];
                if (dtChilds != null && dtChilds.Rows.Count > 0)
                {
                    json.Append(",\"children\":");
                    json.Append(DataSet2JsonForTree_Menu(dsChilds, where,orderby));
                }
                json.Append("},");

            }
            if (dt.Rows.Count > 0)
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
        public static string GetJsonForTreeByDataset_Menu(DataSet ds, string where, int type, string oderby)
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
                json.Append(",\"attributes\":{\"iconCls\":\"" + "icon-man" + "\"" + ",\"url\":\""+dr["url"].ToString()+"\"" + "}");

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

                json.Append("},");

            }
            if (dt.Rows.Count > 0 && json.ToString().Substring(json.Length - 1, 1) == ",")
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
        }


        #region ds转换成Json格式
        /// <summary>    
        /// dataTable转换成Json格式    
        /// </summary>    
        /// <param name="dt"></param>    
        /// <returns></returns>    
        public static string DataTable2JsonArray(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                //如果查询到的数据为空则返回标记ok:false
                return "[]";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int n = ds.Tables[0].Rows.Count;

            foreach (DataTable dt in ds.Tables)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        sb.Append("\"" + dr.Table.Columns[i].ColumnName + "\":" + "\"" + dr[i] + "\",");

                    }
                    sb.Remove(sb.ToString().LastIndexOf(','), 1);
                    sb.Append("},");
                }

                sb.Remove(sb.ToString().LastIndexOf(','), 1);

            }
            sb.Append("]");
            return sb.ToString().ToLower();
        }
        #endregion dataTable转换成Json格式  

    }
}
