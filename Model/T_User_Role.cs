using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_User_Role:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_User_Role
	{
		public T_User_Role()
		{}
		#region Model
		private int _id;
		private int? _id_user;
		private int? _id_role;
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
		public int? id_user
		{
			set{ _id_user=value;}
			get{return _id_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? id_role
		{
			set{ _id_role=value;}
			get{return _id_role;}
		}
		#endregion Model

	}
}

