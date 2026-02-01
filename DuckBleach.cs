using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DuckBleach
{
	public class DuckBleach : Mod
	{
        public static DuckBleach Instance;


      

        public override void Load()
        {
            Instance = this;

            if (Main.netMode != NetmodeID.Server)
            {
                EffectManager.LoadAll();
            }
        }
    }
}
