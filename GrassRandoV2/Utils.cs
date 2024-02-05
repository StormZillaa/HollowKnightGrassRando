using GrassCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GrassRandoV2
{
    /// <summary>
    /// Minor extensions to aid porting to GrassCore without requiring a complete overhaul.
    /// </summary>
    internal static class Utils
    {
        public static GrassKey ToGrassKey(this IC.ICManager.grassdata gd)
        {
            Vector2 pos = new(gd.locations.First().x, gd.locations.First().y);
            return new GrassKey(gd.sceneName, gd.gameObj, pos);
        }
    }
}
