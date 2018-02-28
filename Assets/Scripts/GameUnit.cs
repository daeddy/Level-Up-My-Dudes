using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUnit : GameEntity
{
    public enum UnitStates
    {
        Idle, Move, Attack
    }

    public int speed = 1;
    public int damage = 1;

    public GameBase enemyBase1;
    public GameBase enemyBase2;
    public GameBase enemyBase3;
    public GameBase baseScript;
    public GameEntity target;
    public UnitStates state = UnitStates.Idle;
    public bool sent = false;
    public GameObject hat;
    public GameObject sword;
    public Sprite hat1;
    public Sprite hat2;
    public Sprite hat3;
    public Sprite hat4;
    public Sprite hat5;
    public Sprite sword1;
    public Sprite sword2;
    public Sprite sword3;
    public Sprite sword4;
    public Sprite sword5;

    private float attackTimer = 0;

    protected override void Start()
    {
        base.Start();

        //target = enemyBase1;

        switch (speed)
        {
            case 1:
                hat.GetComponent<SpriteRenderer>().sprite = hat1;
                break;
            case 2:
                hat.GetComponent<SpriteRenderer>().sprite = hat2;
                break;
            case 3:
                hat.GetComponent<SpriteRenderer>().sprite = hat3;
                break;
            case 4:
                hat.GetComponent<SpriteRenderer>().sprite = hat4;
                break;
            case 5:
                hat.GetComponent<SpriteRenderer>().sprite = hat5;
                break;
            default:
                break;
        }

        switch (damage)
        {
            case 1:
                sword.GetComponent<SpriteRenderer>().sprite = sword1;
                break;
            case 2:
                sword.GetComponent<SpriteRenderer>().sprite = sword2;
                break;
            case 3:
                sword.GetComponent<SpriteRenderer>().sprite = sword3;
                break;
            case 4:
                sword.GetComponent<SpriteRenderer>().sprite = sword4;
                break;
            case 5:
                sword.GetComponent<SpriteRenderer>().sprite = sword5;
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (sent && !target)
        {
            if (enemyBase1)
            {
                target = enemyBase1;
                state = UnitStates.Move;
            }
            else if (enemyBase2)
            {
                target = enemyBase2;
                state = UnitStates.Move;
            }
            else if (enemyBase3)
            {
                target = enemyBase3;
                state = UnitStates.Move;
            }
            else
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        else if (target && state == UnitStates.Move)
            Move();

        else if (state == UnitStates.Attack)
            Attack();
    }

    private void Move()
    {
        //Debug.Log("moving!");
        //if your target dies target base

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > 1.0f / speed)
        {
            attackTimer = 0;

            if (target)
            {
                bool isTargetDead = target.TakeDamage(damage);
                if (isTargetDead)
                {
                    baseScript.money += 5;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        GameEntity otherEntity = collision.collider.GetComponent<GameEntity>();

        if (otherEntity && otherEntity.player != player)
        {
            target = otherEntity;
            state = UnitStates.Attack;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameEntity otherEntity = collision.collider.GetComponent<GameEntity>();

        if (otherEntity && otherEntity.player != player)
        {
            target = otherEntity;
            state = UnitStates.Move;
        }
    }

    public void OnAgro(GameEntity otherEntity)
    {
        if (otherEntity.player != player && state != UnitStates.Attack)
        {
            target = otherEntity;
            state = UnitStates.Move;
        }
    }

    public override bool TakeDamage(int damage)
    {
        bool isDead = base.TakeDamage(damage);

        if (health <= 0)
            baseScript.money += 5;

        return isDead;
    }
}
