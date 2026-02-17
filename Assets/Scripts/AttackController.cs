using UnityEngine;


public class AttackController : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private float _damage;
    [SerializeField] private float _heal;
    Health _health;
    BattleManager _battleManager;
    MeleeLogic _meleeLogic;
    RangedLogic _rangedLogic;
    HealerLogic _healerLogic;
    private float _nextAttackTime = 0f;


    void Start()
    {
        _battleManager = FindAnyObjectByType<BattleManager>();
        _meleeLogic = GetComponent<MeleeLogic>();
        _rangedLogic = GetComponent<RangedLogic>();
        _healerLogic = GetComponent<HealerLogic>();
        _health = GetComponent<Health>();
        
    }

    private void PerformAttack()
    {
          if (_meleeLogic != null)
          {
              GameObject[] targetSlots = _unit._faction == FactionType.Player
                  ? _battleManager.EnemiesSlot
                  : _battleManager.CharactersSlot;

              Health targetHealth = _meleeLogic.InTheSlot(targetSlots);
              if (targetHealth != null)
              {
                  if (Time.time >= _nextAttackTime)
                  {
                      targetHealth.TakeDamage(_damage);
                      Debug.Log($"{gameObject.name} выбираю {targetHealth} и наношу {_damage} урона");
                      _nextAttackTime = Time.time + _unit.AttackSpeed;
                  }
                    
              }
          }


          if (_rangedLogic != null && _healerLogic == null)
          {
              GameObject[] targetSlots1 = _unit._faction == FactionType.Player
                  ? _battleManager.EnemiesSlot
                  : _battleManager.CharactersSlot;
              Health targetHealth = _rangedLogic.InTheSlot(targetSlots1);
              if (targetHealth != null)
              {
                  if (Time.time >= _nextAttackTime)
                  {
                      targetHealth.TakeDamage(_damage);
                      Debug.Log($"{gameObject.name} выбираю {targetHealth} стреляю и наношу {_damage} урона");
                      _nextAttackTime = Time.time + _unit.AttackSpeed;
                  }
                    
              }

          }

          if (_healerLogic != null)
          {
              GameObject[] allySlots = _unit._faction == FactionType.Player
                  ? _battleManager.CharactersSlot
                  : _battleManager.EnemiesSlot;

              Health woundedAlly = _healerLogic.InTheSlot(allySlots);

              if (woundedAlly != null)
              {
                  if (Time.time >= _nextAttackTime)
                  {
                      woundedAlly.Heal(_heal);
                      Debug.Log($"{gameObject.name}  Лечу {woundedAlly} на {_heal}");
                      _nextAttackTime = Time.time + _unit.AttackSpeed;
                  }
              }
              else if (_rangedLogic != null)
              {
                  GameObject[] enemySlots = _unit._faction == FactionType.Player
                      ? _battleManager.EnemiesSlot
                      : _battleManager.CharactersSlot;

                  Health enemy = _rangedLogic.InTheSlot(enemySlots);
                  if (enemy != null)
                  {
                      if (Time.time >= _nextAttackTime)
                      {                            
                          enemy.TakeDamage(_damage);
                          Debug.Log($"{gameObject.name}  Атакую магией {enemy} на {_damage}");
                          _nextAttackTime = Time.time + _unit.AttackSpeed;
                      }
                  }
              }
          }
        
    }
    private void FixedUpdate()
    {
        PerformAttack();
    }
}
