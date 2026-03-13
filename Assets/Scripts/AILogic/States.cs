using UnityEngine;

namespace AILogic
{


    public class States : MonoBehaviour
    {
        [Header("Состояния персонажа (0-100)")]
        
        [Range(0f, 100f)] public float stress = 0 * 0.5f;
        [Range(0f, 100f)] public float fear = 0;
        [Range(0f, 100f)] public float exhaustion = 0;  // Усталость


        [Header("Социальные параметры")] 
        [Range(0f, 100f)] public float trust = 0;
    }
}