using System;
using System.Collections.Generic;
using System.Reflection;

namespace Disarray.Core
{
	public abstract class AutoloadedClass
	{
		public static IList<AutoloadedClass> LoadedClasses;

		public static int InternalIDCount = 0;

		public static IDictionary<string, AutoloadedClass> ClassesByName;

		public int Type { get; internal set; }

		public string Name { get; internal set; }

		public static DataType GetClass<DataType>(int ID) where DataType : AutoloadedClass => (ID < 0 || ID >= LoadedClasses.Count) ? null : LoadedClasses[ID] as DataType;

		public static DataType GetClass<DataType>(string name) where DataType : AutoloadedClass => ClassesByName.TryGetValue(name, out AutoloadedClass classData) ? classData as DataType : null;  // string based searches are slow, might want to optimize at a later date

		public static DataType GetClass<DataType>() where DataType : AutoloadedClass => ClassesByName.TryGetValue(typeof(DataType).Name, out AutoloadedClass classData) ? classData as DataType : null; // string based searches are slow, might want to optimize at a later date

		public static void Load()
		{
			LoadedClasses = new List<AutoloadedClass>();

			InternalIDCount = 0;

			ClassesByName = new Dictionary<string, AutoloadedClass>();
		}

		public static void LoadType(Type item)
		{
			if (item.IsSubclassOf(typeof(AutoloadedClass)) && item.GetCustomAttribute(typeof(AutoloadedClassAttribute), false) != null)
			{
				AutoloadedClass classInQuestion = Activator.CreateInstance(item) as AutoloadedClass;
				classInQuestion.Type = InternalIDCount++;
				classInQuestion.Name = item.Name;
				classInQuestion.LoadInstance();
				LoadedClasses.Add(classInQuestion);
				ClassesByName.Add(classInQuestion.Name, classInQuestion);
				classInQuestion.PostLoadType();
			}
		}

		public static void PostLoadType(Assembly assembly)
		{
			foreach (Type item in assembly.GetTypes())
			{
				if (!item.IsAbstract && item.GetConstructor(new Type[0]) != null && item.IsSubclassOf(typeof(AutoloadedClass)) && item.GetCustomAttribute(typeof(AutoloadedClassAttribute), false) == null)
				{
					foreach (AutoloadedClass autoloadedClass in LoadedClasses)
					{
						if (item.IsSubclassOf(autoloadedClass.GetType()))
						{
							AutoloadedClass classInQuestion = Activator.CreateInstance(item) as AutoloadedClass;
							classInQuestion.Type = autoloadedClass.DataIDCount++;
							classInQuestion.Name = item.Name;
							autoloadedClass.GetLoadedData.Add(classInQuestion);
							autoloadedClass.GetDataByName.Add(classInQuestion.Name, classInQuestion);
							break;
						}
					}
				}
			}
		}

		public static void Unload()
		{
			LoadedClasses?.Clear();

			InternalIDCount = 0;

			ClassesByName?.Clear();
		}

		public IList<AutoloadedClass> GetLoadedData // Better way of doing this may be possible but ehhhhhhhhhhhh my brain too smol
		{
			get
			{
				if (!GetType().IsAbstract && GetType().GetCustomAttribute(typeof(AutoloadedClassAttribute), false) == null)
				{
					if (ClassesByName.TryGetValue(GetType().BaseType.Name, out AutoloadedClass classData))
					{
						return classData.LoadedData;
					}
				}
				return LoadedData;
			}
		}

		public IList<AutoloadedClass> LoadedData;

		public int DataIDCount = 0;

		public IDictionary<string, AutoloadedClass> GetDataByName // Better way of doing this may be possible but ehhhhhhhhhhhh my brain too smol
		{
			get
			{
				if (!GetType().IsAbstract && GetType().GetCustomAttribute(typeof(AutoloadedClassAttribute), false) == null)
				{
					if (ClassesByName.TryGetValue(GetType().BaseType.Name, out AutoloadedClass classData))
					{
						return classData.DataByName;
					}
				}
				return DataByName;
			}
		}

		public IDictionary<string, AutoloadedClass> DataByName;

		public DataType GetData<DataType>(int ID) where DataType : AutoloadedClass => (ID < 0 || ID >= GetLoadedData.Count) ? null : GetLoadedData[ID] as DataType;

		public DataType GetData<DataType>(string name) where DataType : AutoloadedClass => GetDataByName.TryGetValue(name, out AutoloadedClass data) ? data as DataType : null;  // string based searches are slow, might want to optimize at a later date

		public DataType GetData<DataType>() where DataType : AutoloadedClass => GetDataByName.TryGetValue(typeof(DataType).Name, out AutoloadedClass data) ? data as DataType : null;  // string based searches are slow, might want to optimize at a later date

		public virtual void LoadInstance()
		{
			LoadedData = new List<AutoloadedClass>();
			DataByName = new Dictionary<string, AutoloadedClass>();
		}

		public virtual void PostLoadType() { }
	}
}