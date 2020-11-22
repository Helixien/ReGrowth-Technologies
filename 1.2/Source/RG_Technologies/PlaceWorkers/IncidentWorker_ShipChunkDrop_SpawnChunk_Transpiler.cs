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
	public class PlaceWorker_OnLavaTerrains : PlaceWorker
	{
		public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
		{
			foreach (IntVec3 intVec in GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size))
			{
				if (map.terrainGrid.TerrainAt(intVec) != TerrainDef.Named("RG_Lava"))
				{
					return "ReGrowth.MustPlaceOnLava".Translate();
				}
			}
			return true;
		}
	}
}
