using HarmonyLib;
using RimWorld;
using System.Reflection;

namespace SK_No_Sympathy_For_Prisoners
{
    public class HarmonyPatcher
    {
        public static Harmony instance;
        public static void PatchVanillaMethods()
        {
            // Patch ThoughtUtility.GiveThoughtsForPawnOrganHarvested method
            MethodInfo giveThoughtsForPawnOrganHarvestedMethod = AccessTools.Method(typeof(ThoughtUtility), "GiveThoughtsForPawnOrganHarvested");
            HarmonyMethod giveThoughtsForPawnOrganHarvestedPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("GiveThoughtsForPawnOrganHarvestedPrefix"));
            instance.Patch(giveThoughtsForPawnOrganHarvestedMethod, giveThoughtsForPawnOrganHarvestedPrefixPatch);

            // Patch PreceptComp_KnowsMemoryThought.Notify_MemberWitnessedAction method
            MethodInfo notifyMemberWitnessedActiondMethod = AccessTools.Method(typeof(PreceptComp_KnowsMemoryThought), "Notify_MemberWitnessedAction");
            HarmonyMethod notifyMemberWitnessedActiondPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberWitnessedActionPrefix"));
            instance.Patch(notifyMemberWitnessedActiondMethod, notifyMemberWitnessedActiondPrefixPatch);

            // Patch PreceptComp_SelfTookMemoryThought.Notify_MemberTookAction method
            MethodInfo notifyMemberTookActiondMethod = AccessTools.Method(typeof(PreceptComp_SelfTookMemoryThought), "Notify_MemberTookAction");
            HarmonyMethod notifyMemberTookActiondPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("NotifyMemberTookActionPrefix"));
            instance.Patch(notifyMemberTookActiondMethod, notifyMemberTookActiondPrefixPatch);

            // Patch PawnBanishUtility.GetBanishPawnDialogText method with transpiler
            MethodInfo getBanishPawnDialogTextMethod = AccessTools.Method(typeof(PawnBanishUtility), "GetBanishPawnDialogText");
            HarmonyMethod getBanishPawnDialogTextTranspilerPatch = new HarmonyMethod(typeof(Patches).GetMethod("GetBanishPawnDialogTextTranspiler"));
            instance.Patch(getBanishPawnDialogTextMethod, transpiler: getBanishPawnDialogTextTranspilerPatch);

            // Patch PawnDiedOrDownedThoughtsUtility.TryGiveThoughts method
            MethodInfo tryGiveThoughtsMethod = AccessTools.FirstMethod(typeof(PawnDiedOrDownedThoughtsUtility), method => method.Name.Contains("TryGiveThoughts"));
            HarmonyMethod tryGiveThoughtsPrefixPatch = new HarmonyMethod(typeof(Patches).GetMethod("TryGiveThoughtsPrefixPatch"));
            instance.Patch(tryGiveThoughtsMethod, tryGiveThoughtsPrefixPatch);
            
        }
    }
}
