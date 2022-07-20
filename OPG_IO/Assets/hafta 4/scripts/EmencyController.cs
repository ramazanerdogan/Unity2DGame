using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmencyController : MonoBehaviour
{
    [SerializeField] private GameObject SoulObj;

    private float maxHealth=100f;
    private float currentHealth=100f;//100*******************

    private Animator enemyAnimator;

    private bool isIdle=true;
    private bool isHurt=false;
    private bool isDead=false;

    

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth=maxHealth;
        isHurt=false;
        isIdle=true;
        isDead=false;

    }
    private void Aweke()
    {
            enemyAnimator=this.GetComponent<Animator>();
    }

    private void Damage(float[] attackDetails)
    {
        float damageValue=attackDetails[0];
        if(currentHealth - damageValue<0f )
        {
            if(isDead== false)
            {
            Debug.Log("enemy dies");
            isDead=true;
            enemyAnimator.SetBool("isDead",isDead);
            //Destroy(gameObject,3f);
           // Die();
            } 
        }
        else
        {
            currentHealth-=damageValue;
            Debug.Log("can"+currentHealth);
            isHurt=true;
            isIdle=false;
            enemyAnimator.SetBool("isHurt",isHurt);
            enemyAnimator.SetBool("isIdle",isIdle);

        }
        
    }
    private void FinisHurtAnimation()
    {
        isHurt=false;
        isIdle=true;
        enemyAnimator.SetBool("isHurt",isHurt);
        enemyAnimator.SetBool("isIdle",isIdle);
    }
    private void Die()
    {  
        //Instantiate Soul
        Instantiate(SoulObj, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}
