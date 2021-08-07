using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    public int maxArmor = 100;
    int armor = 0;

    [SerializeField] RectTransform healthBar, armorBar;

    public override void Awake()
    {
        base.Awake();

        armor = maxArmor;
    }

    public override void Hurt(int damage)
    {
        if (currHealth - damage > 0 && armor != 0)
            currHealth -= damage;
        else
            currHealth = 0;

        if (armor - damage > 0)
            armor -= damage;
        else
            armor = 0;

        UpdateUI(healthBar, (float) currHealth / maxHealth);
        UpdateUI(armorBar, (float) armor / maxArmor);
    }

    public override void Heal(int healing)
    {
        base.Heal(healing);

        if (currHealth + healing < maxHealth)
            currHealth += healing;
        else
            currHealth = maxHealth;
    }

    public void AddArmor(int armorPickup)
    {
        if (armor + armorPickup < maxArmor)
            armor += armorPickup;
        else
            armor = maxArmor;
    }

    void UpdateUI(RectTransform bar, float percentFill)
    {
        bar.localScale = new Vector3(percentFill, 1, 1);
    }
}
