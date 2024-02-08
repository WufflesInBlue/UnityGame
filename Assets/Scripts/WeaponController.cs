using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool grounded;
    public GameObject Sword;
    public float playerHeight;
    public LayerMask whatIsGround;
    public Rigidbody rb;
    public Transform orientation;
    public bool CanAttack = true;
    public KeyCode smashKey = KeyCode.G;
    public KeyCode SlashUpKey = KeyCode.U;
    public float AttackDashForce;
    public float SmashDownForce;
    public float SlashUpForce;
    public float AttackCooldown = 1.0f;
    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }
        if (Input.GetKeyDown(SlashUpKey))
        {
            if (CanAttack)
            {
                SlashUp();
                
            }
        }
        if (Input.GetKeyDown(smashKey))
        {
            if (CanAttack)
            {
                SwordSmash();
                
            }
        }
        
        
    }

    public void SlashUp()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("UpSlash");
        rb.AddForce(orientation.up * SlashUpForce, ForceMode.Impulse);
        StartCoroutine(ResetAttackCooldown());
    }
    public void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        rb.AddForce(orientation.forward * AttackDashForce, ForceMode.Impulse);
        StartCoroutine(ResetAttackCooldown());
    }
public void SwordSmash()
{
    CanAttack = false;
    Animator anim = Sword.GetComponent<Animator>();
    anim.SetTrigger("Smash");

    // Stop all horizontal movement
    rb.velocity = new Vector3(0f, 0f, 0f);

    // Apply a downward force
    rb.AddForce(Vector3.down * SmashDownForce, ForceMode.Impulse);

    StartCoroutine(ResetAttackCooldown());
}

private void OnCollisionEnter(Collision collision)
{
    // Check if the collision is with the ground
    if (collision.gameObject.CompareTag("Ground"))
    {
        // Run the "boom" effect
        StartCoroutine(BoomEffect());
    }
}

private IEnumerator BoomEffect()
{
    // Play the "boom" effect here
    Debug.Log("Boom effect!");
    
    // Add any additional visual or audio effects here
    
    yield return null;
}

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}
