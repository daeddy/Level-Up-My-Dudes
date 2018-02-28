using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthBarImage;
    public int player = 0;
    public float health = 10f;
    public float flashDuration = 0.1f;

    private SpriteRenderer spriteRenderer;
    private float flashTimer = 0;
    private Color color;
    private float maxHealth;
    private AudioSource hitSound;

    // Use this for initialization
    protected virtual void Start()
    {
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitSound = GetComponent<AudioSource>();
        color = spriteRenderer.color;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (spriteRenderer.color == Color.white)
        {
            flashTimer += Time.deltaTime;

            if (flashTimer >= flashDuration)
                spriteRenderer.color = color;
        }
    }

    public virtual bool TakeDamage(int damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        hitSound.Play();
        spriteRenderer.color = Color.white;
        flashTimer = 0;

        bool isDestroyed = false;

        if (health <= 0)
        {
            Destroy(gameObject);
            isDestroyed = true;
        }
        float calcHealth = health / maxHealth; //if cur 80 / 100 = 0.8f
        SetHealthBar(calcHealth);
        return isDestroyed;
    }

    public void SetHealthBar(float myHealth)
    {
        healthBarImage.transform.localScale = new Vector3(myHealth,
                                                     healthBarImage.transform.localScale.y,
                                                     healthBarImage.transform.localScale.z);
    }

}
