using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Plan:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Plan
	{
		public T_Plan()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _year;
		private string _administratorarea;
		private int _plantypeid;
		private decimal? _consarea;
		private decimal? _agriarea;
		private decimal? _arabarea;
		private decimal? _issuedquota;
		private decimal? _remainquota;
		private string _releaseno;
		private DateTime _releasetime;
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
		public string name
		{
			set{ _name=value;}
			get{return _name;}
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
		public int planTypeId
		{
			set{ _plantypeid=value;}
			get{return _plantypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? consArea
		{
			set{ _consarea=value;}
			get{return _consarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? agriArea
		{
			set{ _agriarea=value;}
			get{return _agriarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? arabArea
		{
			set{ _arabarea=value;}
			get{return _arabarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? issuedQuota
		{
			set{ _issuedquota=value;}
			get{return _issuedquota;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? remainQuota
		{
			set{ _remainquota=value;}
			get{return _remainquota;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string releaseNo
		{
			set{ _releaseno=value;}
			get{return _releaseno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime releaseTime
		{
			set{ _releasetime=value;}
			get{return _releasetime;}
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

