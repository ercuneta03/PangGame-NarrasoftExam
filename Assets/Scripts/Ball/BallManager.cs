using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class BallManager : MonoBehaviour
  {
    List<BallController> allCurrentBalls;

    private void Awake()
    {
      allCurrentBalls = new List<BallController>();

      BallController.ballSpawnEvent += AddBall;
      BallController.popEvent += RemoveBall;
    }

    public BallController SpawnBall(Vector3 position, int direction, BallSize ballSize, bool isMoving, bool decreaseSize = false)
    {
      BallController ball = ObjectPoolManager.Instance.SpawnFromPool("Ball", position, Quaternion.identity).GetComponent<BallController>();
      ball.VelocityX *= direction;
      ball.VelocityY *= ball.VelocityY < 0 ? -1 : 1;
      ball.IsMoving = isMoving;
      if (decreaseSize)
      {
        int getNextSizeIndex = (int)ballSize - 1;
        ball.ChangeBallSize((BallSize)getNextSizeIndex);
      }
      return ball;
    }

    public void AddBall(BallController ball)
    {
      allCurrentBalls.Add(ball);
    }

    public void RemoveBall(BallController ball)
    {
      if (allCurrentBalls.Contains(ball))
      {
        allCurrentBalls.Remove(ball);
      }
    }

    public List<BallController> GetAllCurrentBalls()
    {
      return allCurrentBalls;
    }

    private void OnDisable()
    {
      BallController.ballSpawnEvent -= AddBall;
      BallController.popEvent -= RemoveBall;
    }
  }
}