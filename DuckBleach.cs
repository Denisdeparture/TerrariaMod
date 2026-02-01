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


        private const string ShaderPath = "Assets/Effects/";

        public const string CalamityShaderPrefix = "DuckBleach:";

        public const string ShaderName = "Bankai";

        public override void Load()
        {
            Instance = this;

            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Effect> BankaiShader = this.Assets.Request<Effect>(ShaderPath + ShaderName);

                Filters.Scene.Bind($"{CalamityShaderPrefix}{ShaderName}", new Filter(new ScreenShaderData(BankaiShader, ShaderName), EffectPriority.Medium));
            }
        }
    }
}
