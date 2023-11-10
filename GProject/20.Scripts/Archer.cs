using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    PlayerM pm;
    Weapons weapon;

    public GameObject[] weapons;
    public Weapons equipWeapon;
    public float fireDelay;

    public bool isFireReady = true;

    private void Start()
    {
        pm = GetComponent<PlayerM>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (pm.fDown && isFireReady && !pm.isJump && !pm.isDodge)
        {
            equipWeapon.Use();
            if (equipWeapon.type == Weapons.Type.Sword || equipWeapon.type == Weapons.Type.Bow) // Base Attack
            {
                pm.anim.SetTrigger("doSwing");
            }
            fireDelay = 0;
        }

        if (pm.f2Down && isFireReady && !pm.isJump && !pm.isDodge)
        {
            equipWeapon.Use();
            if (equipWeapon.type == Weapons.Type.Sword || equipWeapon.type == Weapons.Type.Bow) // Skill
            {
                pm.anim.SetTrigger("doSwing2");
            }
            fireDelay = 0;
        }

        if (pm.kDown && isFireReady && !pm.isJump && !pm.isDodge)
        {
            equipWeapon.Use();
            equipWeapon.kick = true;
            if (equipWeapon.type == Weapons.Type.Sword || equipWeapon.type == Weapons.Type.Bow) // Kick
            {
                pm.anim.SetTrigger("doKick");
            }
            fireDelay = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Helmet:
                    pm.health += 5 * (int)item.value;
                    break;
                case Item.Type.Armor:
                    pm.health += 10 * (int)item.value;
                    break;
                case Item.Type.Glove:
                    weapon.rate += (float)0.5 * item.value;
                    break;
                case Item.Type.Shose:
                    pm.speed += (float)0.5 * item.value;
                    break;
                case Item.Type.Weapon:
                    weapon.damage += 50 * (int)item.value;
                    break;
                default:
                    break;
            }
        }
    }
}
