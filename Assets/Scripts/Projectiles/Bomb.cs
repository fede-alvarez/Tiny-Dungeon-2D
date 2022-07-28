using UnityEngine;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
  [SerializeField] private Transform explosion;

  private List<Transform> enemiesClose = new List<Transform>();
  private Rigidbody2D rb;
  private bool isExploding = false;

  private void Awake() 
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start() 
  {
    Invoke("Explode", 3);
  }

  public void Shoot()
  {
    rb.AddForce(transform.right * 6, ForceMode2D.Impulse);
  }

  private void OnCollisionEnter2D(Collision2D other) 
  {
    Explode();
  }

  private void OnTriggerEnter2D(Collider2D other) 
  {
    if (other.gameObject.CompareTag("Bat"))
      enemiesClose.Add(other.gameObject.transform);
  }

  private void OnTriggerExit2D(Collider2D other) 
  {
    if (other.gameObject.CompareTag("Bat"))
      enemiesClose.Remove(other.gameObject.transform);
  }

  private void Explode()
  {
    if (isExploding) return;
    isExploding = true;

    foreach(Transform enemy in enemiesClose)
    {
      Bat bat = enemy.GetComponent<Bat>();
      if (bat) bat.Damage();
    }
    Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(gameObject, 0.1f);
  }
}
