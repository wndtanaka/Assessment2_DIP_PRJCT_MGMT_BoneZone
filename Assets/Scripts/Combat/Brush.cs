using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : Shooter
{
    public override void Fire()
    {
        base.Fire();
        if (canFire)
        {
            // we can fire
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
