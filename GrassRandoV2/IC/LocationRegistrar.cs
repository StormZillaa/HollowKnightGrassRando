using GrassCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrassRandoV2.IC
{
    public class LocationRegistrar
    {
        public Dictionary<string, Dictionary<GrassKey, BreakableGrassLocation>> GrassLocations = new();
        // Maintained separately to allow quick access time

        private static LocationRegistrar? _instance;
        public static LocationRegistrar Instance { get {
            _instance ??= new LocationRegistrar();
            return _instance; } 
        }

        public LocationRegistrar()
        {
            GrassEventDispatcher.Instance.GrassWasCut += GrassCutHandler;
        }

        public void Add(BreakableGrassLocation location)
        {
            TryAddScene(location.sceneName);
            GrassLocations[location.sceneName][location.gd.ToGrassKey()] = location;
        }

        public void Remove(BreakableGrassLocation location)
        {
            if (!GrassLocations.TryGetValue(location.sceneName, out var sceneDict)) { return; }
            sceneDict.Remove(location.gd.ToGrassKey());
        }

        private BreakableGrassLocation? GetLocation(GrassKey key)
        {
            if (!GrassLocations.TryGetValue(key.SceneName, out Dictionary<GrassKey, BreakableGrassLocation> sceneDict)) { return null; }
            if (!sceneDict.TryGetValue(key, out BreakableGrassLocation location)) { return null; }
            return location;
        }

        private void GrassCutHandler(GrassKey key)
        {
            var location = GetLocation(key);
            if (location == null) { return; }

            location.Obtain();
            GrassRegister_Rando.Instance.TryCut(key); // Set as cut for WeedKiller
        }

        private void TryAddScene(string sceneName)
        {
            if (!GrassLocations.ContainsKey(sceneName))
            {
                GrassLocations.Add(sceneName, new Dictionary<GrassKey, BreakableGrassLocation>());
            }
        }
    }
}
