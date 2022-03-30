using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class PowerUpSpawnerManager : MonoBehaviour
  {
    public void SpawnPowerUp(Vector3 spawnPosition)
    {
      int randomPowerUp = Random.Range(0, 3);
      string powerUpName = "PopBalls"; //default
      switch (randomPowerUp)
      {
        case 1:
          powerUpName = "StopBalls";
          break;
        case 2:
          powerUpName = "Invincible";
          break;
      }
      ObjectPoolManager.Instance.SpawnFromPool(powerUpName, spawnPosition, Quaternion.identity);
    }
  }
}
