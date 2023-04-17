using ItemChanger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RandoMapMod;

namespace GrassRandoV2.IC
{
    public class BreakableGrassItem : AbstractItem
    {
        public string sceneName;
        public string objectName;
        public ICManager.grassdata gd;

        public override void GiveImmediate(GiveInfo info)
        {
            GrassRandoV2Mod.sd.brokenGrass.Add(gd.getTermName());
            if(gd.persistantBool != "") { PlayerData.instance.SetBool(gd.persistantBool, true); };

            Modding.Logger.Log("Total Grass: " + GrassRandoV2Mod.sd.brokenGrass.Count);

            //Modding.Logger.Log("Item given");

        }

    }
}
