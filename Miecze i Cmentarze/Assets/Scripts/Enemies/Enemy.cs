using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 15;
    public int coinsValue = 50;

    public float triggerLength = 1;
    public float chaseLength = 5;
    public float attackCooldown = 1.5f;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    public float enemyYSpeed = 1;
    public float enemyXSpeed = 1.5f;
    private string hurtTrigger = "Hurt";
    private string deathTrigger = "Death";
    private bool enemyIsAttacking;
    private bool enemyIsHurt;
    private bool alive = true;


    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[20];
    public Animator anim;
    public EnemyHealthBarBehaviour healthBar;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        anim = GetComponent<Animator>();
        healthBar.SetHealth(hitpoint, maxhitpoint);
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                enemyIsAttacking = true;
            }
            else enemyIsAttacking = false;

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
            {
                enemyIsHurt = true;
            }
            else enemyIsHurt = false;

            if ((Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) && !enemyIsAttacking && !enemyIsHurt)
            {
                if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                {
                    chasing = true;
                    if (!collidingWithPlayer) anim.SetBool("EnemyRun", true);
                }

                if (chasing)
                {
                    if (!collidingWithPlayer)
                    {
                        UpdateMotor((playerTransform.position - transform.position).normalized, enemyYSpeed, enemyXSpeed);
                    }
                }
                else
                {
                    //UpdateMotor(startingPosition - transform.position, enemyYSpeed, enemyXSpeed);
                }
            }
            else if ((!enemyIsAttacking && !enemyIsHurt) || !GameManager.instance.player.alive)
            {
                //UpdateMotor(startingPosition - transform.position, enemyYSpeed, enemyXSpeed);
                chasing = false;
                startingPosition = transform.position;
                anim.SetBool("EnemyRun", false);
            }

            collidingWithPlayer = false;
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i] == null)
                {
                    continue;
                }

                if (hits[i].tag == "Fighter" && hits[i].name == "PlayerHitbox")
                {
                    collidingWithPlayer = true;
                }

                hits[i] = null;
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dead"))
            {
                KillReward();
            }
        }
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (alive)
        {
            base.ReceiveDamage(dmg);
            healthBar.SetHealth(hitpoint, maxhitpoint);
            anim.SetTrigger(hurtTrigger);
        }
    }

    protected override void Death()
    {
        alive = false;
        anim.SetTrigger(deathTrigger);
    }

    protected void KillReward()
    {
        CheckQuestGoals();
        GameManager.instance.coins += coinsValue;
        GameManager.instance.experience += xpValue;
        if (GameManager.instance.experience >= GameManager.instance.xpTable[GameManager.instance.playerLevel - 1])
        {
            GameManager.instance.player.LevelUp();
        }
        GameManager.instance.ShowText("+" + xpValue + "xp", 10, Color.magenta, transform.position, Vector3.up * 40, 0.5f);
        GameManager.instance.player.xpBar.SetXpBar();
        Destroy(gameObject);
    }

    protected void CheckQuestGoals()
    {
        foreach (Quest quest in GameManager.instance.playerQuests)
        {
            if (quest.completed == false)
            {
                foreach (QuestGoal questGoal in quest.goals)
                {
                    if (questGoal.killGoal != null)
                    {
                        if (questGoal.killGoal.name + "(Clone)" == transform.name && questGoal.completed == false)
                        {
                            questGoal.currentAmount++;
                            if (questGoal.currentAmount >= questGoal.requiredAmount) questGoal.completed = true;
                            quest.CheckGoals();
                        }
                    }
                }
            }
            /*if (quest.completed == false && quest.currentGoal.killGoal != null) 
            {
                if(quest.currentGoal.killGoal.name + "(Clone)" == transform.name && quest.currentGoal.completed == false)
                {
                    quest.currentGoal.currentAmount++;
                    if (quest.currentGoal.currentAmount >= quest.currentGoal.requiredAmount) quest.currentGoal.completed = true;
                    quest.CheckGoals();
                }

            }*/
        }
    }
}
