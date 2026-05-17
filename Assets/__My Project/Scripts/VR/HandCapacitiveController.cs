using UnityEngine;
using UnityEngine.InputSystem;

public class HandCapacitiveController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty triggerValueAction;
    public InputActionProperty triggerTouchAction; // 食指感应

    public InputActionProperty gripValueAction;
    // grip按钮没有电容感应

    [Header("Animation Settings")]
    public Animator animator;
    public string triggerParamName = "Trigger";
    public string gripParamName = "Grip";

    [Range(0, 1)]
    public float touchBaseValue = 0.1f;//设置触摸button时的数值
    public float lerpSpeed = 15f;

    private float _currentTrigger;
    private float _currentGrip;

    private void OnEnable()
    {
        // 手动定义的 Action 必须 Enable
        triggerValueAction.action?.Enable();
        triggerTouchAction.action?.Enable();
        gripValueAction.action?.Enable();
    }

    void Update()
    {
        if (animator == null) return;

        // Trigger
        float tVal = triggerValueAction.action.ReadValue<float>();//获取trigger的value
        bool isTTouched = triggerTouchAction.action.IsPressed();//获取triggerButton是否按下（电容感应
        float targetT = isTTouched ? (touchBaseValue + tVal * (1f - touchBaseValue)) : 0f;//判断，没有touch就等于0；touch就等于0.1；按下按钮就等于value的值

        // Grip
        float gVal = gripValueAction.action.ReadValue<float>();

        // 平滑过渡
        _currentTrigger = Mathf.Lerp(_currentTrigger, targetT, Time.deltaTime * lerpSpeed);
        _currentGrip = Mathf.Lerp(_currentGrip, gVal, Time.deltaTime * lerpSpeed);

        animator.SetFloat(triggerParamName, _currentTrigger);
        animator.SetFloat(gripParamName, _currentGrip);
    }
}