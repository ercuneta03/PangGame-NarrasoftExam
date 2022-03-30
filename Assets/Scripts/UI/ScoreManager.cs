using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class ScoreManager : MonoBehaviour
  {
    [SerializeField]
    private ScoreView scoreView;

    private long currentScoreHolder;
    public long CurrentScoreHolder
    {
      get { return currentScoreHolder; }
      set
      {
        currentScoreHolder = value + CurrentScore;
        scoreView.SetScoreText(currentScoreHolder);
      }
    }

    private static long currentScore;
    public long CurrentScore
    {
      get { return currentScore; }
      set
      {
        currentScore = value;
        scoreView.SetScoreText(currentScore);
      }
    }

    private long scoreFrag = 0;
    private long previousScore = 0;

    private void Start()
    {
      LevelController.newLevel += LevelComplete;

      scoreView.SetScoreText(currentScore);

      BallController.popEvent += BallPopped;
      Fruit.fruitValueOnClaim += FruitValueClaimed;
    }

    private void BallPopped(BallController ball)
    {
      int getPoppedScore = ball.GetPoppedScore;
      CurrentScoreHolder += getPoppedScore;

      if (ball.GetBallSize == BallSize.Tiny)
        ShowScore(ball.transform.position, getPoppedScore);
    }

    private void FruitValueClaimed(Fruit fruit)
    {
      int getFruitScore = fruit.ScoreValue;
      CurrentScoreHolder += getFruitScore;

      ShowScore(fruit.transform.position, getFruitScore);
    }

    private void ShowScore(Vector3 position, int score)
    {
      ScoreSpawn scoreSpawn = ObjectPoolManager.Instance.SpawnFromPool("Score", position, Quaternion.identity).GetComponent<ScoreSpawn>();
      scoreSpawn.ShowScore(score, 1);
    }

    private IEnumerator SetScoreIncrement(long score)
    {
      scoreFrag = previousScore;
      while (scoreFrag < score)
      {
        scoreFrag += 10;
        scoreView.SetScoreText(scoreFrag);
        yield return new WaitForEndOfFrame();
      }
      scoreView.SetScoreText(score);
      previousScore = score;
      StopCoroutine(SetScoreIncrement(score));
    }

    private void LevelComplete(int newLevel)
    {
      CurrentScore = CurrentScoreHolder;
    }

    private void OnDisable()
    {
      LevelController.newLevel -= LevelComplete;
      BallController.popEvent -= BallPopped;
      Fruit.fruitValueOnClaim -= FruitValueClaimed;
    }
  }

  [System.Serializable]
  public class ScoreView
  {
    [SerializeField]
    Text scoreText;

    public void SetScoreText(long score)
    {
      scoreText.text = score.ToString("n0");
    }

  }
}
