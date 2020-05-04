using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OnlyKeyboardInputModule : StandaloneInputModule
{
    //mouse switcher interface 
    public void MouseSwitcher()
    {
        GameStats.Instance.isMouseInputActive = GameStats.Instance.isMouseInputActive == false ? true : false;
    }

    //inherited event processing interface (called every frame)
    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }

        if (SceneManager.GetActiveScene().name != "LevelSelection" && GameStats.Instance.isMouseInputActive)
            ProcessMouseEvent();
    }
}
