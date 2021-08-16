using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    public NavMeshAgent agent;
    public static EnemyAI instance;
    public float lookRadius = 10f;

    public float maxHealth = 100;

    public float currenthealth;
    public int damage = 25;

    public bool IsAlive = true;

    public Animator anim;
    private GameObject player;

    public bool CanWalk = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        target = Player.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAlive)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            CanWalk = true;
            if(distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        } 
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive == true)
        {
            anim.SetTrigger("hit");

            currenthealth -= damage;

            Debug.Log("O dano recebido foi de: " + damage);

            if (currenthealth <= 0)
            {
                IsAlive = false;
                Die();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "DamageArea")
        {
            //DamagePlayer();
            anim.SetTrigger("Attack_1");
            //Debug.Log("AAAAAAAaaa");
            anim.SetBool("Walk", false);
        }
    }

    public void DamagePlayer()
    {
        //HealthBar.instance.TakeDamage(5);
        HealthBar.instance.TakeDamage(20);
        //Debug.Log("AAAAAAAaaa");
    }

    void Die()
    {
        IsAlive = false;
        Debug.Log("Enemy Died");

        //Die anim
        anim.SetTrigger("die");
        anim.SetBool("hit", false);

        //Disable Enemy
        //GetComponent<CapsuleCollider>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
