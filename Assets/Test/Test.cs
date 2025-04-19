using UnityEngine;
using Libremidi;

public sealed class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log(MidiSystem.GetVersion());

        MidiSystem.QueryMidi1Apis
          ((System.IntPtr ctx, Api api)
              => Debug.Log($"{api}" +
                           $" : {MidiSystem.GetApiIdentifier(api)}" +
                           $" : {MidiSystem.GetApiDisplayName(api)}"));

        ApiConfiguration apiConfig;
        Interop.MidiApiConfigurationInit(out apiConfig);

        Debug.Log($"{apiConfig.api} {apiConfig.configurationType}");
    }
}
