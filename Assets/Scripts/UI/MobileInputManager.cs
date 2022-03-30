using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class MobileInputManager : MonoBehaviour
  {
    [SerializeField]
    MobileInputController mobileInputUI;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
      mobileInputUI.gameObject.SetActive(true);
#endif
    }
  }
}
