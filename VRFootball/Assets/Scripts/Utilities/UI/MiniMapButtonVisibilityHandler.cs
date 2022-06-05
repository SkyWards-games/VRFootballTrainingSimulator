using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapButtonVisibilityHandler : MonoBehaviour
{
    Button button;
    Image image;

    private void Start()
    {
        button = GetComponent<Button>();
        if(transform.childCount > 0)
            image = transform.GetChild(0).GetComponent<Image>();
    }

    void OnEnable()
    {
        EventsManager.onMenuButtonsVisibilityChange += ChangeButtonVisibility;
    }
    
    void ChangeButtonVisibility()
    {
        button.image.enabled = !button.image.enabled;
        button.enabled = !button.enabled;

        if (image != null)
            image.enabled = !image.enabled;
    }

    void OnDisable()
    {
        EventsManager.onMenuButtonsVisibilityChange -= ChangeButtonVisibility;
    }
}
