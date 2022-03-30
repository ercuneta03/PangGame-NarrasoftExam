using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "FruitModel", menuName = "ScriptableObjects/FruitModel")]
  public class FruitModel : ScriptableObject
  {
    public int scoreValue;
    public float fallSpeed;

    public FruitAssets fruitAssets;
  }

  [System.Serializable]
  public struct FruitAssets
  {
    public Sprite fruitSprite;
  }
}
