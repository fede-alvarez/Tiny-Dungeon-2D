using UnityEngine;
using UnityEngine.Events;
public class TriggerZone : MonoBehaviour
{
  [SerializeField] private CameraController cam;
  public UnityEvent OnEnter;
  public UnityEvent OnExit;

  private void OnTriggerEnter2D(Collider2D other) 
  {
    if (other.CompareTag("Player"))
    {
      OnEnter?.Invoke();
      cam.MoveCamera(transform.position);
    }
  }

  private void OnTriggerExit2D(Collider2D other) 
  {
    if (other.CompareTag("Player"))
    {
      OnExit?.Invoke();
    }
  }
}
