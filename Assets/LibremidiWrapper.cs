using System;
using System.Runtime.InteropServices;

namespace Libremidi {

public static class LibremidiWrapper
{
    const string dllName = "liblibremidi.dylib";

    [DllImport(dllName, EntryPoint = "libremidi_get_version")]
    public static extern IntPtr GetVersion();

    [DllImport(dllName, EntryPoint = "libremidi_midi1_available_apis")]
    public static extern void Midi1AvailableApis(IntPtr ctx, IntPtr cb);

    [DllImport(dllName, EntryPoint = "libremidi_midi2_available_apis")]
    public static extern void Midi2AvailableApis(IntPtr ctx, IntPtr cb);

    [DllImport(dllName, EntryPoint = "libremidi_api_identifier")]
    public static extern IntPtr ApiIdentifier(Api api);

    [DllImport(dllName, EntryPoint = "libremidi_api_display_name")]
    public static extern IntPtr ApiDisplayName(Api api);

    [DllImport(dllName, EntryPoint = "libremidi_get_compiled_api_by_identifier")]
    public static extern Api GetCompiledApiByIdentifier(string id);

    [DllImport(dllName, EntryPoint = "libremidi_midi_api_configuration_init")]
    public static extern int MidiApiConfigurationInit(ref ApiConfiguration cfg);

    [DllImport(dllName, EntryPoint = "libremidi_midi_observer_configuration_init")]
    public static extern int MidiObserverConfigurationInit(ref ObserverConfiguration cfg);

    [DllImport(dllName, EntryPoint = "libremidi_midi_configuration_init")]
    public static extern int MidiConfigurationInit(ref MidiConfiguration cfg);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_port_clone")]
    public static extern int MidiInPortClone(IntPtr port, IntPtr dst);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_port_free")]
    public static extern int MidiInPortFree(IntPtr port);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_port_name")]
    public static extern int MidiInPortName(IntPtr port, IntPtr name, IntPtr len);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_port_clone")]
    public static extern int MidiOutPortClone(IntPtr port, IntPtr dst);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_port_free")]
    public static extern int MidiOutPortFree(IntPtr port);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_port_name")]
    public static extern int MidiOutPortName(IntPtr port, IntPtr name, IntPtr len);

    [DllImport(dllName, EntryPoint = "libremidi_midi_observer_new")]
    public static extern int MidiObserverNew(in ObserverConfiguration ob_cfg, ref ApiConfiguration api_cfg, IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_observer_free")]
    public static extern int MidiObserverFree(IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_new")]
    public static extern int MidiInNew(in MidiConfiguration mcfg, in ApiConfiguration acfg, IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_is_connected")]
    public static extern int MidiInIsConnected(IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_absolute_timestamp")]
    public static extern long MidiInAbsoluteTimestamp(IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_in_free")]
    public static extern int MidiInFree(IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_new")]
    public static extern int MidiOutNew(in MidiConfiguration midi, in ApiConfiguration api, IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_is_connected")]
    public static extern int MidiOutIsConnected(IntPtr handle);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_send_message")]
    public static extern int MidiOutSendMessage(IntPtr handle, byte[] msg, int size);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_send_ump")]
    public static extern int MidiOutSendUmp(IntPtr handle, uint[] ump, int size);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_schedule_message")]
    public static extern int MidiOutScheduleMessage(IntPtr handle, long ts, byte[] msg, int size);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_schedule_ump")]
    public static extern int MidiOutScheduleUmp(IntPtr handle, long ts, uint[] ump, int size);

    [DllImport(dllName, EntryPoint = "libremidi_midi_out_free")]
    public static extern int MidiOutFree(IntPtr handle);

}

} // namespace Libremidi
