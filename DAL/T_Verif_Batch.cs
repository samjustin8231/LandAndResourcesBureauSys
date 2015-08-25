﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Verif_Batch
	/// </summary>
	public partial class T_Verif_Batch
	{
		public T_Verif_Batch()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_Verif_Batch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Verif_Batch(");
			strSql.Append("year,administratorArea,verifId,batchId,verifProvArea,verifProvArableArea,verifSelfArea,verifSelfArableArea,userId,createTime,isSubmited,isDeleted,des)");
			strSql.Append(" values (");
			strSql.Append("@year,@administratorArea,@verifId,@batchId,@verifProvArea,@verifProvArableArea,@verifSelfArea,@verifSelfArableArea,@userId,@createTime,@isSubmited,@isDeleted,@des)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,10),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,20),
					new SqlParameter("@verifId", SqlDbType.Int,4),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@verifProvArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifProvArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifSelfArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifSelfArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.verifId;
			parameters[3].Value = model.batchId;
			parameters[4].Value = model.verifProvArea;
			parameters[5].Value = model.verifProvArableArea;
			parameters[6].Value = model.verifSelfArea;
			parameters[7].Value = model.verifSelfArableArea;
			parameters[8].Value = model.userId;
			parameters[9].Value = model.createTime;
			parameters[10].Value = model.isSubmited;
			parameters[11].Value = model.isDeleted;
			parameters[12].Value = model.des;

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
		public bool Update(Maticsoft.Model.T_Verif_Batch model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Verif_Batch set ");
			strSql.Append("year=@year,");
			strSql.Append("administratorArea=@administratorArea,");
			strSql.Append("verifId=@verifId,");
			strSql.Append("batchId=@batchId,");
			strSql.Append("verifProvArea=@verifProvArea,");
			strSql.Append("verifProvArableArea=@verifProvArableArea,");
			strSql.Append("verifSelfArea=@verifSelfArea,");
			strSql.Append("verifSelfArableArea=@verifSelfArableArea,");
			strSql.Append("userId=@userId,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("isSubmited=@isSubmited,");
			strSql.Append("isDeleted=@isDeleted,");
			strSql.Append("des=@des");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,10),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,20),
					new SqlParameter("@verifId", SqlDbType.Int,4),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@verifProvArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifProvArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifSelfArea", SqlDbType.Decimal,9),
					new SqlParameter("@verifSelfArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.verifId;
			parameters[3].Value = model.batchId;
			parameters[4].Value = model.verifProvArea;
			parameters[5].Value = model.verifProvArableArea;
			parameters[6].Value = model.verifSelfArea;
			parameters[7].Value = model.verifSelfArableArea;
			parameters[8].Value = model.userId;
			parameters[9].Value = model.createTime;
			parameters[10].Value = model.isSubmited;
			parameters[11].Value = model.isDeleted;
			parameters[12].Value = model.des;
			parameters[13].Value = model.id;

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
			strSql.Append("delete from T_Verif_Batch ");
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
			strSql.Append("delete from T_Verif_Batch ");
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
		public Maticsoft.Model.T_Verif_Batch GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,year,administratorArea,verifId,batchId,verifProvArea,verifProvArableArea,verifSelfArea,verifSelfArableArea,userId,createTime,isSubmited,isDeleted,des from T_Verif_Batch ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_Verif_Batch model=new Maticsoft.Model.T_Verif_Batch();
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
		public Maticsoft.Model.T_Verif_Batch DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_Verif_Batch model=new Maticsoft.Model.T_Verif_Batch();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["year"]!=null)
				{
					model.year=row["year"].ToString();
				}
				if(row["administratorArea"]!=null)
				{
					model.administratorArea=row["administratorArea"].ToString();
				}
				if(row["verifId"]!=null && row["verifId"].ToString()!="")
				{
					model.verifId=int.Parse(row["verifId"].ToString());
				}
				if(row["batchId"]!=null && row["batchId"].ToString()!="")
				{
					model.batchId=int.Parse(row["batchId"].ToString());
				}
				if(row["verifProvArea"]!=null && row["verifProvArea"].ToString()!="")
				{
					model.verifProvArea=decimal.Parse(row["verifProvArea"].ToString());
				}
				if(row["verifProvArableArea"]!=null && row["verifProvArableArea"].ToString()!="")
				{
					model.verifProvArableArea=decimal.Parse(row["verifProvArableArea"].ToString());
				}
				if(row["verifSelfArea"]!=null && row["verifSelfArea"].ToString()!="")
				{
					model.verifSelfArea=decimal.Parse(row["verifSelfArea"].ToString());
				}
				if(row["verifSelfArableArea"]!=null && row["verifSelfArableArea"].ToString()!="")
				{
					model.verifSelfArableArea=decimal.Parse(row["verifSelfArableArea"].ToString());
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
			strSql.Append("select id,year,administratorArea,verifId,batchId,verifProvArea,verifProvArableArea,verifSelfArea,verifSelfArableArea,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_Verif_Batch ");
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
			strSql.Append(" id,year,administratorArea,verifId,batchId,verifProvArea,verifProvArableArea,verifSelfArea,verifSelfArableArea,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_Verif_Batch ");
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
            strSql.Append("select count(1)  from T_Verif_Batch T,T_Batch B,T_Verif V WHERE  1=1 and T.batchId=B.id and T.verifId = V.id");
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
            //strSql.Append(")AS Row, T.*,V.divisionArea  from T_Verif_Batch T,T_Verif V where T.verifId = V.id ");
            strSql.Append(")AS Row, T.*,B.name as batchName,B.batchTypeId,B.totalArea,B.consArea as consAreaBatch,B.agriArea as agriAreaBatch,B.arabArea as arabAreaBatch,B.hasLevyArea,B.approvalNo,B.approvalTime from T_Verif_Batch T,T_Batch B,T_Verif V WHERE  1=1 and T.batchId=B.id and T.verifId = V.id");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" and " + strWhere);
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
			parameters[0].Value = "T_Verif_Batch";
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

