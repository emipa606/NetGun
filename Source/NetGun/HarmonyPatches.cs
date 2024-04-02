using System.Reflection;
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
        new Harmony("net.yiuaaa.rimworld.mod.NetGun").PatchAll(Assembly.GetExecutingAssembly());
    }
}