using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       //AI 엔진을 따로 써줘야 Nav가 나옴

public class Enemy1 : MonoBehaviour
{
    public enum Type {A,B,C,D};

    [Header("EnemyState")]
    public Type enemyType;
    public int maxHealth;
    public int curHealth;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public GameObject[] itemPrefab = new GameObject[3];
    public bool isDead = false;


    [Header("Player Chase")]
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public float chaseRange = 3.0f;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public Material mat;
    public NavMeshAgent nav;
    public Animator anim;

    BoxCollider box;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;    //material은 MeshRenderer컴포넌트에서 접근 가능
        anim = GetComponentInChildren<Animator>();
        target = FindObjectOfType<PlayerM>().transform;

        nav.isStopped = true;
        if (enemyType == Type.D)
            Invoke("ChaseStart", 2);
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    void ChaseStart()
    {
        isChase = true;
        nav.isStopped = false;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if (nav.enabled && enemyType == Type.D)
        {
            nav.SetDestination(target.position);            //도착할 목표 위치 지정 함수
        }

        if (isChase == false)
        {
            nav.isStopped = true;
        }
    }

    void FreezeVeloCity()
    {
        if (isChase) {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targerting()
    {
        if(!isDead && enemyType == Type.D)
        {
            float targetRadius = chaseRange / 2;
            float targetRange = chaseRange;

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = chaseRange / 2;
                    targetRange = chaseRange;
                    break;
                case Type.B:
                    targetRadius = chaseRange / 3;
                    targetRange = chaseRange * 4;
                    break;
                case Type.C:
                    targetRadius = chaseRange / 6;
                    targetRange = chaseRange * 8;
                    break;
            }

            RaycastHit[] raycastHits =
                Physics.SphereCastAll(transform.position,
                                      targetRadius,
                                      transform.forward,
                                      targetRange,
                                      LayerMask.GetMask("Player"));
            if (raycastHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void FixedUpdate()
    {
        Targerting();
        FreezeVeloCity();
    }

    public void DropItem()
    {
        int rand = Random.Range(0, 3);
        Vector3 spawnItem = new Vector3(transform.position.x, transform.position.y + 3.0f, transform.position.z);
        Instantiate(itemPrefab[rand], spawnItem, this.transform.rotation);
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;            //피가 0이면 흰색
        }
        else
        {
            box.enabled = false;
            mat.color = Color.gray;
            Debug.Log("Dead");
            isDead = true;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            DropItem();

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);            //죽으면넉백 추가
            Destroy(gameObject, 4);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChaseStart();
            Debug.Log("succes");
        }

        if (other.tag == "Weapon")
        {
            Weapons weapon = other.GetComponent<Weapons>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag == "Bullet")
        {
            ProjectileMoveScript bullet = other.GetComponent<ProjectileMoveScript>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec));

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isChase = false;
        }
    }
}
