using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
    {
        public Slider healthBar;
        public float maxHealth = 100;
        public float currentHealth;

        public WaitForSeconds regenTick = new WaitForSeconds(0.3f);
        private Coroutine regen;

        public static HealthBar instance;

        public GameObject player;

    public GameObject respawn;

        void Awake()
        {
            instance = this;

        }

        void Start()
        {
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        void Update()
        {
            if(currentHealth <= 0)
        {
            Die();
        }
        }
        public void Damage()
        {
            TakeDamage(10);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log(transform.name + "takes" + damage + "damage");
            healthBar.value = currentHealth;
        }


        public IEnumerator RegenHealth()
        {
            yield return new WaitForSeconds(2);

            while (currentHealth < maxHealth)
            {
                currentHealth += maxHealth / 100;
                healthBar.value = currentHealth;
                yield return regenTick;
            }
            //regen = null;
        }

        public void RegenManager()
        {
            StartCoroutine(RegenHealth());
        }

        public void Die()
        {
        Application.Quit();           
            respawn.SetActive(true);
            //currentHealth = maxHealth;
            //player.transform.position = new Vector3(0.16f, 0f, 19.18f);
        }
    }