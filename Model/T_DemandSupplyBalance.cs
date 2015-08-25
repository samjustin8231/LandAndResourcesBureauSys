using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_DemandSupplyBalance:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_DemandSupplyBalance
	{
		public T_DemandSupplyBalance()
		{}
		#region Model
		private int _id;
		private string _no;
		private string _name;
		private string _year;
		private string _administratorarea;
		private int _typeid;
		private string _acceptunit;
		private string _acceptno;
		private DateTime? _accepttime;
		private string _position;
		private decimal? _scale;
		private decimal? _agriarea;
		private decimal? _arabarea;
		private decimal? _adjustarea;
		private decimal? _occupyarea;
		private decimal? _remainarea;
		private int _userid;
		private DateTime _createtime;
		private int _issubmited=0;
		private int _isdeleted=0;
		private int _batchid=0;
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
		public string no
		{
			set{ _no=value;}
			get{return _no;}
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
		public int typeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string acceptUnit
		{
			set{ _acceptunit=value;}
			get{return _acceptunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string acceptNo
		{
			set{ _acceptno=value;}
			get{return _acceptno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? acceptTime
		{
			set{ _accepttime=value;}
			get{return _accepttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string position
		{
			set{ _position=value;}
			get{return _position;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? scale
		{
			set{ _scale=value;}
			get{return _scale;}
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
		public decimal? adjustArea
		{
			set{ _adjustarea=value;}
			get{return _adjustarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? occupyArea
		{
			set{ _occupyarea=value;}
			get{return _occupyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? remainArea
		{
			set{ _remainarea=value;}
			get{return _remainarea;}
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
		public int batchId
		{
			set{ _batchid=value;}
			get{return _batchid;}
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

