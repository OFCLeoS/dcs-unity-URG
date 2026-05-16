using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutManager : MonoBehaviour
{
    [Header("Loadouts")]
    public LoadoutData[] loadouts;

    [Header("Loadout List")]
    public Transform scrollViewContent;
    public GameObject loadoutButtonPrefab;

    [Header("Preview")]
    public TextMeshProUGUI selectedLoadoutText;

    public Image primaryImage;
    public TextMeshProUGUI primaryNameText;
    public TextMeshProUGUI primaryDamageText;

    public Image secondaryImage;
    public TextMeshProUGUI secondaryNameText;
    public TextMeshProUGUI secondaryDamageText;

    public Image meleeImage;
    public TextMeshProUGUI meleeNameText;
    public TextMeshProUGUI meleeDamageText;

    public Image utilityImage;
    public TextMeshProUGUI utilityNameText;
    public TextMeshProUGUI utilityDamageText;

    [Header("Buttons")]
    public Button previousButton;
    public Button nextButton;
    public Button applyButton;

    private int previewIndex = 0;
    private int appliedIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildLoadoutList();
        ShowLoadout(previewIndex);
    }

    private void BuildLoadoutList()
    {
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);   
        }

        for (int i = 0; i < loadouts.Length; i++)
        {
            int index = i;
            GameObject button = Instantiate(loadoutButtonPrefab, scrollViewContent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = loadouts[i].getLoadoutName();
            button.GetComponent<Button>().onClick.AddListener(() => OnLoadoutButtonClicked(index));
        }
    }

    private void OnLoadoutButtonClicked(int index)
    {
        previewIndex = index;
        ShowLoadout(previewIndex);
    }

    public void OnPrevious()
    {
        previewIndex = (previewIndex - 1 + loadouts.Length) % loadouts.Length;
        ShowLoadout(previewIndex);
    }

    public void OnNext()
    {
        previewIndex = (previewIndex + 1) % loadouts.Length;
        ShowLoadout(previewIndex);
    }

    public void OnApply()
    {
        appliedIndex = previewIndex;
        Debug.Log("Applied loadout: " + loadouts[appliedIndex].getLoadoutName());
    }

    private void ShowLoadout(int index)
    {
        LoadoutData loadout = loadouts[index];

        selectedLoadoutText.text = loadout.getLoadoutName();

        primaryImage.sprite = loadout.getPrimary().getWeaponIcon();
        primaryNameText.text = loadout.getPrimary().getWeaponName();
        primaryDamageText.text = loadout.getPrimary().getDamage() + " damage";

        secondaryImage.sprite = loadout.getSecondary().getWeaponIcon();
        secondaryNameText.text = loadout.getSecondary().getWeaponName();
        secondaryDamageText.text = loadout.getSecondary().getDamage() + "damage";

        meleeImage.sprite = loadout.getMelee().getWeaponIcon();
        meleeNameText.text = loadout.getMelee().getWeaponName();
        meleeDamageText.text = loadout.getMelee().getDamage() + " damage";

        utilityImage.sprite = loadout.getUtility().getWeaponIcon();
        utilityNameText.text = loadout.getUtility().getWeaponName();
        utilityDamageText.text = loadout.getUtility().getDamage() + " damage";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
