using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RG_Technologies
{
	public class CompCauseGameCondition_ForceWeather : CompCauseGameCondition
	{
		public WeatherDef weather;

		public override void Initialize(CompProperties props)
		{
			base.Initialize(props);
			weather = base.Props.conditionDef.weatherDef;
		}

		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Defs.Look(ref weather, "weather");
		}

		public override IEnumerable<Gizmo> CompGetGizmosExtra()
		{
			CommandAction_RightClickWeather command_Action = new CommandAction_RightClickWeather(this);
			command_Action.defaultLabel = weather.LabelCap;
			command_Action.icon = ContentFinder<Texture2D>.Get("UI/Buttons/ChangeWeather");
			command_Action.action = delegate
			{
				List<WeatherDef> allDefsListForReading = DefDatabase<WeatherDef>.AllDefsListForReading;
				int num = allDefsListForReading.FindIndex((WeatherDef w) => w == weather);
				num++;
				if (num >= allDefsListForReading.Count)
				{
					num = 0;
				}
				ChangeWeather(allDefsListForReading[num]);
			};
			command_Action.hotKey = KeyBindingDefOf.Misc1;
			yield return command_Action;
		}

		public void ChangeWeather(WeatherDef newWeather)
        {
			weather = newWeather;
			ReSetupAllConditions();
		}
		protected override void SetupCondition(GameCondition condition, Map map)
		{
			base.SetupCondition(condition, map);
			((GameCondition_ForceWeather)condition).weather = weather;
		}

		public override string CompInspectStringExtra()
		{
			string text = base.CompInspectStringExtra();
			if (!text.NullOrEmpty())
			{
				text += "\n";
			}
			return text + "Weather".Translate() + ": " + weather.LabelCap;
		}

		public override void RandomizeSettings_NewTemp_NewTemp(Site site)
		{
			weather = DefDatabase<WeatherDef>.AllDefsListForReading.Where((WeatherDef x) => x.isBad).RandomElement();
		}
	}
}
