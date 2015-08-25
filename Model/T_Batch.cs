using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Batch:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Batch
	{
		public T_Batch()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _subname;
		private string _year;
		private string _administratorarea;
		private string _approvalauthority;
		private string _approvalno;
		private DateTime _approvaltime;
		private int _batchtypeid;
		private decimal? _totalarea;
		private decimal? _addconsarea;
		private decimal? _consarea;
		private decimal? _agriarea;
		private decimal? _arabarea;
		private decimal? _unusedarea;
		private decimal? _haslevyarea;
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
		public string subName
		{
			set{ _subname=value;}
			get{return _subname;}
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
		public string approvalAuthority
		{
			set{ _approvalauthority=value;}
			get{return _approvalauthority;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string approvalNo
		{
			set{ _approvalno=value;}
			get{return _approvalno;}
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
		public int batchTypeId
		{
			set{ _batchtypeid=value;}
			get{return _batchtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? totalArea
		{
			set{ _totalarea=value;}
			get{return _totalarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? addConsArea
		{
			set{ _addconsarea=value;}
			get{return _addconsarea;}
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
		public decimal? unusedArea
		{
			set{ _unusedarea=value;}
			get{return _unusedarea;}
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

