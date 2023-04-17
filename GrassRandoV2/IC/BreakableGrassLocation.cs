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
            //this is the hook i assumed would work
            //however, it appears it misses some grass
            ModHooks.SlashHitHook += hitInst;

            //my attempt at adding redundancy to the cutting trigger of the grass
            //On.GrassCut.ShouldCut += cutInst; //note: this did not change much :/


            //logger to make sure all of the grass is actually loaded for debugging purposes
            //believe or not, this is *very* slow
            //Modding.Logger.Log("Loaded a grass: " + gd.getTermName());
        }

        private bool cutInst(On.GrassCut.orig_ShouldCut orig, Collider2D collision)
        {
            bool isCut = orig(collision);

            try
            {
                if (isCut)
                {
                    if (isMe(collision) && !Placement.AllObtained())
                    {
                        //logger to make sure only new grass was being seen
                        //Modding.Logger.Log("Broke a new grass!" + otherCollider.name.ToString() + " " + otherCollider.gameObject.scene.name.ToString());

                        //logs the grass that was broken in a usable format for debugging
                        Modding.Logger.Log(collision.gameObject.name.ToString() + ", " + gd.usrName);

                        //attempts to give the player the item
                        Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = MessageType.None });

                    }
                }
            }
            catch (Exception e)
            {
                Modding.Logger.LogError("Error in cutInst: " + e.ToString());
            }

            return isCut;
        }

        private bool isMe(Collider2D otherCollider)
        {
            //Modding.Logger.Log("Collider name: " + otherCollider.name);

            //makes sure the game object is actually grass
            if (!otherCollider.name.ToLower().Contains("grass")) { return false; }

            //gets the rough locations of the current grass
            int cldX = (int)Math.Round(otherCollider.bounds.center.x);
            int cldY = (int)Math.Round(otherCollider.bounds.center.y);

            //gets the location of the grass from the list
            int tmpX = (int)Math.Round(gd.locations[0].x);
            int tmpY = (int)Math.Round(gd.locations[0].y);

            //makes sure the grass being hit is in the correct scene
            if (gd.sceneName != otherCollider.gameObject.scene.name) { Modding.Logger.Log("Bad Scene name: " + otherCollider.name + ", " + otherCollider.gameObject.scene.name);  return false; }
            //makes sure the grass being hit is in approximately the correct x & y locations 
            if (((cldX - 1.5) > tmpX) || ((cldX + 1.5) < tmpX)) { Modding.Logger.Log("Bad X Loc: " + otherCollider.name + ", " + otherCollider.gameObject.scene.name); return false; }
            if (((cldY - 1) > tmpY) || ((cldY + 1) < tmpY)) { Modding.Logger.Log("Bad Y Loc: " + otherCollider.name + ", " + otherCollider.gameObject.scene.name); return false; }
            //makes sure the game object names are the same for the grasses
            if (gd.gameObj != otherCollider.gameObject.name.ToString()) { Modding.Logger.Log("Bad gameObj name: " + otherCollider.name + ", " + otherCollider.gameObject.scene.name); return false; }
            
            //makes sure the grass being broken is not in the broken list

            //it turns out, for some reason checking the object against the broken grass list
            // sometimes causes the grass to never mark as broken
            //if (GrassRandoV2Mod.sd.brokenGrass.Contains(gd.getTermName())) { Modding.Logger.Log("Already in List: " + otherCollider.name + ", " + otherCollider.gameObject.scene.name); return false; }

            //otherwise it should* be the correct grass
            return true;
        }

        //checks each hit instance to make sure it is a grass and in the correct location
        private void hitInst(Collider2D otherCollider, GameObject slash)
        {
            try{
                if (isMe(otherCollider) && !Placement.AllObtained())
                {
                    //logger to make sure only new grass was being seen
                    //Modding.Logger.Log("Broke a new grass!" + otherCollider.name.ToString() + " " + otherCollider.gameObject.scene.name.ToString());

                    //logs the grass in a usable form for debugging
                    Modding.Logger.Log(otherCollider.gameObject.name.ToString() + ", " + gd.usrName);

                    //attempts to give the player the item
                    Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = MessageType.None });

                }
            }
            catch (Exception e)
            {
                Modding.Logger.LogError("Error in hitInst: " + e.ToString());
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
