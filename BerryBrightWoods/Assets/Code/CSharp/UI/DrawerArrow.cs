using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DrawerArrow : MonoBehaviour
{
    [SerializeField] AnimationCurve inCurve = null;
    [SerializeField] AnimationCurve outCurve = null;
    [SerializeField] float lerpSpeed = 1.0f;
    [Header("References")]
    [SerializeField] GameObject background = null;
    [SerializeField] Transform bgLerpPos = null;

    Vector2 _originalPos = new Vector2();
    bool _opening = false;
    bool shouldOpen = false;
    float _x = 0.0f;
    private void Start()
    {
        _originalPos = background.transform.localPosition;
    }

    //maybe revise this code
    private void Update()
    {
        AnimationCurve currentCurve = null;
        if (shouldOpen)
        {
            _x += Time.deltaTime * lerpSpeed;
            currentCurve = inCurve;
        }
        else
        {
            _x -= Time.deltaTime * lerpSpeed * 1.2f;
            currentCurve = outCurve;
        }

        _x = Mathf.Clamp(_x, 0.0f, 1.0f);
        background.transform.localPosition = Vector3.Lerp(_originalPos, bgLerpPos.localPosition, currentCurve.Evaluate(_x));
        background.transform.localPosition = new Vector3(_originalPos.x, background.transform.localPosition.y, background.transform.localPosition.z);
    }

    public bool IsDrawerOpen()
    {
        return _opening;
    }

    public void SetShouldOpen(bool yn)
    {
        shouldOpen = yn;
    }

    public void ToggleShouldOpen()
    {
        shouldOpen = !shouldOpen;
    }

   

}
