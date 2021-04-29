using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Linq;
using System.Collections.ObjectModel;

namespace Disarray.Forge.Core.Items
{
	public class ForgeItem : ForgeCore
	{
		public ForgeTemplate GetTemplate => AllBases.FirstOrDefault(template => template is ForgeTemplate) as ForgeTemplate;

		public override string Texture => "Disarray/Forge/Core/Items/ForgeItem/ForgeItem";

		public ICollection<ForgeCore> AllBases = new Collection<ForgeCore>();

		public float Quality { get; internal set; }

		public override bool CloneNewInstances => true;

		public override ModItem Clone(Item item)
		{
			ForgeItem newItem = item.modItem as ForgeItem ?? MemberwiseClone() as ForgeItem;
			ForgeItem oldItem = this;
			newItem.Quality = oldItem.Quality;
			newItem.AllBases = oldItem.AllBases.ToList();
			return newItem;
		}

		public override void SetDefaults()
		{
			if (AllBases.Count <= 0 || GetTemplate is null)
			{
				item.width = 32;
				item.height = 32;
				AllBases.Clear();
				return;
			}

			try
			{
				foreach (ForgeCore forgeCore in AllBases)
				{
					forgeCore.ImplementedItem = this;
				}

				GetTemplate.SafeDefaults(item, Quality);
			}
			catch
			{
				Main.NewText("Error. Boo Hoo.");
			}
			item.maxStack = 1;
			item.consumable = false;
		}

		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (GetTemplate != null)
			{
				Rectangle defaultItemFrame = Main.itemTexture[item.type].Frame();
				float defaultDynamicScale = 1f;
				if (defaultItemFrame.Width > 32 || defaultItemFrame.Height > 32)
				{
					defaultDynamicScale = (defaultItemFrame.Width <= defaultItemFrame.Height) ? (32f / defaultItemFrame.Height) : (32f / defaultItemFrame.Width);
				}
				defaultDynamicScale *= Main.inventoryScale;
				Vector2 OriginalPosition = position + defaultItemFrame.Size() * defaultDynamicScale / 2f;

				if (GetTemplate.PreDrawForgeItem(spriteBatch, OriginalPosition, drawColor))
				{
					Texture2D newTexture = ItemTextureData.TryGetValue(GetTemplate.item.type, out Texture2D texture) ? texture : Main.itemTexture[GetTemplate.item.type];

					Rectangle newTextureFrame = newTexture.Frame();
					float newDynamicScale = 1f;
					if (newTextureFrame.Width > 32 || newTextureFrame.Height > 32)
					{
						newDynamicScale = (newTextureFrame.Width <= newTextureFrame.Height) ? (32f / newTextureFrame.Height) : (32f / newTextureFrame.Width);
					}
					newDynamicScale *= Main.inventoryScale;


					spriteBatch.Draw(newTexture, OriginalPosition, newTextureFrame, drawColor, 0f, newTextureFrame.Size() / 2, newDynamicScale, SpriteEffects.None, 0f);
				}

				return false;
			}
			return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (GetTemplate != null)
			{
				Texture2D newTexture = ItemTextureData.TryGetValue(GetTemplate.item.type, out Texture2D texture) ? texture : Main.itemTexture[GetTemplate.item.type];
				spriteBatch.Draw(newTexture, item.position - Main.screenPosition, newTexture.Frame(), lightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				return false;
			}
			return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
		}

		public override bool CanUseItem(Player player)
		{
			foreach (ForgeCore forgeBase in AllBases)
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
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.HoldItem(player);
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			foreach (ForgeCore forgeBase in AllBases)
			{
				IEnumerable<Projectile> newProjectile = forgeBase.ShootButBetter(player, item, forgeBase.item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);

				if (newProjectile != null)
				{
					foreach (ForgeCore forgeBaseAgain in AllBases)
					{
						foreach (Projectile projectile in newProjectile)
						{
							forgeBaseAgain.ModifyFiredProjectiles(projectile);
						}
					}
				}
			}
			return false;
		}

		public override bool? CanHitNPC(Player player, NPC target)
		{
			bool? CanHit = true;
			foreach (ForgeCore forgeBase in AllBases)
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
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.OnHitNPC(player, target, damage, knockBack, crit);
			}
		}

		public override bool CanHitPvp(Player player, Player target)
		{
			foreach (ForgeCore forgeBase in AllBases)
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
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.ModifyHitPvp(player, target, ref damage, ref crit);
			}
		}

		public override void OnHitPvp(Player player, Player target, int damage, bool crit)
		{
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.OnHitPvp(player, target, damage, crit);
			}
		}

		public override void UpdateEquip(Player player)
		{
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.UpdateEquip(player);
			}
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.UpdateAccessory(player, hideVisual);
			}
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			if (!Main.dedServ && !player.HeldItem.noMelee)
			{
				Texture2D actualItemTexture = WeaponTextureData.TryGetValue(item.type, out Texture2D WeaponTexture) ? WeaponTexture : ItemTextureData.TryGetValue(item.type, out Texture2D ItemTexture) ? ItemTexture : Main.itemTexture[item.type];
				hitbox.Width = (int)((float)hitbox.Width * ((float)actualItemTexture.Width / (float)Main.itemTexture[item.type].Width));
				hitbox.Height = (int)((float)hitbox.Height * ((float)actualItemTexture.Height / (float)Main.itemTexture[item.type].Height));
			}

			foreach (ForgeCore forgeBase in AllBases)
			{
				forgeBase.UseItemHitbox(player, ref hitbox, ref noHitbox);
			}
		}

		public override bool NewPreReforge() => false;

		public override TagCompound Save()
		{
			if (AllBases.Count <= 0)
			{
				return null;
			}

			return new TagCompound
			{
				{ "AllBases", AllBases.Select(forgeCore => forgeCore.Name).ToList() },
				{ "Quality", Quality },
			};
		}

		public override void Load(TagCompound tag)
		{
			IList<string> savedData = tag.Get<List<string>>("AllBases");
			AllBases = savedData.Select(data => mod.GetItem(data) as ForgeCore).ToList();
			Quality = tag.Get<float>("Quality");
			SetDefaults();
		}

		public override bool Equals(object obj) => obj is ForgeItem forgeItem && item.type == forgeItem.item.type && EqualityComparer<ICollection<ForgeCore>>.Default.Equals(AllBases, forgeItem.AllBases);
		
		public override int GetHashCode() => 1425257430 + EqualityComparer<ICollection<ForgeCore>>.Default.GetHashCode(AllBases) + item.type;
	}
}