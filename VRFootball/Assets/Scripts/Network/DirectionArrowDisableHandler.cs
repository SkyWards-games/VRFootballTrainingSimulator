using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrowDisableHandler : MonoBehaviour
{
    [SerializeField]
    GameObject arrow;

    public void DisableDirectionArrow()
    {
        arrow.SetActive(false);
    }
}
