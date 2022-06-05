using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MannequinButtonColorChangeHandler : MonoBehaviour
{
    Button button;

    public void SetShirtColor(int targetColorIndex)
    {
        button = GetComponent<Button>();
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

        button.image.color = selectedShirtColor;
    }
}
