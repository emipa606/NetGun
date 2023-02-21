using RimWorld;
using Verse;

namespace NetGun;

internal class Projectile_NetBullet : Bullet
{
    public ThingDef_NetBullet Def => def as ThingDef_NetBullet;

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        base.Impact(hitThing, blockedByShield);
        if (Def == null || hitThing is not Pawn pawn)
        {
            return;
        }

        var value = Rand.Value;
        if (!(value <= Def.AddHediffChance))
        {
            return;
        }

        if (pawn.kindDef.race.race.IsMechanoid)
        {
            Def.HediffToAdd = HediffDefOf.YP_Entangle_Mech;
        }
        else if (pawn.BodySize >= 2.7)
        {
            Def.HediffToAdd = HediffDefOf.YP_Entangle_xLarge;
        }
        else if (pawn.BodySize >= 1.7)
        {
            Def.HediffToAdd = HediffDefOf.YP_Entangle_Large;
        }
        else if (pawn.BodySize >= 0.7)
        {
            Def.HediffToAdd = HediffDefOf.YP_Entangle_Normal;
        }
        else
        {
            Def.HediffToAdd = HediffDefOf.YP_Entangle_Small;
        }

        var hediff = pawn.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
        if (hediff != null)
        {
            return;
        }

        var hediff2 = HediffMaker.MakeHediff(Def.HediffToAdd, pawn);
        pawn.health?.AddHediff(hediff2);
    }
}