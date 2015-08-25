using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_User
	/// </summary>
	public partial class T_User
	{
		public T_User()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "T_User"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_User");
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
        public int Add(Maticsoft.Model.T_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_User(");
            strSql.Append("name,loginName,password,telephone,address,birthday,create_time,isDeleted,isOnline,remarks)");
            strSql.Append(" values (");
            strSql.Append("@name,@loginName,@password,@telephone,@address,@birthday,@create_time,@isDeleted,@isOnline,@remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@loginName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,100),
					new SqlParameter("@telephone", SqlDbType.VarChar,100),
					new SqlParameter("@address", SqlDbType.VarChar,200),
					new SqlParameter("@birthday", SqlDbType.Date,3),
					new SqlParameter("@create_time", SqlDbType.DateTime),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@isOnline", SqlDbType.Int,4),
					new SqlParameter("@remarks", SqlDbType.VarChar,200)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.loginName;
            parameters[2].Value = model.password;
            parameters[3].Value = model.telephone;
            parameters[4].Value = model.address;
            parameters[5].Value = model.birthday;
            parameters[6].Value = model.create_time;
            parameters[7].Value = model.isDeleted;
            parameters[8].Value = model.isOnline;
            parameters[9].Value = model.remarks;

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
        public bool Update(Maticsoft.Model.T_User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_User set ");
            strSql.Append("name=@name,");
            strSql.Append("loginName=@loginName,");
            strSql.Append("password=@password,");
            strSql.Append("telephone=@telephone,");
            strSql.Append("address=@address,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("create_time=@create_time,");
            strSql.Append("isDeleted=@isDeleted,");
            strSql.Append("isOnline=@isOnline,");
            strSql.Append("remarks=@remarks");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@loginName", SqlDbType.VarChar,50),
					new SqlParameter("@password", SqlDbType.VarChar,100),
					new SqlParameter("@telephone", SqlDbType.VarChar,100),
					new SqlParameter("@address", SqlDbType.VarChar,200),
					new SqlParameter("@birthday", SqlDbType.Date,3),
					new SqlParameter("@create_time", SqlDbType.DateTime),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@isOnline", SqlDbType.Int,4),
					new SqlParameter("@remarks", SqlDbType.VarChar,200),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.loginName;
            parameters[2].Value = model.password;
            parameters[3].Value = model.telephone;
            parameters[4].Value = model.address;
            parameters[5].Value = model.birthday;
            parameters[6].Value = model.create_time;
            parameters[7].Value = model.isDeleted;
            parameters[8].Value = model.isOnline;
            parameters[9].Value = model.remarks;
            parameters[10].Value = model.id;

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
			strSql.Append("delete from T_User ");
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
			strSql.Append("delete from T_User ");
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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_User GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 * from T_User ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_User model=new Maticsoft.Model.T_User();
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
        public Maticsoft.Model.T_User DataRowToModel(DataRow row)
        {
            Maticsoft.Model.T_User model = new Maticsoft.Model.T_User();
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
                if (row["loginName"] != null)
                {
                    model.loginName = row["loginName"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["telephone"] != null)
                {
                    model.telephone = row["telephone"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["birthday"] != null && row["birthday"].ToString() != "")
                {
                    model.birthday = DateTime.Parse(row["birthday"].ToString());
                }
                if (row["create_time"] != null && row["create_time"].ToString() != "")
                {
                    model.create_time = DateTime.Parse(row["create_time"].ToString());
                }
                if (row["isDeleted"] != null && row["isDeleted"].ToString() != "")
                {
                    model.isDeleted = int.Parse(row["isDeleted"].ToString());
                }
                if (row["isOnline"] != null && row["isOnline"].ToString() != "")
                {
                    model.isOnline = int.Parse(row["isOnline"].ToString());
                }
                if (row["remarks"] != null)
                {
                    model.remarks = row["remarks"].ToString();
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
			strSql.Append(" FROM T_User ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			strSql.Append(" FROM T_User ");
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
			strSql.Append("select count(1) FROM T_User ");
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
			strSql.Append(")AS Row, T.*  from T_User T ");
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
			parameters[0].Value = "T_User";
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

        public Model.T_User Login(string name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_User ");
            strSql.Append(" where name=@name and password=@password");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
                    new SqlParameter("@password", SqlDbType.VarChar,100)
			};
            parameters[0].Value = name;
            parameters[1].Value = password;

            Maticsoft.Model.T_User model = new Maticsoft.Model.T_User();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}

