using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_LevyCompensate
	/// </summary>
	public partial class T_LevyCompensate
	{
		public T_LevyCompensate()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_LevyCompensate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_LevyCompensate(");
			strSql.Append("year,administratorArea,batchId,landblockId,batchTypeId,administrativeArea,town,village,_group,totalPeopleNumber,planLevyArea,hasLevyArea,levyNationalLandArea,levyColletLandArea,levyPreDeposit,countrySocialSecurityFund,countryCompensate,areaWaterFacilitiesCompensate,provHeavyAgriculturalFunds,provAdditionalFee,provReclamationFee,provArableLandTax,provSurveyFee,provServiceFee,userId,createTime,isSubmited,isDeleted,des)");
			strSql.Append(" values (");
			strSql.Append("@year,@administratorArea,@batchId,@landblockId,@batchTypeId,@administrativeArea,@town,@village,@_group,@totalPeopleNumber,@planLevyArea,@hasLevyArea,@levyNationalLandArea,@levyColletLandArea,@levyPreDeposit,@countrySocialSecurityFund,@countryCompensate,@areaWaterFacilitiesCompensate,@provHeavyAgriculturalFunds,@provAdditionalFee,@provReclamationFee,@provArableLandTax,@provSurveyFee,@provServiceFee,@userId,@createTime,@isSubmited,@isDeleted,@des)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@landblockId", SqlDbType.Int,4),
					new SqlParameter("@batchTypeId", SqlDbType.Int,4),
					new SqlParameter("@administrativeArea", SqlDbType.VarChar,20),
					new SqlParameter("@town", SqlDbType.VarChar,40),
					new SqlParameter("@village", SqlDbType.VarChar,40),
					new SqlParameter("@_group", SqlDbType.VarChar,40),
					new SqlParameter("@totalPeopleNumber", SqlDbType.Decimal,9),
					new SqlParameter("@planLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@hasLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyNationalLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyColletLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyPreDeposit", SqlDbType.Decimal,9),
					new SqlParameter("@countrySocialSecurityFund", SqlDbType.Decimal,9),
					new SqlParameter("@countryCompensate", SqlDbType.Decimal,9),
					new SqlParameter("@areaWaterFacilitiesCompensate", SqlDbType.Decimal,9),
					new SqlParameter("@provHeavyAgriculturalFunds", SqlDbType.Decimal,9),
					new SqlParameter("@provAdditionalFee", SqlDbType.Decimal,9),
					new SqlParameter("@provReclamationFee", SqlDbType.Decimal,9),
					new SqlParameter("@provArableLandTax", SqlDbType.Decimal,9),
					new SqlParameter("@provSurveyFee", SqlDbType.Decimal,9),
					new SqlParameter("@provServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.batchId;
			parameters[3].Value = model.landblockId;
			parameters[4].Value = model.batchTypeId;
			parameters[5].Value = model.administrativeArea;
			parameters[6].Value = model.town;
			parameters[7].Value = model.village;
			parameters[8].Value = model._group;
			parameters[9].Value = model.totalPeopleNumber;
			parameters[10].Value = model.planLevyArea;
			parameters[11].Value = model.hasLevyArea;
			parameters[12].Value = model.levyNationalLandArea;
			parameters[13].Value = model.levyColletLandArea;
			parameters[14].Value = model.levyPreDeposit;
			parameters[15].Value = model.countrySocialSecurityFund;
			parameters[16].Value = model.countryCompensate;
			parameters[17].Value = model.areaWaterFacilitiesCompensate;
			parameters[18].Value = model.provHeavyAgriculturalFunds;
			parameters[19].Value = model.provAdditionalFee;
			parameters[20].Value = model.provReclamationFee;
			parameters[21].Value = model.provArableLandTax;
			parameters[22].Value = model.provSurveyFee;
			parameters[23].Value = model.provServiceFee;
			parameters[24].Value = model.userId;
			parameters[25].Value = model.createTime;
			parameters[26].Value = model.isSubmited;
			parameters[27].Value = model.isDeleted;
			parameters[28].Value = model.des;

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
		public bool Update(Maticsoft.Model.T_LevyCompensate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_LevyCompensate set ");
			strSql.Append("year=@year,");
			strSql.Append("administratorArea=@administratorArea,");
			strSql.Append("batchId=@batchId,");
			strSql.Append("landblockId=@landblockId,");
			strSql.Append("batchTypeId=@batchTypeId,");
			strSql.Append("administrativeArea=@administrativeArea,");
			strSql.Append("town=@town,");
			strSql.Append("village=@village,");
			strSql.Append("_group=@_group,");
			strSql.Append("totalPeopleNumber=@totalPeopleNumber,");
			strSql.Append("planLevyArea=@planLevyArea,");
			strSql.Append("hasLevyArea=@hasLevyArea,");
			strSql.Append("levyNationalLandArea=@levyNationalLandArea,");
			strSql.Append("levyColletLandArea=@levyColletLandArea,");
			strSql.Append("levyPreDeposit=@levyPreDeposit,");
			strSql.Append("countrySocialSecurityFund=@countrySocialSecurityFund,");
			strSql.Append("countryCompensate=@countryCompensate,");
			strSql.Append("areaWaterFacilitiesCompensate=@areaWaterFacilitiesCompensate,");
			strSql.Append("provHeavyAgriculturalFunds=@provHeavyAgriculturalFunds,");
			strSql.Append("provAdditionalFee=@provAdditionalFee,");
			strSql.Append("provReclamationFee=@provReclamationFee,");
			strSql.Append("provArableLandTax=@provArableLandTax,");
			strSql.Append("provSurveyFee=@provSurveyFee,");
			strSql.Append("provServiceFee=@provServiceFee,");
			strSql.Append("userId=@userId,");
			strSql.Append("createTime=@createTime,");
			strSql.Append("isSubmited=@isSubmited,");
			strSql.Append("isDeleted=@isDeleted,");
			strSql.Append("des=@des");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,50),
					new SqlParameter("@administratorArea", SqlDbType.VarChar,50),
					new SqlParameter("@batchId", SqlDbType.Int,4),
					new SqlParameter("@landblockId", SqlDbType.Int,4),
					new SqlParameter("@batchTypeId", SqlDbType.Int,4),
					new SqlParameter("@administrativeArea", SqlDbType.VarChar,20),
					new SqlParameter("@town", SqlDbType.VarChar,40),
					new SqlParameter("@village", SqlDbType.VarChar,40),
					new SqlParameter("@_group", SqlDbType.VarChar,40),
					new SqlParameter("@totalPeopleNumber", SqlDbType.Decimal,9),
					new SqlParameter("@planLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@hasLevyArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyNationalLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyColletLandArea", SqlDbType.Decimal,9),
					new SqlParameter("@levyPreDeposit", SqlDbType.Decimal,9),
					new SqlParameter("@countrySocialSecurityFund", SqlDbType.Decimal,9),
					new SqlParameter("@countryCompensate", SqlDbType.Decimal,9),
					new SqlParameter("@areaWaterFacilitiesCompensate", SqlDbType.Decimal,9),
					new SqlParameter("@provHeavyAgriculturalFunds", SqlDbType.Decimal,9),
					new SqlParameter("@provAdditionalFee", SqlDbType.Decimal,9),
					new SqlParameter("@provReclamationFee", SqlDbType.Decimal,9),
					new SqlParameter("@provArableLandTax", SqlDbType.Decimal,9),
					new SqlParameter("@provSurveyFee", SqlDbType.Decimal,9),
					new SqlParameter("@provServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@createTime", SqlDbType.DateTime),
					new SqlParameter("@isSubmited", SqlDbType.Int,4),
					new SqlParameter("@isDeleted", SqlDbType.Int,4),
					new SqlParameter("@des", SqlDbType.Text),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.year;
			parameters[1].Value = model.administratorArea;
			parameters[2].Value = model.batchId;
			parameters[3].Value = model.landblockId;
			parameters[4].Value = model.batchTypeId;
			parameters[5].Value = model.administrativeArea;
			parameters[6].Value = model.town;
			parameters[7].Value = model.village;
			parameters[8].Value = model._group;
			parameters[9].Value = model.totalPeopleNumber;
			parameters[10].Value = model.planLevyArea;
			parameters[11].Value = model.hasLevyArea;
			parameters[12].Value = model.levyNationalLandArea;
			parameters[13].Value = model.levyColletLandArea;
			parameters[14].Value = model.levyPreDeposit;
			parameters[15].Value = model.countrySocialSecurityFund;
			parameters[16].Value = model.countryCompensate;
			parameters[17].Value = model.areaWaterFacilitiesCompensate;
			parameters[18].Value = model.provHeavyAgriculturalFunds;
			parameters[19].Value = model.provAdditionalFee;
			parameters[20].Value = model.provReclamationFee;
			parameters[21].Value = model.provArableLandTax;
			parameters[22].Value = model.provSurveyFee;
			parameters[23].Value = model.provServiceFee;
			parameters[24].Value = model.userId;
			parameters[25].Value = model.createTime;
			parameters[26].Value = model.isSubmited;
			parameters[27].Value = model.isDeleted;
			parameters[28].Value = model.des;
			parameters[29].Value = model.id;

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
			strSql.Append("delete from T_LevyCompensate ");
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
			strSql.Append("delete from T_LevyCompensate ");
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
		public Maticsoft.Model.T_LevyCompensate GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,year,administratorArea,batchId,landblockId,batchTypeId,administrativeArea,town,village,_group,totalPeopleNumber,planLevyArea,hasLevyArea,levyNationalLandArea,levyColletLandArea,levyPreDeposit,countrySocialSecurityFund,countryCompensate,areaWaterFacilitiesCompensate,provHeavyAgriculturalFunds,provAdditionalFee,provReclamationFee,provArableLandTax,provSurveyFee,provServiceFee,userId,createTime,isSubmited,isDeleted,des from T_LevyCompensate ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_LevyCompensate model=new Maticsoft.Model.T_LevyCompensate();
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
		public Maticsoft.Model.T_LevyCompensate DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_LevyCompensate model=new Maticsoft.Model.T_LevyCompensate();
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
				if(row["batchId"]!=null && row["batchId"].ToString()!="")
				{
					model.batchId=int.Parse(row["batchId"].ToString());
				}
				if(row["landblockId"]!=null && row["landblockId"].ToString()!="")
				{
					model.landblockId=int.Parse(row["landblockId"].ToString());
				}
				if(row["batchTypeId"]!=null && row["batchTypeId"].ToString()!="")
				{
					model.batchTypeId=int.Parse(row["batchTypeId"].ToString());
				}
				if(row["administrativeArea"]!=null)
				{
					model.administrativeArea=row["administrativeArea"].ToString();
				}
				if(row["town"]!=null)
				{
					model.town=row["town"].ToString();
				}
				if(row["village"]!=null)
				{
					model.village=row["village"].ToString();
				}
				if(row["_group"]!=null)
				{
					model._group=row["_group"].ToString();
				}
				if(row["totalPeopleNumber"]!=null && row["totalPeopleNumber"].ToString()!="")
				{
					model.totalPeopleNumber=decimal.Parse(row["totalPeopleNumber"].ToString());
				}
				if(row["planLevyArea"]!=null && row["planLevyArea"].ToString()!="")
				{
					model.planLevyArea=decimal.Parse(row["planLevyArea"].ToString());
				}
				if(row["hasLevyArea"]!=null && row["hasLevyArea"].ToString()!="")
				{
					model.hasLevyArea=decimal.Parse(row["hasLevyArea"].ToString());
				}
				if(row["levyNationalLandArea"]!=null && row["levyNationalLandArea"].ToString()!="")
				{
					model.levyNationalLandArea=decimal.Parse(row["levyNationalLandArea"].ToString());
				}
				if(row["levyColletLandArea"]!=null && row["levyColletLandArea"].ToString()!="")
				{
					model.levyColletLandArea=decimal.Parse(row["levyColletLandArea"].ToString());
				}
				if(row["levyPreDeposit"]!=null && row["levyPreDeposit"].ToString()!="")
				{
					model.levyPreDeposit=decimal.Parse(row["levyPreDeposit"].ToString());
				}
				if(row["countrySocialSecurityFund"]!=null && row["countrySocialSecurityFund"].ToString()!="")
				{
					model.countrySocialSecurityFund=decimal.Parse(row["countrySocialSecurityFund"].ToString());
				}
				if(row["countryCompensate"]!=null && row["countryCompensate"].ToString()!="")
				{
					model.countryCompensate=decimal.Parse(row["countryCompensate"].ToString());
				}
				if(row["areaWaterFacilitiesCompensate"]!=null && row["areaWaterFacilitiesCompensate"].ToString()!="")
				{
					model.areaWaterFacilitiesCompensate=decimal.Parse(row["areaWaterFacilitiesCompensate"].ToString());
				}
				if(row["provHeavyAgriculturalFunds"]!=null && row["provHeavyAgriculturalFunds"].ToString()!="")
				{
					model.provHeavyAgriculturalFunds=decimal.Parse(row["provHeavyAgriculturalFunds"].ToString());
				}
				if(row["provAdditionalFee"]!=null && row["provAdditionalFee"].ToString()!="")
				{
					model.provAdditionalFee=decimal.Parse(row["provAdditionalFee"].ToString());
				}
				if(row["provReclamationFee"]!=null && row["provReclamationFee"].ToString()!="")
				{
					model.provReclamationFee=decimal.Parse(row["provReclamationFee"].ToString());
				}
				if(row["provArableLandTax"]!=null && row["provArableLandTax"].ToString()!="")
				{
					model.provArableLandTax=decimal.Parse(row["provArableLandTax"].ToString());
				}
				if(row["provSurveyFee"]!=null && row["provSurveyFee"].ToString()!="")
				{
					model.provSurveyFee=decimal.Parse(row["provSurveyFee"].ToString());
				}
				if(row["provServiceFee"]!=null && row["provServiceFee"].ToString()!="")
				{
					model.provServiceFee=decimal.Parse(row["provServiceFee"].ToString());
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
			strSql.Append("select id,year,administratorArea,batchId,landblockId,batchTypeId,administrativeArea,town,village,_group,totalPeopleNumber,planLevyArea,hasLevyArea,levyNationalLandArea,levyColletLandArea,levyPreDeposit,countrySocialSecurityFund,countryCompensate,areaWaterFacilitiesCompensate,provHeavyAgriculturalFunds,provAdditionalFee,provReclamationFee,provArableLandTax,provSurveyFee,provServiceFee,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_LevyCompensate ");
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
			strSql.Append(" id,year,administratorArea,batchId,landblockId,batchTypeId,administrativeArea,town,village,_group,totalPeopleNumber,planLevyArea,hasLevyArea,levyNationalLandArea,levyColletLandArea,levyPreDeposit,countrySocialSecurityFund,countryCompensate,areaWaterFacilitiesCompensate,provHeavyAgriculturalFunds,provAdditionalFee,provReclamationFee,provArableLandTax,provSurveyFee,provServiceFee,userId,createTime,isSubmited,isDeleted,des ");
			strSql.Append(" FROM T_LevyCompensate ");
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
            strSql.Append("select count(1) from T_LevyCompensate T,T_LandBlock L WHERE  1=1 and T.landblockId=L.id");
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
            strSql.Append(")AS Row, T.*,L.name as landBlockName from T_LevyCompensate T,T_LandBlock L WHERE  1=1 and T.landblockId=L.id");
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
			parameters[0].Value = "T_LevyCompensate";
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

