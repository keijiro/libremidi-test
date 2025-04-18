using System;
using System.Runtime.InteropServices;

namespace Libremidi {

public static class MidiSystem
{
    public static string GetVersion()
      => Marshal.PtrToStringAnsi(LibremidiWrapper.GetVersion());

    public delegate void AvailableApisCallback(IntPtr ctx, Api api);

    public static void QueryMidi1Apis(AvailableApisCallback cb)
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        LibremidiWrapper.Midi1AvailableApis(IntPtr.Zero, ptr);
    }

    public static void QueryMidi2Apis(AvailableApisCallback cb)
    {
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        LibremidiWrapper.Midi2AvailableApis(IntPtr.Zero, ptr);
    }

    public static string GetApiIdentifier(Api api)
      => Marshal.PtrToStringAnsi(LibremidiWrapper.ApiIdentifier(api));

    public static string GetApiDisplayName(Api api)
      => Marshal.PtrToStringAnsi(LibremidiWrapper.ApiDisplayName(api));
}

} // namespace Libremidi
