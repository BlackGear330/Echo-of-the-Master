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

            return AIDecision.Fight;
        }
        
    }
}