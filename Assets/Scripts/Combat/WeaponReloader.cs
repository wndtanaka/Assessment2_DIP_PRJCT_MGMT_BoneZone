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
        if (isReloading || RoundsRemainingInInventory <= 0)
        {
            return;
        }
        isReloading = true;

        GameManager.Instance.Timer.Add(() =>
        { ExecuteReload(inventory.TakeFromContainer(containerItemId, magSize - RoundsRemainingInMag)); }, reloadTime);
    }

    private void ExecuteReload(int amount)
    {
        isReloading = false;
        shotsFiredInClip -= amount;
        HandleOnAmmoChanged();
    }

    public void TakeFromMag(int amount)
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged(); 
    }
    public void HandleOnAmmoChanged()
    {
        if (onAmmoChanged != null)
        {
            onAmmoChanged();
        }
    }
}
