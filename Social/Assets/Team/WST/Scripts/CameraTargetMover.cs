using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts
{
    public class CameraTargetMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private Vector2 edgeSize;

        private void Update()
        {
            MouseMove();
        }

        private void MouseMove()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            if (mousePos.x < edgeSize.x)
            {
                transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            }
            else if (mousePos.x > Screen.width - edgeSize.x)
            {
                transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            }

            if (mousePos.y < edgeSize.y)
            {
                transform.position -= new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            }
            else if (mousePos.y > Screen.height - edgeSize.y)
            {
                transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            }
        }
    }
}