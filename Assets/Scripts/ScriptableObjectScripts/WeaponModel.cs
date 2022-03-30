using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
  [CreateAssetMenu(fileName = "WeaponModel", menuName = "ScriptableObjects/WeaponModel")]
  public class WeaponModel : ScriptableObject
  {

    public WireWeaponAssets wireWeaponAssets;
    public MissleWeaponAssets missleWeaponAssets;

    public float fireRate;
    public float travelSpeed;

  }

  [System.Serializable]
  public struct WireWeaponAssets
  {
    public Material wireWeaponMaterial;
  }

  [System.Serializable]
  public struct MissleWeaponAssets
  {
    public Sprite missleWeaponSprite;
  }
}
