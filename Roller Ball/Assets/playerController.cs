using DG.Tweening;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -1.9f)
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime , transform.position.y, transform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 1.9f)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    } 
}