using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableSizeKeepHandler : MonoBehaviour
{
    [SerializeField]
    float originalScale;

    void Update()
    {
        transform.localScale = new Vector3(originalScale, originalScale, originalScale / transform.parent.localScale.z);
    }
}
