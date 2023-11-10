using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       //AI ������ ���� ����� Nav�� ����

public class Enemy0 : MonoBehaviour
{
    
    public enum Type { A, B, C};

    [Header("EnemyState")]
    public Type enemyType;
    public int maxHealth;
    public int curHealth;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public Transform missilePortA;
    public GameObject missile;
    public GameObject[] itemPrefab;
    public bool isDead = false;
    public float defence;

    [Header("Player Chase")]
    public Transform target;
    public bool isChase;
    public bool isAttack = false;
    public float chaseRange = 3.0f; 

    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
    BoxCollider box;

    void Awake()
    {
        target = FindObjectOfType<PlayerM>().transform;
        rigid = GetComponentInChildren<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;    //material�� MeshRenderer������Ʈ���� ���� ����
        anim = GetComponentInChildren<Animator>();
        box = GetComponentInChildren<BoxCollider>();

        nav.isStopped = true;
        if (isChase == true)
        {
            Invoke("ChaseStart", 2);
        }
    }

    void ChaseStart()
    {
        isChase = true;
        nav.isStopped = false;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        if (nav.enabled && target != null)
        {
            nav.SetDestination(target.position);
        }

        if (isChase == false) {
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
        if (!isDead)
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

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position,
                                      targetRadius,
                                      transform.forward,
                                      targetRange,
                                      LayerMask.GetMask("Player"));
            if (rayHits.Length > 0 && !isAttack)
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

                yield return new WaitForSeconds(0.7f);
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
                GameObject instantBullet = Instantiate(missile, missilePortA.position, missilePortA.rotation);
                BossMissile bossMissileA = instantBullet.GetComponent<BossMissile>();
                bossMissileA.target = target;

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

        if(curHealth > 0)
        {
            mat.color = Color.white;            //�ǰ� 0�̸� ���
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
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);            //������˹� �߰�
            Destroy(gameObject, 4);      //4�ʵڿ� �����
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
            curHealth -= weapon.damage - (int)defence / weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;

            StartCoroutine(OnDamage(reactVec));
        }
        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isChase = false;
        }
    }
}
