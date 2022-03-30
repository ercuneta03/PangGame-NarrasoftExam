using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [RequireComponent(typeof(BoxCollider2D))]
  public class WeaponPickUp : MonoBehaviour, IPooledObject
  {
    [SerializeField]
    WeaponPickUpModel[] weaponPickUps;

    [SerializeField]
    WeaponPickUpView weaponPickUpView;

    private PlayerController player;
    private WeaponPickUpModel weaponPickUp;

    private Vector3 velocity;
    private bool move;

    private void Start()
    {
      Initialize();
    }

    private void Initialize()
    {
      weaponPickUp = weaponPickUps[Random.Range(0, weaponPickUps.Length)];
      velocity = new Vector3(0, -weaponPickUp.fallSpeed);

      move = true;

      weaponPickUpView.SetWeaponPickUpAsset(weaponPickUp.weaponPickUpAssets);
    }

    private void Update()
    {
      if (move)
        transform.position += (velocity * Time.deltaTime);
    }

    private void ChangePlayerWeapon()
    {
      SoundManager.Instance.PlaySFX((int)SFX.WeaponPickUp);

      player.ChangeWeapon(weaponPickUp.playerWeapon);
      OnObjectDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Player")
      {
        player = collision.GetComponent<PlayerController>();
        ChangePlayerWeapon();
      }

      if (collision.tag == "Ground")
      {
        move = false;
      }
    }

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      Initialize();
    }

    public void OnObjectDestroy()
    {
      gameObject.SetActive(false);
    }
    #endregion
  }

  [System.Serializable]
  public class WeaponPickUpView
  {
    [SerializeField]
    SpriteRenderer weaponPickUp;

    public void SetWeaponPickUpAsset(WeaponPickUpAssets assets)
    {
      weaponPickUp.sprite = assets.playerWeaponSprite;
    }
  }
}