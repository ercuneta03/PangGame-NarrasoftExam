using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class WeaponSpawnerManager : MonoBehaviour
  {
    public void SpawnWeaponPickUp(Vector3 spawnPosition)
    {
      ObjectPoolManager.Instance.SpawnFromPool("Weapon Pick Up", spawnPosition, Quaternion.identity);
    }
  }
}
