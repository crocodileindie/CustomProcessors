using NUnit.Framework;
using RTG.CustomProcessors.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RTG.CustomProcessors
{
	[CustomProcessor(typeof(GetComponentProcessorAttribute), CustomProcessorAttribute.ObjectType.Component)]
	public sealed class GetComponentProcessor : PropertyProcessor
	{
		private InternalProcessor _internalProcessor;

		/// <summary>
		/// Constructor, generate method info appropriately
		/// </summary>
		/// <param name="fieldInfo"></param>
		/// <param name="attribute"></param>
		public GetComponentProcessor(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
		{
			var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;

			//arrays and lists are treated differently
			if (fieldInfo.FieldType.IsArray)
			{
				switch (getComponentProcessorAttribute.Source)
				{
					case Source.Self:
						_internalProcessor = new ArraySelf(fieldInfo, attribute);
						break;

					case Source.Children:
						_internalProcessor = new ArrayChildren(fieldInfo, attribute);
						break;

					case Source.Parent:
						_internalProcessor = new ArrayParent(fieldInfo, attribute);
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else if (fieldInfo.FieldType.IsList())
			{
				switch (getComponentProcessorAttribute.Source)
				{
					case Source.Self:
						_internalProcessor = new ListSelf(fieldInfo, attribute);
						break;

					case Source.Children:
						_internalProcessor = new ListChildren(fieldInfo, attribute);
						break;

					case Source.Parent:
						_internalProcessor = new ListParent(fieldInfo, attribute);
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else
			{
				switch (getComponentProcessorAttribute.Source)
				{
					case Source.Self:
						_internalProcessor = new FieldSelf(fieldInfo, attribute);
						break;

					case Source.Children:
						_internalProcessor = new FieldChildren(fieldInfo, attribute);
						break;

					case Source.Parent:
						_internalProcessor = new FieldParent(fieldInfo, attribute);
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private bool NeedsProcessing(object obj)
		{
			//process everytime if we were told to do so
			var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
			if (getComponentProcessorAttribute.AlwaysProcess)
				return true;

			//or if null or array is empty
			var component = obj as Component;

			if (fieldInfo.FieldType.IsArrayOrList())
			{
				var theArray = (System.Collections.IList)fieldInfo.GetValue(component);
				return theArray == null || theArray.Count == 0;
			}
			else
			{
				return fieldInfo.GetValue(component) as Component == null;
			}
		}

		private bool IsValid(object obj)
		{
			if (obj as Component == null)
				return false;

			if (fieldInfo.IsPrivate && !fieldInfo.IsDefined(typeof(SerializeField), true))
				return false;

			if (!_internalProcessor.ElementType().IsSubclassOf(typeof(Component)))
				return false;

			return true;
		}

		/// <summary>
		/// will process the object
		///
		/// </summary>
		/// <param name="obj">object to process</param>
		public override void Process(object obj)
		{
			//check if we need to do anything
			if (IsValid(obj) && NeedsProcessing(obj))
			{
				var component = obj as Component;

				_internalProcessor.Process(component);
			}
		}

		abstract class InternalProcessor
		{
			protected FieldInfo _fieldInfo;

			public InternalProcessor(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute)
			{
				_fieldInfo = fieldInfo;
			}

			public abstract void Process(Component component);

			public Type ElementType()
			{
				if (_fieldInfo.FieldType.IsArray)
					return _fieldInfo.FieldType.GetElementType();
				else if (_fieldInfo.FieldType.IsList())
					return _fieldInfo.FieldType.GetGenericArguments()[0];
				return _fieldInfo.FieldType;
			}
		}

		class ArraySelf : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;

			public ArraySelf(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for T[] Component.GetComponents<T>();
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponents" && methodInfo.ContainsGenericParameters && methodInfo.GetParameters().Length == 0)
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());
			}

			public override void Process(Component component)
			{
				_fieldInfo.SetValue(component, _genericMethodInfo.Invoke(component, null));
			}
		}

		class ArrayChildren : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;
			private bool _includeInactive;

			public ArrayChildren(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for T[] Component.GetComponentsInChildren<T>(bool includeInactive);
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponentsInChildren" && methodInfo.ContainsGenericParameters &&
						    methodInfo.GetParameters().Length == 1 &&
						    methodInfo.GetParameters()[0].ParameterType == typeof(bool))
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());

				//create method parameter list
				var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
				_includeInactive = getComponentProcessorAttribute.IncludeInactive;
			}

			public override void Process(Component component)
			{
				var parametersArray = new object[] { _includeInactive };
				_fieldInfo.SetValue(component, _genericMethodInfo.Invoke(component, parametersArray));
			}
		}

		class ArrayParent : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;
			private bool _includeInactive;
			private object[] _parametersArray;

			public ArrayParent(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for T[] Component.GetComponentsInParent<T>();
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponentsInParent" &&
						    methodInfo.ContainsGenericParameters && methodInfo.GetParameters().Length == 1)
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());

				//create method parameter list
				var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
				_parametersArray = new object[] { getComponentProcessorAttribute.IncludeInactive };
			}

			public override void Process(Component component)
			{
				_fieldInfo.SetValue(component, _genericMethodInfo.Invoke(component, _parametersArray));
			}
		}

		class ListSelf : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;
			private bool _includeInactive;

			public ListSelf(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for void Component.GetComponents<T>(List<T> results);
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponents" && methodInfo.ContainsGenericParameters && methodInfo.GetParameters().Length == 1)
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());
			}

			public override void Process(Component component)
			{
				//create method parameter list
				var listType = typeof(List<>).MakeGenericType(ElementType());
				var list = Activator.CreateInstance(listType);
				var parametersArray = new object[] { list };
				_genericMethodInfo.Invoke(component, parametersArray);
				_fieldInfo.SetValue(component, parametersArray[0]);
			}
		}

		class ListChildren : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;
			private bool _includeInactive;

			public ListChildren(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for void Component.GetComponentsInChildren<T>(bool includeInactive, List<T> results);
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponentsInChildren" && methodInfo.ContainsGenericParameters &&
						    methodInfo.GetParameters().Length == 2 &&
						    methodInfo.GetParameters()[0].ParameterType == typeof(bool))
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());

				var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
				_includeInactive = getComponentProcessorAttribute.IncludeInactive;
			}

			public override void Process(Component component)
			{
				//create method parameter list
				var listType = typeof(List<>).MakeGenericType(ElementType());
				var list = Activator.CreateInstance(listType);
				var parametersArray = new object[] { _includeInactive, list };

				_genericMethodInfo.Invoke(component, parametersArray);
				_fieldInfo.SetValue(component, parametersArray[1]);
			}
		}

		class ListParent : InternalProcessor
		{
			private static MethodInfo baseMethodInfo;
			private readonly MethodInfo _genericMethodInfo;
			private bool _includeInactive;

			public ListParent(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				if (baseMethodInfo == null)
				{
					//looking for void Component.GetComponentsInParent<T>(bool includeInactive, List<T> results);
					foreach (var methodInfo in typeof(Component).GetMethods())
					{
						if (methodInfo.Name == "GetComponentsInParent" && methodInfo.ContainsGenericParameters &&
						    methodInfo.GetParameters().Length == 2 &&
						    methodInfo.GetParameters()[0].ParameterType == typeof(bool))
						{
							baseMethodInfo = methodInfo;
							break;
						}
					}
					Assert.NotNull(baseMethodInfo);
				}
				//generate method info for T = ElementType
				_genericMethodInfo = baseMethodInfo.MakeGenericMethod(ElementType());

				var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
				_includeInactive = getComponentProcessorAttribute.IncludeInactive;
			}

			public override void Process(Component component)
			{
				//create method parameter list
				var listType = typeof(List<>).MakeGenericType(ElementType());
				var list = Activator.CreateInstance(listType);

				var parametersArray = new object[] { _includeInactive, list };
				_genericMethodInfo.Invoke(component, parametersArray);
				_fieldInfo.SetValue(component, parametersArray[1]);
			}
		}

		class FieldSelf : InternalProcessor
		{
			public FieldSelf(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
			}

			public override void Process(Component component)
			{
				_fieldInfo.SetValue(component, component.GetComponent(ElementType()));
			}
		}

		class FieldChildren : InternalProcessor
		{
			private bool _includeInactive;

			public FieldChildren(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
				var getComponentProcessorAttribute = attribute as GetComponentProcessorAttribute;
				_includeInactive = getComponentProcessorAttribute.IncludeInactive;
			}

			public override void Process(Component component)
			{
				var val = component.GetComponentInChildren(ElementType(), _includeInactive);
				_fieldInfo.SetValue(component, val);
			}
		}

		class FieldParent : InternalProcessor
		{
			public FieldParent(FieldInfo fieldInfo, UnityEngine.PropertyAttribute attribute) : base(fieldInfo, attribute)
			{
			}

			public override void Process(Component component)
			{
				var val = component.GetComponentInParent(ElementType());
				_fieldInfo.SetValue(component, val);
			}
		}
	}

}