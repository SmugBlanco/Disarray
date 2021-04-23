namespace Disarray.Core.Autoload
{
	public abstract partial class AutoloadedClass
	{
		// Included for posterity's sake. Use ModContent.GetInstance<> as it is faster.

		public static AutoloadedClass GetClass(int ID) => (ID < 0 || ID >= LoadedClasses.Count) ? null : LoadedClasses[ID];

		public static AutoloadedClass GetClass(string name) => ClassesByName.TryGetValue(name, out AutoloadedClass classData) ? classData : null;  // string based searches are slow, might want to optimize at a later date

		public static DataType GetClass<DataType>(int ID) where DataType : AutoloadedClass => (ID < 0 || ID >= LoadedClasses.Count) ? null : LoadedClasses[ID] as DataType;

		public static DataType GetClass<DataType>(string name) where DataType : AutoloadedClass => ClassesByName.TryGetValue(name, out AutoloadedClass classData) ? classData as DataType : null;  // string based searches are slow, might want to optimize at a later date

		public static DataType GetClass<DataType>() where DataType : AutoloadedClass => ClassesByName.TryGetValue(typeof(DataType).Name, out AutoloadedClass classData) ? classData as DataType : null; // string based searches are slow, might want to optimize at a later date

		public AutoloadedClass GetData(int ID) => (ID < 0 || ID >= GetLoadedData.Count) ? null : GetLoadedData[ID];

		public AutoloadedClass GetData(string name) => GetDataByName.TryGetValue(name, out AutoloadedClass data) ? data : null;  // string based searches are slow, might want to optimize at a later date

		public DataType GetData<DataType>(int ID) where DataType : AutoloadedClass => (ID < 0 || ID >= GetLoadedData.Count) ? null : GetLoadedData[ID] as DataType;

		public DataType GetData<DataType>(string name) where DataType : AutoloadedClass => GetDataByName.TryGetValue(name, out AutoloadedClass data) ? data as DataType : null;  // string based searches are slow, might want to optimize at a later date

		public DataType GetData<DataType>() where DataType : AutoloadedClass => GetDataByName.TryGetValue(typeof(DataType).Name, out AutoloadedClass data) ? data as DataType : null;  // string based searches are slow, might want to optimize at a later date
	}
}