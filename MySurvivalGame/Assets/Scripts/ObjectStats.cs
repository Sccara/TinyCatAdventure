using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float Health { get; protected set; }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        Health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        damage -= armor.GetValue() / 3f;

        if (damage <= 0)
        {
            damage = 1;
        }

        Health -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");

        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Destroy in some ways
        Interactable.ableClick = true;
        Destroy(gameObject);
    }
}
