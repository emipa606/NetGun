using Verse;

namespace NetGun;

public class ThingDef_NetBullet : ThingDef
{
    public readonly float AddHediffChance = 1f;

    public HediffDef HediffToAdd;

    public override void ResolveReferences()
    {
        HediffToAdd = HediffDefOf.YP_Entangle_Small;
    }
}