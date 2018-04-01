using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : PickUpItem
{
    [SerializeField]
    float respawnTime;
    [SerializeField]
    int amountReplenish;

    public E_WeaponType weaponType;

    public override void OnPickUp(Transform item)
    {
        Container playerInventory = item.GetComponentInChildren<Container>();
        // Change Despawn to Respawn if want to respawn the item, also add respawnTimer parameter to pass after gameObject
        GameManager.Instance.Respawner.Despawn(gameObject);
        playerInventory.Put(weaponType.ToString(),amountReplenish);
        item.GetComponent<Player>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
    }
}
