using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float _maxHealth;
    [SerializeField] public float _health;
    
    
    public void TakeDamage(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0f, _maxHealth);
        BattleManager bm = FindAnyObjectByType<BattleManager>();
        if (bm != null)
            bm.CheckAllState();
        if (_health <= 0) 
            Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated.");
        Destroy(gameObject);
    }

    public void Heal(float heal)
    {
        _health = Mathf.Clamp(_health + heal, 0f, _maxHealth);
    }
}