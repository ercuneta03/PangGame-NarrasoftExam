using UnityEngine;

namespace Game
{
  public interface IPooledObject
  {
    void OnObjectSpawn();
    void OnObjectDestroy();
  }
}
