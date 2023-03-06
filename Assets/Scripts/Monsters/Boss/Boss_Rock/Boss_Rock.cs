using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Rock : MonoBehaviour
{
    List<float> skills_delay;
    List<float> skills_time_ready;
    Rigidbody2D rb;
    //UnityEvent<>
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skills_delay = new List<float>();
        skills_time_ready = new List<float>();
        skillsEvent = new List<UnityEvent<GameObject>>();
        SignUpSkill();
    }

    List<UnityEvent<GameObject>> skillsEvent;
    // Update is called once per frame
    //Ham update dung cho dieu kien check vi tri nguoi choi
    // neu khong check vi tri nguoi choi thi se khong co dieu kien tien quyet la dung skill nao... co the tao 1 list luu cac skill co the kich hoat sau do uu tien skill alone
    void Update()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x && isLeft)
            {
                Flip();
            }

            if (player.transform.position.x < transform.position.x && !isLeft)
            {
                Flip();
            }
            //check xem co alone skill dang run khong, neu co kiem tra xem no dung chua, neu roi thi check cac skill khac chay 
            if (aloneSkill != null)
            {
                if (aloneSkill.isSkillEnd())
                {
                    aloneSkill = null;
                }
                return;

            }
            else
            {
                for (int i = 0; i < skillsEvent.Count; i++)
                {
                    //co kha nang trigger khong va thoi gian hoi chieu het chua
                    if (skills[i].AbleToTrigger() && Time.time > skills_time_ready[i])
                    {
                        skills_time_ready[i] = skills_time_ready[i] + skills_delay[i];
                        if (!skills[i].AbleToTriggerWithOtherSkill())
                        {
                            aloneSkill = skills[i];
                            skillsEvent.ElementAt(i).Invoke(gameObject);
                            break;
                        }
                        else
                            skillsEvent.ElementAt(i).Invoke(gameObject);
                    }
                }
            }
        }

    }
    BaseSkillBoss aloneSkill;//skill chi dung xong cac skill khac moi duoc dung :)))
    BaseSkillBoss[] skills;

    // OPTIONAL : CHUA ADD DIEU KIEN LV TO USE SKILL

    void SignUpSkill()
    {
        skills = (BaseSkillBoss[])gameObject.GetComponents<BaseSkillBoss>();
        if (skills == null)
        {
            Debug.Log("no Skill");
            return;
        }
        for (int i = 0; i < skills.Length; i++)
        {
            skills_delay.Add(skills[i].CD_Skill());
            skills_time_ready.Add(skills[i].FirstTimeUse());
            UnityEvent<GameObject> ue = new UnityEvent<GameObject>();
            ue.AddListener(skills[i].RunSkill);
            this.skillsEvent.Add(ue);
        }
        skills.OrderByDescending<BaseSkillBoss, float>(s => s.CD_Skill());
    }
    bool isLeft = true;
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.Find("Head").transform.localScale;
        theScale.x *= -1;
        transform.Find("Head").transform.localScale = theScale;
        isLeft = !isLeft;
    }

    private float nextCollisionDamageTime;
    [SerializeField]
    private float DelayCollisionDamageTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&Time.time>nextCollisionDamageTime)
        {
            CharacterStatus cs = collision.GetComponent<CharacterStatus>();
            if (aloneSkill == null)
                cs.TakeDamage(GetComponent<BossStatus>().Atk);
            else
                cs.TakeDamage(aloneSkill.AtkSkill);
            nextCollisionDamageTime = Time.time + DelayCollisionDamageTime;

        }

    }

    //mo ta boss : o nhung cap do thap boss se chi ban ra it dan va di chuyen cham, dash it, len cap do cao boss se dash nhieu, ban dan rong hon nhieu hon ep nguoi choi phai di xa no ra
    // khi do boss se co the su dung dc jumpSkill gay chan dong 1 vung, len cap cao hon vung chan dong se rong ra va ThrowHand( ki nang ban tay truy duoi va dap)


}
