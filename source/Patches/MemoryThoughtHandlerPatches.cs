using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class MemoryThoughtHandlerPatches
    {
        [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new[] { typeof(Thought_Memory), typeof(Pawn) })]
        public static class TryGainMemory
        {
            public static bool Prepare()
            {
                return ModSettings.affectMoodInstead;
            }

            public static void Postfix(MemoryThoughtHandler __instance, Thought_Memory newThought)
            {
                if (PatchState.modifyMoodOffset)
                {
                    Thought_Memory memory = __instance.GetFirstMemoryOfDef(newThought.def);
                    float currentOffset = memory.MoodOffset();
                    if (currentOffset < 0)
                    {
                        float counteract = -currentOffset * (ModSettings.moodReductionPercentage / 100f);
                        newThought.moodOffset = Mathf.RoundToInt(counteract);
                    }
                }
            }
        }
    }
}