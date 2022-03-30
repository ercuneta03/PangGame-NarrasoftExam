using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class GameSpawnerManager : MonoBehaviour
  {
    [SerializeField]
    WeaponSpawnerManager weaponSpawnerManager;

    [SerializeField]
    PowerUpSpawnerManager powerUpSpawnerManager;

    [SerializeField]
    FruitSpawnerManager fruitSpawnerManager;

    private PlayerController player;

    private float spawnFruitTime = 15;
    private float timer = 0;

    private void Start()
    {
      BallController.popEvent += BallPoped;
      player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
      timer += Time.deltaTime;
      if (timer >= spawnFruitTime)
      {
        timer = 0;
        fruitSpawnerManager.SpawnFruit(new Vector3(player.Position.x, player.Position.y + 10, 0));
      }
    }

    private void BallPoped(BallController ball)
    {
      int random = Random.Range(0, 10); //resulting other than 3 or 7 would spawn nothing
      switch (random)
      {
        case 3:
          weaponSpawnerManager.SpawnWeaponPickUp(ball.transform.position);
          break;
        case 7:
          powerUpSpawnerManager.SpawnPowerUp(ball.transform.position);
          break;
      }
    }

    private void OnDisable()
    {
      BallController.popEvent -= BallPoped;
    }
  }
}
