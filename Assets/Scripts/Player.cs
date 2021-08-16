using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Transform target1;
    Transform target2;
    public GameObject item1, item2, item3, item4, bt_craft, magic;
    public GameObject inventoryObj, respawn_bt;
    public GameObject player;
    //public Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public Animator anim;
    float InputX;
    float InputZ;
    public Vector3 moveDir = Vector3.zero;
    Vector3 verticalVelocity;
    public float gravity = -9.81f;
    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    public ParticleSystem Eletric;

    public static Player instance;

    public bool canAttack = false;

    public float lookRadius = 10f;

    //public GameObject enemyPrefab;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Movement.performed += Movement;
        playerInputActions.Player.Run.performed += Run;
        playerInputActions.Player.Magic_1.performed += Magic_1;
        playerInputActions.Player.Magic_2.performed += Magic_2;
        playerInputActions.Player.Inventory.performed += inventory;
        playerInputActions.Player.CloseInv.performed += closeInventory;
        playerInputActions.Player.Craft.performed += Craft;
        playerInputActions.Player.Craft.performed += Respawn;

        instance = this;

        //target1 = EnemyAI.instance.transform;
        //target2 = EnemyAI2.instance.transform;
    }

    void Update()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        //float speed = 100f;
        //playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);

        verticalVelocity.y += gravity * Time.deltaTime;
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        controller.Move(verticalVelocity * Time.deltaTime);
        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("walk", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(item1.activeSelf && item2.activeSelf && item3.activeSelf && item4.activeSelf)
        {
            bt_craft.SetActive(true);
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        Vector2 inputVector = context.ReadValue<Vector2>();
        //float speed = 100f;
        //playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);

        verticalVelocity.y += gravity * Time.deltaTime;
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        controller.Move(verticalVelocity * Time.deltaTime);
        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("walk", true);
            //anim.SetBool("Idle", false);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    public void Run(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
             anim.SetBool("run", true);
             speed = 7;
        }
        else
        {
            anim.SetBool("run", false);
            speed = 4;
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if(context.performed)
        {
             //playerRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        
    }

    public void Magic_1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //anim.SetBool("magic1", true);
            
            if(canAttack)
            {
                float distance1 = Vector3.Distance(target1.position, transform.position);
                if (distance1 <= lookRadius)
                {
                    EnemyAI.instance.TakeDamage(10);
                    anim.SetTrigger("magic_1");
                }

                /*float distance2 = Vector3.Distance(target2.position, transform.position);
                if (distance2 <= lookRadius)
                {
                    EnemyAI2.instance.TakeDamage(10);
                    anim.SetTrigger("magic_1");
                }*/
            }
            
        }
        else
        {
            //anim.SetBool("magic1", false);
        }
    }

    public void Magic_2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //anim.SetBool("magic2", true);
            
            if (canAttack)
            {
                float distance1 = Vector3.Distance(target1.position, transform.position);
                if (distance1 <= lookRadius)
                {
                    EnemyAI.instance.TakeDamage(10);
                    anim.SetTrigger("magic_2");
                }

                /*float distance2 = Vector3.Distance(target2.position, transform.position);
                if (distance2 <= lookRadius)
                {
                    EnemyAI2.instance.TakeDamage(10);
                    anim.SetTrigger("magic_2");
                }*/

            }
        }
        else
        {
            //anim.SetBool("magic2", false);
        }
    }

    public void inventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryObj.SetActive(true);
            
        }
    }

    public void closeInventory(InputAction.CallbackContext context)
    {
        inventoryObj.SetActive(false);
        if (context.performed)
        {
            inventoryObj.SetActive(false);

        }
    }

    public void closeInv()
    {     
          inventoryObj.SetActive(false);
    }


    public void Craft(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            if(bt_craft.activeSelf)
            {
                canAttack = true;
                magic.SetActive(true);
                inventoryObj.SetActive(false);
            }
            
        }
    }

    public void Craftmagic()
    {

      if (bt_craft.activeSelf)
      {
           canAttack = true;
           magic.SetActive(true);
           inventoryObj.SetActive(false);
      }

        
    }

    public void Respawn(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(respawn_bt.activeSelf)
            {
            HealthBar.instance.currentHealth = HealthBar.instance.maxHealth;
            player.transform.position = new Vector3(0.16f, 0f, 19.18f);
            player.transform.position = new Vector3(0.16f, 0f, 19.18f);
            respawn_bt.SetActive(false);
            }
            
        }
    }

    public void Respawnbt()
    {
            HealthBar.instance.currentHealth = HealthBar.instance.maxHealth;
            player.transform.position = new Vector3(0.16f, 0f, 19.18f);
            player.transform.position = new Vector3(0.16f, 0f, 19.18f);
            respawn_bt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //canAttack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //canAttack = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
