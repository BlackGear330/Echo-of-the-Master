using UnityEngine;

[CreateAssetMenu(fileName = "Situation", menuName = "Scriptable Objects/Situation")]
public class Situation : ScriptableObject
{
    public string situationName; // Название ситуации
    public string description; // Описание реакции на ситуацию
    public float difficulty; // Сложность ситуации

}
