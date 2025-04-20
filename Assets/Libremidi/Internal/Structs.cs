using System;
using System.Runtime.InteropServices;

namespace Libremidi {

public enum Api
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

public enum ConfigurationType
{
    Observer = 0,
    Input = 1,
    Output = 2
}

public enum MidiVersion
{
    Midi1    = 1 << 1,
    Midi1Raw = 1 << 2,
    Midi2    = 1 << 3,
    Midi2Raw = 1 << 4
}

public enum TimestampMode
{
    NoTimestamp = 0,
    Relative,
    Absolute,
    SystemMonotonic,
    AudioFrame,
    Custom
}

[StructLayout(LayoutKind.Sequential)]
public struct ApiConfiguration
{
    public Api api;
    public ConfigurationType configurationType;
    public IntPtr data;
}

[StructLayout(LayoutKind.Sequential)]
public struct ObserverConfiguration
{
    public IntPtr onErrorContext;
    public IntPtr onError;

    public IntPtr onWarningContext;
    public IntPtr onWarning;

    public IntPtr inputAddedContext;
    public IntPtr inputAdded;

    public IntPtr inputRemovedContext;
    public IntPtr inputRemoved;

    public IntPtr outputAddedContext;
    public IntPtr outputAdded;

    public IntPtr outputRemovedContext;
    public IntPtr outputRemoved;

    [MarshalAs(UnmanagedType.I1)] public bool trackHardware;
    [MarshalAs(UnmanagedType.I1)] public bool trackVirtual;
    [MarshalAs(UnmanagedType.I1)] public bool trackAny;
    [MarshalAs(UnmanagedType.I1)] public bool notifyInConstructor;
}

[StructLayout(LayoutKind.Sequential)]
public struct MidiConfiguration
{

    public MidiVersion version;
    public IntPtr port;

    public IntPtr onDataContext;
    public IntPtr onData;

    public IntPtr onTimestampContext;
    public IntPtr onTimestamp;

    public IntPtr onErrorContext;
    public IntPtr onError;

    public IntPtr onWarningContext;
    public IntPtr onWarning;

    public IntPtr portName;
    [MarshalAs(UnmanagedType.I1)] public bool virtualPort;

    [MarshalAs(UnmanagedType.I1)] public bool ignoreSysex;
    [MarshalAs(UnmanagedType.I1)] public bool ignoreTiming;
    [MarshalAs(UnmanagedType.I1)] public bool ignoreSensing;

    public TimestampMode timestamps;
}

} // namespace Libremidi
