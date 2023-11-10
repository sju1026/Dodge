using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    
    public enum Type { Sword, Bow}

    [Header("Weapon State")]
    public Type type;
    public int damage;
    public int plusDamage;
    public float skillLevelUpDamage;
    public float rate;
    public float plusRate;
    public bool kick = false;
    public float skillCoolTime = 2.0f;
    public Image skillFillAmount;
    public Text skillCoolTimeText;
    bool isUseSkill = true;

    [Header("Weapon Components")]
    PlayerM player;

    public BoxCollider swordArea;
    public BoxCollider leftKickArea;
    public BoxCollider rightKickArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public GameObject bullet2;

    private void Start()
    {
        skillLevelUpDamage = damage;
        player = GetComponentInParent<PlayerM>();

        skillFillAmount.fillAmount = 0;
    }

    private void Update()
    {
        SkillLevelUp();
    }

    public void Use()
    {

        if (player.fDown && type == Type.Sword)
        {
            StopCoroutine(Swing());
            StartCoroutine(Swing());
        }
        else if (player.f2Down && type == Type.Sword && isUseSkill)
        {
            isUseSkill = false;

            skillCoolTimeText.text = "";

            StopCoroutine(StrongSwing());
            StartCoroutine(StrongSwing());

            StartCoroutine(ResetSkillCoroutine());
            StartCoroutine(CoolTimeCountCoroutine(skillCoolTime));

        }
        else if (player.fDown && type == Type.Bow)
        {
            StartCoroutine(Shot());
        }

        else if (player.f2Down && type == Type.Bow)
        {
            isUseSkill = false;

            skillCoolTimeText.text = "";

            StartCoroutine(StrongShot());

            StartCoroutine(ResetSkillCoroutine());
            StartCoroutine(CoolTimeCountCoroutine(skillCoolTime));
        }
        else if (kick)
        {
            StopCoroutine(Kick());
            StartCoroutine(Kick());
        }
    }

    IEnumerator ResetSkillCoroutine() //스킬 쿨타임
    {
        while (skillFillAmount.fillAmount < 1)
        {
            skillFillAmount.fillAmount += 1 * Time.smoothDeltaTime / skillCoolTime;

            yield return null;
        }

        isUseSkill = true;
    }

    IEnumerator CoolTimeCountCoroutine(float number) //스킬 쿨타임 텍스트 표시
    {
        if (number > 0)
        {
            number -= 1;

            skillCoolTimeText.text = number.ToString();

            yield return new WaitForSeconds(1f);
            StartCoroutine(CoolTimeCountCoroutine(number));
        }
        else
        {
            skillCoolTimeText.text = "Skill";
            skillFillAmount.fillAmount = 0;
            yield break;
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        damage = (int)skillLevelUpDamage;
        float temp = player.speed;
        player.speed = 0;
        swordArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(1.0f);
        swordArea.enabled = false;
        trailEffect.enabled = false;
        player.speed = temp;
    }

    IEnumerator StrongSwing()
    {
        yield return new WaitForSeconds(0.1f);
        float temp = player.speed;
        player.speed = 0;
        swordArea.enabled = true;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.5f);
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRb = instantBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bulletPos.forward * 50;
        yield return new WaitForSeconds(0.5f);
        swordArea.enabled = false;
        trailEffect.enabled = false;
        player.speed = temp;
    }

    

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.1f);
        float temp = player.speed;
        player.speed = 0;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRb = instantBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bulletPos.forward * 50;
        yield return new WaitForSeconds(1.0f);
        trailEffect.enabled = false;
        player.speed = temp;
        yield return null;
    }

    IEnumerator StrongShot()
    {
        yield return new WaitForSeconds(0.1f);
        float temp = player.speed;
        player.speed = 0;
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.3f);
        GameObject instantBullet = Instantiate(bullet2, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRb = instantBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bulletPos.forward * 50;
        yield return new WaitForSeconds(1.0f);
        trailEffect.enabled = false;
        player.speed = temp;
        yield return null;
    }

    IEnumerator Kick()
    {
        yield return new WaitForSeconds(0.1f);
        float temp = player.speed;
        player.speed = 0;
        leftKickArea.enabled = true;
        yield return new WaitForSeconds(0.8f);
        leftKickArea.enabled = false;
        rightKickArea.enabled = true;
        yield return new WaitForSeconds(0.8f);
        rightKickArea.enabled = false;
        player.speed = temp;
    }

    void SkillLevelUp()
    {
        // player - supDown = Test
        if(player.supDown && player.skillLevel > 0)
        {
            skillLevelUpDamage = (float)(damage * (1.0f + (player.skillLevel * 0.25)));
        }
    }
}
