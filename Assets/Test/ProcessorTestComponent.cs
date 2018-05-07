using RTG.CustomProcessors.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 414

public class ProcessorTestComponent : MonoBehaviour
{
	public void Clear()
	{
		myPublicComponent = null;
		childPublicComponent = null;
		parentPublicComponent = null;
		myPrivateComponent = null;
		childPrivateComponent = null;
		parentPrivateComponent = null;
		myPublicComponentsArray = null;
		childPublicComponentsArray = null;
		parentPublicComponentsArray = null;
		myPrivateComponentsArray = null;
		childPrivateComponentsArray = null;
		parentPrivateComponentsArray = null;
		myPublicComponentsList = null;
		childPublicComponentsList = null;
		parentPublicComponentsList = null;
		myPrivateComponentsList = null;
		childPrivateComponentsList = null;
		parentPrivateComponentsList = null;
		myPublicComponentsArrayInactive = null;
		childPublicComponentsArrayInactive = null;
		parentPublicComponentsArrayInactive = null;
		myPrivateComponentsArrayInactive = null;
		childPrivateComponentsArrayInactive = null;
		parentPrivateComponentsArrayInactive = null;
		myPublicComponentsListInactive = null;
		childPublicComponentsListInactive = null;
		parentPublicComponentsListInactive = null;
		myPrivateComponentsListInactive = null;
		childPrivateComponentsListInactive = null;
		parentPrivateComponentsListInactive = null;
	}

	public class SomeClass
	{
	}

	[Serializable]
	public class SomeOtherClass
	{
	}

	[GetComponentProcessor(true, Source.Self)]
	public SomeClass someClass;

	[GetComponentProcessor(true, Source.Self)]
	public SomeOtherClass someOtherClass;

	[GetComponentProcessor(true, Source.Self)]
	public SomeClass[] someClassArray;

	[GetComponentProcessor(true, Source.Self)]
	public SomeOtherClass[] someOtherClassArray;

	[GetComponentProcessor(true, Source.Self)]
	public List<SomeClass> someClassList;

	[GetComponentProcessor(true, Source.Self)]
	public List<SomeOtherClass> someOtherClassList;

	[GetComponentProcessor(true, Source.Self)]
	public Renderer myPublicComponent;

	[GetComponentProcessor(true, Source.Children)]
	public Renderer childPublicComponent;

	[GetComponentProcessor(true, Source.Parent)]
	public Renderer parentPublicComponent;

	[SerializeField]
	[GetComponentProcessor(true, Source.Self)]
	private Renderer myPrivateComponent;

	[SerializeField]
	[GetComponentProcessor(true, Source.Children)]
	private Renderer childPrivateComponent;

	[SerializeField]
	[GetComponentProcessor(true, Source.Parent)]
	private Renderer parentPrivateComponent;

	[GetComponentProcessor(true, Source.Self)]
	public Renderer[] myPublicComponentsArray;

	[GetComponentProcessor(true, Source.Children)]
	public Renderer[] childPublicComponentsArray;

	[GetComponentProcessor(true, Source.Parent)]
	public Renderer[] parentPublicComponentsArray;

	[SerializeField]
	[GetComponentProcessor(true, Source.Self)]
	private Renderer[] myPrivateComponentsArray;

	[SerializeField]
	[GetComponentProcessor(true, Source.Children)]
	private Renderer[] childPrivateComponentsArray;

	[SerializeField]
	[GetComponentProcessor(true, Source.Parent)]
	private Renderer[] parentPrivateComponentsArray;

	[GetComponentProcessor(true, Source.Self)]
	public List<Renderer> myPublicComponentsList;

	[GetComponentProcessor(true, Source.Children)]
	public List<Renderer> childPublicComponentsList;

	[GetComponentProcessor(true, Source.Parent)]
	public List<Renderer> parentPublicComponentsList;

	[SerializeField]
	[GetComponentProcessor(true, Source.Self)]
	private List<Renderer> myPrivateComponentsList;

	[SerializeField]
	[GetComponentProcessor(true, Source.Children)]
	private List<Renderer> childPrivateComponentsList;

	[SerializeField]
	[GetComponentProcessor(true, Source.Parent)]
	private List<Renderer> parentPrivateComponentsList;

	[GetComponentProcessor(true, Source.Self, true)]
	public Renderer[] myPublicComponentsArrayInactive;

	[GetComponentProcessor(true, Source.Children, true)]
	public Renderer[] childPublicComponentsArrayInactive;

	[GetComponentProcessor(true, Source.Parent, true)]
	public Renderer[] parentPublicComponentsArrayInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Self, true)]
	private Renderer[] myPrivateComponentsArrayInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Children, true)]
	private Renderer[] childPrivateComponentsArrayInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Parent, true)]
	private Renderer[] parentPrivateComponentsArrayInactive;

	[GetComponentProcessor(true, Source.Self, true)]
	public List<Renderer> myPublicComponentsListInactive;

	[GetComponentProcessor(true, Source.Children, true)]
	public List<Renderer> childPublicComponentsListInactive;

	[GetComponentProcessor(true, Source.Parent, true)]
	public List<Renderer> parentPublicComponentsListInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Self, true)]
	private List<Renderer> myPrivateComponentsListInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Children, true)]
	private List<Renderer> childPrivateComponentsListInactive;

	[SerializeField]
	[GetComponentProcessor(true, Source.Parent, true)]
	private List<Renderer> parentPrivateComponentsListInactive;
}
#pragma warning restore 414
