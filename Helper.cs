using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace DuckBleach
{
    public static class Helper
    {
        public static HelperResult CreateArmorShaderFor<T>(string byName) where T : ModItem
        {
            return new (ModContent.ItemType<T>(), byName);
        }
    }
    public record class HelperResult(int id, string name);
}
