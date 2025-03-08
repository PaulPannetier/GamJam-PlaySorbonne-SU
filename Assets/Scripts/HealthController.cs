using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    [SerializeField] private bool isDead; 

    public void TakeDamage(int amout)
    {
        //coder des degats 
        currentLife -= amout;
        if (currentLife <= 0)
        {
            currentLife = 0;
            isDead = true;
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
        current_nb_hearts = 6;
        currentLife = current_nb_hearts * 2;
        UpdateUI();
    }
    private void UpdateUI()
    {
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

    
}
