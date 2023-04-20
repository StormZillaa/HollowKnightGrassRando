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

        public bool isObt;

        //private bool isGrass;

        public void Init()
        {
            //this is the hook i use for checking what is being hit
            ModHooks.SlashHitHook += hitInst;

            isObt = false;

            //logger to make sure all of the grass is actually loaded for debugging purposes
            //believe or not, this is *very* slow
            //Modding.Logger.Log("Loaded a grass: " + gd.getTermName());
        }

        private bool isMe(Collider2D otherCollider)
        {
            //Modding.Logger.Log("Collider name: " + otherCollider.name);

            //makes sure the game object is actually grass
            if (!otherCollider.name.ToLower().Contains("grass")) { return false; }
            //makes sure the grass being hit is in the correct scene
            if (gd.sceneName != otherCollider.gameObject.scene.name) { return false; }
            //makes sure the game object names are the same for the grasses
            if (gd.gameObj != otherCollider.gameObject.name.ToString()) { return false; }

            //gets the rough locations of the current grass
            int cldX = (int)Math.Round(otherCollider.bounds.center.x);
            int cldY = (int)Math.Round(otherCollider.bounds.center.y);

            //gets the location of the grass from the list
            int tmpX = (int)Math.Round(gd.locations[0].x);
            int tmpY = (int)Math.Round(gd.locations[0].y);

            //makes sure the grass being hit is in approximately the correct x & y locations 
            if (((cldX - 1.5) > tmpX) || ((cldX + 1.5) < tmpX)) { return false; }
            if (((cldY - 1) > tmpY) || ((cldY + 1) < tmpY)) { return false; }
            
            
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
            if (!isObt)
            {
                try
                {
                    if (isMe(otherCollider) && !Placement.AllObtained())
                    {
                        //logger to make sure only new grass was being seen
                        //Modding.Logger.Log("Broke a new grass!" + otherCollider.name.ToString() + " " + otherCollider.gameObject.scene.name.ToString());

                        //logs the grass in a usable form for debugging
                        Modding.Logger.Log(otherCollider.gameObject.name.ToString() + ", " + gd.usrName);

                        //this holds the message type that gets assigned for the
                        //item the grass drops upon breaking

                        //this is toggled in setup settings, and its here because
                        //i didnt like have 'grass' 24/7 in the bottom left
                        MessageType mt;

                        if (GrassRandoV2Mod.settings.displayPickups)
                        {
                            mt = MessageType.Corner;
                        }
                        else
                        {
                            mt = MessageType.None;
                        }

                        //attempts to give the player the item
                        Placement.GiveAll(new GiveInfo() { FlingType = FlingType.DirectDeposit, MessageType = mt });

                        //sets the obtained bool to true
                        isObt = true;
                    }
                }
                catch (Exception e)
                {
                    Modding.Logger.LogError("Error in hitInst: " + e.ToString());
                }
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
