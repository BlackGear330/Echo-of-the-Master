using AILogic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{ 
    public static WorldManager Instance { get; private set; }
    
    [SerializeField] Situations _activeSituations;
    public Situations activeSituations => _activeSituations;

  void Awake()
  {
      Instance = this;
  }
}
