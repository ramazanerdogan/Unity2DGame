using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberSc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position=new Vector3(this.transform.position.x+1f, this.transform.position.y+2f, 0f);
        //Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ShowDamage(float value)
    {
        this.GetComponent<TMP_Text>().text=value.ToString();

         if(value>0f)
         {
         this.GetComponent<TMP_Text>().color=Color.red;
         }
         else
         {
            this.GetComponent<TMP_Text>().color=Color.green;
         }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
