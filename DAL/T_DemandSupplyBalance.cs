using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_DemandSupplyBalance
	/// </summary>
	public partial class T_DemandSupplyBalance
	{
		public T_DemandSupplyBalance()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_DemandSupplyBalance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_DemandSupplyBalance(");
			strSql.Append("no,name,year,administratorArea,typeId,acceptUnit,acceptNo,acceptTime,position,scale,agriArea,arabArea,adjustArea,occupyArea,remainArea,userId,createTime,isSubmited,isDeleted,batchId,des)");
			strSql.Append(" values (");
			strSql.Append("@no,@name,@year,@administratorArea,@typeId,@acceptUnit,@acceptNo,@acceptTime,@position,@scale,@agriArea,@arabArea,@adjustArea,@occupyArea,@remainArea,@userId,@createTime,@isSubmited,@isDeleted,@batchId,@des)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@no", SqlDbType.VarChar,50),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@acceptUnit", SqlDbType.VarChar,50),
					new SqlParameter("@acceptNo", SqlDbType.VarChar,50),
					new SqlParameter("@acceptTime", SqlDbType.DateTime),
					new SqlParameter("@position", SqlDbType.VarChar,50),
					new SqlParameter("@scale", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustArea", SqlDbType.Decimal,9),
					new SqlParameter("@occupyArea", SqlDbType.Decimal,9),
					new SqlParameter("@remainArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text)};
			parameters[0].Value = model.no;
			parameters[1].Value = model.name;
			parameters[2].Value = model.year;
			parameters[3].Value = model.administratorArea;
			parameters[4].Value = model.typeId;
			parameters[5].Value = model.acceptUnit;
			parameters[6].Value = model.acceptNo;
			parameters[7].Value = model.acceptTime;
			parameters[8].Value = model.position;
			parameters[9].Value = model.scale;
			parameters[10].Value = model.agriArea;
			parameters[11].Value = model.arabArea;
			parameters[12].Value = model.adjustArea;
			parameters[13].Value = model.occupyArea;
			parameters[14].Value = model.remainArea;
			parameters[15].Value = model.userId;
			parameters[16].Value = model.createTime;
			parameters[17].Value = model.isSubmited;
			parameters[18].Value = model.isDeleted;
			parameters[19].Value = model.batchId;
			parameters[20].Value = model.des;

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
		public bool Update(Maticsoft.Model.T_DemandSupplyBalance model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_DemandSupplyBalance set ");
			strSql.Append("no=@no,");
			strSql.Append("name=@name,");
			strSql.Append("year=@year,");
			strSql.Append("administratorArea=@administratorArea,");
			strSql.Append("typeId=@typeId,");
			strSql.Append("acceptUnit=@acceptUnit,");
			strSql.Append("acceptNo=@acceptNo,");
			strSql.Append("acceptTime=@acceptTime,");
			strSql.Append("position=@position,");
			strSql.Append("scale=@scale,");
			strSql.Append("agriArea=@agriArea,");
			strSql.Append("arabArea=@arabArea,");
			strSql.Append("adjustArea=@adjustArea,");
			strSql.Append("occupyArea=@occupyArea,");
			strSql.Append("remainArea=@remainArea,");
			strSql.Append("userId=@userId,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("isSubmited=@isSubmited,");
			strSql.Append("isDeleted=@isDeleted,");
			strSql.Append("batchId=@batchId,");
			strSql.Append("des=@des");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@no", SqlDbType.VarChar,50),
					new SqlParameter("@name", SqlDbType.VarChar,50),
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@acceptUnit", SqlDbType.VarChar,50),
					new SqlParameter("@acceptNo", SqlDbType.VarChar,50),
					new SqlParameter("@acceptTime", SqlDbType.DateTime),
					new SqlParameter("@position", SqlDbType.VarChar,50),
					new SqlParameter("@scale", SqlDbType.Decimal,9),
					new SqlParameter("@agriArea", SqlDbType.Decimal,9),
					new SqlParameter("@arabArea", SqlDbType.Decimal,9),
					new SqlParameter("@adjustArea", SqlDbType.Decimal,9),
					new SqlParameter("@occupyArea", SqlDbType.Decimal,9),
					new SqlParameter("@remainArea", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.no;
			parameters[1].Value = model.name;
			parameters[2].Value = model.year;
			parameters[3].Value = model.administratorArea;
			parameters[4].Value = model.typeId;
			parameters[5].Value = model.acceptUnit;
			parameters[6].Value = model.acceptNo;
			parameters[7].Value = model.acceptTime;
			parameters[8].Value = model.position;
			parameters[9].Value = model.scale;
			parameters[10].Value = model.agriArea;
			parameters[11].Value = model.arabArea;
			parameters[12].Value = model.adjustArea;
			parameters[13].Value = model.occupyArea;
			parameters[14].Value = model.remainArea;
			parameters[15].Value = model.userId;
			parameters[16].Value = model.createTime;
			parameters[17].Value = model.isSubmited;
			parameters[18].Value = model.isDeleted;
			parameters[19].Value = model.batchId;
			parameters[20].Value = model.des;
			parameters[21].Value = model.id;

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
			strSql.Append("delete from T_DemandSupplyBalance ");
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
			strSql.Append("delete from T_DemandSupplyBalance ");
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
		public Maticsoft.Model.T_DemandSupplyBalance GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,no,name,year,administratorArea,typeId,acceptUnit,acceptNo,acceptTime,position,scale,agriArea,arabArea,adjustArea,occupyArea,remainArea,userId,createTime,isSubmited,isDeleted,batchId,des from T_DemandSupplyBalance ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_DemandSupplyBalance model=new Maticsoft.Model.T_DemandSupplyBalance();
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
		public Maticsoft.Model.T_DemandSupplyBalance DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_DemandSupplyBalance model=new Maticsoft.Model.T_DemandSupplyBalance();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["no"]!=null)
				{
					model.no=row["no"].ToString();
				}
				if(row["name"]!=null)
				{
					model.name=row["name"].ToString();
				}
				if(row["year"]!=null)
				{
					model.year=row["year"].ToString();
				}
				if(row["administratorArea"]!=null)
				{
					model.administratorArea=row["administratorArea"].ToString();
				}
				if(row["typeId"]!=null && row["typeId"].ToString()!="")
				{
					model.typeId=int.Parse(row["typeId"].ToString());
				}
				if(row["acceptUnit"]!=null)
				{
					model.acceptUnit=row["acceptUnit"].ToString();
				}
				if(row["acceptNo"]!=null)
				{
					model.acceptNo=row["acceptNo"].ToString();
				}
				if(row["acceptTime"]!=null && row["acceptTime"].ToString()!="")
				{
					model.acceptTime=DateTime.Parse(row["acceptTime"].ToString());
				}
				if(row["position"]!=null)
				{
					model.position=row["position"].ToString();
				}
				if(row["scale"]!=null && row["scale"].ToString()!="")
				{
					model.scale=decimal.Parse(row["scale"].ToString());
				}
				if(row["agriArea"]!=null && row["agriArea"].ToString()!="")
				{
					model.agriArea=decimal.Parse(row["agriArea"].ToString());
				}
				if(row["arabArea"]!=null && row["arabArea"].ToString()!="")
				{
					model.arabArea=decimal.Parse(row["arabArea"].ToString());
				}
				if(row["adjustArea"]!=null && row["adjustArea"].ToString()!="")
				{
					model.adjustArea=decimal.Parse(row["adjustArea"].ToString());
				}
				if(row["occupyArea"]!=null && row["occupyArea"].ToString()!="")
				{
					model.occupyArea=decimal.Parse(row["occupyArea"].ToString());
				}
				if(row["remainArea"]!=null && row["remainArea"].ToString()!="")
				{
					model.remainArea=decimal.Parse(row["remainArea"].ToString());
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
				if(row["batchId"]!=null && row["batchId"].ToString()!="")
				{
					model.batchId=int.Parse(row["batchId"].ToString());
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
			strSql.Append("select id,no,name,year,administratorArea,typeId,acceptUnit,acceptNo,acceptTime,position,scale,agriArea,arabArea,adjustArea,occupyArea,remainArea,userId,createTime,isSubmited,isDeleted,batchId,des ");
			strSql.Append(" FROM T_DemandSupplyBalance ");
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
			strSql.Append(" id,no,name,year,administratorArea,typeId,acceptUnit,acceptNo,acceptTime,position,scale,agriArea,arabArea,adjustArea,occupyArea,remainArea,userId,createTime,isSubmited,isDeleted,batchId,des ");
			strSql.Append(" FROM T_DemandSupplyBalance ");
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
			strSql.Append("select count(1) FROM T_DemandSupplyBalance ");
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
			strSql.Append(")AS Row, T.*  from T_DemandSupplyBalance T ");
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
			parameters[0].Value = "T_DemandSupplyBalance";
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

