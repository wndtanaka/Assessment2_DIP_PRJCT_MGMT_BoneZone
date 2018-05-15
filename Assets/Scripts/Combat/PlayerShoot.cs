using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    float weaponSwitchTime;

    Shooter[] weapons;
    Shooter activeWeapon;
    int currentWeaponIndex;
    bool canFire;
    Transform weaponHolder;

    public delegate void OnWeaponSwitch(Shooter shooter);
    public event OnWeaponSwitch onWeaponSwitch;

    public Shooter ActiveWeapon
    {
        get
        {
            return activeWeapon;
        }
    }

    void Start()
    {
        canFire = true;
        weaponHolder = transform.FindChild("WeaponsHolder"); // find all weapons available in Weapons gameObject
        weapons = weaponHolder.GetComponentsInChildren<Shooter>(); // getting all the Shooter component and store it as weapons

        if (weapons.Length > 0)
        {
            // equipping default weapons when started, choose the top Weapon GameObject as default
            Equip(0);
        }
        
    }

    void Equip(int index)
    {
        // Deactivate all the weapons for momenteraly after default weapon chosen
        DeactiveWeapons();
        canFire = true;
        // activate default weapon gameObject
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        weapons[index].gameObject.SetActive(true);
        if (onWeaponSwitch != null)
        {
            onWeaponSwitch(activeWeapon);
        }
    }

    void DeactiveWeapons()
    {
        // deactivate all weapons, and store them in weaponHolder gameObject
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolder);
        }
    }

    void SwitchWeapon(int direction)
    {
        // switching weapons mechanics, weaponSwitchTime can be increased to set Switch time delay
        canFire = false;
        currentWeaponIndex += direction;
        if (currentWeaponIndex > weapons.Length - 1)
        {
            currentWeaponIndex = 0;
        }
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        GameManager.Instance.Timer.Add(() =>
        {
            Equip(currentWeaponIndex);
        }, weaponSwitchTime);
    }

    void Update()
    {
        // switching weapons by scrolling mouse button and down
        if (GameManager.Instance.InputController.MouseWheelDown)
        {
            SwitchWeapon(1);
        }
        if (GameManager.Instance.InputController.MouseWheelUp)
        {
            SwitchWeapon(-1);
        }
        if (!canFire)
        {
            return;
        }
        // if left mouse button click, then Fire
        if (GameManager.Instance.InputController.Fire1)
        {
            activeWeapon.Fire();
        }
    }
}
