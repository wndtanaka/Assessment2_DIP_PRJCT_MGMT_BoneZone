using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherit from Shooter class
public class Brush : Shooter
{
    public override void Fire()
    {
        base.Fire();
        if (canFire)
        {
            // further added mechanics here
        }
    }

    public void Update()
    {
        if (GameManager.Instance.InputController.Reload)
        {
            Reload();
        }
    }
}
