using HarmonyLib;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class CorpsePatches
    {
        [HarmonyPatch(typeof(Corpse), "ButcherProducts")]
        public static class ButcherProducts
        {
            public static void Prefix(Corpse __instance)
            {
                if (__instance.InnerPawn.IsPrisoner)
                {
                    PatchState.lastVictim = __instance.InnerPawn;
                }
            }
        }
    }
}