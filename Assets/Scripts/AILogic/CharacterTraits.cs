using UnityEngine;

namespace AILogic
{


    [CreateAssetMenu(fileName = "CharacterTraits", menuName = "Scriptable Objects/CharacterTraits")]
    public class CharacterTraits : ScriptableObject
    {
        public string traitName;
        public float bravery; //Храбрость
        public float cowardice; //Трусость
        public float empathy; //Эмпатия
        public float closedness; //Закрытость
        public float composure; //Хладнокровие
        public float anxiety; //Тревожность
    }
}