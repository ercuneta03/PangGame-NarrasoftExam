using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class WeaponController : MonoBehaviour
  {
    [SerializeField]
    protected WeaponModel weaponModel;

    protected float travelSpeed;
    protected float fireRate;

    protected virtual void Start()
    {
      travelSpeed = weaponModel.travelSpeed;
      fireRate = weaponModel.fireRate;
    }

    protected virtual void Update() { }

    protected virtual void SpawnObject() { Start(); }

    protected virtual void DestroyObject() { }
  }
}
