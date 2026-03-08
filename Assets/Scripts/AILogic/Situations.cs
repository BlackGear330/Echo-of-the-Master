using UnityEngine;

namespace AILogic
{


    [CreateAssetMenu(fileName = "Situations", menuName = "Scriptable Objects/Situations")]
    public class Situations : ScriptableObject
    {
        public string situationName;
        [Range(0f, 100f)] public float situationStress = 0;
        [Range(0f, 100f)] public float situationFear = 0;
        [Range(0f, 100f)] public float situationExhaustion = 0;
    }
}