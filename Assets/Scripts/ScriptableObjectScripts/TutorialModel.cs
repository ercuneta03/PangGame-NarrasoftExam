using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
  [CreateAssetMenu(fileName = "TutorialModel", menuName = "ScriptableObjects/TutorialModel")]
  public class TutorialModel : ScriptableObject
  {
    public Sprite[] tutorialImages;
  }
}
