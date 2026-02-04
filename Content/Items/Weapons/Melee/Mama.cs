using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.RGB;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace DuckBleach.Content.Items.Weapons.Melee
{
	public class Mama : ModItem
	{
		public double GlobalTimer;

		public bool IsFirst = true;

        public double UsageTime = 10.0;

        public override bool? UseItem(Player player)
        {
			const string AttackShader = "Bankai";
           
            if (Main.keyState.IsKeyDown(Keys.Q))
            {

                if (IsFirst)
				{
                    GlobalTimer = Main.time + UsageTime;

                    IsFirst = false;
                }

				if(Main.time == GlobalTimer)
				{
                    Filters.Scene.Deactivate($"{EffectManager.DuckShaderPrefix}{AttackShader}");
					IsFirst = true;
                }

                float lerpAmount = Main.GameUpdateCount % 60 / 60f;

                var shader = Filters.Scene[$"{EffectManager.DuckShaderPrefix}{AttackShader}"].GetShader();
				

                shader.Shader.Parameters["uTime"].SetValue(lerpAmount);

                Filters.Scene.Activate($"{EffectManager.DuckShaderPrefix}{AttackShader}");
            }
			else
			{
                Filters.Scene.Deactivate($"{EffectManager.DuckShaderPrefix}{AttackShader}");
            }

			return base.UseItem(player);
        }
     
        public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = true;

        }
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
