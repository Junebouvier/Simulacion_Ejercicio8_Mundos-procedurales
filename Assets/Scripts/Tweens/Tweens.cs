using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tweens : MonoBehaviour
{
    private enum EasingMode
    {
        Standard,
        outBounce
    }

    [SerializeField] private EasingMode easingMode;
    
    [SerializeField] private Transform targetTransform;
    
    [Header("Tween related")]
    [SerializeField, Range(0, 1)] private float normalizedTime;
    [SerializeField] private float duration = 5;
    
    [SerializeField] Color initialColor;
    [SerializeField] Color finalColor;

    private float currentTime = 2.5f;
    private Vector3 initialPos;
    private Vector3 finalPos;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartTween();
    }
    private void Update()
    {
        normalizedTime = currentTime / duration;
        CallEasingMode();
        currentTime += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space)) StartTween();
    }

    private void StartTween()
    {
        currentTime = 0f;
        initialPos = transform.position;
        finalPos = targetTransform.position;
    }

    private void CallEasingMode()
    {
        if (easingMode == EasingMode.Standard)
        {
            transform.position = Vector3.Lerp(initialPos, finalPos, normalizedTime);
            spriteRenderer.color = Color.Lerp(initialColor, finalColor, normalizedTime);
        }
        
        else if (easingMode == EasingMode.outBounce)
        {
            transform.position = Vector3.Lerp(initialPos, finalPos, outBounce(normalizedTime));
            spriteRenderer.color = Color.Lerp(initialColor, finalColor, outBounce(normalizedTime));
        }
    }
    
    private float outBounce(float x){
        float div = 2.75f;
        float mult = 7.5625f;

        if (x < 1 / div)
        {
            return mult * x * x;
        }
        else if (x < 2 / div)
        {
            x -= 1.5f / div;
            return mult * x * x + 0.75f;
        }
        else if (x < 2.5 / div)
        {
            x -= 2.25f / div;
            return mult * x * x + 0.9375f;
        }
        else
        {
            x -= 2.625f / div;
            return mult * x * x + 0.984375f;
        }
    }
}
