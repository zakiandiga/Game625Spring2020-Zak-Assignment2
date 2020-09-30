using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector = Vector2.zero;
    private Rigidbody2D rb;

    public float speed = 20;
    
    PlayerInput input;
    Animator anim;

    public static event Action<PlayerController> OnStayBig;
    public static event Action<PlayerController> OnExitBig;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    
    public void MoveInput(InputAction.CallbackContext con)
    {
        inputVector = con.ReadValue<Vector2>();            
        if (inputVector != Vector2.zero)
        {
            anim.SetFloat("Horizontal", inputVector.x);
            anim.SetFloat("Vertical", inputVector.y);
        }
        anim.SetFloat("Speed", inputVector.sqrMagnitude);        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == ("big"))
        {
            if (OnStayBig != null)
            {
                OnStayBig(this);
            }
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == ("big"))
        {
            if (OnExitBig != null)
            {
                OnExitBig(this);
            }
        }
    }    

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * speed * Time.deltaTime);
    }
}
