using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
  public class MobileInputController : MonoBehaviour
  {
    public static event Action<bool> pressLeftMobile;
    public static event Action<bool> pressRightMobile;
    public static event Action pressShootMobile;


    [SerializeField]
    MobileInputView mobileInputView;

    private void Start()
    {
      PlayerController.newWeaponEvent += ChangeWeaponIcon;
    }

    #region Mobile Controls
    public void Left(bool isPressed)
    {
      pressLeftMobile?.Invoke(isPressed);
    }

    public void Right(bool isPressed)
    {
      pressRightMobile?.Invoke(isPressed);
    }

    public void Shoot()
    {
      pressShootMobile?.Invoke();
    }
    #endregion

    private void ChangeWeaponIcon(Sprite weaponIcon)
    {
      mobileInputView.SetWeaponIcon(weaponIcon);
    }

    private void OnDisable()
    {
      PlayerController.newWeaponEvent -= ChangeWeaponIcon;
    }

  }

  [System.Serializable]
  public class MobileInputView
  {
    [SerializeField]
    Image weaponIcon;

    public void SetWeaponIcon(Sprite weaponSprite)
    {
      weaponIcon.sprite = weaponSprite;
    }
  }
}
