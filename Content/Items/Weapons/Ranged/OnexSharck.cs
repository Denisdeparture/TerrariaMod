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

namespace DuckBleach.Content.Items.Weapons.Ranged
{
    public class OnexSharck : ModItem
    {
        public override void SetDefaults()
        {
            //Item.damage = 1000;
            //Item.DamageType = DamageClass.Ranged;
            //Item.shootSpeed = 24f;
            //Item.width = 128;
            //Item.height = 128;
            //Item.useTime = 3;
            //Item.useAnimation = 12;
            //Item.reuseDelay = 8;
            //Item.useLimitPerAnimation = 4;
            //Item.useStyle = ItemUseStyleID.Shoot;
            //Item.knockBack = 10;
            //Item.value = Item.buyPrice(platinum: 1);
            //Item.rare = ItemRarityID.Master;
            //Item.UseSound = SoundID.Item36;
            //Item.autoReuse = true;
            //Item.useAmmo = AmmoID.Bullet;
            //Item.noMelee = true;

            Item.width = 41; // Hitbox width of the item.
            Item.height = 16; // Hitbox height of the item.
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.Master; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 8; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 8; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.


            // Weapon Properties
            Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
            Item.damage = 1000; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 10f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            // Gun Properties
            Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 0.1f; 
            Item.useAmmo = AmmoID.Bullet; 
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

        // TODO: Move this to a more specifically named example. Say, a paint gun?
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // You can use Rnadom Main.rand.NextBool(3 for it but if you want only your prj dont us it 
            type = ModContent.ProjectileType<OnexProjectile>();

        }
    }
}
