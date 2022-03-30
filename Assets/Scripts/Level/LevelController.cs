using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

namespace Game
{
  public class LevelController : MonoBehaviour
  {
    public static event Action<int> newLevel;

    public LevelModel[] levelModels;

    [SerializeField]
    BallManager ballManager;

    [SerializeField]
    PostGameManager postGameManager;

    [SerializeField]
    GameObject ready;

    [SerializeField]
    LevelView levelView;

    private static int currentLevel;
    private bool levelStarted;

    private float ballsNeedToPop;
    private int ballsPopped;

    private void Start()
    {
      SoundManager.Instance.PlayBGM((int)BGM.InGame);

      PlayerController.deadPlayer += PlayerDead;
      BallController.popEvent += BallPop;

      ballsNeedToPop = 0;

      if (!levelStarted)
      {
        StartCoroutine(SetDelayStartLevel(2));
        levelStarted = true;
      }
    }

    private void SetDataLevel()
    {
      levelView.Initialize(levelModels.Length);

      SetLevel(currentLevel);
    }

    private void BallPop(BallController ball)
    {
      ballsPopped++;
      LevelChecker();
    }

    private void LevelChecker()
    {
      if (ballsPopped == ballsNeedToPop)
      {
        levelStarted = false;
        SetLevel(++currentLevel);
        ResetLevel();
        newLevel?.Invoke(currentLevel);
      }
    }

    private void InitializeLevelOnUI(LevelModel currentLevelModel)
    {
      ballsNeedToPop = 0;

      levelView.SetLevelAsset(currentLevelModel.levelAssets);
      levelView.SetLevelName(currentLevelModel.levelName);
      levelView.SetLevel(currentLevel + 1); //for text purpose

      if (ballManager.GetAllCurrentBalls().Count > 0)
      {
        foreach (BallController ball in ballManager.GetAllCurrentBalls().ToList())
        {
          ball.OnObjectDestroy();
        }
      }

      foreach (BallModel ballData in currentLevelModel.balls)
      {
        ballsNeedToPop += Mathf.Pow(2, ((int)ballData.ballSizeStart + 1)) - 1;
        BallController ball = ObjectPoolManager.Instance.SpawnFromPool("Ball", Vector3.zero, Quaternion.identity).GetComponent<BallController>();
        ball.SetBallData(ballData);
      }
    }

    public void SetLevel(int level)
    {
      if (level >= levelModels.Length)
      {
        level = levelModels.Length - 1;
      }

      currentLevel = level;
      InitializeLevelOnUI(levelModels[currentLevel]);

    }

    private void PlayerDead(int currentLife)
    {
      if (currentLife == 0)
      {
        postGameManager.gameObject.SetActive(true);
        return;
      }

      StartCoroutine(SetDelayAfterDeath(3));
    }

    private void ResetLevel()
    {
      SceneManager.LoadScene("GameScene");
    }

    private IEnumerator SetDelayStartLevel(int delay)
    {
      SetDataLevel();
      ready.SetActive(true);
      Time.timeScale = 0;
      yield return new WaitForSecondsRealtime(delay);
      Time.timeScale = 1;
      ready.SetActive(false);
    }

    private IEnumerator SetDelayAfterDeath(int delay)
    {
      Time.timeScale = 0;
      yield return new WaitForSecondsRealtime(delay);
      Time.timeScale = 1;
      levelStarted = false;
      ResetLevel();
    }

    public void ChangeScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
      PlayerController.deadPlayer -= PlayerDead;
      BallController.popEvent -= BallPop;
    }
  }

  [System.Serializable]
  public class LevelView
  {
    [SerializeField]
    SpriteRenderer levelBg;

    [SerializeField]
    Text levelName;

    [SerializeField]
    Text levelStage;

    private int maxStage;

    public void Initialize(int maxStage)
    {
      this.maxStage = maxStage;
    }

    public void SetLevelAsset(LevelAssets levelAssets)
    {
      levelBg.sprite = levelAssets.levelBG;
    }

    public void SetLevelName(string name)
    {
      levelName.text = name.ToString();
    }

    public void SetLevel(int level)
    {
      levelStage.text = level.ToString() + "-" + maxStage.ToString() + " STAGE";
    }
  }
}

