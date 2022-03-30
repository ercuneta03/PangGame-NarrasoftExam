using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
  [RequireComponent(typeof(BoxCollider2D))]
  public class Fruit : MonoBehaviour, IPooledObject
  {
    public static event Action<Fruit> fruitValueOnClaim;

    [SerializeField]
    FruitModel[] fruitModels;

    [SerializeField]
    FruitView fruitView;

    private FruitModel fruit;

    private Vector3 velocity;
    private bool move;

    public int ScoreValue => fruit.scoreValue;

    private void Start()
    {
      Initialize();
    }

    private void Initialize()
    {
      fruit = fruitModels[UnityEngine.Random.Range(0, fruitModels.Length)];
      velocity = new Vector3(0, -fruit.fallSpeed);

      move = true;

      fruitView.SetFruitAsset(fruit.fruitAssets);
      fruitView.SetSortingOrder(3);
    }

    private void Update()
    {
      if (move)
        transform.position += (velocity * Time.deltaTime);
    }

    private void GiveScore()
    {
      fruitValueOnClaim?.Invoke(this);
      OnObjectDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.tag == "Player")
      {
        GiveScore();
      }

      if (collision.tag == "Ground")
      {
        move = false;
      }
    }


    #region IPooledObject Methods
    public void OnObjectSpawn()
    {
      Initialize();
    }

    public void OnObjectDestroy()
    {
      gameObject.SetActive(false);
    }
    #endregion
  }

  [System.Serializable]
  public class FruitView
  {
    [SerializeField]
    SpriteRenderer fruitRenderer;

    public void SetFruitAsset(FruitAssets assets)
    {
      fruitRenderer.sprite = assets.fruitSprite;
    }

    public void SetSortingOrder(int sortingOrder)
    {
      fruitRenderer.sortingOrder = sortingOrder;
    }
  }
}
