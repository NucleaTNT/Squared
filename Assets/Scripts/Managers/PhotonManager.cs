using UnityEngine;

namespace Dev.NucleaTNT.Squared.Managers
{
	public class PhotonManager : MonoBehaviour
	{
		public static Color BackgroundImageColor = Color.red;
		public static GameSettings GameSettings => Instance._gameSettings;
		public static PhotonManager Instance { get; private set; }
		public static PhotonConnector PhotonConnector => Instance._photonConnector;
		public static UsernameConfirmButton UsernameConfirmButton => Instance._usernameConfirmButton;

		[SerializeField] private GameSettings _gameSettings;
		[SerializeField] private PhotonConnector _photonConnector;
        [SerializeField] private UsernameConfirmButton _usernameConfirmButton;

        private void Awake()
		{
			SingletonCheck();
			InitializeMembers();
		}

		private void InitializeMembers()
		{
			if (_gameSettings == null) _gameSettings = Resources.Load<GameSettings>("GameSettings");
			if (_photonConnector == null) _photonConnector = GetComponentInChildren<PhotonConnector>(); 
			if (_usernameConfirmButton == null) _usernameConfirmButton = GetComponentInChildren<UsernameConfirmButton>(); 
		}

		private void SingletonCheck()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(this);
			}
			else if (Instance != this) Destroy(this);
		}
	}
}
