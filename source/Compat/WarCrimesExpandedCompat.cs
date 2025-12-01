
using HarmonyLib;
using RimWorld;
using SK_No_Sympathy_For_Prisoners.Patches;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Compat
{
    public class WarCrimesExpandedCompat
    {
        private const string ModId = "mersid.wce2updated.core";

        public static bool IsActive()
        {
            return ModsConfig.IsActive(ModId);
        }

        public static bool ShouldPatchType(string typeName)
        {
            return IsActive() && AccessTools.TypeByName(typeName) != null;
        }

        public static void Init()
        {
            List<string> historyEventDefNames = new List<string>() { "WCE2_TortureGuest", "WCE2_TorturePrisonerGuilty" };
            List<HistoryEventDef> tortureEvents = DefDatabase<HistoryEventDef>.AllDefsListForReading.Where(def => historyEventDefNames.Contains(def.defName)).ToList();
            List<string> preceptDefNames = new List<string>() { "WCE2_Torture_Vanilla", "WCE2_Torture_Abhorrent", "WCE2_Torture_Horrible", "WCE2_Torture_HorribleInnocent" };
            List<PreceptDef> torturePrecepts = DefDatabase<PreceptDef>.AllDefsListForReading.Where(def => preceptDefNames.Contains(def.defName)).ToList();
            PatchState.modDefined.Add(ModId, (torturePrecepts, tortureEvents));
        }
    }
}
