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
	public class CommandAction_RightClickWeather : Command_Action
	{
        public CompCauseGameCondition_ForceWeather comp;
        public CommandAction_RightClickWeather(CompCauseGameCondition_ForceWeather comp)
        {
            this.comp = comp;
        }
        public override IEnumerable<FloatMenuOption> RightClickFloatMenuOptions
        {
            get
            {
                foreach (WeatherDef weatherDef in DefDatabase<WeatherDef>.AllDefsListForReading)
                {
                    yield return new FloatMenuOption(weatherDef.LabelCap, delegate
                    {
                        comp.ChangeWeather(weatherDef);
                    }, MenuOptionPriority.Default, null, null, 29f, null, null);
                }
            }
        }
    }
}
