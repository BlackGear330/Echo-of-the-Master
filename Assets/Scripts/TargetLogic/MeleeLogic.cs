using UnityEngine;

public class MeleeLogic : MonoBehaviour
{
  BattleManager _battleManager;

  public Health InTheSlot(GameObject[] inSlot)
  {

    for (int i = 0; i < inSlot.Length; i++)
    {
      if (inSlot[i] == null) continue;
      Health inSlotHealth = inSlot[i].GetComponentInChildren<Health>();
        if (inSlotHealth != null && inSlotHealth._health >0)
        {
          return inSlotHealth;
        }
      
    }
    return null;
  }
  void Start()
  {
    _battleManager = FindAnyObjectByType<BattleManager>();

  }
 
}
