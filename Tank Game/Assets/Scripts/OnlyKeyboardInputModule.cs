using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        if (GameStats.Instance.isMouseInputActive)
            ProcessMouseEvent();
    }
}
