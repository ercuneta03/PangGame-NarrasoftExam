using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallSize { Tiny, Small, Medium, Large, Huge }

namespace Game
{
  public class BallController : MonoBehaviour, IPooledObject
  {
    public static event Action<BallController> ballSpawnEvent;
    public static event Action<BallController> popEvent;

    [SerializeField]
    private BallSize ballSize; //view purposes

    [SerializeField]
    BallModel ballModel;

    [SerializeField]
    BallView ballView;

    private BallManager ballManager;

    private Vector3 velocity;

    private float velocityX;
    public float VelocityX
    {
      get { return velocityX; }
      set
      {
        velocityX = value;
        velocity = new Vector3(velocityX, velocity.y);
      }
    }

    private float velocityY;
    public float VelocityY
    {
      get { return velocityY; }
      set
      {
        velocityX = value;
        velocity = new Vector3(velocity.x, velocityY);
      }
    }
    private bool isMoving;
    public bool IsMoving
    {
      get { return isMoving; }
      set { isMoving = value; }
    }

    public int GetPoppedScore => ballModel.ballStats.Find(ball => ball.ballSize == ballSize).score;
    public BallSize GetBallSize => ballSize;

    private void Awake()
    {
      ballManager = FindObjectOfType<BallManager>();

      SetBallData(ballModel);
    }

    public void SetBallData(BallModel ballModel)
    {
      ballSize = ballModel.ballSizeStart;
      velocityX = ballModel.velocityX;

      isMoving = true;

      ChangeBallSize(ballSize);

      velocity = new Vector2(velocityX, velocityY);

      ballView.SetBallAsset(ballModel.ballAsset);
      ballView.SetBallSortOrder(1);
    }

    private void Update()
    {
      if (isMoving)
        transform.position += (velocity * Time.deltaTime);
    }

    public void DecreaseSize()
    {
      int getNextSizeIndex = (int)ballSize - 1;
      ChangeBallSize((BallSize)getNextSizeIndex);
    }

    public void ChangeBallSize(BallSize ballSize)
    {
      this.ballSize = ballSize;
      switch (ballSize)
      {
        case BallSize.Huge:
          velocityY = 11f;
          transform.localScale = Vector3.one;
          break;
        case BallSize.Large:
          velocityY = 9.5f;
          transform.localScale = Vector3.one * 0.8f;
          break;
        case BallSize.Medium:
          velocityY = 8;
          transform.localScale = Vector3.one * 0.6f;
          break;
        case BallSize.Small:
          velocityY = 6.5f;
          transform.localScale = Vector3.one * 0.4f;
          break;
        case BallSize.Tiny:
          velocityY = 5f;
          transform.localScale = Vector3.one * 0.2f;
          break;
      }
    }

    IEnumerator DeleteGameObjectOnNextFrame()
    {
      yield return new WaitForEndOfFrame();
      OnObjectDestroy();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
      int randomValue = UnityEngine.Random.Range(0, 2);
      if (collision.tag == "Ground")
      {
        velocity = new Vector2(randomValue == 0 ? -velocityX : velocityX, velocityY < 0 ? -velocityY : velocityY);
      }
      else if (collision.tag == "WallRight")
      {
        velocity = new Vector2(velocityX < 0 ? velocityX : -velocityX, randomValue == 0 ? -velocityY : velocityY);
      }
      else if (collision.tag == "WallLeft")
      {
        velocity = new Vector2(velocityX < 0 ? -velocityX : velocityX, randomValue == 0 ? -velocityY : velocityY);
      }
      else if (collision.tag == "WallAbove")
      {
        velocity = new Vector2(randomValue == 0 ? -velocityX : velocityX, velocityY < 0 ? velocityY : -velocityY);
      }
    }

    #region PowerUp Effects
    public void Pop()
    {
      if (ballSize != BallSize.Tiny)
      {
        ballManager.SpawnBall(transform.position, -1, ballSize, isMoving, true);
        ballManager.SpawnBall(transform.position, 1, ballSize, isMoving, true);
      }

      SoundManager.Instance.PlaySFX((int)SFX.Pop);

      popEvent?.Invoke(this);

      StartCoroutine(DeleteGameObjectOnNextFrame());
    }

    public void StopMoving(float duration)
    {
      StartCoroutine(StopMovingDuration(duration));
    }

    private IEnumerator StopMovingDuration(float duration)
    {
      isMoving = false;
      yield return new WaitForSecondsRealtime(duration);
      isMoving = true;
    }

    #endregion

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      ballSpawnEvent?.Invoke(this);
    }

    public void OnObjectDestroy()
    {
      gameObject.SetActive(false);
    }
    #endregion
  }

  [System.Serializable]
  public class BallView
  {
    [SerializeField]
    SpriteRenderer ballRenderer;

    public void SetBallAsset(BallAsset ballAsset)
    {
      ballRenderer.color = ballAsset.ballColor;
    }

    public void SetBallSortOrder(int sortingOrder)
    {
      ballRenderer.sortingOrder = sortingOrder;
    }
  }
}

