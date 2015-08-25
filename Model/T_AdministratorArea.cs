using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_AdministratorArea:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_AdministratorArea
	{
		public T_AdministratorArea()
		{}
		#region Model
		private int _id;
		private string _name;
		private int _sort;
		private string _pingyin;
		private string _initial;
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
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pingyin
		{
			set{ _pingyin=value;}
			get{return _pingyin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string initial
		{
			set{ _initial=value;}
			get{return _initial;}
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

