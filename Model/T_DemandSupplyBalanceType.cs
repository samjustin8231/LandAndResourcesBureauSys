using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_DemandSupplyBalanceType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_DemandSupplyBalanceType
	{
		public T_DemandSupplyBalanceType()
		{}
		#region Model
		private int _id;
		private string _name;
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
		public string des
		{
			set{ _des=value;}
			get{return _des;}
		}
		#endregion Model

	}
}

