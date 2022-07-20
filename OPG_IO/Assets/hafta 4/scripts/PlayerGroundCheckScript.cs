using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheckScript : MonoBehaviour
{
    private bool isOnGround;
    private int enteredContactCount=0;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        isOnGround = true;
        enteredContactCount++;
       
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        enteredContactCount--;
        isOnGround = false;
        if(enteredContactCount==0)
        {
            isOnGround=false;
        }
    
        
    }

    public bool getIsOnGroundFlag()
    {
        return this.isOnGround;
    }

}