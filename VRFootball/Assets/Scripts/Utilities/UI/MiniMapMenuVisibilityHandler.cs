using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapMenuVisibilityHandler : MonoBehaviour
{
    bool visible;

    public void ChangeButtonsVisibility()
    {
        visible = !visible;

        EventsManager.onMenuButtonsVisibilityChange();

        if (!visible)
            MiniMapUIEventsHandler.instance.mode = EditMode.Teleportation;
    }
}
