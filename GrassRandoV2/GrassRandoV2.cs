using Modding;
using System;
using System.Collections.Generic;
using GrassRandoV2.IC;
using GrassRandoV2.Rando;
using RandoSettingsManager;
using RandoSettingsManager.SettingsManagement;
using System.Reflection;
using GrassCore;

namespace GrassRandoV2
{
    public class SaveData
    {
        public string? serializedGrassRegister; // TODO: replace with grass register inferred from ItemChanger
        public int knightDreamGrassBroken;
        public int mageDreamGrassBroken;
    }
    public class GrassRandoV2Mod : Mod, ILocalSettings<SaveData>, IGlobalSettings<GrassRandoSettings>
    {
        public static GrassRandoV2Mod Instance;

        public SaveData sd = new();

        public readonly GrassRegister_Global grassRegister = new();
        public static GrassRandoSettings settings = new();
        //public static RandoSettings settings = new RandoSettings(new GrassRandoSettings(), new GrassLocationSettings());
        //public static GrassLocationSettings gls = settings.GetLocationSettings();

        //public static GrassRandoSettings st = new GrassRandoSettings();
        //public static GrassLocationSettings locSt = new GrassLocationSettings();

        //sets up the ui mod stuff for the mod manager
        new public string GetName() => "Grass Randomizer";
        public override string GetVersion() => "v1.0";

        public GrassRandoV2Mod() : base("GrassRandoV2")
        {
            Instance = this;
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

            if (ModHooks.GetMod("RandoMapMod") is Mod)
            {

            }

            GrassCore.GrassCore.Instance.CutsEnabled = true; // Get grass events from GrassCore
            GrassCore.GrassCore.Instance.WeedkillerEnabled = true; // Despawn already collected grass
            GrassCore.GrassCore.Instance.DisconnectWeedKiller = true; // Do not use GrassCore's internal grass dict (we will use our own)

            GrassCore.WeedKiller.Instance.Blacklist = grassRegister._grassStates; // Use our internal tracker for WeedKiller

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

        //location where I will set up hooks for the rando map mod
        private void HookRMM()
        {
            
        }

        public void OnLoadLocal(SaveData s)
        {
            sd = s;
            grassRegister.Clear();
            grassRegister.AddSerializedData(s.serializedGrassRegister);
        }

        public SaveData OnSaveLocal()
        {
            sd.serializedGrassRegister = grassRegister.Serialize();
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
