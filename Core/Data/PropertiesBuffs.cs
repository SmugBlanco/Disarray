using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace Disarray.Core.Data
{
	public class PropertiesBuffs
	{
		public static ICollection<PropertiesBuffs> LoadedBuffProperties;
		public static IDictionary<int, PropertiesBuffs> BuffPropertiesID;
		public static IDictionary<string, PropertiesBuffs> BuffPropertiesNameID;

		private static int InternalID = -1;

		public int NumberOfBuffProperties => InternalID;

		public string name;

		public int type { get; internal set; }

		public virtual void Update(NPC npc) { }

		public virtual void Update(Player player) { }

		public virtual void UpdateLifeRegen(NPC npc, ref int damage) { }

		public virtual void UpdateBadLifeRegen(Player player) { }

		public static void AutoloadBuffProperties(Assembly assembly)
		{
			LoadedBuffProperties = new Collection<PropertiesBuffs>();
			BuffPropertiesID = new Dictionary<int, PropertiesBuffs>();
			BuffPropertiesNameID = new Dictionary<string, PropertiesBuffs>();

			if (assembly == null)
			{
				return;
			}

			Mod mod = ModLoader.GetMod("Disarray");

			foreach (Type item in assembly.GetTypes())
			{
				if (!item.IsAbstract && item.IsSubclassOf(typeof(PropertiesBuffs)) && !(item.GetConstructor(new Type[0]) == null))
				{
					PropertiesBuffs property = Activator.CreateInstance(item) as PropertiesBuffs;
					property.type = ++InternalID;
					property.name = item.Name;
					LoadedBuffProperties.Add(property);
					BuffPropertiesID.Add(property.type, property);
					BuffPropertiesNameID.Add(property.name, property);
				}
			}

			foreach (Type item in assembly.GetTypes())
			{
				if (!item.IsAbstract && item.IsSubclassOf(typeof(DisarrayBuff)) && !(item.GetConstructor(new Type[0]) == null))
				{
					if (BuffPropertiesNameID.TryGetValue(item.Name, out PropertiesBuffs property))
					{
						ModBuff buff = mod.GetBuff(item.Name); // Probably should use a more elegant solution
						DisarrayBuff disarrayBuff = buff as DisarrayBuff;
						disarrayBuff.Properties = property;
					}
					else
					{
						throw new Exception(item.FullName + "'s properties not loaded");
					}
				}
			}
		}

		public static void Unload()
		{
			if (LoadedBuffProperties != null)
			{
				LoadedBuffProperties.Clear();
			}

			if (BuffPropertiesID != null)
			{
				BuffPropertiesID.Clear();
			}

			InternalID = 0;
		}
	}
}