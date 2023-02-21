using RimWorld;

namespace NetGun;

[DefOf]
public static class FleshTypeDefOf_Extend
{
    public static FleshTypeDef YP_Entangle_Small;

    public static FleshTypeDef YP_Entangle_Normal;

    public static FleshTypeDef YP_Entangle_Large;

    public static FleshTypeDef YP_Entangle_xLarge;

    public static FleshTypeDef YP_Entangle_Mech;

    static FleshTypeDefOf_Extend()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(FleshTypeDefOf));
    }
}