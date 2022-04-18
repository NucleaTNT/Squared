using Photon.Pun;
using UnityEngine;

namespace Dev.NucleaTNT.Squared.Gameplay
{    
    public class BeamSpawner : MonoBehaviour
    {
        [SerializeField] private float _beamLayerSpacing;
        [SerializeField] private SteelBeamGroupInfo[] _steelBeamGroupsList;
        private SteelBeamGroupInfo _lastBeamGroup = null;

        private SteelBeamGroupInfo GetRandomBeamGroup() => _steelBeamGroupsList[Random.Range(0, _steelBeamGroupsList.Length)];

        public void GenerateNextBeamLayer()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            SteelBeamGroupInfo beamGroup;
            do {
                beamGroup = GetRandomBeamGroup();
            } while (beamGroup == _lastBeamGroup);
            _lastBeamGroup = beamGroup;
            
            PhotonNetwork.Instantiate(
                $"Prefabs/SteelBeam/{beamGroup.Prefab.name}", 
                new Vector2(transform.position.x, transform.position.y), 
                Quaternion.identity
            );
            transform.position += new Vector3(0, _beamLayerSpacing + beamGroup.Height, 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            GenerateNextBeamLayer();
        }
    }
}