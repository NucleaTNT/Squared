using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.NucleaTNT.PUNTesting
{
    public class MasterClientButton : MonoBehaviourPun
    {
        [SerializeField] private Button _button;
        
        protected virtual void Awake()
        {
            if (_button == null) _button = GetComponent<Button>();
        }

        protected virtual void OnEnable()
        {
            _button.interactable = PhotonNetwork.IsMasterClient;
        }
    }
}