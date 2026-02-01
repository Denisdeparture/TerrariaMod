using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ITEM = Terraria.Item;


namespace DuckBleach.Content.Items.Armor.Shinigami
{
    [AutoloadEquip(EquipType.Legs)]
    public class ShinigamiPants : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = ITEM.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 16); // add custom ingredient 
            recipe.AddTile(TileID.WorkBenches); // change work space
            recipe.Register();
        }
    }
}
