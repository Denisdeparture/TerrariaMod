using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DuckBleach
{
    public class DuckBleachKeybindSystem : ModSystem
    {
        public static ModKeybind BankaiHotKey { get; private set; }
        public static ModKeybind HyperShotHotKey { get; private set; }
        public override void Load()
        {
            // Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)
            // Register keybinds            
            BankaiHotKey = KeybindLoader.RegisterKeybind(Mod, "Bankai", Microsoft.Xna.Framework.Input.Keys.Q);
            HyperShotHotKey = KeybindLoader.RegisterKeybind(Mod, "HyperShot", Microsoft.Xna.Framework.Input.Keys.G);
        }
        public override void Unload()
        {
            BankaiHotKey = null;
        }
    }
}
