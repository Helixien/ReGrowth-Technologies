using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RG_Technologies
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("regrowth.rimworld.shipchunks.main");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(IncidentWorker_ShipChunkDrop), "SpawnChunk")]
        public static class SpawnChunk_Patch
        {
            public static bool Prefix(IntVec3 pos, Map map)
            {
                var thingDef = SelectChunkFromAvailableOptions();
                SkyfallerMaker.SpawnSkyfaller(ThingDefOf.ShipChunkIncoming, thingDef, pos, map);
                return false;
            }
        }

        public static ThingDef SelectChunkFromAvailableOptions()
        {
            if (Rand.Chance(0.05f))
            {
                return DefDatabase<ThingDef>.GetNamed("RG-Tech_MechandoiShipChunk");
            }
            else
            {
                IEnumerable<ThingDef> chunk = from defs in DefDatabase<ThingDef>.AllDefs
                                              where defs.defName.ToLower().Contains("shipchunk") && !defs.defName.Contains("Incoming") && defs.defName != "RG-Tech_MechandoiShipChunk"
                                              select defs;

                return chunk.RandomElement();
            }

        }
    }
}
