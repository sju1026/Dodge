using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        SkillEnforce, Helmet, Armor, Glove, Shose ,Weapon
    };
    public Type type;
    public float value;

    Rigidbody rigid;
    SphereCollider spherCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spherCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 20f * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            spherCollider.enabled = false;
        }
    }
}
