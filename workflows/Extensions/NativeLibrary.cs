using System;
using System.Runtime.InteropServices;

public static class NativeLibrary
{
    // Declare the dlopen function from libdl
    [DllImport("libdl.so.2")]
    public static extern IntPtr dlopen(string fileName, int flags);

    // Declare the dlclose function from libdl
    [DllImport("libdl.so.2")]
    public static extern int dlclose(IntPtr handle);

    // Declare the dlerror function from libdl to retrieve error messages
    [DllImport("libdl.so.2")]
    public static extern IntPtr dlerror();

    // Constants for dlopen
    private const int RTLD_NOW = 2;
    private const int RTLD_GLOBAL = 0x100;

    public static IntPtr Load(string libraryPath)
    {
        IntPtr handle = dlopen(libraryPath, RTLD_NOW | RTLD_GLOBAL);
        if (handle == IntPtr.Zero)
        {
            IntPtr errorPtr = dlerror();
            string errorMsg = Marshal.PtrToStringAnsi(errorPtr);
            Console.WriteLine($"Failed to load library: {errorMsg}");
            return IntPtr.Zero;
        }

        return handle;
    }
}