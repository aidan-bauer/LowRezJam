using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    [Header("Base Health")]
    public int maxHealth = 10;
    public int currHealth = 0;
    public bool invulnerable = false;
    public bool isDead = false;
    public UnityEvent onDeath;

    public virtual void Awake()
    {
        currHealth = maxHealth;
    }

    public abstract void Hurt(int damage);
    public virtual void Heal(int healing) { }
    //public just in case there's an insta-gib attack
    public void OnDeath() {
        isDead = true;
        onDeath.Invoke();
    }

    public virtual IEnumerator IFrame(float duration)
    {
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }
}
