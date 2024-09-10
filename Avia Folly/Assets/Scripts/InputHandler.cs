using Airplanes;
using Platforms;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Aircraft _airplane;

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            var hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Airplane"))
                    _airplane = hit.collider?.gameObject?.GetComponent<Aircraft>();
            }
            
            if (_airplane == null) return;

            _airplane.ChangeFlightPath(mousePos);                
        }
            
        if (Input.GetMouseButton(0))
        {
            _airplane?.DrawFlightPath(mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_airplane == null) return;

            _airplane?.StopDrawFlightPath();

            var hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
            Platform platform = null;

            foreach (var hit in hits)
            {

                if (hit.collider.CompareTag("Platform"))
                    platform = hit.collider?.gameObject.GetComponent<Platform>();
            }

            if (platform != null &&
                platform.ColorType == _airplane.ColorType &&
                platform.AircraftType == _airplane.AircraftType)
            {
                _airplane?.ReadyToLand();
            }
                
            else
            {
                _airplane?.NotReadyToLand();
            }

            _airplane = null;
        }
    }
}
