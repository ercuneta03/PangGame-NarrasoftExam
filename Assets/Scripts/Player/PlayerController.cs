using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class PlayerController : MonoBehaviour
  {
    public static event Action<Sprite> newWeaponEvent;
    public static event Action<int> deadPlayer;

    [SerializeField]
    PlayerModel playerModel;

    [SerializeField]
    Invincible playerInvincible;

    [SerializeField]
    PlayerView playerView;

    private WeaponController defaultWeapon;
    private PlayerWeapon defaultPlayerWeapon;

    private Vector3 velocity;
    public Vector3 Position => transform.position;

    private float speed;

    private bool canMoveLeft = true, canMoveRight = true;
    private bool isInvincible;

    private bool isLeftButtonPressed;
    private bool isRightButtonPressed;

    private static bool gameStarted;
    public bool GameStarted
    {
      set { gameStarted = value; }
    }

    private int wireSpawnCount;
    private int powerWireSpawnCount;

    private int wireLimit;
    private int powerWireLimit;

    private int maximumLife;
    private static int currentLife;

    void Start()
    {
      PowerWire.powerWireOnDespawn += OnPowerWireDespawn;
      DoubleWire.doubleWireOnDespawn += OnDoubleWireDespawn;

      MobileInputController.pressLeftMobile += MoveLeft;
      MobileInputController.pressRightMobile += MoveRight;
      MobileInputController.pressShootMobile += Shoot;

      speed = playerModel.playerStats.speed;

      if (!gameStarted)
      {
        maximumLife = playerModel.playerStats.maximumLifeCount;
        currentLife = maximumLife;
        gameStarted = true;
      }

      defaultPlayerWeapon = playerModel.playerDefaultWeapon;
      ChangeWeapon(defaultPlayerWeapon);

      wireLimit = playerModel.arsenalPerWeapon.Find(weapon => weapon.playerWeapon == PlayerWeapon.Default).arsenal;
      powerWireLimit = playerModel.arsenalPerWeapon.Find(weapon => weapon.playerWeapon == PlayerWeapon.PowerWire).arsenal;

      velocity = new Vector3(speed, 0);

      playerView.SetPlayerAsset(playerModel);
      playerView.SetPlayerSortOrder(2);
    }

    void Update()
    {
      //if(Input.GetKeyDown(KeyCode.Alpha1))
      //{
      //  ChangeWeapon(PlayerWeapon.DoubleMissle);
      //}

      if ((Input.GetKey(KeyCode.A) || isLeftButtonPressed) && canMoveLeft)
      {
        transform.position -= (velocity * Time.deltaTime);
      }
      else if ((Input.GetKey(KeyCode.D) || isRightButtonPressed) && canMoveRight)
      {
        transform.position += (velocity * Time.deltaTime);
      }
      else if (Input.GetKeyDown(KeyCode.Space))
      {
        Shoot();
      }
    }

    #region Player Behavior
    private void MoveLeft(bool isPressed)
    {
      isLeftButtonPressed = isPressed;
      isRightButtonPressed = false;
    }

    private void MoveRight(bool isPressed)
    {
      isRightButtonPressed = isPressed;
      isLeftButtonPressed = false;
    }

    private void Shoot()
    {
      switch (defaultPlayerWeapon)
      {
        case PlayerWeapon.Default:
        case PlayerWeapon.DoubleWire:
          if (wireSpawnCount < wireLimit)
          {
            wireSpawnCount++;
            ObjectPoolManager.Instance.SpawnFromPool("Single Wire", transform.position, Quaternion.identity);
          }
          break;
        case PlayerWeapon.PowerWire:
          if (powerWireSpawnCount < powerWireLimit)
          {
            powerWireSpawnCount++;
            ObjectPoolManager.Instance.SpawnFromPool("Power Wire", transform.position, Quaternion.identity);
          }
          break;
        case PlayerWeapon.DoubleMissle:
          ObjectPoolManager.Instance.SpawnFromPool("Double Missle", transform.position, Quaternion.identity);
          break;
      }
    }

    private void Dead()
    {
      deadPlayer?.Invoke(currentLife);
      currentLife--;
    }


    public void ChangeWeapon(PlayerWeapon newPlayerWeapon)
    {
      defaultPlayerWeapon = newPlayerWeapon;
      wireLimit = playerModel.arsenalPerWeapon.Find(weapon => weapon.playerWeapon == defaultPlayerWeapon).arsenal;

      Sprite weaponIcon = playerModel.arsenalPerWeapon.Find(weapon => weapon.playerWeapon == defaultPlayerWeapon).playerWeaponIcon;
      newWeaponEvent?.Invoke(weaponIcon);
    }

    #endregion

    #region Weapon Despawn Calls

    private void OnPowerWireDespawn()
    {
      powerWireSpawnCount--;
      if (powerWireSpawnCount < 0)
      {
        powerWireSpawnCount = 0;
      }
    }

    private void OnDoubleWireDespawn()
    {
      wireSpawnCount--;
      if (wireSpawnCount < 0)
      {
        wireSpawnCount = 0;
      }
    }

    #endregion

    #region Player Skill

    public void Invincible(float timer)
    {

      StartCoroutine(PlayerInvincible(timer));
    }

    private IEnumerator PlayerInvincible(float timer)
    {
      isInvincible = true;
      playerInvincible.gameObject.SetActive(isInvincible);
      yield return new WaitForSecondsRealtime(timer);
      isInvincible = false;
      playerInvincible.gameObject.SetActive(isInvincible);
    }

    #endregion

    #region Trigger Events

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "WallLeft")
      {
        canMoveLeft = false;
      }
      else if (collision.tag == "WallRight")
      {
        canMoveRight = false;
      }

      if (collision.tag == "Ball")
      {
        if (!isInvincible)
          Dead();
      }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      if (collision.tag == "WallLeft")
      {
        canMoveLeft = true;
      }
      else if (collision.tag == "WallRight")
      {
        canMoveRight = true;
      }
    }

    #endregion

    private void OnDisable()
    {
      PowerWire.powerWireOnDespawn -= OnPowerWireDespawn;
      DoubleWire.doubleWireOnDespawn -= OnDoubleWireDespawn;

      MobileInputController.pressLeftMobile -= MoveLeft;
      MobileInputController.pressRightMobile -= MoveRight;
      MobileInputController.pressShootMobile -= Shoot;
    }
  }

  [System.Serializable]
  public class PlayerView
  {
    [SerializeField]
    SpriteRenderer playerRenderer;

    public void SetPlayerAsset(PlayerModel playerModel)
    {
      playerRenderer.sprite = playerModel.playerAssets.playerSprite;
    }

    public void SetPlayerSortOrder(int sortingOrder)
    {
      playerRenderer.sortingOrder = sortingOrder;
    }
  }
}