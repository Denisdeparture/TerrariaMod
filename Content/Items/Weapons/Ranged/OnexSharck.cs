using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using DuckBleach.Content.Projectiles;
using Microsoft.Xna.Framework.Input;

namespace DuckBleach.Content.Items.Weapons.Ranged
{
    public class OnexSharck : ModItem
    {
        public const int Width = 82;
        public const int Height = 32;
        public bool IsFirst = true;
        public bool MegaShoot = false;
        public int TimeBeforeMegaShoot = 30;
        public double Nowtimer = 0;
        public double Oldtimer = 0;
        public override void SetDefaults()
        {
            Item.width = Width; // Hitbox width of the item.
            Item.height = Height; // Hitbox height of the item.
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Master; // The color that the item's name will be in-game.
            Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 1000; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 10f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 0.1f; 
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item36;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.Megashark, 1);
            recipe.AddIngredient(ItemID.OnyxBlaster, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
        public override bool? UseItem(Player player)
        {
            if (Main.keyState.IsKeyDown(Keys.G))
            {
                if (IsFirst)
                {
                    Oldtimer = Main.time + TimeBeforeMegaShoot;
                    IsFirst = false;
                }
                Nowtimer = Main.time;
                if (Nowtimer >= Oldtimer)
                {
                    MegaShoot = true;
                    Oldtimer = 0;
                    Nowtimer = 0;
                    IsFirst = true;
                }
                SoundEngine.PlaySound(SoundID.Clown, player.position);

                return false;
            }
            else
            {
                IsFirst = true;
                Nowtimer = 0;
                MegaShoot = false;
                Oldtimer = 0;
            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // You can use Rnadom Main.rand.NextBool(3 for it but if you want only your prj dont us it
            //
            if (Main.keyState.IsKeyDown(Keys.G) & MegaShoot == false)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(30));
                return;
            }
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));

            if (MegaShoot)
            {
                RotateBullet(ref position, ref velocity);
                type = ModContent.ProjectileType<MegaOnexProjectile>();
            }
            else
            {
                RotateBullet(ref position, ref velocity);
                type = ModContent.ProjectileType<OnexProjectile>();
            }

        }
        private void RotateBullet(ref Vector2 position, ref Vector2 velocity)
        {
            var rotation = (int)Math.Round(velocity.ToRotation() * 100);
            var offsetX = 40;

            var offsetY = 10 + velocity.Y;

            position.Y -= offsetY;

            if (rotation > 170)
            {
                position.Y -= offsetY;
                position.X -= (offsetX / 2);
            }
            if (rotation < 0 | rotation > 200)
            {
                position.X -= offsetX;
            }
        }
    }
}
