using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public static CharacterStats instance;


    public int maxHealth = 100;
    public int curHealth { get; private set; }

    public Stat damage;
    public Stat armor;
    public Stat block;

    public bool blocking = false;

    private void Awake()
    {
        instance = this;
        curHealth = maxHealth;
    }

    

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        Debug.Log(block.GetValue());
        if (blocking)
        {
            
            damage -= block.GetValue();
        }
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        curHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        
        if(curHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Die in some way
        //Method meant to be overwritten by player and enemy die systems
        Debug.Log(transform.name + " is dead");
    }
}
