using HarmonyLib;
using SK_No_Sympathy_For_Prisoners.Compat;

namespace SK_No_Sympathy_For_Prisoners.Patches.Compat.CMBeatPrisoners
{
    public static class BeatPrisonersUtilityPatches
    {
        private static bool interceptingPrisonerBeatingThoughts;
        private static bool victimThoughtProcessed;

        [HarmonyPatch("CM_Beat_Prisoners.BeatPrisonersUtility", "GiveThoughtsForPrisonerBeaten")]
        public static class GiveThoughtsForPrisonerBeaten
        {
            public static bool Prepare()
            {
                return BeatYourPrisonersCompat.ShouldPatchType("CM_Beat_Prisoners.BeatPrisonersUtility");
            }
            public static void Prefix()
            {
                interceptingPrisonerBeatingThoughts = true;
                victimThoughtProcessed = false;
            }

            public static void Postfix()
            {
                interceptingPrisonerBeatingThoughts = false;
                victimThoughtProcessed = false;
            }
        }

        [HarmonyPatch("CM_Beat_Prisoners.BeatPrisonersUtility", "TryGiveThoughts")]
        public static class TryGiveThoughts
        {
            public static bool Prepare()
            {
                return BeatYourPrisonersCompat.ShouldPatchType("CM_Beat_Prisoners.BeatPrisonersUtility");
            }
            public static bool Prefix()
            {
                if (!interceptingPrisonerBeatingThoughts)
                {
                    return true;
                }

                if (victimThoughtProcessed)
                {
                    return false;
                }

                victimThoughtProcessed = true;
                return true;
            }
        }
    }
}
