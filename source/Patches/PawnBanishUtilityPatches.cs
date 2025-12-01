using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;
using RimWorld;

namespace SK_No_Sympathy_For_Prisoners.Patches
{
    public static class PawnBanishUtilityPatches
    {
        [HarmonyPatch(typeof(PawnBanishUtility), "GetBanishPawnDialogText")]
        public static class GetBanishPawnDialogText
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);

                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Call &&
                        codes[i].operand is MethodInfo method &&
                        method.Name == "IsQuestLodger")
                    {
                        if (i + 1 < codes.Count && codes[i + 1].opcode == OpCodes.Brtrue_S)
                        {
                            var skipLabel = codes[i + 1].operand;
                            var insertIndex = i + 2;

                            var newInstructions = new List<CodeInstruction>
                            {
                                new CodeInstruction(OpCodes.Ldarg_0),
                                new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(Pawn), nameof(Pawn.IsPrisoner))),
                                new CodeInstruction(OpCodes.Brtrue_S, skipLabel)
                            };

                            codes.InsertRange(insertIndex, newInstructions);
                            break;
                        }
                    }
                }

                return codes;
            }
        }
    }
}