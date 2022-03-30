using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class PowerWire : WireWeaponController
  {
    public static event Action powerWireOnDespawn;

    protected override void Start()
    {
      base.Start();

      onHitEvent = OnHitPowerWire;
    }

    protected override void Update()
    {
      base.Update();
    }

    private void OnHitPowerWire()
    {
      timer += Time.deltaTime;
      if (timer >= wireLifeSpan)
      {
        Destroy();
      }

      if (hit.collider.tag == "Ball")
      {
        hit.collider.GetComponent<BallController>().Pop();
        Destroy();
      }

      travelSpeed = 0;
    }

    private void Destroy()
    {
      powerWireOnDespawn?.Invoke();
      OnObjectDestroy();
    }
  }
}
