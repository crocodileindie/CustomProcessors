using System.Reflection;
using UnityEngine;

namespace RTG.CustomProcessors
{
	/// <summary>
	///   <para>Base class to derive custom property processors from. Use this to create custom cookers for your own Serializable classes or for script variables with custom PropertyAttributes.</para>
	/// </summary>
	public abstract class PropertyProcessor : IProcessor
	{
		internal PropertyAttribute m_Attribute;
		internal System.Reflection.FieldInfo m_FieldInfo;

		protected PropertyProcessor(FieldInfo fieldInfo, PropertyAttribute propertyAttribute)
		{
			m_FieldInfo = fieldInfo;
			m_Attribute = propertyAttribute;
		}

		/// <summary>
		///   <para>The PropertyAttribute for the property. Not applicable for custom class processors. (Read Only)</para>
		/// </summary>
		public PropertyAttribute attribute
		{
			get
			{
				return this.m_Attribute;
			}
		}

		/// <summary>
		///   <para>The reflection FieldInfo for the member this property represents. (Read Only)</para>
		/// </summary>
		public System.Reflection.FieldInfo fieldInfo
		{
			get
			{
				return this.m_FieldInfo;
			}
		}

		public abstract void Process(object obj);
	}

	public abstract class ComponentProcessor : IProcessor
	{
		public abstract void Process(object obj);
	}

	public interface IProcessor
	{
		void Process(object obj);
	}
}