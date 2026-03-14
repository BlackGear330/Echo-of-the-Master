using AILogic;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    public GameObject[] EnemiesSlot;
    public GameObject[] CharactersSlot;
    MeleeLogic _meleeLogic;
    DecisionMaker _decisionMaker;

    public bool IsSlotOccupied(GameObject[] currentSlots, int i) // проверка, что у слота массива есть компонент Health
    {
        if (currentSlots[i] == null) return false;
        Health unitHealth = currentSlots[i].GetComponentInChildren<Health>();
        return unitHealth != null;
    }
    


    void Start()
    {
        MakeSituation();
       
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

    void MakeSituation()
    {
        //получить Situation и в зависимости от типа отдать.
        
        DecisionMaker[] _decisionMakers = new DecisionMaker[CharactersSlot.Length]; //создается массив

        for (int i = 0; i < CharactersSlot.Length; i++)
        {
            _decisionMakers[i] = CharactersSlot[i].GetComponentInChildren<DecisionMaker>(); //находим каждый скрипт DecisionMaker в массиве
            Debug.Log($"слот {i}: {_decisionMakers[i]}");
            if (_decisionMakers[i] != null)
            {
                _decisionMakers[i].MakeDecision();
            }
        }
        
    }
    
}
