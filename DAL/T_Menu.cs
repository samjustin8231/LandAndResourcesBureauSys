using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Menu
	/// </summary>
	public partial class T_Menu
	{
		public T_Menu()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "T_Menu"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Menu");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.T_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Menu(");
            strSql.Append("name,url,icon,description,pid,isDeleted,_sort)");
            strSql.Append(" values (");
            strSql.Append("@name,@url,@icon,@description,@pid,@isDeleted,@_sort)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,40),
					new SqlParameter("@url", SqlDbType.VarChar,200),
					new SqlParameter("@icon", SqlDbType.VarChar,30),
					new SqlParameter("@description", SqlDbType.VarChar,200),
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@_sort", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.url;
            parameters[2].Value = model.icon;
            parameters[3].Value = model.description;
            parameters[4].Value = model.pid;
            parameters[5].Value = model.isDeleted;
            parameters[6].Value = model._sort;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.T_Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Menu set ");
            strSql.Append("name=@name,");
            strSql.Append("url=@url,");
            strSql.Append("icon=@icon,");
            strSql.Append("description=@description,");
            strSql.Append("pid=@pid,");
            strSql.Append("isDeleted=@isDeleted,");
            strSql.Append("_sort=@_sort");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,40),
					new SqlParameter("@url", SqlDbType.VarChar,200),
					new SqlParameter("@icon", SqlDbType.VarChar,30),
					new SqlParameter("@description", SqlDbType.VarChar,200),
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@_sort", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.url;
            parameters[2].Value = model.icon;
            parameters[3].Value = model.description;
            parameters[4].Value = model.pid;
            parameters[5].Value = model.isDeleted;
            parameters[6].Value = model._sort;
            parameters[7].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Menu ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Menu ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// 级联删除数据
        /// </summary>
        public void DeleteCascode(int pid)
        {
            //是否有孩子，如果有，先删除孩子
            String sql = "select * from T_Menu where pid=" + pid;

            DataSet ds = DbHelperSQL.Query(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DeleteCascode(Convert.ToInt32(dr["id"].ToString()));

                }
                Delete(pid);
            }
            else
            {
                Delete(pid);
            }
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_Menu GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from T_Menu ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_Menu model=new Maticsoft.Model.T_Menu();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.T_Menu DataRowToModel(DataRow row)
        {
            Maticsoft.Model.T_Menu model = new Maticsoft.Model.T_Menu();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["url"] != null)
                {
                    model.url = row["url"].ToString();
                }
                if (row["icon"] != null)
                {
                    model.icon = row["icon"].ToString();
                }
                if (row["description"] != null)
                {
                    model.description = row["description"].ToString();
                }
                if (row["pid"] != null && row["pid"].ToString() != "")
                {
                    model.pid = int.Parse(row["pid"].ToString());
                }
                if (row["isDeleted"] != null && row["isDeleted"].ToString() != "")
                {
                    model.isDeleted = int.Parse(row["isDeleted"].ToString());
                }
                if (row["_sort"] != null && row["_sort"].ToString() != "")
                {
                    model._sort = int.Parse(row["_sort"].ToString());
                }
            }
            return model;
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM T_Menu ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by _sort asc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM T_Menu ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Menu ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from T_Menu T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "T_Menu";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod

        public DataSet GetLevelListByPId(int pid, bool b, string where, string orderby)
        {
            String sql = "select * from T_Menu where pid=" + pid + where;

            if (orderby != "") {
                sql += " order by " + orderby;
            }


            DataSet ds = DbHelperSQL.Query(sql);
            return ds;
        }
    }
}

