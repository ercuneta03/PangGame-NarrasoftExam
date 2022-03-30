using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class FruitSpawnerManager : MonoBehaviour
  {
    public void SpawnFruit(Vector3 spawnPosition)
    {
      ObjectPoolManager.Instance.SpawnFromPool("Fruit", spawnPosition, Quaternion.identity);
    }
  }
}
