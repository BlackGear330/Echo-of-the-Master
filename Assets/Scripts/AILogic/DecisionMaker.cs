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

        public AIDecision MakeDecision(int wounded, int dead)
        {
            Situations situations = WorldManager.Instance.activeSituations;
            float sitStress = situations != null ? situations.situationStress : 0f;
            float sitFear = situations != null ? situations.situationFear : 0f;
            float sitExhaustion = situations != null ? situations.situationExhaustion : 0f;

            
            
            float fightWeight = _characterTraits.bravery;
            float apathyWeight = _characterTraits.anxiety;
            float retreatWeight = _characterTraits.cowardice;
            float randomFactorfight = UnityEngine.Random.Range(0.95f, 1.05f); 
            float randomFactorapathy = UnityEngine.Random.Range(0.95f, 1.05f);
            float randomFactorretreat = UnityEngine.Random.Range(0.95f, 1.05f);
            float woundedPenalty = wounded * 15f + dead * 25f;
            Debug.Log($"раненых {wounded}, мертвых {dead}");
            float effectivePanic = Mathf.Clamp(_states.stress + _states.fear + sitStress + sitFear + woundedPenalty, 0f, 100f);
            float effectiveExhaust = Mathf.Clamp(_states.exhaustion + sitExhaustion, 0f, 100f);
            float panicMod = effectivePanic / 100f; 
            float exhaustionMod = effectiveExhaust / 100f;
            
            // БОЙ: паника мешает, усталость мешает
            fightWeight *= (1f - panicMod) * (1f - exhaustionMod);
            // ОТСТУПЛЕНИЕ: паника СИЛЬНО гонит (в квадрате), усталость немного.
            retreatWeight *= (1f + panicMod * panicMod) * (1f + exhaustionMod * 0.5f);
            // АПАТИЯ: паника усиливает ступор, усталость СИЛЬНО усиливает (в квадрате)
            apathyWeight *= (1f + panicMod) * (1f + exhaustionMod * exhaustionMod);

            //Расчет с модификаторами с защитой от 0.
            fightWeight = Mathf.Max(fightWeight * randomFactorfight, 0.1f);
            retreatWeight = Mathf.Max(retreatWeight * randomFactorretreat, 0.1f);
            apathyWeight = Mathf.Max(apathyWeight * randomFactorapathy, 0.1f);
            
            
            Debug.Log($"Fight weight {fightWeight}, храбрость: {_characterTraits.bravery}, стресс: {_states.stress}, страх: {_states.fear}, усталость {_states.exhaustion}, стресс ситуации {sitStress}, страх ситуации {sitFear}, усталость ситуации {sitExhaustion}");
            Debug.Log($"Apathy weight {apathyWeight}, тревожность: {_characterTraits.anxiety}, стресс: {_states.stress}, страх: {_states.fear}, стресс ситуации {sitStress}, страх ситуации {sitFear}, усталость ситуации {sitExhaustion}");
            Debug.Log($"Retreat weight {retreatWeight}, трусость: {_characterTraits.cowardice}, стресс: {_states.stress}, страх: {_states.fear}, стресс ситуации {sitStress}, страх ситуации {sitFear}, усталость ситуации {sitExhaustion}");
            
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