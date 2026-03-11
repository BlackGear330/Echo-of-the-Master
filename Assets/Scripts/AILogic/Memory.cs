using System.Collections.Generic;
using UnityEngine;

namespace AILogic
{


    public class Memory : MonoBehaviour
    {
        private List<MemoryEntry> _memoryEntry = new List<MemoryEntry>();
        States _states;
        void Start()
        {
            _states = GetComponent<States>();
        }

        [System.Serializable]
        public class MemoryEntry
        {
            public Situations situations;
            public float trustChange;
            public float stressResult;
            public bool isPlayerAdvice; // Было ли решение принято по совету игрока или нет
            
        }

        public void RecordResult()
        {
        
        }

        
    }
}