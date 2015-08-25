using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_LevyCompensate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_LevyCompensate
	{
		public T_LevyCompensate()
		{}
		#region Model
		private int _id;
		private string _year;
		private string _administratorarea;
		private int _batchid;
		private int _landblockid;
		private int? _batchtypeid;
		private string _administrativearea;
		private string _town;
		private string _village;
		private string __group;
		private decimal? _totalpeoplenumber;
		private decimal? _planlevyarea;
		private decimal? _haslevyarea;
		private decimal? _levynationallandarea;
		private decimal? _levycolletlandarea;
		private decimal? _levypredeposit;
		private decimal? _countrysocialsecurityfund;
		private decimal? _countrycompensate;
		private decimal? _areawaterfacilitiescompensate;
		private decimal? _provheavyagriculturalfunds;
		private decimal? _provadditionalfee;
		private decimal? _provreclamationfee;
		private decimal? _provarablelandtax;
		private decimal? _provsurveyfee;
		private decimal? _provservicefee;
		private int _userid;
		private DateTime _createtime;
		private int _issubmited=0;
		private int _isdeleted=0;
		private string _des;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string year
		{
			set{ _year=value;}
			get{return _year;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string administratorArea
		{
			set{ _administratorarea=value;}
			get{return _administratorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int batchId
		{
			set{ _batchid=value;}
			get{return _batchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int landblockId
		{
			set{ _landblockid=value;}
			get{return _landblockid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? batchTypeId
		{
			set{ _batchtypeid=value;}
			get{return _batchtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string administrativeArea
		{
			set{ _administrativearea=value;}
			get{return _administrativearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string town
		{
			set{ _town=value;}
			get{return _town;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string village
		{
			set{ _village=value;}
			get{return _village;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string _group
		{
			set{ __group=value;}
			get{return __group;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalPeopleNumber
		{
			set{ _totalpeoplenumber=value;}
			get{return _totalpeoplenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? planLevyArea
		{
			set{ _planlevyarea=value;}
			get{return _planlevyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? hasLevyArea
		{
			set{ _haslevyarea=value;}
			get{return _haslevyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? levyNationalLandArea
		{
			set{ _levynationallandarea=value;}
			get{return _levynationallandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? levyColletLandArea
		{
			set{ _levycolletlandarea=value;}
			get{return _levycolletlandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? levyPreDeposit
		{
			set{ _levypredeposit=value;}
			get{return _levypredeposit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? countrySocialSecurityFund
		{
			set{ _countrysocialsecurityfund=value;}
			get{return _countrysocialsecurityfund;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? countryCompensate
		{
			set{ _countrycompensate=value;}
			get{return _countrycompensate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? areaWaterFacilitiesCompensate
		{
			set{ _areawaterfacilitiescompensate=value;}
			get{return _areawaterfacilitiescompensate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provHeavyAgriculturalFunds
		{
			set{ _provheavyagriculturalfunds=value;}
			get{return _provheavyagriculturalfunds;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provAdditionalFee
		{
			set{ _provadditionalfee=value;}
			get{return _provadditionalfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provReclamationFee
		{
			set{ _provreclamationfee=value;}
			get{return _provreclamationfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provArableLandTax
		{
			set{ _provarablelandtax=value;}
			get{return _provarablelandtax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provSurveyFee
		{
			set{ _provsurveyfee=value;}
			get{return _provsurveyfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? provServiceFee
		{
			set{ _provservicefee=value;}
			get{return _provservicefee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int userId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime createTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isSubmited
		{
			set{ _issubmited=value;}
			get{return _issubmited;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string des
		{
			set{ _des=value;}
			get{return _des;}
		}
		#endregion Model

	}
}

