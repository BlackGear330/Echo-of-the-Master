using UnityEngine;


namespace AILogic
{
    public enum AIDecision
    {
        Fight,
        Apathy,
        Retreat
    }
    public class DecisionMaker : MonoBehaviour
    {
        States _states;
        // Situation будет передаваться извне (из BattleManager)
        Memory _memory;
        [SerializeField] private CharacterTraits _characterTraits;
        
        void Awake()
        {
            _states = GetComponent<States>();
            _memory = GetComponent<Memory>();

            if (_characterTraits is null || _states is null || _memory is null)
            {
                Debug.LogError($"{name}: Проверить назначены ли компоненты: States, CharacterTraits, Memory");
            }
        }

        public AIDecision MakeDecision()
        {
            //должен получать Situation из BattleManager

            float fightWeight = _characterTraits.bravery;
            float apathyWeight = _characterTraits.anxiety;
            float retreatWeight = _characterTraits.cowardice;
            float randomFactor = UnityEngine.Random.Range(0.85f, 1.15f); // для генерации рандома.
            
            //TODO Модификаторы для Fight , пока что тест. (Переделать по плану, сделать общие множители для всех 3 вариантов "см. план реализации, шаг 2")
            float stressMod = 1.0f - (_states.stress / 100f);
            stressMod = Mathf.Max(stressMod, 0f);
            float fearMod = 1.0f - (_states.fear / 100f);
            fearMod = Mathf.Max(fearMod, 0f);
            float exhaustMod = 1.0f - (_states.exhaustion / 100f);
            exhaustMod = Mathf.Max(exhaustMod, 0f);

            //TODO Расчет с модификаторами
            fightWeight *= stressMod * fearMod * exhaustMod * randomFactor;

            
            Debug.Log($"Fight chanceMod {fightWeight}");
            
            
            // TODO Расчет без модификаторов.
            //Расчет вероятности боя
            fightWeight +=  -_states.stress -_states.fear -_states.exhaustion;
            fightWeight *= randomFactor;
            fightWeight = Mathf.Clamp(fightWeight, 1f, 100f); 
            
            //расчет вероятности стазиса на 1-2 хода, сам стазис еще не реализован 
            apathyWeight += (_states.exhaustion + _states.fear + _states.stress) - _characterTraits.bravery; 
            apathyWeight *= randomFactor;
            apathyWeight = Mathf.Clamp(apathyWeight, 1f, 100f);
            
            //расчет вероятности отступления
            retreatWeight += (_states.exhaustion + _states.fear + _states.stress) - _characterTraits.bravery;
            retreatWeight *= randomFactor;
            retreatWeight = Mathf.Clamp(retreatWeight, 1f, 100f);
            
            // TODO далее должна быть нормализация
            
            Debug.Log($"fight chance {gameObject.name} {fightWeight}, храбрость {_characterTraits.bravery}, стресс {_states.stress}, страх {_states.fear}, усталость {_states.exhaustion}");
            Debug.Log($"apathy chance {gameObject.name} {apathyWeight}, храбрость {_characterTraits.bravery}, стресс {_states.stress}, страх {_states.fear}, усталость {_states.exhaustion}");
            Debug.Log($"retreat chance {gameObject.name} {retreatWeight}, храбрость {_characterTraits.bravery}, стресс {_states.stress}, страх {_states.fear}, усталость {_states.exhaustion}");
            
            //заглушка пока что
            return AIDecision.Fight;
        }
        
    }
}