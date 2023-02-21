using HarmonyLib;
using Verse;

namespace NetGun;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
        DoPatching();
    }

    public static void DoPatching()
    {
        var harmony = new Harmony("net.yiuaaa.rimworld.mod.NetGun");
        harmony.PatchAll();
    }
}