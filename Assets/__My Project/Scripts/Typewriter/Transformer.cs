using System.Collections;
using UnityEngine;

public class Transformer : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    float speed;
    public float yScaleLimit = 5.0f;
    bool movable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetTransform.rotation, speed * Time.deltaTime);
    }

    public void MoveUp(float distance)
    {
        targetTransform.position += Vector3.up * distance;
    }

    public void MoveLeft(float distance)
    {
        if (movable)
        {
            StartCoroutine(StrikeDelay(distance));
        }
    }
    IEnumerator StrikeDelay(float distance)
    {
        yield return new WaitForSeconds(0.3f);
        targetTransform.position += Vector3.left * distance;
    }
    public void MoveTo(Transform position)
    {
        targetTransform.position = position.position;
        enableMoveLeft();
    }

    public void ScaleYAxis(float amount)
    {
        if (transform.localScale.y < yScaleLimit)
        {

            transform.localScale += Vector3.up * amount;
        }
    }

    public void RotateTo(Transform rotation)
    {
        targetTransform.rotation = rotation.rotation;
    }

    public void disableMoveLeft()
    {
        movable = false;
    }

    public void enableMoveLeft()
    {
        movable = true;
    }
}
