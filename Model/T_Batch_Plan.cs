﻿using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Batch_Plan:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Batch_Plan
	{
		public T_Batch_Plan()
		{}
		#region Model
		private int _id;
		private int _batchid;
		private int _planid;
		private decimal? _consarea;
		private decimal? _agriarea;
		private decimal? _arabarea;
		private decimal? _issuedquota;
		private DateTime _createtime;
		private int _issubmited=0;
		private int _isdeleted=0;
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
		public int batchId
		{
			set{ _batchid=value;}
			get{return _batchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int planId
		{
			set{ _planid=value;}
			get{return _planid;}
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
		#endregion Model

	}
}

