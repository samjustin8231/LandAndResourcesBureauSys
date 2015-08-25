using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace Maticsoft.Web.utils
{
    public static class Util
    {
        


        public static string GetNameByPrivilegeId(string code)
        {
            string name = "";
            switch (code)
            {
                case "100":
                    name = "浏览";
                    break;
                case "200":
                    name = "添加";
                    break;
                case "300":
                    name = "修改";
                    break;
                case "400":
                    name = "删除";
                    break;
                case "500":
                    name = "启用/关闭";
                    break;
                case "600":
                    name = "审核";
                    break;
                case "700":
                    name = "导出";

                    break;
                case "800":
                    name = "查处详细";
                    break;
                case "900":
                    name = "授权";

                    break;
                case "1000":
                    name = "移动";

                    break;
                case "1100":
                    name = "打印";
                    break;
                case "1200":
                    name = "下载";
                    break;
                case "1300":
                    name = "修改已征收面积";
                    break;
            }
            return name;
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        } 

        /// <summary> 
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串 
        /// </summary> 
        /// <param name="CnStr">汉字字符串</param> 
        /// <returns>相对应的汉语拼音首字母串</returns> 
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;

            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }

            return strTemp;
        }


        /// <summary> 
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                // get the array of byte from the single char 
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }


        public static string[] ConvertStringToArrayString(String strIds) {
            string[] ids = strIds.Split(',');
            return ids;
        }


        public static string ConvertPath(String fName)
        {
            //fName.Replace('\\','\\\\');
            return null;
        }

        /// <summary>
      /// 获取客户端IP地址（无视代理）
      /// </summary>
      /// <returns>若失败则返回回送地址</returns>
      public static string GetIP()
      {
          string userHostAddress = HttpContext.Current.Request.UserHostAddress;
  
          if (string.IsNullOrEmpty(userHostAddress))
         {
             userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
         }
 
         //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
         if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
         {
             return userHostAddress;
         }
         return "127.0.0.1";
     }
 
     /// <summary>
     /// 检查IP地址格式
     /// </summary>
     /// <param name="ip"></param>
     /// <returns></returns>
     public static bool IsIP(string ip)
     {
         return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
     }

     


        /// <summary>
        /// 把DataTable内容导出伟excel并返回客户端
        /// </summary>
        /// <param name="dgData">待导出的DataTable</param>
        /// 创 建 人：陈文凯
        /// 创建日期：2005年10月08日
        /// 修 改 人：
        /// 修改日期：
        public static void DataTable2Excel(System.Data.DataTable dtData)
        {
            System.Web.UI.WebControls.DataGrid dgExport = null;
            // 当前对话
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            // IO用于导出并返回excel文件
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (dtData != null)
            {
                // 设置编码和附件格式
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding =System.Text.Encoding.UTF8;
                curContext.Response.Charset = "";
                
                // 导出excel文件
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid
                dgExport = new System.Web.UI.WebControls.DataGrid();
                dgExport.DataSource = dtData.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.DataBind();

                // 返回客户端
                dgExport.RenderControl(htmlWriter);    
                curContext.Response.Write(strWriter.ToString());
                curContext.Response.End();
            }
        }



        /// <summary>
        /// 删除多余的and 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static StringBuilder removeLastAnd(StringBuilder strWhere)
        {
            //删除多余的and  
            int startindex = strWhere.ToString().LastIndexOf("and");//获取最后一个and的位置  
            if (startindex >= 0)
            {
                strWhere.Remove(startindex, 3);//删除多余的and关键字  
            }
            return strWhere;
        }


    }


}