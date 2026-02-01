using DuckBleach.Content.Items.Armor.Shinigami;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckBleach.ConstStorage
{
    public static class Effects 
    {
        // use prefix Armor|Miscu|Scene 

        public static string SceneBankai { get; set; } = "Bankai"; // only name of scene

        public static HelperResult ArmorBankai => Helper.CreateArmorShaderFor<ShinigamiChest>("Shinigami");

    }

}
