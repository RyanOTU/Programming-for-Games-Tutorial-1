using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 1;

    [SerializeField] private float _lookSensitivity = 5;
    [SerializeField] public Camera playerCamera;
    [SerializeField] private Transform camFollowTarget;
    [SerializeField, Range(0, 180)] float viewAngleClamp = 40f;
    private Vector3 cameraRotation;

    [SerializeField] private Rigidbody projectile;
    [SerializeField] public Transform projectilePos;

    [SerializeField] private List<WeaponBase> storedWeapons;
    private WeaponBase weapon;
    [SerializeField] private TMP_Text weaponUI;
    //[SerializeField] private int _coinAmount = 0;

    private Vector2 _moveDir;
    private Vector2 rotate;
    [SerializeField] private EnemyController[] controlledEnemy;

    private Rigidbody rb;
    private bool isGrounded = false;
    private bool isAttacking = false;
    private bool isBurst = true;

    PlayerAction actions;
    public ParticleSystem gunParticles;

    void Start()
    {
        weapon = storedWeapons[0];
        rb = GetComponent<Rigidbody>();
        Controls.Init(this);
        actions = new PlayerAction();
        ScoreManager.scoreManager.UpdateAmmo(GetWeapon());
        gunParticles.Pause();
        weaponUI.text = "Weapon: " + storedWeapons[0].name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (_moveDir.y * Time.deltaTime * _moveSpeed), Space.Self);
        transform.Translate(Vector3.right * (_moveDir.x * Time.deltaTime * _moveSpeed), Space.Self);
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);
        //SetLook(actions.Game.Look.ReadValue<Vector2>());
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = storedWeapons[0];
            ScoreManager.scoreManager.UpdateAmmo(weapon);
            weaponUI.text = "Weapon: " + storedWeapons[0].name;
            isBurst = true;
        }
        if (storedWeapons[1] != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weapon = storedWeapons[1];
                ScoreManager.scoreManager.UpdateAmmo(weapon);
                weaponUI.text = "Weapon: " + storedWeapons[1].name;
                isBurst = false;
            }
        }
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
        isAttacking = !isAttacking;
        if (isAttacking)
        {
            weapon.StartShooting();
            if (isBurst) gunParticles.Play();
        }
        else
        {
            weapon.StopShooting();
            if (isBurst) gunParticles.Stop();
        }

        
       
        //Rigidbody rbBullet = Instantiate(projectile, projectilePos.position, projectilePos.rotation).GetComponent<Rigidbody>();
        //rbBullet.velocity = transform.TransformDirection(new Vector3(0, 0, 25));
    }
    public void Die()
    {

    }
    public void TakeDamage(float damageTaken)
    {

    }
    public WeaponBase GetWeapon()
    {
        return weapon;
    }
    public void AddGun(WeaponBase weapon)
    {
        weapon.gameObject.SetActive(true);
        storedWeapons.Add(weapon);
    }
    public void MoveTo(Ray camToWorldRay)
    {
        if (!Physics.Raycast(camToWorldRay, out RaycastHit hitObj, StaticUtility.MoveLayer)) return;

        foreach(EnemyController en in controlledEnemy)
        {
            en.MoveToTarget(hitObj.point);
        }
    }
}