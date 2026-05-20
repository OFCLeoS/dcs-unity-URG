using System;
using System.Threading;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class PlayerUIBehaviour : MonoBehaviour
{

    private float health, maxHealth;
    public float width = 480.025f;
    public float height = 16.001f;
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text waveTextNumber;
    [SerializeField] private TMP_Text waveTimerText;
    [SerializeField] private TMP_Text objectiveText;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = health;
        float newWidth  = this.health / this.maxHealth * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
        healthText.text = Convert.ToString(Mathf.RoundToInt(health));
    }

    public void ChangeWaveNumber(int waveTextNumber)
    {
        this.waveTextNumber.text = Convert.ToString(waveTextNumber);
    }

    public void ChangeWaveTimer(float waveTimerText)
    {
        int minutes = TimeSpan.FromSeconds(waveTimerText).Minutes;
        int seconds = TimeSpan.FromSeconds(waveTimerText).Seconds;
        int milliseconds = TimeSpan.FromSeconds(waveTimerText).Milliseconds;
        this.waveTimerText.text = Convert.ToString(minutes + ":" + seconds + ":" + milliseconds);
    }

}
