using MenuChanger;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using MenuChanger.Extensions;
using static RandomizerMod.Localization;
using RandomizerMod.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RandomizerMod.RandomizerData;

namespace GrassRandoV2.Rando
{

    public class GrassRandoSettings
    {
        public bool randomizeGrass = false;
        public bool randomizeQuantumGrass = false;
        public bool randomizeDreamNailGrass = false;
        public bool addShop = false;

        public bool kingsPassAndDM = false;
        public bool crossroads = false;
        public bool greenpath = false;
        public bool fogCan = false;
        public bool gardens = false;
        public bool fungal = false;
        //public bool resting = false;
        public bool edge = false;
        public bool deepNest = false;
        public bool abyssAndBasin = false;
        public bool whitePalace = false;

        //GrassLocationSettings gls = new();

        [Newtonsoft.Json.JsonIgnore]
        public bool anygrass => randomizeGrass || randomizeQuantumGrass || kingsPassAndDM || crossroads || greenpath
            || fogCan || gardens || fungal || edge || deepNest || abyssAndBasin || whitePalace;
    }

    public class RandoMenuPage
    {

        internal MenuPage grassRandoPage;
        internal MenuElementFactory<GrassRandoSettings> grassMEF;
        internal GridItemPanel grassVIP;
        internal SmallButton openGrassRandoSet;
        //internal SmallButton openLocRandoSet;
        internal List<bool> gRandoElm;

        //internal MenuPage locPage;
        //internal LocationMenuPage locMenuPage;

        //internal SmallButton openLocationSettings;

        internal static RandoMenuPage Instance { get; private set; }

        public static void OnExitMenu()
        {
            Instance = null;
        }

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(ConstructMenu, HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private static bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            button = Instance.openGrassRandoSet;
            return true;
        }

        private void SetTopLevelButtonColor()
        {
            if (openGrassRandoSet != null)
            {
                openGrassRandoSet.Text.color = Colors.DEFAULT_COLOR;
            }
        }

        private static void ConstructMenu(MenuPage landingPage) => Instance = new(landingPage);

        public void PasteSettings(GrassRandoSettings settings)
        {
            if (settings == null )
            {
                grassMEF.ElementLookup[nameof(GrassRandoSettings.randomizeGrass)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.randomizeQuantumGrass)].SetValue(false);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.randomizeDreamNailGrass)].SetValue(false);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.addShop)].SetValue(false);

                grassMEF.ElementLookup[nameof(GrassRandoSettings.kingsPassAndDM)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.crossroads)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.greenpath)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.fogCan)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.gardens)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.fungal)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.whitePalace)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.edge)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.deepNest)].SetValue(true);
                grassMEF.ElementLookup[nameof(GrassRandoSettings.abyssAndBasin)].SetValue(true);

                return;
            }

            grassMEF.SetMenuValues(GrassRandoV2Mod.settings);
            //locMenuPage.locMEF.SetMenuValues(GrassRandoV2Mod.settings);

        }

        private RandoMenuPage(MenuPage lp)
        {
            grassRandoPage = new MenuPage("Grass Rando Settings", lp);
            grassMEF = new(grassRandoPage, GrassRandoV2Mod.settings);
            grassVIP = new(grassRandoPage, new(0, 300), 3, 75f, 450, true, grassMEF.Elements);

            //locMenuPage = new LocationMenuPage(grassRandoPage);


            foreach (IValueElement e in grassMEF.Elements)
            {
                e.SelfChanged += obj => SetTopLevelButtonColor();
            }

            openGrassRandoSet = new(lp, Localize("Grass Rando"));
            openGrassRandoSet.AddHideAndShowEvent(lp, grassRandoPage);

            //openLocRandoSet = new(grassRandoPage, "Location Settings");
            //openLocRandoSet.AddHideAndShowEvent(grassRandoPage, locMenuPage.lPage);

            SetTopLevelButtonColor();

        }



    }
}
