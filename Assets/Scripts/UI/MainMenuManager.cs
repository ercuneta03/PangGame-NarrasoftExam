using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
  public class MainMenuManager : MonoBehaviour
  {
    [SerializeField]
    TutorialModel tutorialModel;

    [SerializeField]
    Image tutorialPreviewImage;

    [SerializeField]
    MainMenuView mainMenuView;

    private int tutorialIndex = 1;

    private void Start()
    {
      SoundManager.Instance.PlayBGM((int)BGM.MainMenu);
#if UNITY_ANDROID
      mainMenuView.SetMainMenuText("Tap the screen to start");
#endif
    }

    public void ShowNextTutorialPreview()
    {
      if (tutorialIndex >= tutorialModel.tutorialImages.Length)
      {
        tutorialIndex = 0;
        SceneManager.LoadScene("GameScene");
        return;
      }
      tutorialPreviewImage.sprite = tutorialModel.tutorialImages[tutorialIndex++];
    }
  }

  [System.Serializable]
  public class MainMenuView
  {
    [SerializeField]
    Text pressText;

    public void SetMainMenuText(string message)
    {
      pressText.text = message;
    }
  }
}
