using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class InvinciblePowerUp : PowerUpController
  {
    protected override void Start()
    {
      base.Start();
    }

    IEnumerator TakeEffectPowerUp()
    {
      playerController.Invincible(durationOfPowerUp);
      yield return new WaitForEndOfFrame();
      OnObjectDestroy();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
      base.OnTriggerEnter2D(collision);
      if (collision.tag == "Player")
      {
        StartCoroutine(TakeEffectPowerUp());
      }
    }
  }
}
