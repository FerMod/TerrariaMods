using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CuboJuan.Items {

   public class CuboJuanPetProjectile : ModProjectile {

      public override string Texture => "CuboJuan/Assets/Textures/Projectile_CuboJuan";

      private const string DeathSoundPath = "CuboJuan/Assets/Sounds/Death_CuboJuan";
      private static readonly float[] screamCooldown = new float[byte.MaxValue];

      public override void SetStaticDefaults() {
         DisplayName.SetDefault(Language.GetTextValue("Mods.CuboJuan.Projectiles.CuboJuanPetProjectile.DisplayName"));

         Main.projPet[Projectile.type] = true;
      }

      public override void SetDefaults() {
         // Copy the stats of the Companion Cube projectile
         Projectile.CloneDefaults(ProjectileID.CompanionCube);

         Projectile.width = 34;
         Projectile.height = 34;

         AIType = ProjectileID.CompanionCube;
      }

      public override void AI() {
         Player player = Main.player[Projectile.owner];

         // Keep the projectile from disappearing as long as the player has the pet buff
         if (player.HasBuff(ModContent.BuffType<CuboJuanPetBuff>())) {
            Projectile.timeLeft = 2;
         }

         Tile tileSafely = Framing.GetTileSafely(Projectile.Center);
         Projectile.localAI[0] += tileSafely.LiquidType == LiquidID.Lava ? 1 : -1;
         --screamCooldown[Projectile.owner];
         Projectile.localAI[0] = MathHelper.Clamp(Projectile.localAI[0], 0.0f, 20f);
         if (Projectile.localAI[0] >= 20.0 && screamCooldown[Projectile.owner] <= 0.0) {
            screamCooldown[Projectile.owner] = 3600f;
            SoundEngine.PlaySound(new SoundStyle(DeathSoundPath), Projectile.position);
         }

      }

   }

}
