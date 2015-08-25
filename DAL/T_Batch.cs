using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Batch
	/// </summary>
	public partial class T_Batch
	{
		public T_Batch()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_Batch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Batch(");
			strSql.Append("name,subName,year,administratorArea,approvalAuthority,approvalNo,approvalTime,batchTypeId,totalArea,addConsArea,consArea,agriArea,arabArea,unusedArea,hasLevyArea,userId,createTime,isSubmited,isDeleted,des)");
			strSql.Append(" values (");
			strSql.Append("@name,@subName,@year,@administratorArea,@approvalAuthority,@approvalNo,@approvalTime,@batchTypeId,@totalArea,@addConsArea,@consArea,@agriArea,@arabArea,@unusedArea,@hasLevyArea,@userId,@createTime,@isSubmited,@isDeleted,@des)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@subName", SqlDbType.VarChar,50),
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@approvalAuthority", SqlDbType.VarChar,50),
					new SqlParameter("@approvalNo", SqlDbType.VarChar,50),
					new SqlParameter("@approvalTime", SqlDbType.Date,3),
					new SqlParameter("@batchTypeId", SqlDbType.Int,4),
					new SqlParameter("@totalArea", SqlDbType.Decimal,9),
					new SqlParameter("@addConsArea", SqlDbType.Decimal,9),
					new SqlParameter("@consArea", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@unusedArea", SqlDbType.Decimal,9),
					new SqlParameter("@hasLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.subName;
			parameters[2].Value = model.year;
			parameters[3].Value = model.administratorArea;
			parameters[4].Value = model.approvalAuthority;
			parameters[5].Value = model.approvalNo;
			parameters[6].Value = model.approvalTime;
			parameters[7].Value = model.batchTypeId;
			parameters[8].Value = model.totalArea;
			parameters[9].Value = model.addConsArea;
			parameters[10].Value = model.consArea;
			parameters[11].Value = model.agriArea;
			parameters[12].Value = model.arabArea;
			parameters[13].Value = model.unusedArea;
			parameters[14].Value = model.hasLevyArea;
			parameters[15].Value = model.userId;
			parameters[16].Value = model.createTime;
			parameters[17].Value = model.isSubmited;
			parameters[18].Value = model.isDeleted;
			parameters[19].Value = model.des;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Maticsoft.Model.T_Batch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Batch set ");
			strSql.Append("name=@name,");
			strSql.Append("subName=@subName,");
			strSql.Append("year=@year,");
			strSql.Append("administratorArea=@administratorArea,");
			strSql.Append("approvalAuthority=@approvalAuthority,");
			strSql.Append("approvalNo=@approvalNo,");
			strSql.Append("approvalTime=@approvalTime,");
			strSql.Append("batchTypeId=@batchTypeId,");
			strSql.Append("totalArea=@totalArea,");
			strSql.Append("addConsArea=@addConsArea,");
			strSql.Append("consArea=@consArea,");
			strSql.Append("agriArea=@agriArea,");
			strSql.Append("arabArea=@arabArea,");
			strSql.Append("unusedArea=@unusedArea,");
			strSql.Append("hasLevyArea=@hasLevyArea,");
			strSql.Append("userId=@userId,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("isSubmited=@isSubmited,");
			strSql.Append("isDeleted=@isDeleted,");
			strSql.Append("des=@des");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@subName", SqlDbType.VarChar,50),
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@approvalAuthority", SqlDbType.VarChar,50),
					new SqlParameter("@approvalNo", SqlDbType.VarChar,50),
					new SqlParameter("@approvalTime", SqlDbType.Date,3),
					new SqlParameter("@batchTypeId", SqlDbType.Int,4),
					new SqlParameter("@totalArea", SqlDbType.Decimal,9),
					new SqlParameter("@addConsArea", SqlDbType.Decimal,9),
					new SqlParameter("@consArea", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@unusedArea", SqlDbType.Decimal,9),
					new SqlParameter("@hasLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.name;
			parameters[1].Value = model.subName;
			parameters[2].Value = model.year;
			parameters[3].Value = model.administratorArea;
			parameters[4].Value = model.approvalAuthority;
			parameters[5].Value = model.approvalNo;
			parameters[6].Value = model.approvalTime;
			parameters[7].Value = model.batchTypeId;
			parameters[8].Value = model.totalArea;
			parameters[9].Value = model.addConsArea;
			parameters[10].Value = model.consArea;
			parameters[11].Value = model.agriArea;
			parameters[12].Value = model.arabArea;
			parameters[13].Value = model.unusedArea;
			parameters[14].Value = model.hasLevyArea;
			parameters[15].Value = model.userId;
			parameters[16].Value = model.createTime;
			parameters[17].Value = model.isSubmited;
			parameters[18].Value = model.isDeleted;
			parameters[19].Value = model.des;
			parameters[20].Value = model.id;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Batch ");
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
			strSql.Append("delete from T_Batch ");
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
		public Maticsoft.Model.T_Batch GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,name,subName,year,administratorArea,approvalAuthority,approvalNo,approvalTime,batchTypeId,totalArea,addConsArea,consArea,agriArea,arabArea,unusedArea,hasLevyArea,userId,createTime,isSubmited,isDeleted,des from T_Batch ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_Batch model=new Maticsoft.Model.T_Batch();
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
		public Maticsoft.Model.T_Batch DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_Batch model=new Maticsoft.Model.T_Batch();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["subName"]!=null)
				{
					model.subName=row["subName"].ToString();
				}
				if(row["year"]!=null)
				{
					model.year=row["year"].ToString();
				}
				if(row["administratorArea"]!=null)
				{
					model.administratorArea=row["administratorArea"].ToString();
				}
				if(row["approvalAuthority"]!=null)
				{
					model.approvalAuthority=row["approvalAuthority"].ToString();
				}
				if(row["approvalNo"]!=null)
				{
					model.approvalNo=row["approvalNo"].ToString();
				}
				if(row["approvalTime"]!=null && row["approvalTime"].ToString()!="")
				{
					model.approvalTime=DateTime.Parse(row["approvalTime"].ToString());
				}
				if(row["batchTypeId"]!=null && row["batchTypeId"].ToString()!="")
				{
					model.batchTypeId=int.Parse(row["batchTypeId"].ToString());
				}
				if(row["totalArea"]!=null && row["totalArea"].ToString()!="")
				{
					model.totalArea=decimal.Parse(row["totalArea"].ToString());
				}
				if(row["addConsArea"]!=null && row["addConsArea"].ToString()!="")
				{
					model.addConsArea=decimal.Parse(row["addConsArea"].ToString());
				}
				if(row["consArea"]!=null && row["consArea"].ToString()!="")
				{
					model.consArea=decimal.Parse(row["consArea"].ToString());
				}
				if(row["agriArea"]!=null && row["agriArea"].ToString()!="")
				{
					model.agriArea=decimal.Parse(row["agriArea"].ToString());
				}
				if(row["arabArea"]!=null && row["arabArea"].ToString()!="")
				{
					model.arabArea=decimal.Parse(row["arabArea"].ToString());
				}
				if(row["unusedArea"]!=null && row["unusedArea"].ToString()!="")
				{
					model.unusedArea=decimal.Parse(row["unusedArea"].ToString());
				}
				if(row["hasLevyArea"]!=null && row["hasLevyArea"].ToString()!="")
				{
					model.hasLevyArea=decimal.Parse(row["hasLevyArea"].ToString());
				}
				if(row["userId"]!=null && row["userId"].ToString()!="")
				{
					model.userId=int.Parse(row["userId"].ToString());
				}
				if(row["createTime"]!=null && row["createTime"].ToString()!="")
				{
					model.createTime=DateTime.Parse(row["createTime"].ToString());
				}
				if(row["isSubmited"]!=null && row["isSubmited"].ToString()!="")
				{
					model.isSubmited=int.Parse(row["isSubmited"].ToString());
				}
				if(row["isDeleted"]!=null && row["isDeleted"].ToString()!="")
				{
					model.isDeleted=int.Parse(row["isDeleted"].ToString());
				}
				if(row["des"]!=null)
				{
					model.des=row["des"].ToString();
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
			strSql.Append("select id,name,subName,year,administratorArea,approvalAuthority,approvalNo,approvalTime,batchTypeId,totalArea,addConsArea,consArea,agriArea,arabArea,unusedArea,hasLevyArea,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_Batch ");
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
			strSql.Append(" id,name,subName,year,administratorArea,approvalAuthority,approvalNo,approvalTime,batchTypeId,totalArea,addConsArea,consArea,agriArea,arabArea,unusedArea,hasLevyArea,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_Batch ");
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
			strSql.Append("select count(1) FROM T_Batch ");
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
			strSql.Append(")AS Row, T.*  from T_Batch T ");
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
			parameters[0].Value = "T_Batch";
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
	}
}

