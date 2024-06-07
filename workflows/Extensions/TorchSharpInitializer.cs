using HarmonyLib;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

public class TorchSharpInitializer
{
    public static void Initialize()
    {
        var harmony = new Harmony("torchsharp");
        var method = GetTryLoadMethod();
        if (method != null)
        {
            harmony.Patch(method, new HarmonyMethod(typeof(NativeLibraryPatch).GetMethod(nameof(NativeLibraryPatch.Prefix))));
        }
    }

    private static MethodInfo GetTryLoadMethod()
    {
        var assembly = typeof(TorchSharp.torch).Assembly;
        if (assembly == null)
        {
            Console.WriteLine("TorchSharp assembly not found.");
            return null;
        }

        var nativeLibraryType = assembly.GetType("System.NativeLibrary");
        if (nativeLibraryType == null)
        {
            Console.WriteLine("System.NativeLibrary type in TorchSharp assembly not found.");
            return null;
        }

        var method = nativeLibraryType.GetMethod("TryLoad", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), typeof(Assembly), typeof(DllImportSearchPath?), typeof(IntPtr).MakeByRefType() }, null);
        if (method == null)
        {
            Console.WriteLine("System.NativeLibrary.TryLoad method not found.");
            return null;
        }

        return method;
    }
}

 public static class NativeLibraryPatch
{
    public static bool Prefix(string libraryName, Assembly assembly, DllImportSearchPath? searchPath, out IntPtr handle, ref bool __result)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            libraryName = "lib" + libraryName + ".so";
        }
        handle = NativeLibrary.Load(libraryName);
        __result = handle != IntPtr.Zero;
        return false;
    }
}