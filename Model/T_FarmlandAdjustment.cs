using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_FarmlandAdjustment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_FarmlandAdjustment
	{
		public T_FarmlandAdjustment()
		{}
		#region Model
		private int _id;
		private string _year;
		private string _administratorarea;
		private int _verifid;
		private string _adjustmentreason;
		private string _approvalarticleno;
		private DateTime _approvaltime;
		private decimal? _startfarmlandarea=0M;
		private decimal? _endfarmlandarea=0M;
		private decimal? _startfarmlandarablelandarea=0M;
		private decimal? _endfarmlandarablelandarea=0M;
		private decimal? _taskarea=0M;
		private decimal? _adjustbeforearea=0M;
		private decimal? _adjustbeforearablearea=0M;
		private decimal? _adjustafterarea=0M;
		private decimal? _adjustafterarablearea=0M;
		private decimal? _adjustinarea=0M;
		private decimal? _adjustinarablearea=0M;
		private decimal? _adjustoutarea=0M;
		private decimal? _adjustoutarablearea=0M;
		private int? _isyearstart;
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
		public int verifId
		{
			set{ _verifid=value;}
			get{return _verifid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string adjustmentReason
		{
			set{ _adjustmentreason=value;}
			get{return _adjustmentreason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string approvalArticleNo
		{
			set{ _approvalarticleno=value;}
			get{return _approvalarticleno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime approvalTime
		{
			set{ _approvaltime=value;}
			get{return _approvaltime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? startFarmlandArea
		{
			set{ _startfarmlandarea=value;}
			get{return _startfarmlandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? endFarmlandArea
		{
			set{ _endfarmlandarea=value;}
			get{return _endfarmlandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? startFarmlandArableLandArea
		{
			set{ _startfarmlandarablelandarea=value;}
			get{return _startfarmlandarablelandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? endFarmlandArableLandArea
		{
			set{ _endfarmlandarablelandarea=value;}
			get{return _endfarmlandarablelandarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? taskArea
		{
			set{ _taskarea=value;}
			get{return _taskarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustBeforeArea
		{
			set{ _adjustbeforearea=value;}
			get{return _adjustbeforearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustBeforeArableArea
		{
			set{ _adjustbeforearablearea=value;}
			get{return _adjustbeforearablearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustAfterArea
		{
			set{ _adjustafterarea=value;}
			get{return _adjustafterarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustAfterArableArea
		{
			set{ _adjustafterarablearea=value;}
			get{return _adjustafterarablearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustInArea
		{
			set{ _adjustinarea=value;}
			get{return _adjustinarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustInArableArea
		{
			set{ _adjustinarablearea=value;}
			get{return _adjustinarablearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustOutArea
		{
			set{ _adjustoutarea=value;}
			get{return _adjustoutarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? adjustOutArableArea
		{
			set{ _adjustoutarablearea=value;}
			get{return _adjustoutarablearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isYearStart
		{
			set{ _isyearstart=value;}
			get{return _isyearstart;}
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

