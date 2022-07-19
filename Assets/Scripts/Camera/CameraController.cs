using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private float cameraSpeed = 2.0f;
  private bool inTransition = false;
  private Vector3 moveTo;

  public void MoveCamera(Vector3 position)
  {
    moveTo = position;
    moveTo.z = transform.position.z;
    
    inTransition = true;
  }

  private void LateUpdate()
  {
    if (inTransition)
    {
      transform.position = Vector3.Lerp(transform.position, moveTo, Time.deltaTime * cameraSpeed);

      if (transform.position == moveTo)
        inTransition = false;
    }
  }
}
