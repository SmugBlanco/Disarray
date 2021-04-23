using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using System.Collections.Generic;
using Disarray.Forge.Core.Items;
using System;

namespace Disarray.Forge.Core
{
	public class ForgeItemPlayerLayers : ModPlayer
	{
        /*public float ItemUseProgress => 1 - (player.itemAnimationMax > 0 ? (float)player.itemAnimation / (float)player.itemAnimationMax : 0); // 1 = end, 0 = beginning

        public static void HandleSwingThrow(Player player, Item item, ref Texture2D texture, ref Vector2 drawPosition, ref Rectangle sourceRectangle, ref Color drawColor, ref float rotation, ref Vector2 drawOrigin, ref float scale, ref SpriteEffects spriteEffects)
        {
            ForgeItemPlayerLayers modPlayer = player.GetModPlayer<ForgeItemPlayerLayers>();

            Vector2 drawPositionOffset = new Vector2(0 * player.direction, 0);

            if (modPlayer.ItemUseProgress <= 0.333)
            {
                drawPositionOffset = new Vector2(-4 * player.direction, -4);
            }
            else if (modPlayer.ItemUseProgress <= 0.666)
            {
                drawPositionOffset = new Vector2(0 * player.direction, -4);
            }

            Vector2 playerCenter = player.mount.Type == -1 ? player.Center : player.MountedCenter;
            drawPosition = playerCenter + drawPositionOffset - Main.screenPosition;
            float InitialRot = -120 * player.direction;
            float IdealRot = 180 * player.direction;
            rotation = player.itemRotation = MathHelper.ToRadians(InitialRot + (IdealRot * modPlayer.ItemUseProgress));
            drawOrigin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height);
            spriteEffects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public static void HandleStab(Player player, Item item, ref Texture2D texture, ref Vector2 drawPosition, ref Rectangle sourceRectangle, ref Color drawColor, ref float rotation, ref Vector2 drawOrigin, ref float scale, ref SpriteEffects spriteEffects)
        {
            ForgeItemPlayerLayers modPlayer = player.GetModPlayer<ForgeItemPlayerLayers>();

            Vector2 drawPositionOffset = new Vector2(4 * player.direction, 1);

            if (modPlayer.ItemUseProgress <= 0.75f)
            {
                float TotalMovement = 6f * (float)player.direction;
                drawPositionOffset.X += TotalMovement * modPlayer.ItemUseProgress;
            }
            else
            {
                drawPositionOffset.X += 2f * (float)player.direction;
            }

            Vector2 playerCenter = player.mount.Type == -1 ? player.Center : player.MountedCenter;
            drawPosition = playerCenter + drawPositionOffset - Main.screenPosition;
            rotation = player.itemRotation = MathHelper.ToRadians(45 * player.direction);
            drawOrigin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height);
            spriteEffects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public static void HandleHoldOut(Player player, Item item, ref Texture2D texture, ref Vector2 drawPosition, ref Rectangle sourceRectangle, ref Color drawColor, ref float rotation, ref Vector2 drawOrigin, ref float scale, ref SpriteEffects spriteEffects)
        {
            ForgeItemPlayerLayers modPlayer = player.GetModPlayer<ForgeItemPlayerLayers>();

            Vector2 drawPositionOffset = new Vector2(-2 * player.direction, 1);
            Vector2 playerCenter = player.mount.Type == -1 ? player.Center : player.MountedCenter;
            drawPosition = playerCenter + drawPositionOffset - Main.screenPosition;
            rotation = player.itemRotation;
            drawOrigin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height / 2);

            if (item.modItem is ForgeItem forgeItem && forgeItem.GetTemplate != null && Item.staff[forgeItem.GetTemplate.item.type])
            {
                rotation = player.itemRotation + (player.direction == 1 ? MathHelper.PiOver4 : -MathHelper.PiOver4);
                drawOrigin = new Vector2(player.direction == 1 ? 0 : texture.Width, texture.Height);
            }

            spriteEffects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public static readonly PlayerLayer HeldDraw = new PlayerLayer("Disarray", "ForgeItemHeld", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;

            Item item = drawPlayer.HeldItem;
            ForgeItem forgeItem = item.modItem as ForgeItem;
            if (forgeItem.GetTemplate is Crossbow crossbow)
			{
                float ClampRot(float Rot, float ClampRange)
                {
                    if (Rot > MathHelper.ToRadians(-ClampRange) && Rot < MathHelper.ToRadians(ClampRange))
                    {
                        return Rot = MathHelper.ToRadians(ClampRange) * Math.Sign(Rot);
                    }
                    return Rot;
                }

                Vector2 Center = drawPlayer.mount.Type == -1 ? drawPlayer.Center : drawPlayer.MountedCenter;
                Vector2 DrawPosition = new Vector2(Center.X + (6 * drawPlayer.direction), Center.Y - 3) - Main.screenPosition;
                Rectangle sourceRect = new Rectangle(0, crossbow.LoadedType > 0 ? crossbow.HeldTexture.Height / 2 : 0, crossbow.HeldTexture.Width, crossbow.HeldTexture.Height / 2);
                Color drawColor = Lighting.GetColor((int)(Center.X / 16f), (int)(Center.Y / 16f));
                float CurrentRotation = (Main.MouseWorld - Center).ToRotation();
                float ClampRotation = crossbow.ReloadTimer > 0 ? 45 : 90;
                float RotIfFaceRight = MathHelper.Clamp(CurrentRotation, MathHelper.ToRadians(-ClampRotation), MathHelper.ToRadians(ClampRotation));
                ClampRotation = crossbow.ReloadTimer > 0 ? 135 : 90;
                float RotIfFaceLeft = ClampRot(CurrentRotation, ClampRotation);
                float Rotation = drawPlayer.direction > 0 ? RotIfFaceRight : RotIfFaceLeft;
                Vector2 origin = new Vector2(5, 10);
                DrawData data = new DrawData(crossbow.HeldTexture, DrawPosition, sourceRect, drawColor, Rotation, origin, 1f, drawPlayer.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically, 0);
                Main.playerDrawData.Add(data);
                return;
			}

            if (drawPlayer.itemAnimation > 0)
            {
                if (item.noUseGraphic)
                {
                    return;
                }

                Texture2D newTexture = OldForgeBase.WeaponTextureData.TryGetValue(forgeItem.GetTemplate.item.type, out Texture2D WeaponTexture) ? WeaponTexture : OldForgeBase.ItemTextureData.TryGetValue(forgeItem.GetTemplate.item.type, out Texture2D ItemTexture) ? ItemTexture : Main.itemTexture[forgeItem.GetTemplate.item.type];
                Vector2 drawPosition = drawPlayer.Center - Main.screenPosition;
                Rectangle sourceRect = new Rectangle(0, 0, newTexture.Width, newTexture.Height);
                Color drawColor = Color.White;
                float rotation = drawPlayer.itemRotation;
                Vector2 origin = Vector2.Zero;
                float itemScale = item.scale;
                SpriteEffects spriteEffects = SpriteEffects.None;

                switch (item.useStyle)
                {
                    case 1:
                        HandleSwingThrow(drawPlayer, item, ref newTexture, ref drawPosition, ref sourceRect, ref drawColor, ref rotation, ref origin, ref itemScale, ref spriteEffects);
                        break;

                    case 3:
                        HandleStab(drawPlayer, item, ref newTexture, ref drawPosition, ref sourceRect, ref drawColor, ref rotation, ref origin, ref itemScale, ref spriteEffects);
                        break;

                    case 5:
                        HandleHoldOut(drawPlayer, item, ref newTexture, ref drawPosition, ref sourceRect, ref drawColor, ref rotation, ref origin, ref itemScale, ref spriteEffects);
                        break;
                }

                foreach (OldForgeBase bases in forgeItem.AllBases)
                {
                    bases.PreDrawWeaponAnimation(ref newTexture, ref drawPosition, ref sourceRect, ref drawColor, ref rotation, ref origin, ref itemScale, ref spriteEffects);
                }

                DrawData data = new DrawData(newTexture, drawPosition, sourceRect, drawColor, rotation, origin, itemScale, spriteEffects, 0);
                Main.playerDrawData.Add(data);
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (player.HeldItem.modItem is ForgeItem forgeItem)
            {
                int Index = layers.IndexOf(PlayerLayer.HeldItem);
                PlayerLayer.HeldItem.visible = false;
                HeldDraw.visible = true;
                layers.Insert(Index + 1, HeldDraw);
            }
        }

        public override void PostUpdate()
        {
            if (player.HeldItem.modItem is ForgeItem forgeItem && forgeItem.GetTemplate is Crossbow)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
            }
        }*/

		public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff) // throwing this here because why not, might want to move it later but eh
        {
            if (player.HeldItem?.modItem is ForgeItem forgeItem)
            {
                forgeItem.HoldItem_Functional(player);
            }
        }
    }
}