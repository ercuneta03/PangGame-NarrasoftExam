using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
  public class ScoreSpawn : MonoBehaviour, IPooledObject
  {
    [SerializeField]
    ScoreSpawnView scoreSpawnView;

    public void ShowScore(int score, int showDuration)
    {
      StartCoroutine(ShowScoreWithDuration(score, showDuration));
    }

    private IEnumerator ShowScoreWithDuration(int score, int showDuration)
    {
      scoreSpawnView.SetScoreText(score);
      yield return new WaitForSecondsRealtime(showDuration);
      OnObjectDestroy();
    }

    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
    }

    public void OnObjectDestroy()
    {
      gameObject.SetActive(false);
    }
    #endregion

  }

  [System.Serializable]
  public class ScoreSpawnView
  {
    [SerializeField]
    Text scoreText;

    public void SetScoreText(int score)
    {
      scoreText.text = score.ToString();
    }

  }
}
