using System;
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
            float panicMod = Mathf.Clamp01((_states.stress + _states.fear) / 100f); // общий множитель для 3 вариантов
            float exhaustionMod =  Mathf.Clamp01(_states.exhaustion / 100f);
            
            // БОЙ: паника мешает, усталость мешает
            fightWeight *= (1f - panicMod) * (1f - exhaustionMod);
            // АПАТИЯ: паника усиливает ступор, усталость СИЛЬНО усиливает (в квадрате)
            apathyWeight *= (1f + panicMod) * (1f + exhaustionMod * exhaustionMod);
            // ОТСТУПЛЕНИЕ: паника СИЛЬНО гонит (в квадрате), усталость немного.
            retreatWeight *= (1f + panicMod * panicMod) * (1f + exhaustionMod * 0.5f);
            
            //Расчет с модификаторами с защитой от 0.
            fightWeight = Mathf.Max(fightWeight * randomFactor, 1f);
            apathyWeight = Mathf.Max(apathyWeight * randomFactor, 1f);
            retreatWeight = Mathf.Max(retreatWeight * randomFactor, 1f);
            
            
            Debug.Log($"Fight weight {fightWeight}, храбрость: {_characterTraits.bravery}, стресс: {_states.stress}, страх: {_states.fear}, усталость {_states.exhaustion}");
            Debug.Log($"Apathy weight {apathyWeight}, тревожность: {_characterTraits.anxiety}, стресс: {_states.stress}, страх: {_states.fear}");
            Debug.Log($"Retreat weight {retreatWeight}, трусость: {_characterTraits.cowardice}, стресс: {_states.stress}, страх: {_states.fear}");
            
            //Нормализация
            float totalWeight = fightWeight + apathyWeight + retreatWeight;
            
            float fightWeightPercent = fightWeight /  totalWeight * 100f;
            float apathyWeightPercent = apathyWeight /  totalWeight * 100f;
            float retreatWeightPercent = retreatWeight /  totalWeight * 100f;
            
            Debug.Log($"В процентах шанс боя {fightWeightPercent}%");
            Debug.Log($"В процентах шанс апатии {apathyWeightPercent}%");
            Debug.Log($"В процентах шанс отступления {retreatWeightPercent}%");
            
            float randomRoll = UnityEngine.Random.Range(0f, 100f);

            if (randomRoll < fightWeightPercent)
            {
                Debug.Log($"Выбор: Бой | Шанс: {fightWeightPercent:F1}% | Roll: {randomRoll:F1}");
                return AIDecision.Fight;
            }
            else if (randomRoll < fightWeightPercent + retreatWeightPercent)
            {
                Debug.Log($"Выбор: Отступление | Шанс: {retreatWeightPercent:F1}% | Roll: {randomRoll:F1}");
                return AIDecision.Retreat;
            }
            else
            {
                Debug.Log($"Выбор: Апатия | Шанс: {apathyWeightPercent:F1}% | Roll: {randomRoll:F1}");
                return AIDecision.Apathy;
            }
            
        }
        
    }
}