using UnityEngine;

public class InvisiblePlayerFollower : MonoBehaviour
{
    public GameObject object1;
    //public GameObject object2;
    //public float distance1;

    void Start()
    {
        //distance1 = 0;
    }

    void Update()
    {
        object1 = GameObject.Find("Player");
        if (object1 != null)
        {
            //Debug.Log("Found you");
            transform.position = object1.transform.position;
            //distance1 = Vector3.Distance(object1.transform.position, object2.transform.position);
        }
        else
        {
            Debug.Log("Where are you?");
        }
    }
}
