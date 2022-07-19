using UnityEngine;
using System.Collections.Generic;
public class Room : MonoBehaviour
{
  [SerializeField] private List<GameObject> enemies;

  public void ActivateAll()
  {
    foreach(GameObject enemy in enemies)
    {
      if (enemy != null) enemy.SetActive(true);
    }
  }

  public void DeactivateAll()
  {
    foreach(GameObject enemy in enemies)
    {
      if (enemy != null) enemy.SetActive(false);
    }
  }
}
