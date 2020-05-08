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

        {"bullet/bounce", "event:/Bullet/bullet_bounce" },
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
