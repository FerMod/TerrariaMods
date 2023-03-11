using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CuboJuan.Items {

   public class CuboJuanPetBuff : ModBuff {

      public override string Texture => "CuboJuan/Assets/Textures/Buff_CuboJuan";

      public override void SetStaticDefaults() {
         DisplayName.SetDefault(Language.GetTextValue("Mods.CuboJuan.Buffs.CuboJuanPetBuff.DisplayName"));
         Description.SetDefault(Language.GetTextValue("Mods.CuboJuan.Buffs.CuboJuanPetBuff.Description"));

         Main.buffNoTimeDisplay[Type] = true;
         Main.vanityPet[Type] = true;
      }

      public override void Update(Player player, ref int buffIndex) { // This method gets called every frame your buff is active on your player.
         player.buffTime[buffIndex] = 18000;

         int projType = ModContent.ProjectileType<CuboJuanPetProjectile>();

         // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
         if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0) {
            var entitySource = player.GetSource_Buff(buffIndex);
            Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
         }
      }

   }

}