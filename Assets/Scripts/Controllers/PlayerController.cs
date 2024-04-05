using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

// Author: Sakshi Kakkad (entire script)
// Sakshi: Fixed bugs from PlayerControllerOLD

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    // SET/CHANGE IN INSPECTOR
    public float heightLimit = 100f; // this value can't be lower than the terrain y height
    public float animationSpeed = 1f;
    public float forwardSpeed = 15f;
    public float turnSpeed = 15f;
    public float flySpeed = 5f;

    // components
    private Animator anim;
    private Rigidbody rbody;
    private InputController input;

    // inputs
    bool _inputClick = false;
    float _inputForward = 0f;
    float _inputTurn = 0f;
    float _inputFly = 0f;

    // helper
    private int groundContacts = 0;
    private bool isFlying = false;

    public bool IsGrounded
    {
        get
        {
            return groundContacts > 0;
        }
    }

    // Check for required components
    void Awake()
    {

        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("No Animator :(");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("No Rigid Body :(");

        input = GetComponent<InputController>();
        if (input == null)
            Debug.Log("No InputController :(");
    }

    // Initialization stuff (nothing atm)
    void Start()
    { }

    // set inputs in Update
    void Update()
    {
        _inputForward = input.Forward;
        _inputTurn = input.Turn;
        _inputFly = input.Fly;

        // don't overwrite bool
        _inputClick = _inputClick | input.Click;
        isFlying = isFlying || _inputFly > 0.1f;
    }

    void FixedUpdate()
    {
        anim.SetFloat("vel_turn", _inputTurn);
        anim.SetFloat("vel_forward", _inputForward);
        anim.SetBool("isFlying", isFlying);

        anim.speed = animationSpeed;

        if (_inputClick)
        {
            _inputClick = false;
            // Debug.Log("Clicked");
            // Click is used in Garden and Quest Managers
        }

        // move player
        Vector3 velocity;
        Quaternion rotation;

        // check groundedness - not used rn but may be needed
        //bool isGrounded = IsGrounded || CheckGrounded(this.transform.position, 0.1f, 1f);

        // set rotation based on turn input
        rotation = Quaternion.Euler(0, _inputTurn * turnSpeed * Time.deltaTime, 0);

        // set forward position based on forward input
        //velocity = rbody.position + (this.transform.forward * _inputForward * Time.deltaTime * forwardSpeed);
        velocity = this.transform.forward * _inputForward * forwardSpeed * Time.deltaTime;

        // set height if flying
        if (isFlying)
        {
            isFlying = false;
            velocity += Vector3.up * _inputFly * flySpeed * Time.deltaTime;
            rbody.useGravity = false;
        } else
        {
            rbody.useGravity = true;
        }
        rbody.velocity = velocity;
        rbody.MoveRotation(rbody.rotation * rotation);
        Debug.Log("velocity: " + rbody.velocity.magnitude);
    }

    // physics callback
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Ground")
        {
            ++groundContacts;

            // trigger event here for audio
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Ground")
        {
            --groundContacts;
        }
    }

    // in case OnCollisionEnter() fails from uneven ground
    private bool CheckGrounded(Vector3 charPos, 
        float rayDepth, // how far down from charPos will we look for ground?
        float rayOriginOffset // fudge factor up away from charPos for ray origin
        )
    {
        bool grounded = false;

        float totalRayLen = rayOriginOffset + rayDepth;
        Ray ray = new Ray(charPos + Vector3.up * rayOriginOffset, Vector3.down);
        int layerMask = 1 << LayerMask.NameToLayer("Default");
        RaycastHit[] hits = Physics.RaycastAll(ray, totalRayLen, layerMask);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                grounded = true;
                break; // we found ground we dip
            }
        }

        return grounded;
    }
}
