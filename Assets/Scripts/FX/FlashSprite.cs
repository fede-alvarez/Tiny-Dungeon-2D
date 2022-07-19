using UnityEngine;

public class FlashSprite : MonoBehaviour
{
  [SerializeField] private Sprite flashSprite;
  [SerializeField] private float flashDuration = 0.1f;

  private SpriteRenderer spriteRenderer;
  private Sprite startingSprite;

  private void Awake() 
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Start() 
  {
    startingSprite = spriteRenderer.sprite;
  }
  
  public void Flash()
  {
    spriteRenderer.sprite = flashSprite;
    Invoke("ResetFlash", flashDuration);
  }

  private void ResetFlash()
  {
    spriteRenderer.sprite = startingSprite;
  }
}
