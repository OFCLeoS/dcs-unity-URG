using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

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
    //[SerializeField] private Sprite equippedIconSprite;
    [SerializeField] private Image equippedIconImage;

    private int selectedWeaponIndex = -1;

    private float timer;
    private bool timerRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SetSelectedSlot(0);
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
        if(selectedWeaponIndex != -1)
        {
            SetDeselectedSlot(selectedWeaponIndex);
        }
        selectedWeaponIndex = index;
        hotbarSlots[index].SetSelected(selectedSlotImage);
    }

    public void SetDeselectedSlot(int index)
    {
        hotbarSlots[index].SetDeselected(defaultSlotImage);
    }

    public void SetSlotIcon(int index, Sprite icon)
    {
        hotbarSlots[index].SetIcon(icon);
        if (hotbarSlots[index].GetIsSelected())
        {
            //equippedIconSprite = icon;
            SetEquippedIcon(icon);
        }
    }

    public void SetEquippedIcon(Sprite icon)
    {
        equippedIconImage.sprite = icon;
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

        //if(primaryWeaponAction.WasPressedThisFrame())
        //{
        //    Debug.Log(0);
        //    SetSelectedSlot(0);
        //}
        //if(secondaryWeaponAction.WasPressedThisFrame())
        //{
        //    Debug.Log(1);
        //    SetSelectedSlot(1);
        //}
        //if(meleeWeaponAction.WasPressedThisFrame())
        //{
        //    Debug.Log(2);
        //    SetSelectedSlot(2);
        //}
    }
}
