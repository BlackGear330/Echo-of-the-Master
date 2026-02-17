using UnityEngine;
using System.Collections.Generic;
public class HealerLogic : MonoBehaviour
{
    BattleManager _battleManager;
    
    public Health InTheSlot(GameObject[] inSlot)
    {
        List<Health> targets = new List<Health>();
        foreach (GameObject slot in inSlot)
        {
            if (slot != null)
            {
                Health h = slot.GetComponentInChildren<Health>();

                if (h !=null && h._health > 0 && h._health < h._maxHealth)
                {
                    targets.Add(h);
                }
                
            }
        }

        if (targets.Count != 0)
        {
            return targets[Random.Range(0, targets.Count)];
        }
        else
        {
            return null;
        }
    }

    void Start()
    {   
        _battleManager = FindAnyObjectByType<BattleManager>();
    }
}
