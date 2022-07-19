using UnityEngine;

public class Bullet : MonoBehaviour
{

  private Rigidbody2D rb;

  private void Awake() 
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start() 
  {
    Destroy(gameObject, 3);
  }

  public void Shoot(Vector3 direction)
  {
    rb.AddForce(direction * 12, ForceMode2D.Impulse);
  }

  private void OnCollisionEnter2D(Collision2D other) 
  {
    Destroy(gameObject);
  }
}
