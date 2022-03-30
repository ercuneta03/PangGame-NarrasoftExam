using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  public class WallManager : MonoBehaviour
  {
    [SerializeField]
    GameObject[] walls;

    [SerializeField]
    Animator wallsAnimator;

    Camera cam;

    Vector2 wallPosition;
    Vector2 wallScale;

    private void Start()
    {
      PlayerController.deadPlayer += PlayDeadIndicator;
      LevelController.newLevel += NewLevelIndicator;

      wallsAnimator.Play("WallDefault");

      cam = Camera.main;
      wallScale = cam.ViewportToWorldPoint(Vector2.one);

      foreach (GameObject go in walls)
      {
        wallScale = cam.ViewportToWorldPoint(Vector2.one);
        switch (go.name)
        {
          case "Wall Above":
            wallPosition = cam.ViewportToWorldPoint(new Vector2(0.5f, 1f));
            wallPosition.y += (go.transform.localScale.y / 4f);

            go.transform.localScale = new Vector3(ScreenToWorldWidth, go.transform.localScale.y);
            break;
          case "Wall Right":
            wallPosition = cam.ViewportToWorldPoint(new Vector2(1f, 0.5f));
            wallPosition.x += (go.transform.localScale.x / 4f);

            go.transform.localScale = new Vector3(go.transform.localScale.x, ScreenToWorldHeight);
            break;
          case "Wall Left":
            wallPosition = cam.ViewportToWorldPoint(new Vector2(0, 0.5f));
            wallPosition.x -= (go.transform.localScale.x / 4f);

            go.transform.localScale = new Vector3(go.transform.localScale.x, ScreenToWorldHeight);
            break;
          case "Ground":
            wallPosition = cam.ViewportToWorldPoint(new Vector2(0.5f, 0.15f));
            wallPosition.y -= (go.transform.localScale.y / 4f);

            go.transform.localScale = new Vector3(ScreenToWorldWidth, go.transform.localScale.y);
            break;
        }
        go.transform.position = wallPosition;
      }
    }

    private void PlayDeadIndicator(int currentLife)
    {
      wallsAnimator.Play("WallAnimPlayerDead");
    }

    private void NewLevelIndicator(int currentLevel)
    {
      wallsAnimator.Play("WallAnimNextLevel");
    }

    private void OnDisable()
    {
      PlayerController.deadPlayer -= PlayDeadIndicator;
      LevelController.newLevel -= NewLevelIndicator;
    }

    private float ScreenToWorldWidth
    {
      get
      {
        return wallScale.x * 2.5f;
      }
    }

    private float ScreenToWorldHeight
    {
      get
      {
        return wallScale.y * 2.5f;
      }
    }

  }
}
