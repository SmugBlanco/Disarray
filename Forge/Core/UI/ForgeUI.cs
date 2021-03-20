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

namespace Disarray.Forge.Core.UI
{
	public class ForgeUI : UIState
	{
		public const string AssetDirectory = "Disarray/Forge/Core/UI/Textures/ForgeUI";

		public Vector2 ForgePosition = Vector2.Zero;
		
		public ForgeUI(Vector2 forgePosition)
		{
			ForgePosition = forgePosition;
		}

		public UIDraggableImage Background;
		public ForgeItemSlot[] ItemSlots = new ForgeItemSlot[15];
		public UIImageButton CreateItem;

		public override void OnInitialize()
		{
			Texture2D bgTexture = ModContent.GetTexture(AssetDirectory + "_Background");
			Background = new UIDraggableImage(bgTexture, Color.White);
			Background.Left.Set(Main.screenWidth / 2 - bgTexture.Width / 2, 0f);
			Background.Top.Set(Main.screenHeight / 2 - bgTexture.Height / 2, 0f);
			Background.Width.Set(bgTexture.Width, 0);
			Background.Height.Set(bgTexture.Height, 0);

			for (int Index = 0; Index < ItemSlots.Length; Index++)
			{
				string iconFileName = "_BlueIcon";
				Type validType = typeof(Materials);
				ICollection<ForgeItemSlot> AccountedSlots = null;

				if (Index >= 5 && Index < 10)
				{
					iconFileName = "_BlueIcon";
					validType = typeof(Materials);
				}

				if (Index >= 10 && Index < 14)
				{
					iconFileName = "_GreenIcon";
					validType = typeof(Modifiers);
					AccountedSlots = new Collection<ForgeItemSlot>();
					for (int index = 10; index < 14; index++)
					{
						AccountedSlots.Add(ItemSlots[index]);
					}
				}

				if (Index == 14)
				{
					iconFileName = "_RedIcon";
					validType = typeof(Templates);
				}

				Texture2D iconTexture = ModContent.GetTexture(AssetDirectory + iconFileName);
				ItemSlots[Index] = new ForgeItemSlot(iconTexture, validType, AccountedSlots);
				ItemSlots[Index].Width.Set(iconTexture.Width, 0);
				ItemSlots[Index].Height.Set(iconTexture.Height, 0);
				Background.Append(ItemSlots[Index]);
			}

			int ItemSlotIndex = 0;

			void SetItemSlotPositioning(ref int Index, Vector2 Position)
			{
				ItemSlots[Index].Left.Set(Position.X, 0f);
				ItemSlots[Index].Top.Set(Position.Y, 0f);
				Index++;
			}

			//Blues
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(20, 84));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(78, 26));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(142, 26));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(206, 26));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(264, 84));

			//Yellows
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(20, 158));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(78, 216));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(142, 216));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(206, 216));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(264, 158));

			//Greens
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(6, 12));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(6, 228));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(276, 12));
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(276, 228));

			//Reds
			SetItemSlotPositioning(ref ItemSlotIndex, new Vector2(96, 112));

			Texture2D createTexture = ModContent.GetTexture(AssetDirectory + "_Create");
			CreateItem = new UIImageButton(createTexture);
			CreateItem.Width.Set(createTexture.Width, 0);
			CreateItem.Height.Set(createTexture.Height, 0);
			CreateItem.Left.Set(192, 0);
			CreateItem.Top.Set(128, 0);
            CreateItem.OnClick += CreateItem_OnClick;
			Background.Append(CreateItem);

			Append(Background);
		}

        private void CreateItem_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			int RedSlotIndex = ItemSlots.Length - 1;

			bool CanForge()
            {
				for (int Indexer = 0; Indexer < RedSlotIndex; Indexer++)
                {
					UIItemSlot itemSlot = ItemSlots[Indexer];
					if (!itemSlot.item.IsAir)
					{
						return true;
					}
				}
				return false;
			}

			if (!ItemSlots[RedSlotIndex].item.IsAir && ItemSlots[RedSlotIndex].item.type != ModContent.ItemType<ForgeItem>() && CanForge())
            {
				int TemplateType = ItemSlots[RedSlotIndex].item.type;
				ItemSlots[RedSlotIndex].item.SetDefaults();
				ItemSlots[RedSlotIndex].item.SetDefaults(ModContent.ItemType<ForgeItem>());
				ForgeItem newItem = ItemSlots[RedSlotIndex].item.modItem as ForgeItem;
				newItem.AllBases = new List<ForgeBase>
				{
					ModContent.GetModItem(TemplateType) as Templates
				};

				bool BasicItemCheck(Item item)
                {
					if (!item.IsAir && item.modItem != null)
					{
						return true;
					}
					return false;
                }

				for (int Indexer = 0; Indexer < RedSlotIndex; Indexer++)
				{
					if (BasicItemCheck(ItemSlots[Indexer].item))
					{
						newItem.AllBases.Add(Disarray.GetMod.GetItem(ItemSlots[Indexer].item.modItem?.Name) as ForgeBase);
						ItemSlots[Indexer].item.SetDefaults();
					}
				}

				newItem.SetDefaults();
			}
        }

        public override void OnDeactivate()
		{
			for (int Index = 0; Index < ItemSlots.Length; Index++)
			{
				ItemSlots[Index].ReleaseItem();
				ItemSlots[Index] = null;
			}
			Background = null;
		}

        public override void Update(GameTime gameTime)
        {
			if (!Main.playerInventory || Vector2.Distance(Main.LocalPlayer.Center, ForgePosition) > 500)
			{
				ModContent.GetInstance<Disarray>().ForgeUserInterface?.SetState(null);
				return;
			}
		}
    }
}