using Unity.VectorGraphics;
using UnityEngine;

public class WeaponLodoutUI : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private SVGImage[] weaponSlots;
    [SerializeField] private GameObject[] weaponBlueprints;
    [SerializeField] private Sprite[] selectedSprites;
    private int selectedWeaponMenuIndex = 0;
    private Sprite[] normalSprites;
    
    
    private void Awake()
    {
        normalSprites = new Sprite[weaponSlots.Length];
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] != null)
            {
                normalSprites[i] = weaponSlots[i].sprite;
            }
        }
    }
    
    private void OnEnable()
    {
        if (playerCombat != null)
        {
            selectedWeaponMenuIndex = (int)playerCombat.currentWeapon;
        }
        UpdateLoadoutVisuals();
    }
    
    public void ScrollUp()
    {
        selectedWeaponMenuIndex--;
        if (selectedWeaponMenuIndex < 0) 
            selectedWeaponMenuIndex = weaponSlots.Length - 1;

        UpdateLoadoutVisuals();
    }
    
    public void ScrollDown()
    {
        selectedWeaponMenuIndex++;
        if (selectedWeaponMenuIndex >= weaponSlots.Length) 
            selectedWeaponMenuIndex = 0;

        UpdateLoadoutVisuals();
    }
    
    private void UpdateLoadoutVisuals()
    {
        for (int i = 0; i < weaponBlueprints.Length; i++)
        {
            if (weaponBlueprints[i] != null)
            {
                weaponBlueprints[i].SetActive(i == selectedWeaponMenuIndex); 
            }
        }

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i] == null) continue;

            if (i == selectedWeaponMenuIndex)
            {
                if (selectedSprites != null && i < selectedSprites.Length && selectedSprites[i] != null)
                {
                    weaponSlots[i].sprite = selectedSprites[i];
                }
            }
            else
            {
                weaponSlots[i].sprite = normalSprites[i];
            }
        }
    }
}
