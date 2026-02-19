using UnityEngine;

[CreateAssetMenu(fileName = "CharactersTraits", menuName = "Scriptable Objects/CharactersTraits")]
public class CharactersTraits : ScriptableObject
{
    public string traitsName;
    
    public float cowardiceMin;
    public float cowardiceMax;
    
    public float braveMin;
    public float braveMax;

    public float loaltyMin;
    public float loaltyMax;

    public float selfcontrolMin;
    public float selfcontrolMax;
}
