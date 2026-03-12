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
        
        void Start()
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
            
            
            fightWeight +=  + (-_states.stress/100 -_states.fear/100 -_states.exhaustion/100);
            float randomFactor = UnityEngine.Random.Range(0.85f, 1.15f);// для генерации рандома.
            fightWeight *= randomFactor;
            fightWeight = Mathf.Clamp(fightWeight, 0f, 100f) ; 
            
            Debug.Log($"fight chance {gameObject.name} {fightWeight}");

            return AIDecision.Fight;
        }
        
    }
}