using UnityEngine;

namespace AILogic
{


    public class States : MonoBehaviour
    {
        [Header("Состояния персонажа (10-100)")]
        
        [Range(0f, 100f)] public float stress;
        [Range(0f, 100f)] public float fear;
        [Range(0f, 100f)] public float exhaustion;  // Усталость


        [Header("Социальные параметры")] 
        [Range(0f, 100f)] public float trust;
    }
}