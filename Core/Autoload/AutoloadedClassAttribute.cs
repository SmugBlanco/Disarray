using System;

namespace Disarray.Core.Autoload
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]	
	public class AutoloadedClassAttribute : Attribute // May want to come up with a better solution later
	{
	}
}