using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.ModuleManager;

namespace GameInterface.Tests.Bootstrap.Patches
{
    [HarmonyPatch(typeof(ModuleHelper))]
    internal class ModuleHelperPatches
    {
        [HarmonyPatch("GetModules")]
        [HarmonyPrefix]
        private static bool GetModulesPrefix(ref IEnumerable<ModuleInfo> __result)
        {
            __result = new List<ModuleInfo>();
            return false;
        }

        [HarmonyPatch("GetActiveGameAssemblies")]
        [HarmonyPrefix]
        private static bool GetActiveGameAssembliesPrefix(ref TaleWorlds.Library.MBList<System.Reflection.Assembly> __result)
        {
            __result = new TaleWorlds.Library.MBList<System.Reflection.Assembly>();
            return false;
        }

        [HarmonyPatch("GetAllModules")]
        [HarmonyPrefix]
        private static bool GetAllModulesPrefix(ref System.Collections.Generic.Dictionary<string, ModuleInfo>.ValueCollection __result)
        {
            __result = new System.Collections.Generic.Dictionary<string, ModuleInfo>().Values;
            return false;
        }

        [HarmonyPatch("GetOfficialModuleIds")]
        [HarmonyPrefix]
        private static bool GetOfficialModuleIdsPrefix(ref TaleWorlds.Library.MBList<string> __result)
        {
            __result = new TaleWorlds.Library.MBList<string>();
            return false;
        }
    }
}
