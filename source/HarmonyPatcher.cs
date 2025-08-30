using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using System;

namespace SK_No_Sympathy_For_Prisoners
{
    public class HarmonyPatcher
    {
        public static void PatchVanillaMethods(Harmony instance)
        {
            // Patch ThoughtUtility.GiveThoughtsForPawnOrganHarvested method
            MethodInfo giveThoughtsForPawnOrganHarvestedMethod = AccessTools.Method(typeof(ThoughtUtility), "GiveThoughtsForPawnOrganHarvested");
            HarmonyMethod giveThoughtsForPawnOrganHarvestedPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("GiveThoughtsForPawnOrganHarvestedPrefix"));
            instance.Patch(giveThoughtsForPawnOrganHarvestedMethod, giveThoughtsForPawnOrganHarvestedPrefixPatch);

            // Patch PreceptComp_KnowsMemoryThought.Notify_MemberWitnessedAction method
            MethodInfo notifyMemberWitnessedActiondMethod = AccessTools.Method(typeof(PreceptComp_KnowsMemoryThought), "Notify_MemberWitnessedAction");
            HarmonyMethod notifyMemberWitnessedActiondPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberWitnessedActionPrefix"));
            HarmonyMethod notifyMemberWitnessedActiondPostfixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberWitnessedActionPostfix"));
            instance.Patch(notifyMemberWitnessedActiondMethod, notifyMemberWitnessedActiondPrefixPatch);

            // Patch PreceptComp_SelfTookMemoryThought.Notify_MemberTookAction method
            MethodInfo notifyMemberTookActiondMethod = AccessTools.Method(typeof(PreceptComp_SelfTookMemoryThought), "Notify_MemberTookAction");
            HarmonyMethod notifyMemberTookActiondPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberTookActionPrefix"));
            HarmonyMethod notifyMemberTookActiondPostfixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberTookActionPostfix"));
            instance.Patch(notifyMemberTookActiondMethod, notifyMemberTookActiondPrefixPatch);

            if (ModSettings.affectMoodInstead)
            {
                // Patch MemoryThoughtHandler.TryGainMemory method
                MethodInfo tryGainMemoryMethod = AccessTools.Method(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[] { typeof(Thought_Memory), typeof(Pawn) });
                HarmonyMethod tryGainMemoryPostfixPatch = new HarmonyMethod(typeof(Patches).GetMethod("TryGainMemoryPostfix"));
                instance.Patch(tryGainMemoryMethod, null, tryGainMemoryPostfixPatch);
            }

            // Patch PawnBanishUtility.GetBanishPawnDialogText method with transpiler
            MethodInfo getBanishPawnDialogTextMethod = AccessTools.Method(typeof(PawnBanishUtility), "GetBanishPawnDialogText");
            HarmonyMethod getBanishPawnDialogTextTranspilerPatch = new HarmonyMethod(typeof(Patches).GetMethod("GetBanishPawnDialogTextTranspiler"));
            instance.Patch(getBanishPawnDialogTextMethod, transpiler: getBanishPawnDialogTextTranspilerPatch);

            // Patch PawnDiedOrDownedThoughtsUtility.TryGiveThoughts method
            MethodInfo tryGiveThoughtsMethod = AccessTools.FirstMethod(typeof(PawnDiedOrDownedThoughtsUtility), method => method.Name.Contains("TryGiveThoughts"));
            HarmonyMethod tryGiveThoughtsPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("TryGiveThoughtsPrefixPatch"));
            instance.Patch(tryGiveThoughtsMethod, tryGiveThoughtsPrefixPatch);

            // Patch Corpse.ButcherProducts method
            MethodInfo butcherProductsMethod = AccessTools.Method(typeof(Corpse), "ButcherProducts");
            HarmonyMethod butcherProductsPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("ButcherProductsPrefixPatch"));
            instance.Patch(butcherProductsMethod, butcherProductsPrefixPatch);

            // Patch RecipeWorker.ReportViolation method
            if (ModSettings.disableOrganHarvestingNegativeGoodwill)
            {
                MethodInfo reportViolationMethod = AccessTools.Method(typeof(RecipeWorker), "ReportViolation");
                HarmonyMethod reportViolationPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("ReportViolationPrefixPatch"));
                instance.Patch(reportViolationMethod, reportViolationPrefixPatch);
            }
        }
    }
}