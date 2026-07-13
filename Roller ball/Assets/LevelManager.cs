using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject objects;
    public float timer,interval;
    public int count = 0;
    public Transform ObjectParent;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval && count <= 2)
        {
            Instantiate(objects, transform.forward*25,Quaternion.identity,ObjectParent);
            timer = 0;
            count++;
        }
    }
 
}


