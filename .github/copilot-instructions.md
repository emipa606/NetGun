# GitHub Copilot Instructions for Net Gun (Continued) Mod

## Mod Overview and Purpose

The Net Gun (Continued) mod is an update to the original Net Gun mod, adding new weapons and systems to the RimWorld game. The primary purpose of this mod is to introduce a non-lethal weapon called the Net Gun, which fires a net to entangle and restrain targets. This mod also includes a new Hediff called "Entangle" that affects the movement and manipulation of entangled targets.

## Key Features and Systems

### Net Gun
- A non-lethal weapon designed to fire a net.
- Good for catching escaping prisoners.
- Costs 50 steels and 4 components.

### Net Turret
- A stationary turret that fires nets to entangle targets.

### Entangle Hediff
- A Hediff that reduces the moving speed and manipulation of entangled targets based on their body size.
- Lasts for 1800 to 7200 ticks (30 seconds to 2 minutes).

## Coding Patterns and Conventions

### C# Coding Patterns
- Follow the RimWorld coding conventions and best practices.
- Use meaningful variable and method names.
- Comment your code to explain complex logic or reasoning.

### XML Integration
- Use XML files to define game data such as Hediffs, DamageDefs, ThingDefs, and FleshTypeDefs.
- Ensure XML files are well-formatted and follow RimWorld's XML schema.

### Harmony Patching
- Use Harmony to patch existing RimWorld methods to add or modify behavior.
- Ensure patches are clean and do not interfere with other mods.

## XML Integration

To integrate XML files into the mod, ensure that:
1. XML files are placed in the appropriate directories within the mod folder.
2. XML files are well-formatted and follow RimWorld's XML schema.
3. XML files are loaded correctly by the game.

Example XML file structure:

xml
<DamageDef>
  <defName>NetDamage</defName>
  <label>Net Damage</label>
  <armorCategory>Blunt</armorCategory>
  <armorPenetration>0</armorPenetration>
  <averageDamage>10</averageDamage>
  <flameImmunity>true</flameImmunity>
</DamageDef>


## Harmony Patching

To patch existing RimWorld methods using Harmony, follow these steps:
1. Define a patch class in a C# file.
2. Use the Harmony library to create patches.
3. Ensure patches are applied correctly and do not interfere with other mods.

Example Harmony patch:

csharp
using HarmonyLib;
using Verse;

namespace NetGun
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("SomeMethod")]
    public static class HarmonyPatches
    {
        [HarmonyPostfix]
        public static void SomeMethodPatch(Pawn __instance, ref int someValue)
        {
            // Modify someValue based on the Entangle Hediff
        }
    }
}


## Suggestions for Copilot

1. When generating code or XML, ensure it follows RimWorld's coding conventions and best practices.
2. When suggesting patches, ensure they are clean and do not interfere with other mods.
3. When providing suggestions for XML files, ensure they are well-formatted and follow RimWorld's XML schema.

By following these guidelines, you can help ensure that the Net Gun (Continued) mod is well-maintained and continues to provide an enjoyable experience for players.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

