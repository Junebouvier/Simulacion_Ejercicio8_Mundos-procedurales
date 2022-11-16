using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePosition = GetWorldMousePosition();
        LookAtMouseDirection(mousePosition);
    }

    private void LookAtMouseDirection(Vector3 mousePosition)
    {
        Vector3 myPosition = new Vector2(transform.position.x, transform.position.y);
        Vector3 direction = mousePosition - myPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) - Mathf.PI / 2f;
        RotateZ(angle);
    }

    private Vector4 GetWorldMousePosition()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector4 worldPos = camera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void RotateZ(float radians)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, radians * Mathf.Rad2Deg);
    }
}