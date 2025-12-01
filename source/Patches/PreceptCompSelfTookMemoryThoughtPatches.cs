using HarmonyLib;
using RimWorld;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class PreceptCompSelfTookMemoryThoughtPatches
    {
        [HarmonyPatch(typeof(PreceptComp_SelfTookMemoryThought), "Notify_MemberTookAction")]
        public static class NotifyMemberTookAction
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
