using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;



public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct EventPath
    {
        public string key;

        [FMODUnity.EventRef]
        public string path;
    }

    public EventPath[] eventPaths;
    private Dictionary<string, string> dict_eventPaths;

    private EventInstance instance;

    // Start is called before the first frame update
    void Start()
    {
        // Convert all eventPath from array to a dictionary
        dict_eventPaths = new Dictionary<string, string>();
        for (int i = 0; i < eventPaths.Length; i++)
        {
            dict_eventPaths[eventPaths[i].key] = eventPaths[i].path;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string key)
    {
        //instance.release();
        instance = FMODUnity.RuntimeManager.CreateInstance(dict_eventPaths[key]);
        instance.start();
        instance.release();
    }
}
