using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "BallSettings", menuName = "BallSettings", order = 0)]
    public class BallSettings : ScriptableObject
    {
        public Material[] BallMaterials;
    }
}