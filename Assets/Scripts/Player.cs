using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 1;

    [SerializeField] private float _lookSensitivity = 5;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform camFollowTarget;
    [SerializeField, Range(0, 180)] float viewAngleClamp = 40f;
    private Vector3 cameraRotation;
    
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private Transform projectilePos;

    [SerializeField] private Weapon weapon;
    //[SerializeField] private int _coinAmount = 0;

    private Vector2 _moveDir;
    private Vector2 rotate;

    private Rigidbody rb;
    private bool isGrounded = false;
    private bool isAttacking = false;

    PlayerAction actions;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Controls.Init(this);
        actions = new PlayerAction();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (_moveDir.y * Time.deltaTime * _moveSpeed), Space.Self);
        transform.Translate(Vector3.right * (_moveDir.x * Time.deltaTime * _moveSpeed), Space.Self);
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);
        SetLook(actions.Game.Look.ReadValue<Vector2>());
    }
    public void SetMovementDirection(Vector3 direction)
    {
        _moveDir = direction;
    }

    public void Jump()
    {
        if (isGrounded) rb.velocity = new Vector3(rb.velocity.x, _jumpForce, rb.velocity.z);
    }
    public void SetLook(Vector2 direction)
    {
        rotate = direction;
        transform.rotation *= Quaternion.AngleAxis(direction.x * _lookSensitivity, Vector3.up);
        camFollowTarget.rotation *= Quaternion.AngleAxis(direction.y * -_lookSensitivity, Vector3.right);

        Vector3 angles = camFollowTarget.eulerAngles;
        float anglesX = angles.x;
        if (anglesX > 180 && anglesX < 360 - viewAngleClamp)
        {
            anglesX = 360 - viewAngleClamp;
        }
        else if (anglesX < 180 && anglesX > viewAngleClamp)
        {
            anglesX = viewAngleClamp;
        }
        camFollowTarget.localEulerAngles = new Vector3(anglesX, 0, 0);
    }
    public void Shoot()
    {
        //isAttacking = !isAttacking;
        //if (!isAttacking) weapon.StartAttack();
        //else weapon.EndAttack();

        Rigidbody rbBullet = Instantiate(projectile, projectilePos.position, projectilePos.rotation).GetComponent<Rigidbody>();
        rbBullet.velocity = transform.TransformDirection(new Vector3(0, 0, 25));
    }
    public void Die()
    {

    }
    public void TakeDamage(float damageTaken)
    {

    }
}