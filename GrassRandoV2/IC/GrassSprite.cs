using System;
using UnityEngine;
using ItemChanger;
using ItemChanger.Internal;

namespace GrassRandoV2.IC
{
    [Serializable]
    public class GrassSprite : ISprite
    {
        private static SpriteManager ebsm = new(typeof(GrassSprite).Assembly, "GrassRandoV2.Resources.Sprites.");

        public string key;
        public GrassSprite(string key)
        {
            this.key = key;
        }

        [Newtonsoft.Json.JsonIgnore]
        public Sprite Value => ebsm.GetSprite("grass-icon.png");
        public ISprite Clone() => (ISprite)MemberwiseClone();
    }
}
