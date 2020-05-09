using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : Singleton<AudioManager>
{
    #region Fields

    private Dictionary<string, string> dict_eventPaths = new Dictionary<string, string>
    {
        {"ambient/1", "event:/Ambience/Ambient1" },

        {"bullet/bounce", "event:/Bullet/bullet_bounce" },
        {"bullet/bounce2", "event:/Bullet/bullet_bounce2" },
        {"bullet/cratedestruction", "event:/Bullet/bullet_creatdestruction" },
        {"bullet/destruction", "event:/Bullet/bullet_destruction" },

        {"game/arrow", "event:/InGameUI/arrow_notification" },
        {"game/end", "event:/InGameUI/game_end" },
        {"game/final", "event:/InGameUI/game_finalscreen" },
        {"game/transition", "event:/InGameUI/game_transition" },

        {"menu/buttonclick", "event:/MenuUI/click_button01" },
        {"menu/buttonclick2", "event:/MenuUI/click_button02" },
        {"menu/gametoggle", "event:/MenuUI/game_toggle" },
        {"menu/selector1", "event:/MenuUI/move_selector1" },
        {"menu/selector2", "event:/MenuUI/move_selector2" },
        {"game/countdown", "event:/MenuUI/game_countdown" },
        
        {"player/destroyeffect", "event:/PlayerEffect/player_destroy_effect" },
        {"player/respawneffect", "event:/PlayerEffect/player_respawn_effect" },
        {"player/shooteffect", "event:/PlayerEffect/player_shoot_effect" },
        {"player/shootfail", "event:/PlayerEffect/player_shoot_fail" },
        {"player/shootfail2", "event:/PlayerEffect/player_shoot_fail2" },
        {"player/pickupammo", "event:/PlayerEffect/player_pickup_ammo" },
        {"player/pickuppowerup", "event:/PlayerEffect/player_pickup_powerup" },
        {"player/movement", "event:/PlayerEffect/player_movement" },

        {"music/UI", "event:/Music/music_UI" },
        {"music/game", "event:/Music/music_game" },



    };
    // Handle looping
    private bool isLooping = false;
    private EventInstance loopInstance;
    private EventInstance instance;
    private EventInstance manyMusic;



    private Bus bus;

    [SerializeField] private float masterVolume = 3f;

    #endregion

    public void PlayMusicLoop(string key)
    {
        manyMusic = FMODUnity.RuntimeManager.CreateInstance(dict_eventPaths[key]);

        // Set volume level
        manyMusic.setVolume(masterVolume);

        manyMusic.start();
    }

    public void StopMusicLoop()
    {
        manyMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        manyMusic.release();
    }



    public void PlaySound(string key)
    {
        //instance.release();
        instance = FMODUnity.RuntimeManager.CreateInstance(dict_eventPaths[key]);

        // Set volume level
        instance.setVolume(masterVolume);

        instance.start();
        instance.release();
    }

    public void PlayLoop(string key)
    {
        if (!isLooping)
        {
            isLooping = true;
            loopInstance = FMODUnity.RuntimeManager.CreateInstance(dict_eventPaths[key]);

            // Set volume level
            loopInstance.setVolume(masterVolume);

            loopInstance.start();
        }
            
    }
    private void GetOutOfLoopAndPlayRest()
    {
        // Play the end track
        loopInstance.setParameterByName("End", 0.5f);
    }

    public void StopLoop()
    {
        if (isLooping)
        {
            GetOutOfLoopAndPlayRest();

            loopInstance.release();
            isLooping = false;
        }
    }

    public void StopAllSounds()
    {
        bus = RuntimeManager.GetBus("event:/PlayerEffect");
        bus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
