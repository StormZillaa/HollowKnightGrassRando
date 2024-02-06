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
            LocationRegistrar.Instance.Add(this);
            if (gd.fsmType == "quantum grass")
            {

            }
        }

        protected override void OnUnload() { }

        public void Obtain()
        {
            if (!Placement.AllObtained())
            {
                MessageType mt = GrassRandoV2Mod.settings.DisplayPickups ? MessageType.Corner : MessageType.None;
                Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = mt });
            } else
            {
                GrassRandoV2Mod.Instance.LogWarn($"Obtain called on pre-obtained grass {gd.ToGrassKey()}");
            }
        }
    }
}
