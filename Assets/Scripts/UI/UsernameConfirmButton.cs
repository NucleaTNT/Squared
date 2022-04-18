using Dev.NucleaTNT.Squared.Managers;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UsernameConfirmButton : MonoBehaviour
{
    [SerializeField] private Text _userInputPlaceText;
    [SerializeField] private Button _usernameButton; 
    [SerializeField] private InputField _usernameInput; 
    [SerializeField] private GameObject _usernameMenuObj;

    public bool IsInteractable 
    {
        get 
        {
            return _usernameButton.interactable;
        }

        set 
        {
            _usernameButton.interactable = value;
            if (value) _usernameMenuObj.SetActive(true);
        }
    }

    public void OnClick_ConfirmUsername()
    {
        string username = _usernameInput.text;

        if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
        {
            Debug.Log("UsernameButton: Invalid Username");

            _userInputPlaceText.color = Color.red;
            _userInputPlaceText.text = "Invalid [min 3 char]";
            _usernameInput.text = null;
        } else 
        {
            PhotonNetwork.LocalPlayer.NickName = username;
		    SaveUsername(username);

            PhotonNetwork.LoadLevel(PhotonManager.PhotonConnector.LobbySceneIndex);
            _usernameMenuObj.SetActive(false);

            if (PhotonNetwork.InLobby) return;
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
    }

    private void AttemptLoadUsername()
	{
		if (PlayerPrefs.HasKey("Username")) _usernameInput.text = PlayerPrefs.GetString("Username");
	}
	
	private void SaveUsername(string username) => PlayerPrefs.SetString("Username", username);

    private void Awake()
    {
        AttemptLoadUsername();
    }
}