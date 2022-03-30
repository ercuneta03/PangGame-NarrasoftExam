using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [RequireComponent(typeof(BoxCollider2D))]
  public class Invincible : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Ball")
      {
        collision.GetComponent<BallController>().Pop();
      }
    }
  }
}
