using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Verif_Batch:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Verif_Batch
	{
		public T_Verif_Batch()
		{}
		#region Model
		private int _id;
		private string _year;
		private string _administratorarea;
		private int _verifid;
		private int _batchid;
		private decimal? _verifprovarea=0M;
		private decimal? _verifprovarablearea=0M;
		private decimal? _verifselfarea=0M;
		private decimal? _verifselfarablearea=0M;
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
		public int batchId
		{
			set{ _batchid=value;}
			get{return _batchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? verifProvArea
		{
			set{ _verifprovarea=value;}
			get{return _verifprovarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? verifProvArableArea
		{
			set{ _verifprovarablearea=value;}
			get{return _verifprovarablearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? verifSelfArea
		{
			set{ _verifselfarea=value;}
			get{return _verifselfarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? verifSelfArableArea
		{
			set{ _verifselfarablearea=value;}
			get{return _verifselfarablearea;}
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

