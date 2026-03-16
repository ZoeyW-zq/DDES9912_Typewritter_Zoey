using UnityEngine;

public class Transformer : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    float speed;
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
        targetTransform.position += Vector3.left * distance;
    }
    public void MoveTo(Transform position)
    {
        targetTransform.position = position.position;
    }

    public void ScaleYAxis(float amount)
    {
        transform.localScale += Vector3.up * amount;
    }

    public void RotateTo(Transform rotation)
    {
        targetTransform.rotation = rotation.rotation;
    }
}
