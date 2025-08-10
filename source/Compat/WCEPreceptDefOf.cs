using RimWorld;

namespace SK_No_Sympathy_For_Prisoners.Compat
{
    [DefOf]
    public class WCEPreceptDefOf
    {
        public static PreceptDef WCE2_Torture_Vanilla;
        public static PreceptDef WCE2_Torture_Abhorrent;
        public static PreceptDef WCE2_Torture_Horrible;
        public static PreceptDef WCE2_Torture_HorribleInnocent;
        static WCEPreceptDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(WCEPreceptDefOf));
        }
    }
}
