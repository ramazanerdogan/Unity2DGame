using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombobat : MonoBehaviour
{
     [SerializeField] private GameObject DamageNumberObj;
    [SerializeField] private bool canAttack;
    [SerializeField] private float attackTimer;

    [SerializeField] private float attack1Radius, attack1Damage;
    [SerializeField] private float attack2Radius, attack2Damage;
    [SerializeField] private float attack3Radius, attack3Damage;
    
    [SerializeField] private Transform attack1Point, attack2Point, attack3Point;

    [SerializeField] LayerMask isDamageable;

    private float[] attackDetails=new float[3];
  
    private float lastInputTime;

    private Animator combatAnimator;

    [SerializeField] private bool isAttacking, gotAttack1Input, Attack1 , Attack2 , Attack3; 

    private void Awake()
    {
        combatAnimator=this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        combatAnimator = this.GetComponent<Animator>();
        canAttack = true;
        combatAnimator.SetBool("canAttack", canAttack);
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackInput();
        CheckAttack();
    }

    private void CheckAttackInput()
    {
        if(Input.GetButtonDown("Fire1") && canAttack)
        {
        
            gotAttack1Input = true;
            lastInputTime = Time.time;
        }
    }
    
    private void CheckAttack()
    {
        if (gotAttack1Input == true && (isAttacking == false))
        {
            isAttacking = true;
            Attack1=true;
            combatAnimator.SetBool("attack1", true);
            combatAnimator.SetBool("isAttacking", isAttacking);
            gotAttack1Input = false;
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        Attack1=false;
        combatAnimator.SetBool("isAttacking", isAttacking);
        combatAnimator.SetBool("attack1", false);
    }
    private void CheckAttack1HitBox()
    {
       Collider2D[] hitObjects=Physics2D.OverlapCircleAll(attack1Point.transform.position,attack1Radius,isDamageable);

       
        attackDetails[0]=attack1Damage;
        attackDetails[1]=this.transform.position.x;
        attackDetails[2]=this.transform.position.y;
       foreach(Collider2D collider in hitObjects )
       {
        //collider.gameObject.SendMessage("Damage",attackDetails);
         void NewMethod(Collider2D collider) => collider.gameObject.SendMessage("Damage", attackDetails);
        GameObject damageNumberClone = Instantiate(DamageNumberObj,
                                                  new Vector3(attack1Point.transform.position.x, attack1Point.transform.position.y+2f, attack1Point.transform.position.z),
                                                  Quaternion.identity);
        damageNumberClone.SendMessage("ShowDamage",attack1Damage);
       }
       
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(attack1Point.position, attack1Radius);
        // Gizmos.DrawWireSphere(attack2Point.position, attack2Radius);
        // Gizmos.DrawWireSphere(attack3Point.position, attack3Radius);
    }
}
