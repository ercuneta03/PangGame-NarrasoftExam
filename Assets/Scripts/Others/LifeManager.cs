using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class LifeManager : MonoBehaviour
  {
    [SerializeField]
    LifeView lifeView;

    void Start()
    {
      PlayerController.deadPlayer += PlayerDead;


    }

    private void PlayerDead(int currentLife)
    {
      lifeView.SetLifeIcon(currentLife);
    }

    private void OnDisable()
    {
      PlayerController.deadPlayer -= PlayerDead;
    }

  }

  [System.Serializable]
  public class LifeView
  {
    [SerializeField]
    Image[] lifeIcons;

    public void SetLifeIcon(int currentLife)
    {
      foreach (Image i in lifeIcons)
      {
        i.gameObject.SetActive(false);
      }

      for (int i = 0; i < currentLife; i++)
      {
        lifeIcons[i].gameObject.SetActive(true);
      }
    }
  }
}