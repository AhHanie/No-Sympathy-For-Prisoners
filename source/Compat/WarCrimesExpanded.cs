
using HarmonyLib;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld;
using System.Linq;

namespace SK_No_Sympathy_For_Prisoners.Compat
{
    public class WarCrimesExpanded
    {
        public static void PatchAllModMethods(Harmony instance)
        {
            HarmonyMethod wce2HarmPrisonerPrefixPatch = new HarmonyMethod(typeof(WarCrimesExpanded).GetMethod("WCE2HarmPrisonerPrefixPatch"));

            // Patch Recipe_BludgeonBodyPart.ApplyOnPawn method
            MethodInfo bludeonApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_BludgeonBodyPart:ApplyOnPawn");
            instance.Patch(bludeonApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_InstallImplant_Brand.ApplyOnPawn method
            MethodInfo brandApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_InstallImplant_Brand:ApplyOnPawn");
            instance.Patch(brandApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_InstallImplant_MangleBodyPart.ApplyOnPawn method
            MethodInfo mangleApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_InstallImplant_MangleBodyPart:ApplyOnPawn");
            instance.Patch(mangleApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_InstallImplant_MutilateFaceAcid.ApplyOnPawn method
            MethodInfo mutilateApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_InstallImplant_MutilateFaceAcid:ApplyOnPawn");
            instance.Patch(mutilateApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_InstallImplant_PracticeSurgery.ApplyOnPawn method
            MethodInfo practiceSurgeryApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_InstallImplant_PracticeSurgery:ApplyOnPawn");
            instance.Patch(practiceSurgeryApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_InstallImplantWithThought.ApplyOnPawn method
            MethodInfo withThoughtApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_InstallImplantWithThought:ApplyOnPawn");
            instance.Patch(withThoughtApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_RemoveBodyPart_Torture.ApplyOnPawn method
            MethodInfo removeBodyPartApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_RemoveBodyPart_Torture:ApplyOnPawn");
            instance.Patch(removeBodyPartApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);

            // Patch Recipe_RemoveHediffMutilation.ApplyOnPawn method
            MethodInfo removeHediffApplyOnPawnMethod = AccessTools.Method("WarCrimesExpanded2.Recipe_RemoveHediffMutilation:ApplyOnPawn");
            instance.Patch(removeHediffApplyOnPawnMethod, wce2HarmPrisonerPrefixPatch);  
        }

        public static bool IsActive()
        {
            return ModsConfig.IsActive("mersid.wce2updated.core");
        }
        
        public static void WCE2HarmPrisonerPrefixPatch(Pawn victim)
        {
            Patches.lastVictim = victim;
        }

        public static void Init()
        {
            List<string> historyEventDefNames = new List<string>() { "WCE2_TortureGuest", "WCE2_TorturePrisonerGuilty" };
            List<HistoryEventDef> tortureEvents = DefDatabase<HistoryEventDef>.AllDefsListForReading.Where(def => historyEventDefNames.Contains(def.defName)).ToList();
            List<string> preceptDefNames = new List<string>() { "WCE2_Torture_Vanilla", "WCE2_Torture_Abhorrent", "WCE2_Torture_Horrible", "WCE2_Torture_HorribleInnocent" };
            List<PreceptDef> torturePrecepts = DefDatabase<PreceptDef>.AllDefsListForReading.Where(def => preceptDefNames.Contains(def.defName)).ToList();
            Patches.modDefined.Add("mersid.wce2updated.core", (torturePrecepts, tortureEvents));
        }
    }
}
