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
    public float gravity = 100f;
    public float animationSpeed = 1f;
    public float forwardSpeed = 10f;
    public float turnSpeed = 1f;
    public float flySpeed = 11f;
    public float smoothingFactor = 0.1f; // between 0 and 1

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
        // round data from input controller
        _inputForward = Mathf.Round(input.Forward * 10f) / 10f;
        _inputTurn = Mathf.Round(input.Turn * 10f) / 10f;
        _inputFly = Mathf.Round(input.Fly * 10f) / 10f;

        // don't overwrite bool
        _inputClick = _inputClick | input.Click;
        isFlying = isFlying || _inputFly > 0.1f;
    }

    void FixedUpdate()
    {
        anim.SetFloat("vel_turn", _inputTurn);
        anim.SetFloat("vel_forward", _inputForward);
        anim.SetFloat("vel_fly", _inputFly);

        anim.speed = animationSpeed;

        if (_inputClick)
        {
            _inputClick = false;
            // Debug.Log("Clicked");
            // Click is used in Garden and Quest Managers
        }

        // move player
        Vector3 rawVelocity;
        Quaternion rotation;

        // check groundedness - not used rn but may be needed
        //bool isGrounded = IsGrounded || CheckGrounded(this.transform.position, 0.1f, 1f);

        // set rotation based on turn input
        rotation = Quaternion.Euler(0, _inputTurn * turnSpeed, 0);

        // set forward position based on forward input
        rawVelocity = transform.forward * _inputForward * forwardSpeed;

        // set height if flying
        if (isFlying)
        {
            isFlying = false;
            rbody.AddForce(Vector3.up * _inputFly * flySpeed, ForceMode.Acceleration);
        } else
        {
            rbody.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }

        // set velocity of rigidbody
        rbody.velocity = Vector3.Lerp(rbody.velocity, rawVelocity, smoothingFactor);

        // override position for height limit
        if (transform.position.y >= heightLimit)
        {
            rbody.MovePosition(new Vector3(transform.position.x, heightLimit, transform.position.z));
        }

        rbody.MoveRotation(rbody.rotation * rotation);
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
