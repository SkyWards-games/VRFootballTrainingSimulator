using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCommentHolder : MonoBehaviour
{
    TextMesh text;

    private void Awake()
    {
        text = GetComponent<TextMesh>();
    }

    public void SetComment(string comment, float rotation)
    {
        text.text = comment;
        if(rotation > 0.7f)
        {
            text.anchor = TextAnchor.MiddleLeft;
            text.transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            text.anchor = TextAnchor.MiddleRight;
            text.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
