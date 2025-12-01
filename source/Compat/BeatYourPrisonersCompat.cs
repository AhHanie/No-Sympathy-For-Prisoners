using HarmonyLib;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Compat
{
    public static class BeatYourPrisonersCompat
    {
        public const string ModName = "Beat Your Prisoners";
        private const string ModId = "mlie.beatyourprisoners";

        public static bool IsActive()
        {
            return ModsConfig.IsActive(ModId);
        }

        public static bool ShouldPatchType(string typeName)
        {
            return IsActive() && AccessTools.TypeByName(typeName) != null;
        }
    }
}
