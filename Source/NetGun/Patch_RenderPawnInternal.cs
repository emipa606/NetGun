using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace NetGun;

[HarmonyPatch(typeof(PawnRenderer), "RenderPawnInternal", typeof(Vector3), typeof(float), typeof(bool), typeof(Rot4),
    typeof(RotDrawMode), typeof(PawnRenderFlags))]
internal class Patch_RenderPawnInternal
{
    private static void Postfix(PawnRenderer __instance, Vector3 rootLoc, float angle, bool renderBody, Rot4 bodyFacing,
        RotDrawMode bodyDrawType, PawnRenderFlags flags, Pawn ___pawn)
    {
        var graphics = __instance.graphics;
        var quat = Quaternion.AngleAxis(angle, Vector3.up);
        if (!renderBody)
        {
            return;
        }

        var hediffs = ___pawn.health.hediffSet.hediffs;
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

        var vector = rootLoc;
        vector.y += 1f / 132f;
        if (bodyDrawType == RotDrawMode.Dessicated && !___pawn.RaceProps.Humanlike &&
            graphics.dessicatedGraphic != null && !flags.FlagSet(PawnRenderFlags.Portrait))
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

        if (bodyDrawType != 0)
        {
            return;
        }

        var loc = rootLoc;
        loc.y += 1.0189394f;
        if (resolvedWound != null)
        {
            if (___pawn.BodySize >= 2.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshMakerPlanes.NewPlaneMesh(2.5f), loc, quat,
                    resolvedWound.GetMaterial(bodyFacing, out _), flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            if (___pawn.BodySize >= 1.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshPool.plane20, loc, quat, resolvedWound.GetMaterial(bodyFacing, out _),
                    flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            if (___pawn.BodySize >= 0.7)
            {
                GenDraw.DrawMeshNowOrLater(MeshPool.plane14, loc, quat, resolvedWound.GetMaterial(bodyFacing, out _),
                    flags.FlagSet(PawnRenderFlags.DrawNow));
                return;
            }

            GenDraw.DrawMeshNowOrLater(MeshPool.plane10, loc, quat, resolvedWound.GetMaterial(bodyFacing, out _),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (___pawn.BodySize >= 2.7)
        {
            GenDraw.DrawMeshNowOrLater(MeshMakerPlanes.NewPlaneMesh(2.5f), loc, quat,
                MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (___pawn.BodySize >= 1.7)
        {
            GenDraw.DrawMeshNowOrLater(MeshPool.plane20, loc, quat,
                MaterialPool.MatFrom("Things/Pawn/Wounds/Net_retexture_Genom-X", ShaderDatabase.WoundSkin, Color.white),
                flags.FlagSet(PawnRenderFlags.DrawNow));
            return;
        }

        if (___pawn.BodySize >= 0.7)
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