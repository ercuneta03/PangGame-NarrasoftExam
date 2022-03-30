using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [CreateAssetMenu(fileName = "LevelModel", menuName = "ScriptableObjects/LevelModel")]
  public class LevelModel : ScriptableObject
  {
    public string levelName;
    public BallModel[] balls;

    public LevelAssets levelAssets;
  }

  [System.Serializable]
  public struct LevelAssets
  {
    public Sprite levelBG;
  }
}

