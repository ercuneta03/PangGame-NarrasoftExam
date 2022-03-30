using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "WeaponPickUpModel", menuName = "ScriptableObjects/WeaponPickUpModel")]
  public class WeaponPickUpModel : ScriptableObject
  {
    public PlayerWeapon playerWeapon;
    public WeaponPickUpAssets weaponPickUpAssets;
    public float fallSpeed;
  }

  [System.Serializable]
  public struct WeaponPickUpAssets
  {
    public Sprite playerWeaponSprite;
  }
}
