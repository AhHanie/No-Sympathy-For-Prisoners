using HarmonyLib;
using RimWorld;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class PreceptCompKnowsMemoryThoughtPatches
    {
        [HarmonyPatch(typeof(PreceptComp_KnowsMemoryThought), "Notify_MemberWitnessedAction")]
        public static class NotifyMemberWitnessedAction
        {
            public static bool Prefix(HistoryEvent ev, Precept precept)
            {
                if (PatchState.ShouldSuppressThought(ev, precept))
                {
                    if (ModSettings.affectMoodInstead)
                    {
                        PatchState.modifyMoodOffset = true;
                        return true;
                    }

                    return false;
                }

                return true;
            }

            public static void Postfix()
            {
                PatchState.modifyMoodOffset = false;
            }
        }
    }
}
