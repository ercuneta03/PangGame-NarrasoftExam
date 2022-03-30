using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
  public class PostGameManager : MonoBehaviour
  {
    [SerializeField]
    PostGameView postGameView;

    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField]
    LevelController levelController;

    private int score;
    private int highScore;

    private string highScoreKey = "High Score";

    private void Start()
    {
      SoundManager.Instance.PlayBGM((int)BGM.PostGame);

      if (PlayerPrefs.HasKey(highScoreKey))
      {
        highScore = PlayerPrefs.GetInt(highScoreKey);
      }

      score = (int)scoreManager.CurrentScore;

      if (score > highScore)
      {
        PlayerPrefs.SetInt(highScoreKey, (int)score);
      }

      postGameView.SetScoreText(score, highScore);
    }

    public void Restart()
    {
      levelController.SetLevel(0);
      scoreManager.CurrentScore = 0;
      FindObjectOfType<PlayerController>().GameStarted = false;

      SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
      Application.Quit();
    }
  }

  [System.Serializable]
  public class PostGameView
  {
    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text highScoreText;

    public void SetScoreText(int score, int highScore)
    {
      scoreText.text = "Score: " + score.ToString();
      highScoreText.text = "High Score: " + highScore.ToString();
    }

  }
}
