using DG.Tweening;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float MoveSpeed;
    public bool XAxis, YAxis, ZAxis;
    public Transform parent;
    public int index;

    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,-3,transform.localPosition.z);
    }


    void Update()
    {
        if (transform.position.z <= -10)
        {
           transform.localPosition = new Vector3(transform.localPosition.x,-3,transform.localPosition.z+30);
            transform.DOLocalMoveY(0, 1);
            SpwanObjectsChild();
        }

       
        transform.localPosition = new Vector3(
            XAxis ? transform.localPosition.x + MoveSpeed * Time.deltaTime : transform.localPosition.x,
            YAxis ? transform.localPosition.y + MoveSpeed * Time.deltaTime : transform.localPosition.y,
            ZAxis ? transform.localPosition.z + MoveSpeed * Time.deltaTime : transform.localPosition.z
        );
    }

    public void SpwanObjectsChild()
    {
        var count = Random.Range(1,transform.childCount-1);
        index = Random.Range(0,transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (i <= count)
            {
                transform.GetChild(index).gameObject.SetActive(true);

                var Childindex = Random.Range(0,transform.GetChild(index).childCount);

                for (int j = 0; j < transform.GetChild(index).childCount; j++)
                {
                    transform.GetChild(index).GetChild(j).gameObject.SetActive(Childindex == j);
                }
            }
            else
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
                index = (index + 1) % transform.childCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2); 
        }
    }
   public void restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
