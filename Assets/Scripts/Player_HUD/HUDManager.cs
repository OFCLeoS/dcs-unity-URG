using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Wave objective")]
    [SerializeField] private TextMeshProUGUI waveObjectiveText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("HP")]
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("Hotbar")]

    public Sprite selectedSlotImage;
    public Sprite defaultSlotImage;

    [SerializeField] private HotbarSlot[] hotbarSlots;

    [Header("Equipped Weapon Preview")]
    [SerializeField] private Image equippedWeaponIcon;

    private float timer;
    private bool timerRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSelectedSlot(0);
    }

    public void SetHP(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void SetObjective(string text)
    {
        waveObjectiveText.text = text;
    }

    public void StartTimer(float seconds)
    {
        timer = seconds;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void SetSelectedSlot(int index)
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].SetSelected(i == index, selectedSlotImage, defaultSlotImage);
        }
    }

    public void SetSlotIcon(int index, Sprite icon)
    {
        hotbarSlots[index].SetIcon(icon);
        if (hotbarSlots[index].GetIsSelected())
        {
            equippedWeaponIcon.sprite = icon;
        }
    }

    public void SetEquippedIcon(Sprite icon)
    {
        equippedWeaponIcon.sprite = icon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning) 
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
