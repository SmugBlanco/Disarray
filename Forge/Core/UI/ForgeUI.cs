using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using System.Collections.Generic;
using Disarray.Core.UI;
using Disarray.Forge.Core.Items;
using System.Collections.ObjectModel;
using System.Linq;
using Terraria.ID;

namespace Disarray.Forge.Core.UI
{
	public class ForgeUI : UIState
	{
		public const string CommonAssetDirectory = "Disarray/Forge/Core/UI";

		public ForgeUI(Vector2 forgePosition)
		{
			ForgePosition = forgePosition;
			MaterialSlots = new ForgeItemSlot[5];
			ComponentSlots = new ForgeItemSlot[5];
			ModifierSlots = new ForgeItemSlot[3];
		}

		public Vector2 ForgePosition { get; private set; }

		public UIDraggableImage Background { get; private set; }

		public UIImageButton ForgeButton { get; private set; }

		public ForgeItemSlot GetTemplateSlot => TemplateSlot;

		private ForgeItemSlot TemplateSlot;

		public ForgeItemSlot[] MaterialSlots { get; private set; }

		public ForgeItemSlot[] ComponentSlots { get; private set; }

		public ForgeItemSlot[] ModifierSlots { get; private set; }

		public UIImageButton CloseButton { get; private set; }

		public bool Locked { get; set; } = true;

		public UIImageButton LockButton { get; private set; }

		public UIImageButton ResetButton { get; private set; }

		public override void OnInitialize()
		{
			Texture2D backgroundTexture = ModContent.GetTexture(CommonAssetDirectory + "/ForgeUI");
			Background = new UIDraggableImage(backgroundTexture, Color.White);
			Background.Left.Set(Main.screenWidth / 2 - backgroundTexture.Width / 2, 0f);
			Background.Top.Set(Main.screenHeight / 2 - backgroundTexture.Height / 2, 0f);
			Background.Width.Set(backgroundTexture.Width, 0);
			Background.Height.Set(backgroundTexture.Height, 0);

			void InitializeItemSlot(ref ForgeItemSlot itemSlot, Texture2D texture, Vector2 position, Func<Item, Item, bool> limiter)
			{
				itemSlot = new ForgeItemSlot(texture, limiter);
				itemSlot.Left.Set(position.X, 0);
				itemSlot.Top.Set(position.Y, 0);
				itemSlot.Width.Set(texture.Width, 0);
				itemSlot.Height.Set(texture.Height, 0);
			}

			InitializeItemSlot(ref TemplateSlot, ModContent.GetTexture(CommonAssetDirectory + "/Textures/TemplateSlot"), new Vector2(152, 196), (template, item) => item.modItem is ForgeTemplate);
			TemplateSlot.OnItemChange += InitializeSlots;
			TemplateSlot.OnItemChange += InitializeForgeButton;
			Background.Append(TemplateSlot);

			for (int index = 0; index < MaterialSlots.Length; index++)
			{
				Vector2 slotPosition;
				switch (index)
				{
					case 1:
						slotPosition = new Vector2(34, 186);
						break;

					case 2:
						slotPosition = new Vector2(80, 186);
						break;

					case 3:
						slotPosition = new Vector2(34, 232);
						break;

					case 4:
						slotPosition = new Vector2(80, 232);
						break;

					default:
						slotPosition = new Vector2(58, 140);
						break;
				}

				InitializeItemSlot(ref MaterialSlots[index], ModContent.GetTexture(CommonAssetDirectory + "/Textures/MaterialSlot"), slotPosition, (oldItem, item) => item.modItem is ForgeMaterial material && (TemplateSlot.Item.IsAir || material.PreInsert(TemplateSlot.Item.modItem as ForgeTemplate)));
				MaterialSlots[index].OnItemChange += InitializeForgeButton;
			}

			for (int index = 0; index < ComponentSlots.Length; index++)
			{
				Vector2 slotPosition;
				switch (index)
				{
					case 1:
						slotPosition = new Vector2(224, 186);
						break;

					case 2:
						slotPosition = new Vector2(270, 186);
						break;

					case 3:
						slotPosition = new Vector2(224, 232);
						break;

					case 4:
						slotPosition = new Vector2(270, 232);
						break;

					default:
						slotPosition = new Vector2(246, 140);
						break;
				}

				InitializeItemSlot(ref ComponentSlots[index], ModContent.GetTexture(CommonAssetDirectory + "/Textures/ComponentSlot"), slotPosition, (oldItem, item) => item.modItem is ForgeComponent component && (TemplateSlot.Item.IsAir || component.PreInsert(TemplateSlot.Item.modItem as ForgeTemplate)));
			}

			for (int index = 0; index < ModifierSlots.Length; index++)
			{
				Vector2 slotPosition;
				switch (index)
				{
					case 1:
						slotPosition = new Vector2(104, 36);
						break;

					case 2:
						slotPosition = new Vector2(150, 36);
						break;

					default:
						slotPosition = new Vector2(196, 36);
						break;
				}

				InitializeItemSlot(ref ModifierSlots[index], ModContent.GetTexture(CommonAssetDirectory + "/Textures/ModifierSlot"), slotPosition, (template, item) => item.modItem is ForgeMaterial modifier && (TemplateSlot.Item.IsAir || modifier.PreInsert(TemplateSlot.Item.modItem as ForgeTemplate)));
			}

			Texture2D forgeButtonTexture = ModContent.GetTexture(CommonAssetDirectory + "/ForgeCreate");
			ForgeButton = new UIImageButton(forgeButtonTexture);
			ForgeButton.Left.Set(136, 0f);
			ForgeButton.Top.Set(226, 0f);
			ForgeButton.Width.Set(forgeButtonTexture.Width, 0);
			ForgeButton.Height.Set(forgeButtonTexture.Height, 0);
			ForgeButton.OnClick += OnForge;

			Texture2D closeButtonTexture = ModContent.GetTexture("Disarray/Assets/UI/Close");
			CloseButton = new UIImageButton(closeButtonTexture);
			CloseButton.Left.Set(328, 0f);
			CloseButton.Top.Set(2, 0f);
			CloseButton.Width.Set(closeButtonTexture.Width, 0);
			CloseButton.Height.Set(closeButtonTexture.Height, 0);
			CloseButton.OnClick += (UIMouseEvent evt, UIElement listeningElement) => Close();
			Background.Append(CloseButton);

			Texture2D lockButtonTexture = ModContent.GetTexture("Disarray/Assets/UI/Locked");
			LockButton = new UIImageButton(lockButtonTexture);
			LockButton.Left.Set(328, 0f);
			LockButton.Top.Set(22, 0f);
			LockButton.Width.Set(lockButtonTexture.Width, 0);
			LockButton.Height.Set(lockButtonTexture.Height, 0);
			LockButton.OnClick += ToggleLocking;
			Background.Append(LockButton);
			Background.PreDrag = (evt) => !Locked && !LockButton.ContainsPoint(evt.MousePosition);

			Texture2D resetButtonTexture = ModContent.GetTexture("Disarray/Assets/UI/Reset");
			ResetButton = new UIImageButton(resetButtonTexture);
			ResetButton.Left.Set(326, 0f);
			ResetButton.Top.Set(42, 0f);
			ResetButton.Width.Set(resetButtonTexture.Width, 0);
			ResetButton.Height.Set(resetButtonTexture.Height, 0);
			ResetButton.OnClick += (UIMouseEvent evt, UIElement listeningElement) => ToAllSlots(slot => { slot.ReleaseItem(true); slot.Item.SetDefaults(); });
			Background.Append(ResetButton);

			Append(Background);
		}

		private void ToAllSlots(Action<ForgeItemSlot> toSlot, bool includeTemplateSlot = true)
		{
			if (includeTemplateSlot)
			{
				toSlot.Invoke(TemplateSlot);
			}

			foreach (ForgeItemSlot itemSlot in MaterialSlots)
			{
				toSlot.Invoke(itemSlot);
			}

			foreach (ForgeItemSlot itemSlot in ComponentSlots)
			{
				toSlot.Invoke(itemSlot);
			}

			foreach (ForgeItemSlot itemSlot in ModifierSlots)
			{
				toSlot.Invoke(itemSlot);
			}
		}

		private void InitializeSlots()
		{
			if (TemplateSlot.Item.IsAir)
			{
				ToAllSlots(slot => { slot.ReleaseItem(); slot.Item.SetDefaults(); slot.Remove(); }, false);
			}
			else
			{
				ToAllSlots(slot => Background.Append(slot), false);
			}
		}

		private void InitializeForgeButton()
		{
			if (TemplateSlot.Item.IsAir)
			{
				ForgeButton.Remove();
				return;
			}

			bool CheckMaterialSlot()
			{
				foreach (ForgeItemSlot itemSlot in MaterialSlots)
				{
					if (!itemSlot.Item.IsAir)
					{
						return true;
					}
				}
				return false;
			}

			if (CheckMaterialSlot())
			{
				Background.Append(ForgeButton);
			}
			else
			{
				ForgeButton.Remove();
			}
		}

		private void OnForge(UIMouseEvent evt, UIElement listeningElement)
		{
			try
			{
				Item newItem = new Item();
				newItem.SetDefaults(ModContent.ItemType<ForgeItem>());
				ForgeItem forgeItem = newItem.modItem as ForgeItem;
				ICollection<ForgeItemSlot> allSlots = MaterialSlots.Concat(ComponentSlots).Concat(ModifierSlots).ToList();
				allSlots.Add(TemplateSlot);
				IEnumerable<ForgeCore> forgeCores = from slot in allSlots where !slot.Item.IsAir select slot.Item.Clone().modItem as ForgeCore;
				forgeItem.AllBases = forgeCores.ToList();

				foreach (ForgeCore forgeCore in from items in forgeItem.AllBases where items is ForgeMaterial select items)
				{
					ForgeTemplate template = forgeItem.GetTemplate;
					ForgeMaterial material = forgeCore as ForgeMaterial;
					foreach ((string identity, float qualityInfluence) materialType in material.MaterialIdentity)
					{
						if (template.MaterialTypeInfluence.TryGetValue(materialType.identity, out float influence))
						{
							forgeItem.Quality += influence * materialType.qualityInfluence;
						}
					}
				}

				forgeItem.SetDefaults();

				foreach (ForgeItemSlot itemSlot in allSlots)
				{
					itemSlot.Item.SetDefaults();
				}

				TemplateSlot.Item = newItem;
			}
			catch
			{
				Main.NewText("An error has occured that prevents forging from completing.");
			}
		}

		public override void OnDeactivate()
		{
			ToAllSlots(slot => slot.ReleaseItem(true));
		}

		public override void Update(GameTime gameTime)
		{
			if (!Main.playerInventory)
			{
				Close();
				return;
			}
		}

		private void Close()
		{
			Main.PlaySound(SoundID.MenuClose);
			ModContent.GetInstance<Disarray>().ForgeUserInterface?.SetState(null);
		}

		private void ToggleLocking(UIMouseEvent evt, UIElement listeningElement)
		{
			Locked = !Locked;
			LockButton.SetImage(ModContent.GetTexture("Disarray/Assets/UI/" + (Locked ? "Locked" : "Unlocked")));
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (CloseButton.ContainsPoint(Main.MouseScreen))
			{
				Main.mouseText = true;
				Main.instance.MouseText("Close");
			}

			if (LockButton.ContainsPoint(Main.MouseScreen))
			{
				Main.mouseText = true;
				Main.instance.MouseText(Locked ? "Unlock" : "Lock");
			}

			if (ResetButton.ContainsPoint(Main.MouseScreen))
			{
				Main.mouseText = true;
				Main.instance.MouseText("Reset");
			}
		}
	}
}