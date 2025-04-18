using UnityEngine;
using Libremidi;

public sealed class Test : MonoBehaviour
{
    void Start()
    {
        MidiSystem.QueryMidi1Apis(
                (System.IntPtr ctx, Api api)
                  => Debug.Log($"{api} : {MidiSystem.GetApiIdentifier(api)} : {MidiSystem.GetApiDisplayName(api)}"));
        Debug.Log(MidiSystem.GetVersion());
    }
}
