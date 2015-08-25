using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_Administrator:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_Administrator
	{
		public T_Administrator()
		{}
		#region Model
		private int _id;
		private string _user_name;
		private string _password;
		/// <summary>
		/// id
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string user_name
		{
			set{ _user_name=value;}
			get{return _user_name;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string password
		{
			set{ _password=value;}
			get{return _password;}
		}
		#endregion Model

	}
}

