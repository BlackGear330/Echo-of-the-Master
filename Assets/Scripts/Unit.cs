using UnityEngine;

public enum  FactionType
{
    Player,
    Enemy
}

public enum UnitClassType
{
    Melee,
    Ranged,
    Healer
}


[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    public FactionType _faction;
    public UnitClassType _unitClass;

    [SerializeField] private float _attackSpeed;
    
    public float AttackSpeed
    {
        get { return _attackSpeed; }
    }
}
