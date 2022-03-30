using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
  Pop,
  PowerUp,
  WeaponPickUp
}
public enum BGM { MainMenu, InGame, PostGame }


public class SoundManager : MonoBehaviour
{

  public static SoundManager Instance = null;

  [SerializeField]
  private AudioSource bgmAS = null;

  [SerializeField]
  private AudioSource sfxAS = null;

  [SerializeField]
  private AudioClip[] bgmClips = null;

  [SerializeField]
  private AudioClip[] sfxClips = null;

  private void Awake()
  {
    if (Instance != this)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void PlaySFX(int index)
  {
    sfxAS.clip = sfxClips[index];
    sfxAS.Play();
  }

  public void PlayBGM(int index)
  {
    bgmAS.clip = bgmClips[index];
    bgmAS.Play();
  }

  public AudioSource getSFXAS()
  {
    return sfxAS;
  }

  public AudioSource getBGMAS()
  {
    return bgmAS;
  }


}
