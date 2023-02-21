using RimWorld;
using UnityEngine;
using Verse;

namespace NetGun;

public class Hediff_YP_Entangle : Hediff
{
    private static readonly Color PermanentInjuryColor = new Color(0.1f, 0.1f, 0.72f);

    public Hediff_YP_Entangle(HediffDef def)
    {
        var hediffStage = new HediffStage();
        var item = new PawnCapacityModifier
        {
            capacity = PawnCapacityDefOf.Moving,
            postFactor = 0.8f
        };
        var item2 = new PawnCapacityModifier
        {
            capacity = PawnCapacityDefOf.Manipulation,
            postFactor = 0.7f
        };
        hediffStage.capMods.Add(item);
        hediffStage.capMods.Add(item2);
        def.stages.Add(hediffStage);
        this.def = def;
    }

    public override Color LabelColor => this.IsPermanent() ? PermanentInjuryColor : Color.white;
}