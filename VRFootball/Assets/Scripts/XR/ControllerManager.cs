using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerManager : MonoBehaviour
{
    [SerializeField]
    TextMesh debugText;
    InputDevice controller;

    [SerializeField]
    Vector3 positionOffset;

    [SerializeField]
    bool trigger;

    [SerializeField]
    bool triggerPressed;

    [SerializeField]
    bool touch;

    [SerializeField]
    bool touched;

    [SerializeField]
    bool touchPressed;

    [SerializeField]
    bool touchClicked;

    public delegate void OnTrigger(bool trigger);
    public static OnTrigger onTrigger;

    public delegate void OnTriggerPressed(bool pressed);
    public static OnTriggerPressed onTriggerPressed;

    public delegate void OnTouch(bool touch);
    public static OnTouch onTouch;

    public delegate void OnTouched(bool touched);
    public static OnTouched onTouched;

    public delegate void OnTouchPressed(bool pressed);
    public static OnTouchPressed onTouchPressed;

    public delegate void OnTouchClicked(bool cliked);
    public static OnTouchClicked onTouchClicked;

    // Start is called before the first frame update
    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        Quaternion rot = new Quaternion();
        controller.TryGetFeatureValue(CommonUsages.devicePosition, out pos);
        controller.TryGetFeatureValue(CommonUsages.deviceRotation, out rot);
        transform.localPosition = pos + positionOffset;
        transform.rotation = rot;

        bool pressed;
        controller.TryGetFeatureValue(CommonUsages.triggerButton, out pressed);
        Trigger(pressed);
        TriggerPressed(pressed);


        bool t = false;
        controller.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out t);
        Touch(t);
        Touched(t);

        
        bool tPressed = false;
        controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out tPressed);
        TouchPressed(tPressed);
        TouchClicked(tPressed);

        debugText.text = "Pr: " + pressed + "Tr: " + trigger + " Cl: " + triggerPressed + " To:" + touched + " ToPr:" + touchPressed + " ToCl:" + touchClicked;
    }

    void Trigger(bool pressed)
    {
        onTrigger?.Invoke(trigger = pressed);
    }

    void TriggerPressed(bool pressed)
    {
        if (!triggerPressed && pressed)
        {
            onTriggerPressed?.Invoke(triggerPressed = true);
        }
        if (triggerPressed && !pressed)
        {
            onTriggerPressed?.Invoke(triggerPressed = false);
        }
    }

    void Touch(bool t)
    {
        onTouch?.Invoke(touch = t);
    }

    void Touched(bool th)
    {
        if (!touched && th)
        {
            onTouched?.Invoke(touched = true);
        }
        if (touched && !th)
        {
            onTouched?.Invoke(touched = false);
        }
    }

    void TouchPressed(bool tPressed)
    {
        onTouchPressed?.Invoke(touchPressed = tPressed);
    }

    void TouchClicked(bool tClicked)
    {
        if (!touchClicked && tClicked)
        {
            onTouchClicked?.Invoke(touchClicked = true);
        }
        if (touchClicked && !tClicked)
        {
            onTouchClicked?.Invoke(touchClicked = false);
        }
    }
}
