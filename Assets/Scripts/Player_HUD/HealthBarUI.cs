using UnityEngine;

public class HealthBarUI : MonoBehaviour
{

    public float health, maxHealth, width, height;
    [SerializeField] private RectTransform healthBar;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = health;
        float newWidth  = this.health / this.maxHealth * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }

}
