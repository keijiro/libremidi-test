using UnityEngine;
using Libremidi;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public sealed class MidiInTest : MonoBehaviour
{
    List<IntPtr> _inPorts = new List<IntPtr>();
    List<IntPtr> _outPorts = new List<IntPtr>();
    IntPtr _observer, _midiIn;

    public delegate void PortCallback(IntPtr ctx, IntPtr port);
    public delegate void EventCallback(IntPtr ctx, long time, IntPtr data, nuint size);

    void OnMidiInEvent(IntPtr ctx, long time, IntPtr pData, nuint size)
    {
        var line = "";
        unsafe
        {
            var data = new Span<byte>((byte*)pData, (int)size);
            if (data[0] == 0xf8) return;
            for (var i = 0; i < data.Length; i++) line += $"{data[i]:X2} ";
        }
        Debug.Log(line);
    }

    void OnQueryApi(IntPtr ctx, Api api)
      => Debug.Log($"API found: {MidiSystem.GetApiDisplayName(api)}");

    void OnQueryInputPort(IntPtr ctx, IntPtr port)
    {
        IntPtr pname;
        Interop.MidiInPortName(port, out pname, out _);
        Debug.Log($"MIDI-In port found: {Marshal.PtrToStringAnsi(pname)}");

        IntPtr clone;
        Interop.MidiInPortClone(port, out clone);
        _inPorts.Add(clone);
    }

    void OnQueryOutputPort(IntPtr ctx, IntPtr port)
    {
        IntPtr pname;
        Interop.MidiOutPortName(port, out pname, out _);
        Debug.Log($"MIDI-Out port found: {Marshal.PtrToStringAnsi(pname)}");

        IntPtr clone;
        Interop.MidiOutPortClone(port, out clone);
        _outPorts.Add(clone);
    }

    void Start()
    {
        Debug.Log($"libremidi version: {MidiSystem.GetVersion()}");

        MidiSystem.QueryMidi1Apis(OnQueryApi);

        ObserverConfiguration ob_cfg;
        Interop.MidiObserverConfigurationInit(out ob_cfg);
        ob_cfg.trackHardware = true;

        ApiConfiguration api_cfg;
        Interop.MidiApiConfigurationInit(out api_cfg);
        api_cfg.configurationType = ApiConfiguration.ConfigurationType.Observer;

        Interop.MidiObserverNew(ob_cfg, ref api_cfg, out _observer);

        var pOnInputPortFound = Marshal.GetFunctionPointerForDelegate((PortCallback)OnQueryInputPort);
        Interop.MidiObserverEnumerateInput(_observer, IntPtr.Zero, pOnInputPortFound);

        var pOnOutputPortFound = Marshal.GetFunctionPointerForDelegate((PortCallback)OnQueryOutputPort);
        Interop.MidiObserverEnumerateOutput(_observer, IntPtr.Zero, pOnOutputPortFound);

        MidiConfiguration midi;
        Interop.MidiConfigurationInit(out midi);
        midi.version = MidiConfiguration.MidiVersion.MIDI1;
        midi.port = _inPorts[0];
        midi.midi1_cb.callback = 
          Marshal.GetFunctionPointerForDelegate((EventCallback)OnMidiInEvent);

        api_cfg.configurationType = ApiConfiguration.ConfigurationType.Input;

        Interop.MidiInNew(midi, api_cfg, out _midiIn);
    }

    void OnDestroy()
    {
        foreach (var port in _inPorts) Interop.MidiInPortFree(port);
        foreach (var port in _outPorts) Interop.MidiOutPortFree(port);
        if (_midiIn != IntPtr.Zero) Interop.MidiInFree(_midiIn);
        if (_observer != IntPtr.Zero) Interop.MidiObserverFree(_observer);
    }
}
