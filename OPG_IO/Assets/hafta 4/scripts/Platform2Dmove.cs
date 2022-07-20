using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2Dmove : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private int currentTarget, incrementDir = 1;
    [SerializeField] private float moveSpeed = 2.5f;

   // [SerializeField] private List<GameObject> ObjectsOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = 0;
       // ObjectsOnPlatform = new List<GameObject>();
        foreach(GameObject target in targets)
        {
            if(target == null)
            {
                Debug.LogError("Platform target is null!");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlatformMove();
    }

    private void PlatformMove()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, 
                                                                     targets[currentTarget].transform.position,
                                                                     moveSpeed * Time.deltaTime);
        
        /*
        if(ObjectsOnPlatform.Capacity > 0)
        {
            foreach(GameObject ObjectOnPlatform in ObjectsOnPlatform)
            {
                ObjectOnPlatform.transform.position = Vector2.MoveTowards(ObjectOnPlatform.transform.position,
                                                                        targets[currentTarget].transform.position,
                                                                        moveSpeed * Time.deltaTime);
            }
        }
        */

        float distance = Vector3.Distance(this.transform.position, targets[currentTarget].transform.position);
        if(distance < 0.25f)
        {
            //currentTarget = (currentTarget+1)%(targets.Capacity);
            currentTarget = nextTarget();
        }
    }

    private int nextTarget()
    {
        int tempTarget = currentTarget;

        if(tempTarget + incrementDir == targets.Capacity)
        {
            incrementDir = -1;
        }
        else if(tempTarget + incrementDir == -1)
        {
            incrementDir = 1;
        }
        tempTarget += incrementDir;
        return tempTarget;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log(col.gameObject.name);
        //ObjectsOnPlatform.Add(col.gameObject);
        col.gameObject.transform.SetParent(this.transform);
    }

    void OnCollisionExit2D(Collision2D col)
    {
       // ObjectsOnPlatform.Remove(col.gameObject);
       col.gameObject.transform.SetParent(null);
    }

}
