using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Linq;

namespace Disarray.Core.Forge.Items
{
	public class ForgeItem : ForgeBase
	{
		public ForgeItem() { }

		public Templates ForgedTemplate => AllBases.First(template => template is Templates) as Templates;

		public ICollection<ForgeBase> AllBases = new List<ForgeBase>();

		public IEnumerable<ForgeBase> UniqueBases => AllBases.Distinct();

		public override bool CloneNewInstances => true;

		public override ModItem Clone(Item item)
        {
			ForgeItem newItem = item.modItem as ForgeItem ?? MemberwiseClone() as ForgeItem;
			ForgeItem oldItem = this;
			newItem.AllBases = oldItem.AllBases.ToList();
			return newItem;
		}

		public void Reset()
        {
			AllBases.Clear();
		}

		public override string ItemStatistics()
		{
			int TotalDamage = (int)((float)(item.damage + DamageFlat) * (Damage + 1));
			float Multiplier = ((float)item.damage / (float)ForgedTemplate.item.damage) * (Damage + 1);
			string DamageStatistics = TotalDamage == 0 ? string.Empty : "Totaled Damage Bonus: " + TotalDamage + " ( Base: " + ForgedTemplate.item.damage + " | Additive : " + DamageFlat + " | Multiplier : " + Multiplier + " )";

			string DefenseStatistics = Defense == 0 ? string.Empty : "Totaled Defense Bonus: " + Defense;

			string DamageReductionStatistics = DamageReduction == 0 ? string.Empty : "Totaled Damage Reduction Bonus: " + DamageReduction + "%";

			string MaxHealthStatistics = MaxHealth == 0 ? string.Empty : "Totaled Max Health Bonus: " + MaxHealth + "%";

			return DamageStatistics + "\n" + DefenseStatistics + "\n" + DamageReductionStatistics + "\n" + MaxHealthStatistics;
		}

		public override string ObtainingDetails()
		{
			return "Created in 'The Forge'";
		}

        public override void SetDefaults()
		{
			if (AllBases.Count <= 0)
			{
				item.width = 32;
				item.height = 32;
				Reset();
				return;
			}

			ForgedTemplate.SafeDefaults(item);

			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.ImplementedItem = this;
            }

			item.maxStack = 1;
			item.consumable = false;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (ForgedTemplate != null)
			{
				Texture2D newTexture = ItemTextureData.TryGetValue(ForgedTemplate.item.type, out Texture2D texture) ? texture : Main.itemTexture[ForgedTemplate.item.type];

				Rectangle defaultItemFrame = Main.itemTexture[item.type].Frame();
				float defaultDynamicScale = 1f;
				if (defaultItemFrame.Width > 32 || defaultItemFrame.Height > 32)
				{
					defaultDynamicScale = (defaultItemFrame.Width <= defaultItemFrame.Height) ? (32f / (float)defaultItemFrame.Height) : (32f / (float)defaultItemFrame.Width);
				}
				defaultDynamicScale *= Main.inventoryScale;
				Vector2 OriginalPosition = position + defaultItemFrame.Size() * defaultDynamicScale / 2f;

				Rectangle newTextureFrame = newTexture.Frame();
				float newDynamicScale = 1f;
				if (newTextureFrame.Width > 32 || newTextureFrame.Height > 32)
				{
					newDynamicScale = (newTextureFrame.Width <= newTextureFrame.Height) ? (32f / (float)newTextureFrame.Height) : (32f / (float)newTextureFrame.Width);
				}
				newDynamicScale *= Main.inventoryScale;
				spriteBatch.Draw(newTexture, OriginalPosition, newTextureFrame, drawColor, 0f, newTextureFrame.Size() / 2, newDynamicScale, SpriteEffects.None, 0f);
				return false;
			}
			return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
			if (ForgedTemplate != null)
			{
				Texture2D newTexture = ItemTextureData.TryGetValue(ForgedTemplate.item.type, out Texture2D texture) ? texture : Main.itemTexture[ForgedTemplate.item.type];
				spriteBatch.Draw(newTexture, item.position - Main.screenPosition, newTexture.Frame(), lightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        public override bool CanUseItem(Player player)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				if (!forgeBase.CanUseItem(player))
				{
					return false;
				}
			}
			return true;
		}

		public sealed override void HoldItem(Player player) { } // Called too late to be useful

		public void HoldItem_Functional(Player player)
        {
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.HoldItem(player);
			}

			ImplementStats(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				Projectile newProjectile = forgeBase.ShootButBetter(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);

				if (newProjectile != null)
                {
					foreach (ForgeBase forgeBaseAgain in AllBases)
					{
						forgeBaseAgain.ModifyFiredProjectiles(newProjectile);
					}
				}
			}
			return false;
		}

		public override bool? CanHitNPC(Player player, NPC target)
		{
			bool? CanHit = true;
			foreach (ForgeBase forgeBase in AllBases)
			{
				CanHit = forgeBase.CanHitNPC(player, target);
				if (CanHit.HasValue)
				{
					if (!CanHit.Value)
					{
						return false;
					}

					CanHit = true;
				}
			}
			return CanHit;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.OnHitNPC(player, target, damage, knockBack, crit);
			}
		}

		public override bool CanHitPvp(Player player, Player target)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				if (!forgeBase.CanHitPvp(player, target))
				{
					return false;
				}
			}
			return true;
		}

		public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.ModifyHitPvp(player, target, ref damage, ref crit);
			}
		}

		public override void OnHitPvp(Player player, Player target, int damage, bool crit)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.OnHitPvp(player, target, damage, crit);
			}
		}

		public override void UpdateEquip(Player player)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.UpdateEquip(player);
			}

			ImplementStats(player);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.UpdateAccessory(player, hideVisual);
			}

			ImplementStats(player);
		}

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
			if (!Main.dedServ && !player.HeldItem.noMelee)
			{
				Texture2D actualItemTexture = WeaponTextureData.TryGetValue(item.type, out Texture2D WeaponTexture) ? WeaponTexture : ItemTextureData.TryGetValue(item.type, out Texture2D ItemTexture) ? ItemTexture : Main.itemTexture[item.type];
				hitbox.Width = (int)((float)hitbox.Width * ((float)actualItemTexture.Width / (float)Main.itemTexture[item.type].Width));
				hitbox.Height = (int)((float)hitbox.Height * ((float)actualItemTexture.Height / (float)Main.itemTexture[item.type].Height));
			}

			foreach (ForgeBase forgeBase in AllBases)
			{
				forgeBase.UseItemHitbox(player, ref hitbox, ref noHitbox);
			}
		}

        public override bool NewPreReforge()
		{
			return false;
		}

		public override TagCompound Save()
		{
			if (AllBases.Count <= 0)
            {
				return null;
            }

			List<string> SavedData = new List<string>();
			foreach (ForgeBase bases in AllBases)
            {
				SavedData.Add(bases.Name);
            }

			return new TagCompound
			{
				{ "ForgedBases", SavedData },
			};
		}

		public override void Load(TagCompound tag)
		{
			AllBases = new List<ForgeBase>();
			AllBases.Clear();
			List<string> SavedData = tag.Get<List<string>>("ForgedBases");
			List<ForgeBase> allBases = new List<ForgeBase>();
			foreach (string bases in SavedData)
			{
				allBases.Add(mod.GetItem(bases) as ForgeBase);
			}
			AllBases = allBases.ToList();
			allBases.Clear();
			SetDefaults();
		}

		/*public override void NetSend(BinaryWriter writer)
		{
			List<string>[] SavedForgedMaterials = new List<string>[3];
			for (int Indexer = 0; Indexer < SavedForgedMaterials.Length; Indexer++)
			{
				SavedForgedMaterials[Indexer] = new List<string>();
			}

			foreach (Materials materials in ForgedMaterials)
			{
				SavedForgedMaterials[0].Add(materials.Name);
			}

			foreach (Components components in ForgedComponents)
			{
				SavedForgedMaterials[1].Add(components.Name);
			}

			foreach (Modifiers modifiers in ForgedModifiers)
			{
				SavedForgedMaterials[2].Add(modifiers.Name);
			}

			writer.Write(ForgedTemplate.Name);
			writer.Write(ForgedMaterials.Count);
			foreach (string materials in SavedForgedMaterials[0])
			{
				writer.Write(materials);
			}

			writer.Write(ForgedComponents.Count);
			foreach (string components in SavedForgedMaterials[1])
			{
				writer.Write(components);
			}

			writer.Write(ForgedModifiers.Count);
			foreach (string modifiers in SavedForgedMaterials[2])
			{
				writer.Write(modifiers);
			}
		}

        public override void NetRecieve(BinaryReader reader)
        {
			ForgedTemplate = (Templates)reader.ReadItem().modItem;
			for (int Index = 0; Index < reader.ReadInt32(); Index++)
            {
				ForgedMaterials.Add((Materials)mod.GetItem(reader.ReadString()));
			}

			for (int Index = 0; Index < reader.ReadInt32(); Index++)
			{
				ForgedComponents.Add((Components)mod.GetItem(reader.ReadString()));
			}

			for (int Index = 0; Index < reader.ReadInt32(); Index++)
			{
				ForgedModifiers.Add((Modifiers)mod.GetItem(reader.ReadString()));
			}
		}*/
    }
}