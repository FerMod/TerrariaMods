using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;

namespace CuboJuan.Items {

   public class CuboJuanPetItem : ModItem {

      public override string Texture => "CuboJuan/Assets/Textures/Item_CuboJuan";

      public override void SetStaticDefaults() {
         DisplayName.SetDefault(Language.GetTextValue("Mods.CuboJuan.Items.CuboJuanPetItem.DisplayName"));
         Tooltip.SetDefault(Language.GetTextValue("Mods.CuboJuan.Items.CuboJuanPetItem.Tooltip"));

         CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
      }

      public override void SetDefaults() {
         Item.CloneDefaults(ItemID.CompanionCube);

         Item.rare = ItemRarityID.Master;
         Item.shoot = ModContent.ProjectileType<CuboJuanPetProjectile>(); // "Shoot" pet projectile
         Item.buffType = ModContent.BuffType<CuboJuanPetBuff>(); // Apply buff upon usage of the Item
      }

      public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
         player.AddBuff(Item.buffType, 2); // The item applies the buff, the buff spawns the projectile
         return false;
      }

      public override void AddRecipes() {
         // Add recipe to craft this item from Companion Cube
         CreateRecipe()
            .AddIngredient(ItemID.CompanionCube)
            .Register();

         // Add recipe to craft Companion Cube from this item
         Recipe.Create(ItemID.CompanionCube)
            .AddIngredient<CuboJuanPetItem>()
            .Register();
      }

   }

}
