using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCommentPanelController : MonoBehaviour
{
    public const byte PlayerTeleport = 0;
    public const byte EditObject = 1;
    public const byte ShirtColorChange = 2;
    public const byte ResetArrowsCount = 3;
    public const byte SendArrowComment = 4;

    public static int currentArrowPhotonViewID;
    internal static ArrowCommentPanelController instance;

    Transform panel;
    [SerializeField]
    InputField input;
    
    private void Awake()
    {
        instance = this;
        panel = transform.GetChild(0);
    }

    public void EnableCommentPanel()
    {
        //Comment the line below to disable commenting functionality
        //panel.gameObject.SetActive(true);
    }

    public void DisableCommentPanel()
    {
        panel.gameObject.SetActive(false);
        input.text = "";
    }

    public void SaveComment()
    {
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        Debug.Log(input.text);
        Debug.Log(currentArrowPhotonViewID);

        content = new object[] { input.text, currentArrowPhotonViewID }; // Array contains the target position and the IDs of the selected units
        PhotonNetwork.RaiseEvent(SendArrowComment, content, raiseEventOptions, SendOptions.SendReliable);

        DisableCommentPanel();
    }
}
