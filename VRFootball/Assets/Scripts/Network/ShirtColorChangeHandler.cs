using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtColorChangeHandler : MonoBehaviour
{
    SkinnedMeshRenderer rend;

    [SerializeField]
    GameObject arrow;

    public void SetShirtColor(int targetColorIndex)
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        Color selectedShirtColor = new Color();

        switch (targetColorIndex)
        {
            case 0:
                selectedShirtColor = Color.red;
                break;

            case 1:
                selectedShirtColor = new Color(1f, 0.5f, 0f, 1f);
                break;

            case 2:
                selectedShirtColor = Color.yellow;
                break;

            case 3:
                selectedShirtColor = Color.green;
                break;

            case 4:
                selectedShirtColor = Color.cyan;
                break;

            case 5:
                selectedShirtColor = Color.blue;
                break;

            case 6:
                selectedShirtColor = Color.magenta;
                break;
        }

        rend.materials[2].color = selectedShirtColor;
        GetComponentInChildren<NetworkPlayerIconHolder>().SetPlayerIconColor(selectedShirtColor);
    }

    public void DisableDirectionArrow()
    {
        arrow.SetActive(false);
    }
}
