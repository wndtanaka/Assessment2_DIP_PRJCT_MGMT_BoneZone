using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    int magSize;
    [SerializeField]
    Container inventory;

    // accessing weaponType enum, so we can match the ammo with it's respective weapon
    public E_WeaponType weaponType;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public delegate void OnAmmoChanged();
    public event OnAmmoChanged onAmmoChanged;

    public int RoundsRemainingInMag
    {
        get
        {
            return magSize - shotsFiredInClip; // will return the number of magSize - shotsFiredInMag
        }
    }

    public int RoundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId); // will return the number of amount remaining witch containerItemID constructor
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    private void Awake()
    {
        // subscribe containerItemId to onContainerReady delegates
        inventory.onContainerReady += () =>
        {
            containerItemId = inventory.Add(weaponType.ToString(), maxAmmo);
        };
    }

    public void Reload()
    {
        // can not reload RoundsRemainingInInventory is less or equal to 0
        if (isReloading || RoundsRemainingInInventory <= 0)
        {
            return;
        }
        // set isReloading to true, so can not shoot while relaoding
        isReloading = true;

        // setting timer to Reload, as long as reloadTimer
        // ExecuteReload after reloadTime has passed, also the amount needed to reload which is actual magSize - RoundsRemainingInMag, in certain containerItemId back to the weaponID
        GameManager.Instance.Timer.Add(() =>
        { ExecuteReload(inventory.TakeFromContainer(containerItemId, magSize - RoundsRemainingInMag)); }, reloadTime);
    }

    private void ExecuteReload(int amount)
    {
        // isReloading set to false so we can shoot.
        isReloading = false;
        shotsFiredInClip -= amount;
        HandleOnAmmoChanged();
    }

    public void TakeFromMag(int amount)
    {
        // when shooting, take the amount of ammo passed in the function.
        shotsFiredInClip += amount;
        HandleOnAmmoChanged(); 
    }
    
    // this function needed to updating the UI after the shot is fired or reloading.
    public void HandleOnAmmoChanged()
    {
        if (onAmmoChanged != null)
        {
            onAmmoChanged();
        }
    }
}
