using System;
using System.Runtime.InteropServices;

namespace Libremidi {

public enum Api : int
{
    // MIDI 1.0 APIs
    Unspecified = 0x0,      // Search for a working compiled API.
    CoreMIDI = 0x1,         // macOS CoreMIDI API.
    AlsaSeq,                // Linux ALSA Sequencer API.
    AlsaRaw,                // Linux Raw ALSA API.
    JackMidi,               // JACK Low-Latency MIDI Server API.
    WindowsMM,              // Microsoft Multimedia MIDI API.
    WindowsUWP,             // Microsoft WinRT MIDI API.
    WebMIDI,                // Web MIDI API through Emscripten
    PipeWire,               // PipeWire
    Keyboard,               // Computer keyboard input
    Network,                // MIDI over IP

    // MIDI 2.0 APIs
    AlsaRawUMP = 0x1000,    // Raw ALSA API for MIDI 2.0
    AlsaSeqUMP,             // Linux ALSA Sequencer API for MIDI 2.0
    CoreMIDIUMP,            // macOS CoreMIDI API for MIDI 2.0. Requires macOS 11+
    WindowsMidiServices,    // Windows API for MIDI 2.0. Requires Windows 11
    KeyboardUMP,            // Computer keyboard input
    NetworkUMP,             // MIDI2 over IP
    JackUMP,                // MIDI2 over JACK (PipeWire v1.4+ required)
    PipeWireUMP,            // MIDI2 over PipeWire (PipeWire v1.4+ required)

    Dummy = 0xFFFF          // A compilable but non-functional API
}

[StructLayout(LayoutKind.Sequential)]
public struct ApiConfiguration
{
    public int api;
    public ConfigurationType configuration_type;
    public IntPtr data;

    public enum ConfigurationType
    {
        Observer = 0,
        Input = 1,
        Output = 2
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct ObserverConfiguration
{
    public libremidi_callback on_error;
    public libremidi_callback on_warning;

    public libremidi_input_port_event input_added;
    public libremidi_input_port_event input_removed;
    public libremidi_output_port_event output_added;
    public libremidi_output_port_event output_removed;

    [MarshalAs(UnmanagedType.I1)] public bool track_hardware;
    [MarshalAs(UnmanagedType.I1)] public bool track_virtual;
    [MarshalAs(UnmanagedType.I1)] public bool track_any;
    [MarshalAs(UnmanagedType.I1)] public bool notify_in_constructor;

    [StructLayout(LayoutKind.Sequential)]
    public struct libremidi_callback
    {
        public IntPtr context;
        public IntPtr callback; // void (*)(void*, const char*, size_t, const void*)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libremidi_input_port_event
    {
        public IntPtr context;
        public IntPtr callback; // void (*)(void*, const libremidi_midi_in_port*)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct libremidi_output_port_event
    {
        public IntPtr context;
        public IntPtr callback; // void (*)(void*, const libremidi_midi_out_port*)
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct libremidi_midi1_callback
{
    public IntPtr context;
    public IntPtr callback; // void (*)(void*, libremidi_timestamp, const uint8_t*, size_t)
}

[StructLayout(LayoutKind.Sequential)]
public struct libremidi_midi2_callback
{
    public IntPtr context;
    public IntPtr callback; // void (*)(void*, libremidi_timestamp, const uint32_t*, size_t)
}

[StructLayout(LayoutKind.Sequential)]
public struct libremidi_timestamp_callback
{
    public IntPtr context;
    public IntPtr callback; // libremidi_timestamp (*)(void*, libremidi_timestamp)
}

[StructLayout(LayoutKind.Sequential)]
public struct MidiConfiguration
{
    public MidiVersion version;

    public IntPtr port; // in_port or out_port

    // Callbacks (union, so only one is used at a time)
    public libremidi_midi1_callback midi1_cb;
    // Alternatively: public libremidi_midi2_callback midi2_cb;

    public libremidi_timestamp_callback get_timestamp;

    public ObserverConfiguration.libremidi_callback on_error;
    public ObserverConfiguration.libremidi_callback on_warning;

    public IntPtr port_name; // const char*
    [MarshalAs(UnmanagedType.I1)] public bool virtual_port;

    [MarshalAs(UnmanagedType.I1)] public bool ignore_sysex;
    [MarshalAs(UnmanagedType.I1)] public bool ignore_timing;
    [MarshalAs(UnmanagedType.I1)] public bool ignore_sensing;

    public libremidi_timestamp_mode timestamps;

    public enum MidiVersion
    {
        MIDI1 = (1 << 1),
        MIDI1_RAW = (1 << 2),
        MIDI2 = (1 << 3),
        MIDI2_RAW = (1 << 4)
    }

    public enum libremidi_timestamp_mode
    {
        NoTimestamp = 0,
        Relative,
        Absolute,
        SystemMonotonic,
        AudioFrame,
        Custom
    }
}

} // namespace Libremidi
