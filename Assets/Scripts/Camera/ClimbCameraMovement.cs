using System.Collections.Generic;
using Dev.NucleaTNT.Squared.Gameplay;
using Dev.NucleaTNT.Squared.Managers;
using Photon.Realtime;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.CameraScripts
{
    public class ClimbCameraMovement : MonoBehaviour
    {
        public static ClimbCameraMovement Instance { get; set; } 
        public Transform TargetTransform;
        
        [SerializeField] private float _yOffset;
        private Player[] _lastPlayerList = null;
        private IEnumerator<Player> _currentPlayerEnumerator = null;

        public bool CycleTargetPlayer() 
        {
            bool success = false;

            Player[] playerList = ClimbGameManager.Instance.PlayerList;
            if (_lastPlayerList != playerList) 
            {
                _lastPlayerList = playerList;
                _currentPlayerEnumerator = ((IEnumerable<Player>)playerList).GetEnumerator();
                _currentPlayerEnumerator.MoveNext();
            }

            do 
            {
                GameObject current = (GameObject)_currentPlayerEnumerator.Current.TagObject;
                if (
                    current.GetComponent<PlayerDeathHandler>()!.IsDead    // Is this player dead?
                    || current.transform == TargetTransform               // Is this player already selected?
                ) continue;

                TargetTransform = current.transform;
                success = true;
                
                break;
            } while (_currentPlayerEnumerator.MoveNext());
            

            if (!success)   // Reset and reloop to check any at beginning
            {
                _currentPlayerEnumerator.Reset();       // Reset for next loop
                _currentPlayerEnumerator.MoveNext();

                do 
                {
                    GameObject current = (GameObject)_currentPlayerEnumerator.Current.TagObject;
                    if (
                        current.GetComponent<PlayerDeathHandler>()!.IsDead    
                        || current.transform == TargetTransform               
                    ) continue;

                    TargetTransform = current.transform;
                    success = true;

                    break;
                } while (_currentPlayerEnumerator.MoveNext());
            }

            if (success) Debug.Log($"Camera Focused On: {TargetTransform.name}");
            else Debug.Log("Couldn't cycle to next player! All other players are dead or only user in lobby!");
            
            return success;
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(this);
        }

        private void LateUpdate()
        {
            if (TargetTransform == null) return;

            float targetYPos = TargetTransform.position.y + _yOffset;
            Vector3 currentPosition = transform.position;

            transform.position = new Vector3(
                currentPosition.x,
                Mathf.Lerp(currentPosition.y, Mathf.Clamp(targetYPos, 0, float.MaxValue), Time.deltaTime * 2),
                currentPosition.z
            );
        }
    }

}