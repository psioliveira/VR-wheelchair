using System;
using UnityEngine;
using VRTK.Controllables;

public class PlayerMovement : MonoBehaviour
{

    public VRTK_BaseControllable controllable;
    public GameObject platform;
    public GameObject pivotPoint;

    private const float MAX_FORWARD_ACCELERATION = 28.0f;
    private const float GRAVITY_ACCELERATION = 8.0f;

    private const float MOUSE_ANGULAR_VELOCITY_FACTOR = 5.0f;
    private const float MAX_ANGULAR_VELOCITY = 60.0f;

    private const float MAX_FORWARD_VELOCITY = 8.0f;
    private const float MAX_BACKWARD_VELOCITY = -60.0f;

    private const float MAX_FALL_VELOCITY = -18f;

    private const float WALK_VELOCITY_FACTOR = 1.0f;

    private AngleConverter angleConv;
    private Transform player;
    private Transform pivot;

    private Vector3 _acceleration;
    private Vector3 _velocity;

    private float _velocityFactor;

    [SerializeField]
    private float radCheck = 2f;
    [SerializeField]
    private float ofsetGizmo = 1f;
    [SerializeField]
    private string layer;

    public float rotateSpeed;

    //test stuff
    protected Rigidbody rb;
    protected Collider coll;

    public float timer;




    void Start()
    {

        pivot = GameObject.Find("pivot").GetComponent<Transform>();
        angleConv = new AngleConverter();
        _acceleration = Vector3.zero;
        _velocity = Vector3.zero;
        _velocityFactor = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
    internal void ClassUpdate()
    {
        UpdateKeyRotation();
    }

    internal void ClassFixedUpdate()
    {

        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();
    }



    private void UpdateKeyRotation()
    {

        if (Input.GetAxis("Forward") != 0 || Input.GetAxis("Strafe") != 0)
        {
            Vector2 axis = new Vector2(Input.GetAxis("Forward"), Input.GetAxis("Strafe"));
            float angle = Mathf.Round(angleConv.AngleTranslate(axis));

            Quaternion temp = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotateSpeed);
        }

    }

    /// <summary>
    /// Specializes in changing acceleration values.
    /// </summary>
    private void UpdateAcceleration()
    {
        // REWRITE THE JUMP PART OF THIS SCRIPT LATER PLEASE.

        {
            if (!IsOnGround())
                _acceleration.y -= GRAVITY_ACCELERATION;
        }

    }

    /// <summary>
    /// Specializes in updating velocity values(it does touch a little bit the
    /// acceleration values, needs refactoring).
    /// </summary>
    private void UpdateVelocity()
    {
        _velocity += _acceleration * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (_acceleration.z < 0)
                _acceleration.z = +MAX_FORWARD_ACCELERATION * _velocityFactor;
            else
            {
                _acceleration.z += MAX_FORWARD_ACCELERATION * _velocityFactor;
            }
            _velocity += _acceleration * Time.deltaTime;
        }

        else
        {
            _acceleration.z += MAX_FORWARD_ACCELERATION * _velocityFactor;
            _velocity += _acceleration * Time.fixedDeltaTime;
            _velocity.z = 0f;
        }
        _velocity.z = Mathf.Clamp(_velocity.z, MAX_BACKWARD_VELOCITY, MAX_FORWARD_VELOCITY * _velocityFactor);
        _velocity.y = Mathf.Clamp(_velocity.y, MAX_FALL_VELOCITY, 100);
    }

    private void UpdatePosition()
    {
        Vector3 motion = _velocity * Time.deltaTime;
    }



    protected virtual void ValueChanged(object sender, ControllableEventArgs e)
    {
        if (platform != null)
        {
            platform.transform.localPosition = Vector3.forward * e.value;


            Debug.Log("e normalized  " + e.value);
            Debug.Log("e normalized  " + e.normalizedValue);
        }
    }


    internal bool IsOnGround()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - ofsetGizmo, transform.position.z);
        Collider[] col = Physics.OverlapSphere(pos, radCheck, LayerMask.GetMask(layer));

        return (col.Length > 0 && col != null);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - ofsetGizmo, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pos, radCheck);
    }

}


