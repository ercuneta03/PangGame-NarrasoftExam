using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "BallData", menuName = "ScriptableObjects/BallData")]
  public class BallModel : ScriptableObject
  {
    [Range(1, 12)]
    public float velocityX;

    public BallAsset ballAsset;
    public BallSize ballSizeStart;
    public List<BallStats> ballStats;
  }

  [System.Serializable]
  public struct BallAsset
  {
    public Color ballColor;
  }

  [System.Serializable]
  public struct BallStats
  {
    public BallSize ballSize;
    public int score;
  }
}
