
using HarmonyLib;
using System.Reflection;
using Verse;
using System.Collections.Generic;
using RimWorld;

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
            List<HistoryEventDef> tortureEvents = new List<HistoryEventDef>() { WCEHistoryEventDefOf.WCE2_TortureGuest, WCEHistoryEventDefOf.WCE2_TorturePrisonerGuilty };
            List<PreceptDef> torturePrecepts = new List<PreceptDef>() { WCEPreceptDefOf.WCE2_Torture_Vanilla, WCEPreceptDefOf.WCE2_Torture_Abhorrent, WCEPreceptDefOf.WCE2_Torture_Horrible, WCEPreceptDefOf.WCE2_Torture_HorribleInnocent };
            Patches.modDefined.Add("mersid.wce2updated.core", (torturePrecepts, tortureEvents));
        }
    }
}
