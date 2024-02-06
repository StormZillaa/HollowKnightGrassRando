using ItemChanger;

namespace GrassRandoV2.IC
{
    public class BreakableGrassItem : AbstractItem
    {
        GrassRandoV2Mod modInstance = GrassRandoV2Mod.Instance;
        public string sceneName;
        public string objectName;
        public ICManager.grassdata gd;

        public override void GiveImmediate(GiveInfo info)
        {
            //modInstance.sd.grass.Add(gd.getTermName());
            if(gd.persistantBool != "") { PlayerData.instance.SetBool(gd.persistantBool, true); };
            if (gd.sceneName.Contains("Dream_01_False_Knight")) { modInstance.sd.knightDreamGrassBroken++; }
            if (gd.sceneName.Contains("Dream_02_Mage_Lord")) { modInstance.sd.mageDreamGrassBroken++; }
            if (gd.sceneName.Contains("Dream_02_Mage_Lord")) { modInstance.sd.mageDreamGrassBroken++; }

            //Modding.Logger.Log("Total Grass: " + GrassRandoV2Mod.sd.brokenGrass.Count);

            //Modding.Logger.Log("Item given");

        }

    }
}
