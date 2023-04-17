using System.Linq;
using ItemChanger.Locations;
using ItemChanger;
using Satchel;
using ItemChanger.Util;
using UnityEngine;
using HutongGames.PlayMaker.Actions;
using System;
using ItemChanger.Placements;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using Modding;
using static GrassRandoV2.IC.ICManager;

namespace GrassRandoV2.IC
{
    public class BreakableGrassLocation : AutoLocation
    {

        public string objectName;
        public string fsmType;
        public grassdata gd;
        public Fsm fsm;

        //private bool isGrass;

        public void Init()
        {
            ModHooks.SlashHitHook += hitInst;
            //On.GrassCut.ShouldCut += HandleShouldCut;
            //isGrass = false;
            Modding.Logger.Log("Loaded a grass: " + gd.getTermName());
        }


        private bool isMe(Collider2D otherCollider)
        {
            int cldX = (int)Math.Round(otherCollider.bounds.center.x);
            int cldY = (int)Math.Round(otherCollider.bounds.center.y);

            int tmpX = (int)Math.Round(gd.locations[0].x);
            int tmpY = (int)Math.Round(gd.locations[0].y);

            if (gd.sceneName != otherCollider.gameObject.scene.name) { return false; }
            if (((cldX - 1) > tmpX) || ((cldX + 1) < tmpX)) { return false; }
            if (((cldY - 1) > tmpY) || ((cldY + 1) < tmpY)) { return false; }
            if (gd.gameObj != otherCollider.gameObject.name.ToString()) { return false; }
            if (GrassRandoV2Mod.sd.brokenGrass.Contains(gd.getTermName())) { return false; }

            if(!otherCollider.name.Contains("grass")) { return false; }

            return true;
        }

        //checks each hit instance to make sure it is a grass and in the correct location
        private void hitInst(Collider2D otherCollider, GameObject slash)
        {
            int cldX = (int)Math.Round(otherCollider.bounds.center.x);
            int cldY = (int)Math.Round(otherCollider.bounds.center.y);

            int tmpX = (int)Math.Round(gd.locations[0].x);
            int tmpY = (int)Math.Round(gd.locations[0].y);

            if (isMe(otherCollider) && !Placement.AllObtained())
            {
                //logger to make sure only new grass was being seen
                //Modding.Logger.Log("Broke a new grass!" + otherCollider.name.ToString() + " " + otherCollider.gameObject.scene.name.ToString());

                Modding.Logger.Log(otherCollider.gameObject.name.ToString() + ", " + gd.usrName);

                //var item = Finder.GetItem(gd.getItemName());
                Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = MessageType.None });

            }
        }

        protected override void OnLoad()
        {
            this.Init();
            if (gd.fsmType == "quantum grass")
            {

            }

        }

        protected override void OnUnload()
        {
        }

    }
}
