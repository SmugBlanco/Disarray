using Disarray.Content.Paintings.Catacombs.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace Disarray.Content.Reagents
{
	public class Parchment : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Parchment");
			Tooltip.SetDefault("'A blank piece of parchment'");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 22;
			item.maxStack = 999;
			item.value = 30;
		}

		public static void CreatePaintingRecipe(int parchmentCount, int result)
		{
			ModRecipe recipe = new ModRecipe(Disarray.GetMod);
			recipe.AddIngredient(ModContent.ItemType<Parchment>(), parchmentCount);
			recipe.AddIngredient(ItemID.BlackInk);
			recipe.SetResult(result);
			recipe.AddRecipe();
		}

		internal static void TravellingMerchantPaintings()
		{
			CreatePaintingRecipe(24, ItemID.PaintingAcorns);

			CreatePaintingRecipe(24, ItemID.PaintingCastleMarsberg);

			CreatePaintingRecipe(24, ItemID.PaintingColdSnap);

			CreatePaintingRecipe(24, ItemID.PaintingCursedSaint);

			CreatePaintingRecipe(24, ItemID.PaintingMartiaLisa);

			CreatePaintingRecipe(24, ItemID.MoonLordPainting);

			CreatePaintingRecipe(24, ItemID.PaintingTheSeason);

			CreatePaintingRecipe(24, ItemID.PaintingSnowfellas);
		}

		internal static void CabinPaintings()
		{
			CreatePaintingRecipe(6, ItemID.AmericanExplosive);

			CreatePaintingRecipe(9, ItemID.CrownoDevoursHisLunch);

			CreatePaintingRecipe(9, ItemID.Discover);

			CreatePaintingRecipe(9, ItemID.FatherofSomeone);

			CreatePaintingRecipe(6, ItemID.FindingGold);

			CreatePaintingRecipe(6, ItemID.GloriousNight);

			CreatePaintingRecipe(9, ItemID.GuidePicasso);

			CreatePaintingRecipe(6, ItemID.Land);

			CreatePaintingRecipe(9, ItemID.TheMerchant);

			CreatePaintingRecipe(9, ItemID.NurseLisa);

			CreatePaintingRecipe(9, ItemID.OldMiner);

			CreatePaintingRecipe(9, ItemID.RareEnchantment);

			CreatePaintingRecipe(9, ItemID.Sunflowers);

			CreatePaintingRecipe(9, ItemID.TerrarianGothic);

			CreatePaintingRecipe(6, ItemID.Waldo);
		}

		internal static void DungeonPaintings()
		{
			CreatePaintingRecipe(9, ItemID.BloodMoonRising);

			CreatePaintingRecipe(9, ItemID.BoneWarp);

			CreatePaintingRecipe(24, ItemID.TheCreationoftheGuide);

			CreatePaintingRecipe(9, ItemID.TheCursedMan);

			CreatePaintingRecipe(24, ItemID.TheDestroyer);

			CreatePaintingRecipe(24, ItemID.Dryadisque);

			CreatePaintingRecipe(24, ItemID.TheEyeSeestheEnd);

			CreatePaintingRecipe(24, ItemID.FacingtheCerebralMastermind);

			CreatePaintingRecipe(9, ItemID.GloryoftheFire);

			CreatePaintingRecipe(24, ItemID.GoblinsPlayingPoker);

			CreatePaintingRecipe(24, ItemID.GreatWave);

			CreatePaintingRecipe(9, ItemID.TheGuardiansGaze);

			CreatePaintingRecipe(9, ItemID.TheHangedMan);

			CreatePaintingRecipe(24, ItemID.Impact);

			CreatePaintingRecipe(24, ItemID.ThePersistencyofEyes);

			CreatePaintingRecipe(24, ItemID.PoweredbyBirds);

			CreatePaintingRecipe(24, ItemID.TheScreamer);

			CreatePaintingRecipe(9, ItemID.SkellingtonJSkellingsworth);

			CreatePaintingRecipe(24, ItemID.SparkyPainting);

			CreatePaintingRecipe(24, ItemID.SomethingEvilisWatchingYou);

			CreatePaintingRecipe(24, ItemID.StarryNight);

			CreatePaintingRecipe(24, ItemID.TrioSuperHeroes);

			CreatePaintingRecipe(24, ItemID.TheTwinsHaveAwoken);

			CreatePaintingRecipe(24, ItemID.UnicornCrossingtheHallows);
		}

		internal static void UnderworldPaintings()
		{
			CreatePaintingRecipe(6, ItemID.DarkSoulReaper);

			CreatePaintingRecipe(6, ItemID.Darkness);

			CreatePaintingRecipe(6, ItemID.DemonsEye);

			CreatePaintingRecipe(6, ItemID.FlowingMagma);

			CreatePaintingRecipe(9, ItemID.HandEarth);

			CreatePaintingRecipe(9, ItemID.ImpFace);

			CreatePaintingRecipe(24, ItemID.LakeofFire);

			CreatePaintingRecipe(6, ItemID.LivingGore);

			CreatePaintingRecipe(9, ItemID.OminousPresence);

			CreatePaintingRecipe(9, ItemID.ShiningMoon);

			CreatePaintingRecipe(9, ItemID.Skelehead);

			CreatePaintingRecipe(6, ItemID.TrappedGhost);
		}

		internal static void GoodieBagPaintings()
		{
			CreatePaintingRecipe(24, ItemID.BitterHarvest);

			CreatePaintingRecipe(24, ItemID.BloodMoonCountess);

			CreatePaintingRecipe(24, ItemID.HallowsEve);

			CreatePaintingRecipe(24, ItemID.JackingSkeletron);

			CreatePaintingRecipe(24, ItemID.MorbidCuriosity);
		}

		internal static void CatacombPaintings()
		{
			CreatePaintingRecipe(24, ModContent.ItemType<GenesisItem>());

			CreatePaintingRecipe(24, ModContent.ItemType<RootsItem>());

			CreatePaintingRecipe(24, ModContent.ItemType<VictoryItem>());

			CreatePaintingRecipe(24, ModContent.ItemType<SandsOfTimeItem>());
		}

		internal static void Books()
		{
			ModRecipe recipe = new ModRecipe(Disarray.GetMod);
			recipe.AddIngredient(ModContent.ItemType<Parchment>(), 12);
			recipe.AddIngredient(ItemID.BlackInk);
			recipe.SetResult(ItemID.Book);
			recipe.AddRecipe();
		}

		public override void AddRecipes()
		{
			TravellingMerchantPaintings();

			CabinPaintings();

			DungeonPaintings();

			UnderworldPaintings();

			GoodieBagPaintings();

			CatacombPaintings();

			Books();
		}
	}
}