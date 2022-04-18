using UnityEngine;

namespace Dev.NucleaTNT.Squared
{
	[CreateAssetMenu(menuName = "ScriptableObjects/GameSettings")]
	public class GameSettings : ScriptableObject
	{
		public bool IsAutomaticallySyncSceneEnabled => _isAutomaticallySyncSceneEnabled;
		public string GameVersion => _gameVersion;
		// public string NickName => _nickName + Random.Range(0, 99);	// Allows for differentiation between clients, testing purposes only
		
		[SerializeField] private string _gameVersion;
		// [SerializeField] private string _nickName;
		[SerializeField] private bool _isAutomaticallySyncSceneEnabled;
	}
}
