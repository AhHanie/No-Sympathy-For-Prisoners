using RimWorld;

namespace SK_No_Sympathy_For_Prisoners.Compat
{
    [DefOf]
    public class WCEHistoryEventDefOf
    {
        public static HistoryEventDef WCE2_TortureGuest;
        public static HistoryEventDef WCE2_TorturePrisonerGuilty;
        static WCEHistoryEventDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(WCEHistoryEventDefOf));
        }
    }
}
