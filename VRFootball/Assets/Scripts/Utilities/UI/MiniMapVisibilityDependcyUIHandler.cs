using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapVisibilityDependcyUIHandler : MiniMapVisibilityDependcyHandler
{
    [SerializeField]
    GameObject[] panelsToHide;
    
    public override void SetActive()
    {
        base.SetActive();

        foreach (GameObject panel in panelsToHide)
            panel.SetActive(!panel.activeSelf);
    }
}
