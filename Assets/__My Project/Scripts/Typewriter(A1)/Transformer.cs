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
    

    // Update is called once per frame
    void Update()
    {
        if(targetTransform)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetTransform.rotation, speed * Time.deltaTime);
        }
        else
        {
            if (transform.parent.Find("targetPosition"))
            {
                targetTransform = transform.parent.Find("targetPosition");
            }
        }

            
    }

    public void MoveUp(float distance)
    {
        targetTransform.position += Vector3.up * distance;
    }

    private Coroutine moveRoutine;
    public void MoveLeft(float distance)
    {
        if (movable&&moveRoutine==null)
        {
            moveRoutine = StartCoroutine(StrikeDelay(distance));
        }
    }
    IEnumerator StrikeDelay(float distance)
    {
        yield return new WaitForSeconds(0.3f);
        targetTransform.position += Vector3.left * distance;
        moveRoutine = null;
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
