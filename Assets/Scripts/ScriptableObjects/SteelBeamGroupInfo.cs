using UnityEngine;

namespace Dev.NucleaTNT.Squared
{
    [CreateAssetMenu(fileName = "SteelBeamGroupInfo", menuName = "Scriptable Objects/SteelBeamGroupInfo")]
    public class SteelBeamGroupInfo : ScriptableObject
    {
        [SerializeField] private float _height;
        [SerializeField] private GameObject _steelBeamGroupObject;

        public float Height => _height;
        public GameObject Prefab => _steelBeamGroupObject;
    }
}
