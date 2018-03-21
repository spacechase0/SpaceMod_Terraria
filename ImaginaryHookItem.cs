using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpaceMod
{
    public class ImaginaryHookItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imaginary Hook");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.DualHook);
            item.shootSpeed = 25;
            item.shoot = mod.ProjectileType("ImaginaryHookProjectile");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 25);
            recipe.AddIngredient(ItemID.SoulofFlight, 50);
            recipe.AddIngredient(ItemID.Ectoplasm, 50);
            recipe.AddIngredient(ItemID.LunarHook);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
