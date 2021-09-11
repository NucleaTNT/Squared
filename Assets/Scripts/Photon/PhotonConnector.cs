using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private string versionName, sceneToLoad;
    [SerializeField] private InputField hostRoomInput, joinRoomInput, usernameInput;
    [SerializeField] private GameObject usernameMenuObj;
    [SerializeField] private Text hostInpPlaceText, joinInpPlaceText, userInpPlaceText, usernameConfirmText;
    
    private static Hashtable availableColorsHash = new Hashtable { 
                                            {0, false}, 
                                            {1, false},
                                            {2, false},
                                            {3, false},
                                        };
    private Hashtable customRoomProperties = new Hashtable { {"AvailableColors", availableColorsHash} };

	#region Private Methods
	
	private void AttemptLoadUsername()
	{
		if (PlayerPrefs.HasKey("Username")) usernameInput.text = PlayerPrefs.GetString("Username");
	}
	
	private void SaveUsername(string username) => PlayerPrefs.SetString("Username", username);
	
	#endregion

    #region Public Methods

    public void OnHostButtonClicked()
    {
        string attemptedRoomName = hostRoomInput.text;

        if (string.IsNullOrWhiteSpace(attemptedRoomName) || attemptedRoomName.Length < 3)
        { 
            Debug.Log("Host: Invalid Room Name");

            hostInpPlaceText.text = "Invalid [min 3 char]";
            hostInpPlaceText.color = Color.red;
            hostRoomInput.text = null;
        } else PhotonNetwork.CreateRoom(attemptedRoomName, new RoomOptions(){ MaxPlayers = 4, CustomRoomProperties = customRoomProperties });
    }

    public void OnJoinButtonClicked()
    {
        string attemptedRoomName = joinRoomInput.text;

        if (string.IsNullOrWhiteSpace(attemptedRoomName) || attemptedRoomName.Length < 3) 
        { 
            Debug.Log("Join: Invalid Room Name");

            joinInpPlaceText.color = Color.red;
            joinInpPlaceText.text = "Invalid [min 3 char]";
            joinRoomInput.text = null;
        } else PhotonNetwork.JoinRoom(attemptedRoomName);
    }

    public void OnUsernameButtonClicked()
    {
        string username = usernameInput.text;

        if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
        {
            Debug.Log($"UsernameButton: Invalid Username");

            userInpPlaceText.color = Color.red;
            userInpPlaceText.text = "Invalid [min 3 char]";
            usernameInput.text = null;
        } else StartCoroutine(WaitTillConnectedToMaster(username));
    }

    #endregion

    #region Coroutines

    private System.Collections.IEnumerator WaitTillConnectedToMaster(string username) 
    {
        while (!PhotonNetwork.IsConnected) yield return null;
        
        PhotonNetwork.LocalPlayer.NickName = username;
        usernameMenuObj.SetActive(false);
		SaveUsername(username);
    }

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
	{ 
		if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
		AttemptLoadUsername();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    #endregion

    #region PUN Callbacks

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected to Master.");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"Loading Scene: {sceneToLoad}");
        PhotonNetwork.LoadLevel(sceneToLoad);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => Debug.Log($"Error Code [{returnCode}]: {message}");
    public override void OnJoinRoomFailed(short returnCode, string message) => Debug.Log($"Error Code [{returnCode}]: {message}");

    #endregion
}
