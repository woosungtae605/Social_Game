using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts.Cameras
{
    public class CameraTargetMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private Vector2 edgeSize;
        
        [SerializeField] private Vector2 minPos;
        [SerializeField] private Vector2 maxPos;
        
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
            
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y), transform.position.z);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Vector3 center = new Vector3(
                (minPos.x + maxPos.x) * 0.5f, (minPos.y + maxPos.y) * 0.5f, transform.position.z);
            
            Vector3 size = new Vector3(maxPos.x - minPos.x, maxPos.y - minPos.y, 0f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}