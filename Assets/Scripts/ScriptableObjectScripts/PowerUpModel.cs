using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpModel", menuName = "ScriptableObjects/PowerUpModel")]
public class PowerUpModel : ScriptableObject
{
  public float durationOfItem;
  public float durationOfPowerUp;

  public float fallSpeed;
}
