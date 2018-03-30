using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float rateOfFire;
    [SerializeField]
    Projectile projectile;
    [SerializeField]
    Transform hand;

    [HideInInspector]
    public Transform muzzle;

    public bool canFire;

    float nextFireAllowed;
    protected WeaponReloader reloader;

    public void Equip()
    {
        // set the equipped weapon to the hand parent
        transform.SetParent(hand);
    }

    void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
    }

    public void Reload()
    {
        if (reloader == null)
        {
            return;
        }
        reloader.Reload();
    }
    public virtual void Fire()
    {
        canFire = false;
        if (Time.time < nextFireAllowed)
        {
            return;
        }

        if (reloader != null)
        {
            // if is reloading, cant shoot
            if (reloader.IsReloading)
            {
                return;
            }
            // if is remaining ammo in mag is zero, cant shoot
            if (reloader.RoundsRemainingInMag == 0)
            {
                return;
            }
            // otherwise take 1 bullet from mag (changeable to more than 1, if want to make charging weapon)
            reloader.TakeFromMag(1);
        }

        // time counter for continuous shooting
        nextFireAllowed = Time.time + rateOfFire;

        // instantiate the projectile
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        canFire = true;
    }
}
