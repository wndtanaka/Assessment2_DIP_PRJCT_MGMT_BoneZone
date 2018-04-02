using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkCounter : MonoBehaviour
{
    [SerializeField]
    Text text;

    PlayerShoot playerShoot;
    WeaponReloader reloader;

    private void Awake()
    {
        GameManager.Instance.onLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    void HandleOnLocalPlayerJoined(Player player)
    {
        // getting PlayerShoot component from player
        playerShoot = player.PlayerShoot;
        playerShoot.onWeaponSwitch += HandleOnWeaponSwitch;
        //HandleOnAmmoChange();
    }

    private void HandleOnWeaponSwitch(Shooter activeWeapon)
    {
        // getting WeaponReloader component from activeWeapon
        reloader = activeWeapon.reloader;
        reloader.onAmmoChanged += HandleOnAmmoChange;
        HandleOnAmmoChange();
    }

    private void HandleOnAmmoChange()
    {
        int amountInInventory = reloader.RoundsRemainingInInventory;
        int amountInMag = reloader.RoundsRemainingInMag;
        // text formatting
        text.text = string.Format("{0}/{1}",amountInMag,amountInInventory);
    }
}

