using UnityEngine;

public class Bat : MonoBehaviour
{
  [SerializeField] private int hits = 8;
  [SerializeField] private float speed;

  [SerializeField] private Transform player;
  [SerializeField] private FlashSprite flashSprite;
  [SerializeField] private GameObject explosion;

  private Rigidbody2D rb;
  private Animator animator;
  
  private bool isDead = false;
  private void Awake() 
  {
    rb = GetComponent<Rigidbody2D>();  
    animator = GetComponent<Animator>();
  }

  private void FixedUpdate() 
  {
    if (!GameManager.GetInstance.IsGameover)
    {
      if (!isDead)
      {
        Vector3 playerPosRand = player.position + new Vector3(Random.Range(0.1f,0.3f), Random.Range(0.1f,0.3f));
        Vector3 direction = (playerPosRand - transform.position).normalized;
        rb.velocity = direction * speed;
      }else{
        rb.velocity = Vector2.zero;
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D other) 
  {
    if (isDead) return;

    if (other.gameObject.CompareTag("Player"))
    {
      Vector3 direction = (player.position - transform.position).normalized;
      player.GetComponent<PlayerController>().Knockback(direction);
      //Debug.DrawRay(player.position, direction * 5, Color.red, 3);
    } else if (other.gameObject.CompareTag("Bullet")) {
      flashSprite.Flash();

      hits -= 1;
      if (hits <= 0) 
      {
        isDead = true;
        rb.velocity = Vector2.zero;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
      }
    }
  }
}
