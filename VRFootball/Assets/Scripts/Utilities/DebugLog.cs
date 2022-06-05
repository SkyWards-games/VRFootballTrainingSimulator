using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public static DebugLog debug;
    TextMesh text;
    [SerializeField]
    TextMesh focus;
    [SerializeField]
    TextMesh hit;
    // Start is called before the first frame update
    void Start()
    {
        debug = this;
        text = GetComponent<TextMesh>();

        //ControllerManager.onTrigger += Trigger;
        ControllerManager.onTriggerPressed += TriggerPressed;
        //ControllerManager.onTouch += Touch;
        //ControllerManager.onTouchPressed += TouchPressed;
        //ControllerManager.onTouchClicked += TouchClicked;
    }

    //void Trigger(bool trigger)
    //{
    //    if(trigger)
    //        text.text = text.text + " Trigger";
    //}

    void TriggerPressed(bool trigger)
    {
        if (trigger)
            text.text = text.text + " TriggerPressed";
        else
            text.text = text.text + " TriggerReleased";
    }

    //void Touch(bool touch)
    //{
    //    if(touch)
    //        text.text = text.text + " Touch";
    //}

    //void TouchPressed(bool tPressed)
    //{
    //    if (tPressed)
    //        text.text = text.text + " TouchPressed";
    //}

    //void TouchClicked(bool touched)
    //{
    //    if (touched)
    //        text.text = text.text + " TouchClicked";
    //    else
    //        text.text = text.text + " TouchReleased";
    //}

    public static void Print(string log)
    {
        debug.text.text = debug.text.text + log;
    }

    public static void Focus(string focus)
    {
        debug.focus.text = "Focus : " + focus;
    }

    public static void Hit(string hit)
    {
        debug.hit.text = "Hit : " + hit;
    }
}
