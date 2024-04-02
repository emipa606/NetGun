using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace NetGun;

[HarmonyPatch(typeof(PawnRenderUtility), nameof(PawnRenderUtility.DrawEquipmentAndApparelExtras))]
internal class Patch_DrawEquipmentAndApparelExtras
{
    private static void Postfix(Pawn pawn, Vector3 drawPos, Rot4 facing, PawnRenderFlags flags)
    {
        var hediffs = pawn.health.hediffSet.hediffs;
        Hediff hediff = null;
        var num = -1;
        var isEntangled = false;
        foreach (var entangledHediff in hediffs)
        {
            if (entangledHediff.def == HediffDefOf.YP_Entangle_Small)
            {
                num = 0;
                isEntangled = true;
            }
            else if (entangledHediff.def == HediffDefOf.YP_Entangle_Normal)
            {
                num = 1;
                isEntangled = true;
            }
            else if (entangledHediff.def == HediffDefOf.YP_Entangle_Large)
            {
                num = 2;
                isEntangled = true;
            }
            else if (entangledHediff.def == HediffDefOf.YP_Entangle_xLarge)
            {
                num = 3;
                isEntangled = true;
            }
            else if (entangledHediff.def == HediffDefOf.YP_Entangle_Mech)
            {
                num = 4;
                isEntangled = true;
            }

            if (!isEntangled)
            {
                continue;
            }

            hediff = entangledHediff;
            break;
        }

        if (!isEntangled)
        {
            return;
        }

        var vector = drawPos;
        vector.y += 1f / 132f;
        if (pawn.GetRotStage() != RotStage.Fresh)
        {
            return;
        }

        FleshTypeDef.ResolvedWound resolvedWound = null;
        switch (num)
        {
            case 0:
                resolvedWound = FleshTypeDefOf_Extend.YP_Entangle_Small.ChooseWoundOverlay(hediff);
                break;
            case 1:
                resolvedWound = FleshTypeDefOf_Extend.YP_Entangle_Normal.ChooseWoundOverlay(hediff);
                break;
            case 2:
                resolvedWound = FleshTypeDefOf_Extend.YP_Entangle_Large.ChooseWoundOverlay(hediff);
                break;
            case 3:
                resolvedWound = FleshTypeDefOf_Extend.YP_Entangle_xLarge.ChooseWoundOverlay(hediff);
                break;
            case 4:
                resolvedWound = FleshTypeDefOf_Extend.YP_Entangle_Mech.ChooseWoundOverlay(hediff);
                break;
        }


        var loc = pawn.DrawPos;
        loc.y += 1.0189394f;
        var quat = Quaternion.Euler(Vector3.up * pawn.Drawer.renderer.BodyAngle(PawnRenderFlags.None));
        if (resolvedWound != null)
        {
            if (pawn.BodySize >= 2.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshMakerPlanes.NewPlaneMesh(2.5f), loc, quat,
                    resolvedWound.GetMaterial(facing, out _), flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            if (pawn.BodySize >= 1.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshPool.plane20, loc, quat, resolvedWound.GetMaterial(facing, out _),
                    flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            if (pawn.BodySize >= 0.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshPool.plane14, loc, quat, resolvedWound.GetMaterial(facing, out _),
                    flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            GenDraw.DrawMeshNowOrLater(MeshPool.plane10, loc, quat, resolvedWound.GetMaterial(facing, out _),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (pawn.BodySize >= 2.7)
        {
            GenDraw.DrawMeshNowOrLater(MeshMakerPlanes.NewPlaneMesh(2.5f), loc, quat,
                MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (pawn.BodySize >= 1.7)
        {
            GenDraw.DrawMeshNowOrLater(MeshPool.plane20, loc, quat,
                MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (pawn.BodySize >= 0.7)
        {
            GenDraw.DrawMeshNowOrLater(MeshPool.plane14, loc, quat,
                MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        GenDraw.DrawMeshNowOrLater(MeshPool.plane10, loc, quat,
            MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
            flags.FlagSet(PawnRenderFlags.DrawNow));
    }
}