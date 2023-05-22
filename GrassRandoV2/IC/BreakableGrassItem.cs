using ItemChanger;

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
            if (gd.sceneName.Contains("Dream_01_False_Knight")) { GrassRandoV2Mod.sd.knightDreamGrassBroken++; }
            if (gd.sceneName.Contains("Dream_02_Mage_Lord")) { GrassRandoV2Mod.sd.mageDreamGrassBroken++; }
            if (gd.sceneName.Contains("Dream_02_Mage_Lord")) { GrassRandoV2Mod.sd.mageDreamGrassBroken++; }

            Modding.Logger.Log("Total Grass: " + GrassRandoV2Mod.sd.brokenGrass.Count);

            //Modding.Logger.Log("Item given");

        }

    }
}
