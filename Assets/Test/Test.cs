using UnityEngine;
using Libremidi;
using System;
using System.Runtime.InteropServices;

public sealed class Test : MonoBehaviour
{
    public delegate void PortCallback(IntPtr ctx, IntPtr port);

    static void OnQueryApi(IntPtr ctx, Api api)
      => Debug.Log($"API found: {MidiSystem.GetApiDisplayName(api)}");

    static void OnQueryInputPort(IntPtr ctx, IntPtr port)
    {
        IntPtr pname;
        Interop.MidiInPortName(port, out pname, out _);
        Debug.Log($"MIDI-In device found: {Marshal.PtrToStringAnsi(pname)}");
    }

    static void OnQueryOutputPort(IntPtr ctx, IntPtr port)
    {
        IntPtr pname;
        Interop.MidiOutPortName(port, out pname, out _);
        Debug.Log($"MIDI-Out device found: {Marshal.PtrToStringAnsi(pname)}");
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

        IntPtr observer;
        Interop.MidiObserverNew(ob_cfg, ref api_cfg, out observer);

        var pOnInputPortFound = Marshal.GetFunctionPointerForDelegate((PortCallback)OnQueryInputPort);
        Interop.MidiObserverEnumerateInput(observer, IntPtr.Zero, pOnInputPortFound);

        var pOnOutputPortFound = Marshal.GetFunctionPointerForDelegate((PortCallback)OnQueryOutputPort);
        Interop.MidiObserverEnumerateOutput(observer, IntPtr.Zero, pOnOutputPortFound);

        Interop.MidiObserverFree(observer);
    }
}
