using Dev.NucleaTNT.Squared;
using Dev.NucleaTNT.Squared.Managers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonConnector : MonoBehaviourPunCallbacks
{    
    public int LobbySceneIndex => _lobbySceneIndex;

    [SerializeField] private int _lobbySceneIndex;
    [SerializeField] private UsernameConfirmButton _usernameConfirmButton;

    #region MonoBehaviour Callbacks

    private void Awake()
	{ 
        _usernameConfirmButton.IsInteractable = false;
        
        GameSettings gameSettings = PhotonManager.GameSettings;
        PhotonNetwork.AutomaticallySyncScene = gameSettings.IsAutomaticallySyncSceneEnabled;

        if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PhotonNetwork.InRoom) Application.Quit();
    }

    #endregion

    #region PUN Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");
        _usernameConfirmButton.IsInteractable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from Server. Reason: {cause}");
    }

    #endregion
}
