using System;
using Terraria.ModLoader;

namespace Disarray.Core.Autoload
{
	public abstract partial class AutoloadedClass
	{
		public static DataType CreateNewInstance<DataType>(string className, string dataName) where DataType : AutoloadedClass
		{
			AutoloadedClass sourceClass = GetClass(className).GetData(dataName);
			DataType newProperty = Activator.CreateInstance(sourceClass.GetType()) as DataType;
			newProperty.Type = sourceClass.Type;
			newProperty.Name = sourceClass.Name;
			return newProperty;
		}

		public static DataType CreateNewInstance<DataType>() where DataType : AutoloadedClass
		{
			DataType sourceType = ModContent.GetInstance<DataType>();
			DataType newProperty = Activator.CreateInstance(typeof(DataType)) as DataType;
			newProperty.Type = sourceType.Type;
			newProperty.Name = sourceType.Name;
			return newProperty;
		}
	}
}