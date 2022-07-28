using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private Transform arrowTransform;
  [SerializeField] private LivesContainer livesContainer;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private Transform projectilePrefab;
  [SerializeField] private Transform bombPrefab;
  [SerializeField] private float movementSpeed = 2.0f;
  [SerializeField] private float fireRate = 0.2f;
  [SerializeField] private float invulnerableTime = 0.4f;
  [SerializeField] private FlashSprite flashSprite;

  private Rigidbody2D rb;
  private Vector2 inputDirection;
  private Vector2 secondStickDirection;

  private Animator animator;
  private bool isKnocked = false;
  private bool isBombPressed = false;
  private Vector2 knockDirection;

  private float nextFire;
  private float invulnerableCounter;

  private bool godMode = false;
  private bool isDead = false;

  private void Awake() 
  {
    rb = GetComponent<Rigidbody2D>();  
    animator = GetComponent<Animator>();
  }

  private void Update() 
  {
    if (isDead) return;

    inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    secondStickDirection = new Vector2(Input.GetAxisRaw("Horizontal R Stick"), Input.GetAxisRaw("Vertical R Stick"));

    isBombPressed = Input.GetKey(KeyCode.Space);
    arrowTransform.gameObject.SetActive(isBombPressed);

    if (Input.GetKeyUp(KeyCode.Space))
    {
        Transform go = Instantiate(bombPrefab, transform.position + new Vector3(0,0.5f), Quaternion.identity);
        go.right = inputDirection.normalized;
        go.GetComponent<Bomb>().Shoot();
    }

    if (inputDirection.magnitude > 0)
      arrowTransform.right = inputDirection.normalized;

    if (inputDirection.x > 0)
      spriteRenderer.flipX = false;
    else if (inputDirection.x < 0)
      spriteRenderer.flipX = true;

    animator.SetFloat("Velocity", rb.velocity.magnitude);

    if (secondStickDirection.x != 0 || secondStickDirection.y != 0)
      Shoot();

    if (godMode) 
    {
      invulnerableCounter += Time.deltaTime;

      if (invulnerableCounter >= invulnerableTime)
      {
        godMode = false;
        invulnerableCounter = 0;
        spriteRenderer.enabled = true;
      }else{
        spriteRenderer.enabled = !spriteRenderer.enabled;
      }
    }
  }

  private void Shoot()
  {
      if (Time.time > nextFire)
      {
          nextFire = Time.time + fireRate;

          Transform projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0,0.5f), Quaternion.identity);
          if (projectile)
          {
              projectile.right = secondStickDirection.normalized;
              projectile.GetComponent<Bullet>().Shoot(secondStickDirection.normalized);
          }
      }
  }

  private void FixedUpdate() 
  {
    rb.velocity = inputDirection.normalized * movementSpeed;

    if (isKnocked)
    {
      rb.AddForce(knockDirection * 5, ForceMode2D.Impulse);
    }
  }

  public void Knockback(Vector2 direction)
  {
    if (!godMode)
    {
      godMode = true;
      flashSprite.Flash();

      livesContainer.Hit();

      if (livesContainer.IsDead())
      {
        isDead = true;
        GameManager.GetInstance.OnDead();
      }else{
        isKnocked = true;
        knockDirection = direction;
        Invoke("ResetKnockback", 0.3f);
      }
    }
  }

  private void ResetKnockback()
  {
    CancelInvoke();
    isKnocked = false;
  }
}
