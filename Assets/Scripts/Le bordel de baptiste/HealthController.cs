using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class HealthController : MonoBehaviour
{
    // Prefabs des coeurs
    [SerializeField] private GameObject heart_full;
    [SerializeField] private GameObject heart_half;
    [SerializeField] private GameObject heart_empty;

    //[SerializeField] private GameObject shield_heart;
    [SerializeField] private Transform heartLayout; // Contient dans unity la horizontallayoutgroup

    [SerializeField] private int maxhearts = 10;

    [SerializeField] private int current_nb_hearts;
    [SerializeField] private int currentLife; 

    //[SerializeField] private int shield;
    [SerializeField] private Transform gameOverPanel;
    [SerializeField] private bool isDead; 

    private Animator animator;

    public void TakeDamage(int amout)
    {
        //coder des degats 
        currentLife -= amout;
        if (currentLife <= 0)
        {
            currentLife = 0;
            isDead = true;
            animator.SetBool("isDead", true);
            SetUpGameOver();
        }

        UpdateUI();
    }

    public void AddHeart(int amount)
    {
        currentLife += amount;
        if (currentLife > current_nb_hearts * 2)
        {
            currentLife = current_nb_hearts * 2;
        }
        UpdateUI();
    }

    private void Start()
    {
        current_nb_hearts = 3;
        currentLife = current_nb_hearts * 2;
        UpdateUI();
        animator = GetComponent<Animator>();
        gameOverPanel.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if(heartLayout == null)
        {
            return;
        }

        // detruire les enfants dans heartlayout 
        foreach (Transform heart_child in heartLayout){
            Destroy(heart_child.gameObject);
        }

        // Définir le nombre de coeurs pleins et vides
        int fullHearts = currentLife / 2;
        int emptyHearts = current_nb_hearts - fullHearts - (currentLife % 2);

        // Create full hearts
        for (int i = 0; i < fullHearts; i++)
        {
            Instantiate(heart_full, heartLayout);
        }

        // Nombres de coeurs a moitie pleins
        if (currentLife % 2 == 1) {
            Instantiate(heart_half, heartLayout);
        }

        for(int i = 0; i < emptyHearts; i++){
            Instantiate(heart_empty, heartLayout);
        }
        
        //puis instancier des coeurs bleus pour chaque bouclier 
    }

    private void Update()
    {
        if (InputManager.GetKeyDown(InputKey.T))
        {
            TakeDamage(1);
        }
    }

    void SetUpGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.gameObject.SetActive(true);

    }
}
