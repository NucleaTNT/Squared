using UnityEngine;

namespace Dev.NucleaTNT.PUNTesting.Utilities
{
    public static class ExtTransforms
    {
        public static void DestroyChildren(this Transform transform, bool destroyImmediately = false)
        {
            foreach (Transform child in transform)
            {
                if (destroyImmediately) Object.DestroyImmediate(child.gameObject); 
                else Object.Destroy(child.gameObject);
            }
        }
    }
}