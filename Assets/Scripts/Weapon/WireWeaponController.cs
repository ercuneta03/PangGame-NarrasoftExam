using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class WireWeaponController : WeaponController, IPooledObject
  {
    [SerializeField]
    WireWeaponView weaponView;

    protected delegate void OnHitEvent();
    protected OnHitEvent onHitEvent;

    protected LineRenderer wire;
    protected RaycastHit2D hit;

    protected float wireLength;

    protected float timer;
    protected float wireLifeSpan = 4;

    protected override void Start()
    {
      base.Start();

      wire = GetComponentInChildren<LineRenderer>();
      weaponView.SetWeaponAsset(weaponModel.wireWeaponAssets);
    }

    protected override void Update()
    {
      base.Update();

      wireLength += (travelSpeed * Time.deltaTime);
      wire.SetPosition(0, Vector2.up * wireLength);

      Debug.DrawLine(transform.position, transform.TransformPoint(wire.GetPosition(0)), Color.red);

      hit = Physics2D.Linecast(transform.position, transform.TransformPoint(wire.GetPosition(0)), LayerMask.GetMask("Targetable"));

      if (hit)
      {
        onHitEvent?.Invoke();
      }
    }

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      SpawnObject();

      timer = 0;
      wireLength = 0;
      if (wire != null)
        wire.SetPosition(0, Vector2.zero);
    }

    public void OnObjectDestroy()
    {
      DestroyObject();
      gameObject.SetActive(false);
    }
    #endregion
  }

  [System.Serializable]
  public class WireWeaponView
  {
    [SerializeField]
    LineRenderer weaponRenderer;

    public void SetWeaponAsset(WireWeaponAssets weaponAssets)
    {
      weaponRenderer.material = weaponAssets.wireWeaponMaterial;
    }

    public void SetSortingOrder(int sortingOrder)
    {
      weaponRenderer.sortingOrder = sortingOrder;
    }
  }
}
