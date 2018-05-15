using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float rateOfFire;
    [SerializeField]
    int ammoNeededToShoot = 1;
    [SerializeField]
    Projectile projectile;
    [SerializeField]
    Transform hand;
    [SerializeField]
    AudioController attackAudio;
    [SerializeField]
    GameObject shootPoint;

    public AudioSource shootSound;
    public bool canFire;

    float nextFireAllowed;
    public WeaponReloader reloader;

    public void Equip()
    {
        // set the equipped weapon to the hand parent
        transform.SetParent(hand);
    }

    void Start()
    {
        // the projectile's spawn point has ShootPoint tag attached.
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
        // checking if its not ready attack yet.
        if (Time.time < nextFireAllowed)
        {
            return;
        }

        if (reloader != null)
        {
            // if is reloading, cant shoot || if no ammo left, cant shoot || if ammo left < ammo needed to shoot, cant shoot  
            if (reloader.IsReloading || reloader.RoundsRemainingInMag <= 0 || reloader.RoundsRemainingInMag < ammoNeededToShoot)
            {
                return;
            }
            // otherwise take 1 bullet from mag (changeable to more than 1, if want to make charging weapon)
            reloader.TakeFromMag(ammoNeededToShoot);
        }

        // time counter for continuous shooting
        nextFireAllowed = Time.time + rateOfFire;

        // instantiate the projectile
        Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);

        // TODO insert attack audio clip
        shootSound.Play();

        canFire = true;
    }
}
