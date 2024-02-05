using ItemChanger.Locations;
using ItemChanger;
using UnityEngine;
using System;
using HutongGames.PlayMaker;
using Modding;
using static GrassRandoV2.IC.ICManager;
using GrassCore;
using System.Linq;

namespace GrassRandoV2.IC
{
    public class BreakableGrassLocation : AutoLocation
    {

        public string objectName;
        public string fsmType;
        public grassdata gd;

        //private bool isGrass;

        protected override void OnLoad()
        {
            // Hook GrassCore's event;
            GrassCore.GrassEventDispatcher.Instance.GrassWasCut += GrassBroken;
            if (gd.fsmType == "quantum grass")
            {

            }

        }

        private void GrassBroken(GrassKey grassKey)
        {
            if (!CompareGrassBreak(grassKey)) { return; }
            
            if (!Placement.AllObtained())
            {
                GrassRegister_Rando.Instance.TryCut(grassKey);
                MessageType mt = GrassRandoV2Mod.settings.DisplayPickups ? MessageType.Corner : MessageType.None;
                Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = mt });
            }
        }

        private bool CompareGrassBreak(GrassKey grassKey)
        {
            if (grassKey.SceneName != sceneName) { return false; }
            if (grassKey.ObjectName != objectName) { return false; }
            return grassKey.Position == new Vector2(gd.locations.First().x, gd.locations.First().y);
        }



        protected override void OnUnload()
        {
        }

    }
}
