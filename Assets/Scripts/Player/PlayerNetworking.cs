using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public struct PlayerInfo
{
    public int Wins;
    public int PlayerColorIndex;
    public int PlayerID;
    public string Username;
}

public class PlayerNetworking : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;
    private Color[] playerColors = { Color.red, Color.blue, Color.green, Color.yellow, Color.white };
    public static GameObject LocalPlayerInstance; 
    public PlayerInfo Info;

    protected void Awake() 
    {
        if (photonView.IsMine) LocalPlayerInstance = this.gameObject;
        if (!photonView.IsMine) foreach (MonoBehaviour script in scriptsToIgnore) script.enabled = false;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo photonMessageInfo) 
    {
        GameObject instantiatedObj = this.gameObject;
        
        photonMessageInfo.Sender.TagObject = instantiatedObj;
        Info.PlayerColorIndex = (int)photonMessageInfo.photonView.InstantiationData[0];
        Info.PlayerID = photonMessageInfo.Sender.ActorNumber;
        Info.Username = (string)photonMessageInfo.Sender.NickName;

        SetInstantiationData(photonMessageInfo);

        Debug.Log($"OnPhotonInstantiate\n{instantiatedObj}\n{Info.PlayerColorIndex}\n{Info.PlayerID}\n{Info.Username}");

        photonView.RPC("UpdatePlayerAppearance", RpcTarget.AllBufferedViaServer, Info.PlayerID);
    }

    protected virtual void SetInstantiationData(PhotonMessageInfo photonMessageInfo) {}

    [PunRPC]
    protected void UpdatePlayerAppearance(int playerID)
    {
        GameObject playerGO = (GameObject)PhotonNetwork.CurrentRoom.GetPlayer(playerID).TagObject;
        PlayerInfo playerInfo = playerGO.GetComponent<PlayerNetworking>().Info;

        Debug.Log($"RPC Received: UpdatePlayerAppearance -> [{playerID}]{playerInfo.Username}[{playerInfo.PlayerID}]");

        playerGO.GetComponent<SpriteRenderer>().color = playerColors[playerInfo.PlayerColorIndex];
        playerGO.GetComponentInChildren<Text>().text = playerInfo.Username;
    }
}
