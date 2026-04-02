using UnityEngine;

public class RotateAndBack : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float swingAngle = 116f;     //xAxis
    [SerializeField] float speed = 200f;     

    Quaternion startRotation;
    Quaternion targetRotation;

    bool isSwinging = false;
    bool isReturning = false;

    void Start()
    {
        startRotation = transform.localRotation;
        targetRotation = startRotation;
    }

    void Update()
    {
        if (isSwinging)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                speed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                isSwinging = false;
                isReturning = true;
                targetRotation = startRotation;
            }
        }

        if (isReturning)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                speed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                isReturning = false;
            }
        }
    }

    public void Strike()
    {
        if (!isSwinging && !isReturning)
        {
            targetRotation = startRotation * Quaternion.Euler(swingAngle, 0, 0);
            isSwinging = true;
        }
    }
}