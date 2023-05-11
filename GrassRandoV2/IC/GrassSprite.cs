using System;
using UnityEngine;
using ItemChanger;
using ItemChanger.Internal;

namespace GrassRandoV2.IC
{
    [Serializable]
    public class GrassSprite : ISprite
    {
        private static SpriteManager ebsm = new(typeof(GrassSprite).Assembly, "GrassRandoV2.Resources.");

        public GrassSprite()
        {

        }

        [Newtonsoft.Json.JsonIgnore]
        public Sprite Value => ebsm.GetSprite("grassIcon.jpeg");
        public ISprite Clone() => (ISprite)MemberwiseClone();
    }
}
