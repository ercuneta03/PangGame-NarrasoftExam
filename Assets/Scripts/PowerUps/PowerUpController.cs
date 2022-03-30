using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class PowerUpController : MonoBehaviour, IPooledObject
  {
    [SerializeField]
    PowerUpModel powerUpModel;

    protected BallManager ballManager;
    protected PlayerController playerController;

    private Vector3 velocity;

    protected float durationOfItem;
    protected float durationOfPowerUp;

    private bool move = true;

    protected virtual void Start()
    {
      durationOfItem = powerUpModel.durationOfItem;
      durationOfPowerUp = powerUpModel.durationOfPowerUp;

      velocity = new Vector3(0, -powerUpModel.fallSpeed);

      ballManager = FindObjectOfType<BallManager>();
      playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
      if (move)
        transform.position += (velocity * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Player")
      {
        SoundManager.Instance.PlaySFX((int)SFX.PowerUp);
      }

      if (collision.tag == "Ground")
      {
        move = false;
      }
    }

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      move = true;
    }

    public void OnObjectDestroy()
    {
      gameObject.SetActive(false);
    }
    #endregion

  }
}
