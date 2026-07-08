using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts.Cameras
{
    public class CameraWheel : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;

        [Header("Zoom")]
        [SerializeField] private float zoomStep = 1f;
        [SerializeField] private float minZoom = 3f;
        [SerializeField] private float maxZoom = 15f;

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            if (Mouse.current == null || cinemachineCamera == null)
                return;

            float scrollY = Mouse.current.scroll.ReadValue().y;

            if (Mathf.Abs(scrollY) < 0.01f)
                return;

            LensSettings lens = cinemachineCamera.Lens;
            
            if (scrollY > 0)
                lens.OrthographicSize -= zoomStep;
            else if (scrollY < 0)
                lens.OrthographicSize += zoomStep;

            lens.OrthographicSize = Mathf.Clamp(
                lens.OrthographicSize, minZoom, maxZoom);

            cinemachineCamera.Lens = lens;
        }
    }
}