using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Batch_Demand_Supply_Balance
	/// </summary>
	public partial class T_Batch_Demand_Supply_Balance
	{
		public T_Batch_Demand_Supply_Balance()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_Batch_Demand_Supply_Balance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Batch_Demand_Supply_Balance(");
			strSql.Append("batchId,dsBalanceId,consArea,agriArea,arabArea,createTime,isSubmited,isDeleted)");
			strSql.Append(" values (");
			strSql.Append("@batchId,@dsBalanceId,@consArea,@agriArea,@arabArea,@createTime,@isSubmited,@isDeleted)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@dsBalanceId", SqlDbType.Int,4),
					new SqlParameter("@consArea", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4)};
			parameters[0].Value = model.batchId;
			parameters[1].Value = model.dsBalanceId;
			parameters[2].Value = model.consArea;
			parameters[3].Value = model.agriArea;
			parameters[4].Value = model.arabArea;
			parameters[5].Value = model.createTime;
			parameters[6].Value = model.isSubmited;
			parameters[7].Value = model.isDeleted;

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
		public bool Update(Maticsoft.Model.T_Batch_Demand_Supply_Balance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Batch_Demand_Supply_Balance set ");
			strSql.Append("batchId=@batchId,");
			strSql.Append("dsBalanceId=@dsBalanceId,");
			strSql.Append("consArea=@consArea,");
			strSql.Append("agriArea=@agriArea,");
			strSql.Append("arabArea=@arabArea,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("isSubmited=@isSubmited,");
			strSql.Append("isDeleted=@isDeleted");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@dsBalanceId", SqlDbType.Int,4),
					new SqlParameter("@consArea", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.batchId;
			parameters[1].Value = model.dsBalanceId;
			parameters[2].Value = model.consArea;
			parameters[3].Value = model.agriArea;
			parameters[4].Value = model.arabArea;
			parameters[5].Value = model.createTime;
			parameters[6].Value = model.isSubmited;
			parameters[7].Value = model.isDeleted;
			parameters[8].Value = model.id;

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
			strSql.Append("delete from T_Batch_Demand_Supply_Balance ");
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
			strSql.Append("delete from T_Batch_Demand_Supply_Balance ");
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
		public Maticsoft.Model.T_Batch_Demand_Supply_Balance GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,batchId,dsBalanceId,consArea,agriArea,arabArea,createTime,isSubmited,isDeleted from T_Batch_Demand_Supply_Balance ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_Batch_Demand_Supply_Balance model=new Maticsoft.Model.T_Batch_Demand_Supply_Balance();
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
		public Maticsoft.Model.T_Batch_Demand_Supply_Balance DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_Batch_Demand_Supply_Balance model=new Maticsoft.Model.T_Batch_Demand_Supply_Balance();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["batchId"]!=null && row["batchId"].ToString()!="")
				{
					model.batchId=int.Parse(row["batchId"].ToString());
				}
				if(row["dsBalanceId"]!=null && row["dsBalanceId"].ToString()!="")
				{
					model.dsBalanceId=int.Parse(row["dsBalanceId"].ToString());
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
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select T.*,D.name as balanceName,B.name as batchName,D.typeId  from T_Batch_Demand_Supply_Balance T,T_Batch B,T_DemandSupplyBalance D where  1=1 and T.batchId=B.id and T.dsBalanceId = D.id ");
			if(strWhere.Trim()!="")
			{
                strSql.Append(" and " + strWhere);
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
            strSql.Append(" T.*,D.name as balanceName,B.name as batchName,D.typeId  from T_Batch_Demand_Supply_Balance T,T_Batch B,T_DemandSupplyBalance D  where  1=1 and T.batchId=B.id and T.dsBalanceId = D.id ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" and "+strWhere);
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
            strSql.Append("select count(1) from T_Batch_Demand_Supply_Balance T,T_Batch B,T_DemandSupplyBalance D where  1=1 and T.batchId=B.id and T.dsBalanceId = D.id ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" and "+strWhere);
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
            strSql.Append(")AS Row, T.*,B.name as batchName,B.batchTypeId,B.totalArea,B.consArea as consAreaBatch,B.agriArea as agriAreaBatch,B.arabArea as arabAreaBatch,B.hasLevyArea,B.approvalNo,B.approvalTime ,D.name as balanceName,D.typeId,D.administratorArea  from T_Batch_Demand_Supply_Balance T,T_Batch B,T_DemandSupplyBalance D where  1=1 and T.batchId=B.id and T.dsBalanceId = D.id");
            //strSql.Append(")AS Row, T.* ,P.name as planName,B.name as batchName  from T_Batch_Plan T,T_Batch B,T_Plan P WHERE  1=1 and T.batchId=B.id and T.planId = P.id");
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
			parameters[0].Value = "T_Batch_Demand_Supply_Balance";
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

