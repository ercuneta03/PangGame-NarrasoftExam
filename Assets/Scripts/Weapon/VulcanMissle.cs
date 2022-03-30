using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class VulcanMissle : WeaponController, IPooledObject
  {
    [SerializeField]
    VulcanMissleView vulcanView;

    private Vector3 velocity;
    private Vector3 initialPosition;

    protected override void Start()
    {
      base.Start();

      velocity = new Vector2(0, travelSpeed);
      initialPosition = transform.localPosition;

      vulcanView.SetMissleAsset(weaponModel.missleWeaponAssets);
    }

    protected override void Update()
    {
      base.Update();

      transform.position += (velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Ball")
      {
        collision.GetComponent<BallController>().Pop();
        OnObjectDestroy();
      }
      else if (!string.IsNullOrEmpty(collision.tag) && collision.tag.Length >= 3)
      {
        if (collision.tag.Substring(0, 4) == "Wall")
        {
          OnObjectDestroy();
        }
      }
    }

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      SpawnObject();
    }

    public void OnObjectDestroy()
    {
      transform.localPosition = initialPosition;
      transform.parent.gameObject.SetActive(false);
    }
    #endregion
  }

  [System.Serializable]
  public class VulcanMissleView
  {
    [SerializeField]
    SpriteRenderer missleRenderer;

    public void SetMissleAsset(MissleWeaponAssets missleAsset)
    {
      missleRenderer.sprite = missleAsset.missleWeaponSprite;
    }
  }
}
