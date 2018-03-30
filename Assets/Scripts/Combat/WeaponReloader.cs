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

    bool isReloading;
    System.Guid containerItemId;

    public int shotsFiredInClip;

    public int RoundsRemainingInMag
    {
        get
        {
            return magSize - shotsFiredInClip; // will return the number of magSize - shotsFiredInMag
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
        containerItemId = inventory.Add(this.name, maxAmmo);
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }
        isReloading = true;
        int amountFromInventory = inventory.TakeFromContainer(containerItemId, magSize - RoundsRemainingInMag);
        GameManager.Instance.Timer.Add(() =>
        { ExecuteReload(amountFromInventory); }, reloadTime);
    }

    private void ExecuteReload(int amount)
    {
        isReloading = false;
        shotsFiredInClip -= amount;
    }

    public void TakeFromMag(int amount)
    {
        shotsFiredInClip += amount;
    }
}
