using System.Collections.Generic;
using UnityEngine;

namespace AILogic
{


    public class Memory : MonoBehaviour
    {
        States _states;
        void Start()
        {
            _states = GetComponent<States>();
        }

        public class MemoryEntry
        {
            public Situations _situations;
            public float trustChange;
            public float stressChange;
            public bool isPlayerAdvice; // Было ли решение принято по совету игрока или нет
            
        }
    }
}