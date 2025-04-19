using System;
using System.Runtime.InteropServices;

namespace Libremidi {

public static class MidiSystem
{
    public static string GetVersion()
      => Marshal.PtrToStringAnsi(Interop.GetVersion());

    public delegate void AvailableApisCallback(IntPtr ctx, Api api);

    public static void QueryMidi1Apis(AvailableApisCallback cb)
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        Interop.Midi1AvailableApis(IntPtr.Zero, ptr);
    }

    public static void QueryMidi2Apis(AvailableApisCallback cb)
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        Interop.Midi2AvailableApis(IntPtr.Zero, ptr);
    }

    public static string GetApiIdentifier(Api api)
      => Marshal.PtrToStringAnsi(Interop.ApiIdentifier(api));

    public static string GetApiDisplayName(Api api)
      => Marshal.PtrToStringAnsi(Interop.ApiDisplayName(api));
}

} // namespace Libremidi
