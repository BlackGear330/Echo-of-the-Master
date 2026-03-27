using AILogic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject[] EnemiesSlot;
    public GameObject[] CharactersSlot;
    private bool _retriggered = false;

    public bool IsSlotOccupied(GameObject[] currentSlots, int i) // проверка, что у слота массива есть компонент Health
    {
        if (currentSlots[i] == null) return false;
        Health unitHealth = currentSlots[i].GetComponentInChildren<Health>();
        return unitHealth != null;
    }
    


    void Start()
    {
       
        if (EnemiesSlot == null || EnemiesSlot.Length == 0)
        {
            Debug.Log("Массив врагов не инициализирован или пуст");
            return;
        }
        
        //int sizeall = EnemiesSlot.Length;
        //Debug.Log($"Всего слотов врагов: {sizeall}");
        for (int i = 0; i < EnemiesSlot.Length; i++)
        {
            bool occupiedEnemy = IsSlotOccupied(EnemiesSlot, i);
            Debug.Log($"Слот {i} {(occupiedEnemy ? "занят" : "пустой")}");
        }
        
        if (CharactersSlot == null || CharactersSlot.Length == 0)
        {
            //Debug.Log("Массив персонажей не инициализирован или пуст");
            return;
        }
        
        //int sizeCharapterAll = CharactersSlot.Length;
        //Debug.Log($"Всего слотов персонажей: {sizeCharapterAll}");

        for (int i = 0; i < CharactersSlot.Length; i++)
        {
            bool occupiedCharapter = IsSlotOccupied(CharactersSlot, i);
            Debug.Log($"Слот {i} {(occupiedCharapter ? "занят" : "пустой")}");
        }
        
    }

    public int GetWoundedCount()
    {
        int count = 0;
        foreach (GameObject slot in CharactersSlot)
        {
            if (slot == null) continue;
            Health unitHealth = slot.GetComponentInChildren<Health>();
            if (unitHealth != null && unitHealth._health < unitHealth._maxHealth * 0.5f)
                count++;
        }
        return count;
    }

    public int GetDeadCount()
    {
        int count = 0;
        foreach (GameObject slot in CharactersSlot)
        {
            if (slot == null) count++;
        }
        return count;
    }

    public void CheckAllState()
    {
        if (_retriggered == true) return;
        
        foreach (GameObject slot in CharactersSlot)
        {
            if (slot ==null)
            {
                _retriggered = true; break;
            }
            Health h = slot.GetComponentInChildren<Health>();
            if (h != null && h._health < h._maxHealth * 0.5f)
            {
                _retriggered = true; break;
            }
            
        }
        
        if (!_retriggered) return;
        
        foreach (GameObject slot1 in CharactersSlot)
        {
            if (slot1 == null) continue;
            AttackController ac = slot1.GetComponentInChildren<AttackController>();
            if (ac != null)
            {
                ac.ReMakeDecision(GetWoundedCount(), GetDeadCount());
            }
        }
        
    }
    
}
