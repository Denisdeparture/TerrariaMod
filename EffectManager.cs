using DuckBleach.ConstStorage;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace DuckBleach
{
    public static class EffectManager
    {
        // Basic confs
        private const string ShaderPath = "Assets/Effects/";

        public const string DuckShaderPrefix = "DuckBleach:";

        public static void LoadAll()
        {
            var type = typeof(Effects);

            var effects = type.GetProperties();

            foreach ( var effect in effects )
            {
                if (!effect.Name.Contains("Miscu") & !effect.Name.Contains("Scene") & !effect.Name.Contains("Armor"))
                {
                    continue;
                }
                // because all type effect has length == 5
                var name = effect.Name.Substring(5, effect.Name.Length - 5);

                var typeOp = effect.Name.Substring(0, 5);

                if(typeOp is "Armor")
                {
                    var value = effect.GetValue(null) as HelperResult;

                    Switcher(typeOp, value.name, value.id);
                }
                else
                {
                    Switcher(typeOp, name);
                }
            }
        }
        public static void Switcher(string type, string value, int? id = null)
        {
            switch(type)
            {
                case "Miscu":
                    LoadMisc(value);
                    break;
                case "Scene":
                    LoadScene(value);
                    break;
                case "Armor":
                    LoadArmor(value, (int)id);
                    break;
            }
        }
        public static void LoadArmor(string name, int id)
        {
            Asset<Effect> Shader = DuckBleach.Instance.Assets.Request<Effect>(ShaderPath + name);

            GameShaders.Armor.BindShader(id, new ArmorShaderData(Shader, name));
        }

        public static void LoadMisc(string name)
        {
            Asset<Effect> Shader = DuckBleach.Instance.Assets.Request<Effect>(ShaderPath + name);

            GameShaders.Misc[$"{DuckShaderPrefix}{name}"] = new MiscShaderData(Shader, name);
        }

        public static void LoadScene(string name)
        {
            Asset<Effect> Shader = DuckBleach.Instance.Assets.Request<Effect>(ShaderPath + name);

            Filters.Scene.Bind($"{DuckShaderPrefix}{name}", new Filter(new ScreenShaderData(Shader, name), EffectPriority.Medium));
        }
    }
}
