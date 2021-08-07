using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[System.Serializable]
public class ItemCategory 
{
    public WeaponCategory[] Weapon;
    public ArmorCategory[] ArmorPiece;
    public MainConsumableCategory Consumable;
}

[System.Serializable]
public class WeaponCategory
{
    public enum CategoryType { Clubs, Greatswords, Axes, Knives, ShortSwords, RangedWeapons}
    public CategoryType Category;
    public Weapon[] Weapons;
}

[System.Serializable]
public class ArmorCategory
{
    public enum CategoryType { LightArmor, HeavyArmor, Shield }
    public CategoryType Category;
    public Armor[] Armors;
}

[System.Serializable]
public class MainConsumableCategory
{
    public FoodCategory[] FoodCategory;
    public PotionCategory[] PotionCategory;
    public ScrollCategory[] ScrollCategory;
}

[System.Serializable]
public class FoodCategory
{
    public enum CategoryType { LowGradeFood, DecentFood, HighGradeFood }
    public CategoryType Category;
    public Consumable[] Consumables;
}

[System.Serializable]
public class PotionCategory
{
    public enum CategoryType { CommonPotion, UncommonPotion, RarePotion, MythicPotion }
    public CategoryType Category;
    public Consumable[] Consumables;
}

[System.Serializable]
public class ScrollCategory
{
    public GameObject item;
}


[System.Serializable]
public class Consumable
{
    public GameObject item;
}

[System.Serializable]
public class Weapon
{
    public GameObject item;
    public string quality;
    public string rarity;
}

[System.Serializable]
public class Armor
{
    public GameObject item;
}

public class LootSystem : MonoBehaviour
{
    public ItemCategory ItemCategory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Spawnner();
        }
    }

    void RarityQuality(Weapon Current)
    {
        float itemRarity = Random.Range(1, 101);
        float itemQuality = Random.Range(1, 101);

        if (100 == itemRarity)
            Current.rarity = "Ancient";
        else if (96 <= itemRarity)
            Current.rarity = "Legendary";
        else if (88 <= itemRarity)
            Current.rarity = "Mythic";
        else if (71 <= itemRarity)
            Current.rarity = "Rare";
        else if (51 <= itemRarity)
            Current.rarity = "Uncommon";
        else
            Current.rarity = "Common";

        if (98 <= itemQuality)
            Current.quality = "Legendary";
        else if (81 <= itemQuality)
            Current.quality = "High";
        else if (31 <= itemQuality)
            Current.quality = "Normal";
        else
            Current.quality = "Low";
    }

    void Spawnner()
    {

        float itemNum = Random.Range(1, 13);

        if (9 <= itemNum) // COINS
        {
            Debug.Log("<color=red>Coin:</color> " + Random.Range(1, 11));
        }
        else if (5 <= itemNum) // CONSUMABLE
        {
            float consumableCategory = Random.Range(1, 101);
            if (76 <= consumableCategory)
            {
                int CurrentScroll = Random.Range(0, ItemCategory.Consumable.ScrollCategory.Length);
                Debug.Log("<color=red>Scroll:</color> " + ItemCategory.Consumable.ScrollCategory[CurrentScroll].item.name);
            }
            else if (41 <= consumableCategory)
            {
                int PotionCategory = Random.Range(0, 100);
                string currentPotionCategory;

                if (91 <= PotionCategory)
                    currentPotionCategory = "MythicPotion";
                else if (71 <= PotionCategory)
                    currentPotionCategory = "RarePotion";
                else if (51 <= PotionCategory)
                    currentPotionCategory = "UncommonPotion";
                else 
                    currentPotionCategory = "CommonPotion";

                for (int i = 0; i < ItemCategory.Consumable.PotionCategory.Length; i++)
                {
                    if (ItemCategory.Consumable.PotionCategory[i].Category.ToString() == currentPotionCategory)
                    {
                        int CurrentPotion = Random.Range(0, ItemCategory.Consumable.PotionCategory[i].Consumables.Length);
                        Debug.Log("<color=red>Potion Category:</color> " + ItemCategory.Consumable.PotionCategory[i].Category + " <color=red>Potion:</color> " + ItemCategory.Consumable.PotionCategory[i].Consumables[CurrentPotion].item.name);
                    }
                }
            }
            else
            {
                int FoodCategory = Random.Range(0, 100);
                string currentFoodCategory;

                if (81 <= FoodCategory)
                    currentFoodCategory = "HighGradeFood";
                else if (51 <= FoodCategory)
                    currentFoodCategory = "DecentFood";
                else
                    currentFoodCategory = "LowGradeFood";

                for (int i = 0; i < ItemCategory.Consumable.FoodCategory.Length; i++)
                {
                    if (ItemCategory.Consumable.FoodCategory[i].Category.ToString() == currentFoodCategory)
                    {
                        int CurrentFood = Random.Range(0, ItemCategory.Consumable.FoodCategory[i].Consumables.Length);
                        Debug.Log("<color=red>Food Category:</color> " + ItemCategory.Consumable.FoodCategory[i].Category + " <color=red>Food:</color> " + ItemCategory.Consumable.FoodCategory[i].Consumables[CurrentFood].item.name);
                    }
                }
            }

            
        }
        else if (3 <= itemNum) // ARMOR
        {
            string currentArmorCategory;
            currentArmorCategory = Enum.GetName(typeof(ArmorCategory.CategoryType), Random.Range(0, Enum.GetValues(typeof(ArmorCategory.CategoryType)).Length));

            for (int i = 0; i < ItemCategory.ArmorPiece.Length; i++)
            {
                if (ItemCategory.ArmorPiece[i].Category.ToString() == currentArmorCategory)
                {
                    int CurrentArmor = Random.Range(0, 4);
                    Debug.Log("<color=red>Armor Category:</color> " + ItemCategory.ArmorPiece[i].Category + " <color=red>Armor:</color> " + ItemCategory.ArmorPiece[i].Armors[CurrentArmor].item.name);
                }
            }
        }
        else // WEAPON
        {
            string currentWeaponCategory;
            currentWeaponCategory = Enum.GetName(typeof(WeaponCategory.CategoryType), Random.Range(0, Enum.GetValues(typeof(WeaponCategory.CategoryType)).Length));

            for (int i = 0; i < ItemCategory.Weapon.Length; i++)
            {
                if (ItemCategory.Weapon[i].Category.ToString() == currentWeaponCategory)
                {
                    int CurrentWeapon = Random.Range(0, 4);
                    RarityQuality(ItemCategory.Weapon[i].Weapons[CurrentWeapon]);
                    Debug.Log("<color=red>Weapon Category:</color> " + ItemCategory.Weapon[i].Category + " <color=red>Weapon:</color> " + ItemCategory.Weapon[i].Weapons[CurrentWeapon].item.name + " <color=red>Quality:</color> " + ItemCategory.Weapon[i].Weapons[CurrentWeapon].quality + " <color=red>Rarity:</color> " + ItemCategory.Weapon[i].Weapons[CurrentWeapon].rarity);
                }
            }
        }
    }
}