using System;

namespace RTG.CustomProcessors.Attributes
{
    /// <summary>
    ///   <para>Tells a PropertyProcessor class which run-time class or attribute it's a processor for.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class CustomProcessorAttribute : Attribute
    {
		[Flags]
	    public enum ObjectType
	    {
		    Component = 1,
			ScriptableObject = 2,
			SerializableClass = 4,
			All = Component | ScriptableObject | SerializableClass
	    }

		internal System.Type m_Type;
        
        public Type Type { get { return m_Type; } }
        
        /// <summary>
        ///   <para>Tells a PropertyCooker class which run-time class or attribute it's a cooker for.</para>
        /// </summary>
        /// <param name="type">If the drawer is for a custom Serializable class, the type should be that class. If the drawer is for script variables with a specific PropertyAttribute, the type should be that attribute.</param>
        public CustomProcessorAttribute(System.Type type, ObjectType objectType = ObjectType.All)
        {
            this.m_Type = type;
        }

    }
}