using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapSubMenuVisibilityHandler : MonoBehaviour
{
    [SerializeField]
    Button[] buttonsToHide;

    private void Start()
    {
        EventsManager.onMenuButtonsVisibilityChange += DisableSubMenuButtons;
    }

    public void ChangeSubMenuVisibility()
    {
        foreach(Button button in buttonsToHide)
        {
            button.image.enabled = !button.image.enabled;
            button.enabled = !button.enabled;
            button.transform.GetChild(0).GetComponent<Image>().enabled = !button.transform.GetChild(0).GetComponent<Image>().enabled;
        }
    }

    public void DisableSubMenuButtons()
    {
        if(buttonsToHide[0].enabled == true)
        {
            foreach (Button button in buttonsToHide)
            {
                button.image.enabled = false;
                button.enabled = false;
                button.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }
}
