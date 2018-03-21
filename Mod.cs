using Terraria;
using Terraria.ModLoader;
using System;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace SpaceMod
{
    public class Mod : Terraria.ModLoader.Mod
    {
        internal static Mod instance;

        public Mod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public override void Load()
        {
            instance = this;
        }

        public override void Unload()
        {
            instance = null;
        }

        public override void AddRecipes()
        {
            // Unlucky Yarn
            var recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.BlackDye, 1);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(ItemID.UnluckyYarn);
            recipe.AddRecipe();

            // Dog Whistle
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.GoldBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.DogWhistle);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PlatinumBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.DogWhistle);
            recipe.AddRecipe();
        }
    }
}

