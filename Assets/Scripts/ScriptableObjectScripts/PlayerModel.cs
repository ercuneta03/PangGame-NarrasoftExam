using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public enum PlayerWeapon { Default, DoubleWire, PowerWire, DoubleMissle }

  [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
  public class PlayerModel : ScriptableObject
  {
    public PlayerWeapon playerDefaultWeapon;
    public PlayerAssets playerAssets;
    public PlayerStats playerStats;

    public List<ArsenalPerWeapon> arsenalPerWeapon;
  }


  [System.Serializable]
  public struct PlayerAssets
  {
    public Sprite playerSprite;
  }

  [System.Serializable]
  public struct PlayerStats
  {
    public int maximumLifeCount;
    public float speed;
  }

  [System.Serializable]
  public struct ArsenalPerWeapon
  {
    public PlayerWeapon playerWeapon;
    public Sprite playerWeaponIcon;
    public int arsenal;
  }
}
