using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private float maxHealth;

    private bool isDead;
    [SerializeField] private GameManagerScript gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameObject.SetActive(false);
            gameManager.gameOver();
            Debug.Log("Dead");
        }
    }
}
