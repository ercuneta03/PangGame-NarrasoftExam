using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
  public class PopBallsPowerUp : PowerUpController
  {
    protected override void Start()
    {
      base.Start();
    }

    IEnumerator TakeEffectPowerUp()
    {
      yield return new WaitForEndOfFrame();
      foreach (BallController ball in ballManager.GetAllCurrentBalls().ToList()) //since pop will affect allCurrentBall size, create a separate list to avoid: Collection was modified error
      {
        ball.Pop();
        yield return new WaitForEndOfFrame();
      }
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
