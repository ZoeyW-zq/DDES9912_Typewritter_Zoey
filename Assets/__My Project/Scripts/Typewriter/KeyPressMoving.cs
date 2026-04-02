using UnityEngine;

public class KeyPressMoving : MonoBehaviour
{
    [SerializeField] float pressDistance = 0.1f;   
    [SerializeField] float speed = 0.1f;         

    Vector3 startPosition;
    Vector3 targetPosition;

    bool isPressing = false;
    bool isReturning = false;

    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = startPosition;
    }

    void Update()
    {
        if (isPressing)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.0001f)
            {
                isPressing = false;
                isReturning = true;
                targetPosition = startPosition;
            }
        }

        if (isReturning)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.0001f)
            {
                isReturning = false;
            }
        }
    }

    public void PressKey()
    {
        if (!isPressing && !isReturning)
        {
            targetPosition = startPosition + Vector3.down * pressDistance;
            isPressing = true;
        }
    }
}