using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGameManager : MonoBehaviourPunCallbacks
{
    protected Player[] playerList;
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected Vector3 playerInstantiatePos = new Vector3(0, 0, 0);

    private void Awake()
    {
        playerList = PhotonNetwork.PlayerList;
        if (PlayerNetworking.LocalPlayerInstance == null) InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        string username = PhotonNetwork.LocalPlayer.NickName;
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Instantiating Player: {username}[{playerID}]");

        int colorIndex = 4;
        Hashtable roomProperties = (Hashtable)PhotonNetwork.CurrentRoom.CustomProperties;
        foreach (System.Collections.DictionaryEntry keyValue in (Hashtable)roomProperties["AvailableColors"])
        {
            if (!(bool)keyValue.Value)
            {
                colorIndex = int.Parse(keyValue.Key.ToString());
                ((Hashtable)(roomProperties["AvailableColors"]))[keyValue.Key] = true;
                break;
            }
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);

        object[] playerInitData = GetPlayerInitData();
        object[] playerInstantiateData = new object[(playerInitData.Length * sizeof(int)) + 1];
        playerInstantiateData[0] = colorIndex;
        playerInitData.CopyTo(playerInstantiateData, 1);

        PhotonNetwork.Instantiate($"Prefabs/Player/{playerPrefab.name}", playerInstantiatePos, Quaternion.identity, data: playerInstantiateData);
    }

    protected virtual Hashtable SetCustomRoomProperties(Hashtable roomProperties)
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        return roomProperties;
    }

    protected virtual object[] GetPlayerInitData()
    {
        return new object[]{};
    }

    protected void UpdatePlayerList() => playerList = PhotonNetwork.PlayerList;

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"New Player Joined: {newPlayer.NickName}[{newPlayer.ActorNumber}]");
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();

        int index = ((GameObject)otherPlayer.TagObject).GetComponent<PlayerNetworking>().Info.PlayerColorIndex;
        if (index != 4) 
        {
            Hashtable roomProperties = (Hashtable)PhotonNetwork.CurrentRoom.CustomProperties;
            ((Hashtable)(roomProperties["AvailableColors"]))[index] = false;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
