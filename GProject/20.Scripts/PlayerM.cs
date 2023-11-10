using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    [Header("Player State")]
    public float speed;
    public float plusSpeed;
    public int health;
    public int maxHealth;
    public int mp;
    public int maxMP;
    public float defence;
    public float plusDefence;
    public bool isDead;
    public int skillEnforceNum; // skill Enforce Count
    public float skillLevel; // Skill Level => Weapon Enforce

    [Header("Cam")]
    public Camera followCamera;

    float hAxis;
    float vAxis;


    [Header("Player KeySetting")]
    #region InputButton
    public bool fDown;
    public bool f2Down;
    public bool bDown;
    public bool kDown;
    public bool iDown;
    bool jDown;
    bool wDown;
    public bool supDown; // player - supDown = Test
    public bool isJump;
    public bool isDodge;
    public bool isBorder;
    #endregion

    [Header("Player Object")]
    public GameObject boostObject;

    Vector3 moveVec;
    Vector3 dodgeVec;

    public CapsuleCollider cap;
    public Rigidbody rb;
    public Animator anim;
    public ItemBox box;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();

        if (!isDead)
        {
            maxHealth = 100;
            health = maxHealth;
            maxMP = 100;
            speed = 5.0f;
            mp = maxMP;
            defence = 10.0f;
        }
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Boost();
        SkillLevelUp();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // W/D
        vAxis = Input.GetAxisRaw("Vertical"); // A/D
        wDown = Input.GetButton("Walk"); // Left Shift
        jDown = Input.GetButtonDown("Jump"); // SpaceBar
        fDown = Input.GetButtonDown("Fire1"); // Left Click
        f2Down = Input.GetButtonDown("Fire2"); // Q
        bDown = Input.GetButtonDown("Skill1"); // E
        kDown = Input.GetButtonDown("Kick"); // C
        iDown = Input.GetButton("Interaction"); // F

        supDown = Input.GetButtonDown("SkillLevelUp"); // player - supDown => Test / 1
    }

    #region Movement
    void Move()
    {
        moveVec = new Vector3(hAxis, 0f, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (!isBorder && !isDead)
        {
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        }

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);

        if (fDown || f2Down || kDown && !isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge && !isDead)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");

        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isDead)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f);
        }
    }

    void Boost()
    {
        if (bDown && moveVec == Vector3.zero && !isJump && !isDodge && !isDead)
        {
            anim.SetTrigger("doBoost");
            StartCoroutine(Boosting());
        }
    }

    IEnumerator Boosting()
    {
        yield return new WaitForSeconds(0.1f);
        boostObject.SetActive(true);
        float temp = speed;
        speed += 2.5f;

        yield return new WaitForSeconds(10.0f);
        boostObject.SetActive(false);
        speed = temp;

    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 3, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 3, LayerMask.GetMask("Wall"));
    }

    private void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    #endregion

    public void SkillLevelUp()
    {
        if (supDown && skillEnforceNum > 0 && skillLevel <= 3)
        {
            skillLevel++;
            skillEnforceNum--;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            MBullet enemydam = other.GetComponent<MBullet>();
            health -= enemydam.damage - (int)defence / enemydam.damage;

            if (health <= 0)
            {
                isDead = true;
                anim.SetTrigger("doDie");
                cap.enabled = false;
                rb.useGravity = false;
            }
     
        }

        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            Weapons weapon = other.GetComponent<Weapons>();
            switch (item.type)
            {
                case Item.Type.SkillEnforce:
                    skillEnforceNum += (int)item.value;
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
        }
    }

}
