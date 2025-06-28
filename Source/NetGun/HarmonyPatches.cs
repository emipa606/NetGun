using System.Reflection;
using HarmonyLib;
using Verse;

namespace NetGun;

[StaticConstructorOnStartup]
public static class HarmonyPatches
{
    static HarmonyPatches()
    {
        new Harmony("net.yiuaaa.rimworld.mod.NetGun").PatchAll(Assembly.GetExecutingAssembly());
    }
}