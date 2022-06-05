using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerButtonHolder : MonoBehaviour
{
    public NetworkCopier networkPlayer;
    
    Button btn;
    [SerializeField]
    Text text;
    
    void Start()
    {
        btn = GetComponent<Button>();
        //text = GetComponentInChildren<Text>();
        btn.onClick.AddListener(TeleportPlayerMode);
    }

    public void SetPlayerLabelText(string playerId)
    {
        text.text = playerId;
    }

    void TeleportPlayerMode()
    {
        GetComponent<MiniMapModeSetHandler>().SetPlayerTeleportMode();
        MiniMapUIEventsHandler.instance.SetSelectedNetworkPlayer(networkPlayer.photonView.ControllerActorNr);
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveAllListeners();
    }
}
