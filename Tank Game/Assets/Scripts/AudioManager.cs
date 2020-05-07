using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AudioManager : Singleton<AudioManager>
{
    #region Fields

    private Dictionary<string, string> dict_eventPaths = new Dictionary<string, string>
    {
        {"ambient/1", "event:/Ambience/Ambient1" },

        {"menu/selector1", "event:/MenuUI/move_selector1" },

        {"game/arrow", "event:/InGameUI/arrow_notification" },
        {"game/transition", "event:/InGameUI/game_transition" },
        {"game/final", "event:/InGameUI/game_finalscreen" },
        {"game/end", "event:/InGameUI/game_end" },
    };

    private EventInstance instance;

    #endregion


    public void PlaySound(string key)
    {
        //instance.release();
        instance = FMODUnity.RuntimeManager.CreateInstance(dict_eventPaths[key]);
        instance.start();
        instance.release();
    }
}
