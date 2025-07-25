using System.Runtime.CompilerServices;
using Verse;

namespace SK_No_Sympathy_For_Prisoners
{
    public class ModSettings: Verse.ModSettings
    {
        public static StrongBox<bool> disableOrganHarvestingNegativeGoodwill = new StrongBox<bool>(false);
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref disableOrganHarvestingNegativeGoodwill.Value, "disableOrganHarvestingNegativeGoodwill", false);
        }
    }
}
