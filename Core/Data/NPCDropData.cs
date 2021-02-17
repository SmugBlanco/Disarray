using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Terraria;

namespace Disarray.Core.Data
{
	public class NPCDropData
	{
		public static IEnumerable<NPCDropData> LoadedDropData;

		public virtual void NPCLoot(NPC npc, string internalName) { }

		public static void Autoload(Assembly assembly)
		{
			LoadedDropData = Enumerable.Empty<NPCDropData>();

			if (assembly == null)
			{
				return;
			}

			ICollection<NPCDropData> currentData = new Collection<NPCDropData>();

			foreach (Type item in assembly.GetTypes())
			{
				if (!item.IsAbstract && item.IsSubclassOf(typeof(NPCDropData)) && !(item.GetConstructor(new Type[0]) == null))
				{
					NPCDropData property = Activator.CreateInstance(item) as NPCDropData;
					currentData.Add(property);
				}
			}

			LoadedDropData = currentData.ToArray();
		}

		public static void Unload()
		{
			if (LoadedDropData != null)
			{
				LoadedDropData = Enumerable.Empty<NPCDropData>();
			}
		}
	}
}