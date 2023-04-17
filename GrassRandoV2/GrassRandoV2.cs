using Modding;
using System;
using System.Collections.Generic;
using GrassRandoV2.IC;
using GrassRandoV2.Rando;
using RandoSettingsManager;
using RandoSettingsManager.SettingsManagement;



namespace GrassRandoV2
{
    public class SaveData
    {
        public List<string> brokenGrass = new List<string>();
    }
    public class GrassRandoV2Mod : Mod, ILocalSettings<SaveData>, IGlobalSettings<GrassRandoSettings>
    {
        public static SaveData sd = new SaveData();
        //public static RandoSettings settings = new RandoSettings(new GrassRandoSettings(), new GrassLocationSettings());
        public static GrassRandoSettings settings = new GrassRandoSettings();
        //public static GrassLocationSettings gls = settings.GetLocationSettings();

        //public static GrassRandoSettings st = new GrassRandoSettings();
        //public static GrassLocationSettings locSt = new GrassLocationSettings();

        //sets up the ui mod stuff for the mod manager
        new public string GetName() => "Grass Randomizer";
        public override string GetVersion() => "v0.2";

        private static GrassRandoV2Mod? _instance;

        internal static GrassRandoV2Mod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(GrassRandoV2Mod)} was never constructed");
                }
                return _instance;
            }
        }

        //returns the current instance of the mod
        public GrassRandoV2Mod() : base("GrassRandoV2")
        {
            _instance = this;
        }

        //initilizations for the mod
        public override void Initialize()
        {
            Log("Prepping the Grando");

            //item config manager, makes things more organized
            ICManager manager = new();

            //hooks for the rando menu pages
            RandoMenuPage.Hook();

            if (ModHooks.GetMod("RandoSettingsManager") is Mod)
            {
                HookRSM();
            }

            //registers the items, locations, and then sets up the item hooks
            manager.RegisterItemsAndLocations();
            manager.Hook();

            Log("Grando is ready");
        }

        //sets up the hook for the rando settings managers
        private void HookRSM()
        {
            RandoSettingsManagerMod.Instance.RegisterConnection(new SimpleSettingsProxy<GrassRandoSettings>(
                this,
                (st) => RandoMenuPage.Instance.PasteSettings(st),
                () => settings.anygrass ? settings : null
            ));
        }

        public void OnLoadLocal(SaveData s)
        {
            sd = s;
        }

        public SaveData OnSaveLocal()
        {
            return sd;
        }

        public void OnLoadGlobal(GrassRandoSettings s)
        {
            settings = s;
        }

        public GrassRandoSettings OnSaveGlobal()
        {
            return settings;
        }

    }
}
