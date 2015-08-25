using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Notice:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Notice
	{
		public T_Notice()
		{}
		#region Model
		private int _id;
		private string _title;
		private string _content;
		private DateTime? _create_time;
		private int _isdeleted=0;
		private int? _admin_id;
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
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? create_time
		{
			set{ _create_time=value;}
			get{return _create_time;}
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
		public int? admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		#endregion Model

	}
}

