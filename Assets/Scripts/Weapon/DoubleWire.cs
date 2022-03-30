using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class DoubleWire : WireWeaponController
  {
    public static event Action doubleWireOnDespawn;

    protected override void Start()
    {
      base.Start();
      onHitEvent = OnHitDoubleWire;
    }

    protected override void Update()
    {
      base.Update();
    }

    private void OnHitDoubleWire()
    {
      if (hit.collider.tag == "Ball")
      {
        hit.collider.GetComponent<BallController>().Pop();
      }
      Destroy();
    }

    private void Destroy()
    {
      doubleWireOnDespawn?.Invoke();
      OnObjectDestroy();
    }

  }
}
