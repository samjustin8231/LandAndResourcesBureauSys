using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_FarmlandAdjustment
	/// </summary>
	public partial class T_FarmlandAdjustment
	{
		public T_FarmlandAdjustment()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_FarmlandAdjustment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_FarmlandAdjustment(");
			strSql.Append("year,administratorArea,verifId,adjustmentReason,approvalArticleNo,approvalTime,startFarmlandArea,endFarmlandArea,startFarmlandArableLandArea,endFarmlandArableLandArea,taskArea,adjustBeforeArea,adjustBeforeArableArea,adjustAfterArea,adjustAfterArableArea,adjustInArea,adjustInArableArea,adjustOutArea,adjustOutArableArea,isYearStart,userId,createTime,isSubmited,isDeleted,des)");
			strSql.Append(" values (");
			strSql.Append("@year,@administratorArea,@verifId,@adjustmentReason,@approvalArticleNo,@approvalTime,@startFarmlandArea,@endFarmlandArea,@startFarmlandArableLandArea,@endFarmlandArableLandArea,@taskArea,@adjustBeforeArea,@adjustBeforeArableArea,@adjustAfterArea,@adjustAfterArableArea,@adjustInArea,@adjustInArableArea,@adjustOutArea,@adjustOutArableArea,@isYearStart,@userId,@createTime,@isSubmited,@isDeleted,@des)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,10),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,20),
					new SqlParameter("@verifId", SqlDbType.Int,4),
					new SqlParameter("@adjustmentReason", SqlDbType.VarChar,200),
					new SqlParameter("@approvalArticleNo", SqlDbType.VarChar,200),
					new SqlParameter("@approvalTime", SqlDbType.Date,3),
					new SqlParameter("@startFarmlandArea", SqlDbType.Decimal,9),
					new SqlParameter("@endFarmlandArea", SqlDbType.Decimal,9),
					new SqlParameter("@startFarmlandArableLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@endFarmlandArableLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@taskArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustBeforeArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustBeforeArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustAfterArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustAfterArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustInArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustInArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustOutArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustOutArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@isYearStart", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.verifId;
			parameters[3].Value = model.adjustmentReason;
			parameters[4].Value = model.approvalArticleNo;
			parameters[5].Value = model.approvalTime;
			parameters[6].Value = model.startFarmlandArea;
			parameters[7].Value = model.endFarmlandArea;
			parameters[8].Value = model.startFarmlandArableLandArea;
			parameters[9].Value = model.endFarmlandArableLandArea;
			parameters[10].Value = model.taskArea;
			parameters[11].Value = model.adjustBeforeArea;
			parameters[12].Value = model.adjustBeforeArableArea;
			parameters[13].Value = model.adjustAfterArea;
			parameters[14].Value = model.adjustAfterArableArea;
			parameters[15].Value = model.adjustInArea;
			parameters[16].Value = model.adjustInArableArea;
			parameters[17].Value = model.adjustOutArea;
			parameters[18].Value = model.adjustOutArableArea;
			parameters[19].Value = model.isYearStart;
			parameters[20].Value = model.userId;
			parameters[21].Value = model.createTime;
			parameters[22].Value = model.isSubmited;
			parameters[23].Value = model.isDeleted;
			parameters[24].Value = model.des;

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
		public bool Update(Maticsoft.Model.T_FarmlandAdjustment model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_FarmlandAdjustment set ");
			strSql.Append("year=@year,");
			strSql.Append("administratorArea=@administratorArea,");
			strSql.Append("verifId=@verifId,");
			strSql.Append("adjustmentReason=@adjustmentReason,");
			strSql.Append("approvalArticleNo=@approvalArticleNo,");
			strSql.Append("approvalTime=@approvalTime,");
			strSql.Append("startFarmlandArea=@startFarmlandArea,");
			strSql.Append("endFarmlandArea=@endFarmlandArea,");
			strSql.Append("startFarmlandArableLandArea=@startFarmlandArableLandArea,");
			strSql.Append("endFarmlandArableLandArea=@endFarmlandArableLandArea,");
			strSql.Append("taskArea=@taskArea,");
			strSql.Append("adjustBeforeArea=@adjustBeforeArea,");
			strSql.Append("adjustBeforeArableArea=@adjustBeforeArableArea,");
			strSql.Append("adjustAfterArea=@adjustAfterArea,");
			strSql.Append("adjustAfterArableArea=@adjustAfterArableArea,");
			strSql.Append("adjustInArea=@adjustInArea,");
			strSql.Append("adjustInArableArea=@adjustInArableArea,");
			strSql.Append("adjustOutArea=@adjustOutArea,");
			strSql.Append("adjustOutArableArea=@adjustOutArableArea,");
			strSql.Append("isYearStart=@isYearStart,");
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
					new SqlParameter("@adjustmentReason", SqlDbType.VarChar,200),
					new SqlParameter("@approvalArticleNo", SqlDbType.VarChar,200),
					new SqlParameter("@approvalTime", SqlDbType.Date,3),
					new SqlParameter("@startFarmlandArea", SqlDbType.Decimal,9),
					new SqlParameter("@endFarmlandArea", SqlDbType.Decimal,9),
					new SqlParameter("@startFarmlandArableLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@endFarmlandArableLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@taskArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustBeforeArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustBeforeArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustAfterArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustAfterArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustInArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustInArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustOutArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustOutArableArea", SqlDbType.Decimal,9),
					new SqlParameter("@isYearStart", SqlDbType.Int,4),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.verifId;
			parameters[3].Value = model.adjustmentReason;
			parameters[4].Value = model.approvalArticleNo;
			parameters[5].Value = model.approvalTime;
			parameters[6].Value = model.startFarmlandArea;
			parameters[7].Value = model.endFarmlandArea;
			parameters[8].Value = model.startFarmlandArableLandArea;
			parameters[9].Value = model.endFarmlandArableLandArea;
			parameters[10].Value = model.taskArea;
			parameters[11].Value = model.adjustBeforeArea;
			parameters[12].Value = model.adjustBeforeArableArea;
			parameters[13].Value = model.adjustAfterArea;
			parameters[14].Value = model.adjustAfterArableArea;
			parameters[15].Value = model.adjustInArea;
			parameters[16].Value = model.adjustInArableArea;
			parameters[17].Value = model.adjustOutArea;
			parameters[18].Value = model.adjustOutArableArea;
			parameters[19].Value = model.isYearStart;
			parameters[20].Value = model.userId;
			parameters[21].Value = model.createTime;
			parameters[22].Value = model.isSubmited;
			parameters[23].Value = model.isDeleted;
			parameters[24].Value = model.des;
			parameters[25].Value = model.id;

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
			strSql.Append("delete from T_FarmlandAdjustment ");
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
			strSql.Append("delete from T_FarmlandAdjustment ");
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
		public Maticsoft.Model.T_FarmlandAdjustment GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,year,administratorArea,verifId,adjustmentReason,approvalArticleNo,approvalTime,startFarmlandArea,endFarmlandArea,startFarmlandArableLandArea,endFarmlandArableLandArea,taskArea,adjustBeforeArea,adjustBeforeArableArea,adjustAfterArea,adjustAfterArableArea,adjustInArea,adjustInArableArea,adjustOutArea,adjustOutArableArea,isYearStart,userId,createTime,isSubmited,isDeleted,des from T_FarmlandAdjustment ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_FarmlandAdjustment model=new Maticsoft.Model.T_FarmlandAdjustment();
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
		public Maticsoft.Model.T_FarmlandAdjustment DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_FarmlandAdjustment model=new Maticsoft.Model.T_FarmlandAdjustment();
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
				if(row["adjustmentReason"]!=null)
				{
					model.adjustmentReason=row["adjustmentReason"].ToString();
				}
				if(row["approvalArticleNo"]!=null)
				{
					model.approvalArticleNo=row["approvalArticleNo"].ToString();
				}
				if(row["approvalTime"]!=null && row["approvalTime"].ToString()!="")
				{
					model.approvalTime=DateTime.Parse(row["approvalTime"].ToString());
				}
				if(row["startFarmlandArea"]!=null && row["startFarmlandArea"].ToString()!="")
				{
					model.startFarmlandArea=decimal.Parse(row["startFarmlandArea"].ToString());
				}
				if(row["endFarmlandArea"]!=null && row["endFarmlandArea"].ToString()!="")
				{
					model.endFarmlandArea=decimal.Parse(row["endFarmlandArea"].ToString());
				}
				if(row["startFarmlandArableLandArea"]!=null && row["startFarmlandArableLandArea"].ToString()!="")
				{
					model.startFarmlandArableLandArea=decimal.Parse(row["startFarmlandArableLandArea"].ToString());
				}
				if(row["endFarmlandArableLandArea"]!=null && row["endFarmlandArableLandArea"].ToString()!="")
				{
					model.endFarmlandArableLandArea=decimal.Parse(row["endFarmlandArableLandArea"].ToString());
				}
				if(row["taskArea"]!=null && row["taskArea"].ToString()!="")
				{
					model.taskArea=decimal.Parse(row["taskArea"].ToString());
				}
				if(row["adjustBeforeArea"]!=null && row["adjustBeforeArea"].ToString()!="")
				{
					model.adjustBeforeArea=decimal.Parse(row["adjustBeforeArea"].ToString());
				}
				if(row["adjustBeforeArableArea"]!=null && row["adjustBeforeArableArea"].ToString()!="")
				{
					model.adjustBeforeArableArea=decimal.Parse(row["adjustBeforeArableArea"].ToString());
				}
				if(row["adjustAfterArea"]!=null && row["adjustAfterArea"].ToString()!="")
				{
					model.adjustAfterArea=decimal.Parse(row["adjustAfterArea"].ToString());
				}
				if(row["adjustAfterArableArea"]!=null && row["adjustAfterArableArea"].ToString()!="")
				{
					model.adjustAfterArableArea=decimal.Parse(row["adjustAfterArableArea"].ToString());
				}
				if(row["adjustInArea"]!=null && row["adjustInArea"].ToString()!="")
				{
					model.adjustInArea=decimal.Parse(row["adjustInArea"].ToString());
				}
				if(row["adjustInArableArea"]!=null && row["adjustInArableArea"].ToString()!="")
				{
					model.adjustInArableArea=decimal.Parse(row["adjustInArableArea"].ToString());
				}
				if(row["adjustOutArea"]!=null && row["adjustOutArea"].ToString()!="")
				{
					model.adjustOutArea=decimal.Parse(row["adjustOutArea"].ToString());
				}
				if(row["adjustOutArableArea"]!=null && row["adjustOutArableArea"].ToString()!="")
				{
					model.adjustOutArableArea=decimal.Parse(row["adjustOutArableArea"].ToString());
				}
				if(row["isYearStart"]!=null && row["isYearStart"].ToString()!="")
				{
					model.isYearStart=int.Parse(row["isYearStart"].ToString());
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
			strSql.Append("select id,year,administratorArea,verifId,adjustmentReason,approvalArticleNo,approvalTime,startFarmlandArea,endFarmlandArea,startFarmlandArableLandArea,endFarmlandArableLandArea,taskArea,adjustBeforeArea,adjustBeforeArableArea,adjustAfterArea,adjustAfterArableArea,adjustInArea,adjustInArableArea,adjustOutArea,adjustOutArableArea,isYearStart,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_FarmlandAdjustment ");
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
			strSql.Append(" id,year,administratorArea,verifId,adjustmentReason,approvalArticleNo,approvalTime,startFarmlandArea,endFarmlandArea,startFarmlandArableLandArea,endFarmlandArableLandArea,taskArea,adjustBeforeArea,adjustBeforeArableArea,adjustAfterArea,adjustAfterArableArea,adjustInArea,adjustInArableArea,adjustOutArea,adjustOutArableArea,isYearStart,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_FarmlandAdjustment ");
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
			strSql.Append("select count(1) FROM T_FarmlandAdjustment ");
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
			strSql.Append(")AS Row, T.*  from T_FarmlandAdjustment T ");
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
			parameters[0].Value = "T_FarmlandAdjustment";
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

