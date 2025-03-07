using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TrucBidule : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartLayout;// doit avoir un horizontal layout 

    [SerializeField] private int maxLife = 10;
    [SerializeField] private int currentLife; 
    [SerializeField] private int shield;

    [SerializeField] private bool isDead; 

    public void TakeDamage(int amout)
    {
        //coder des degats 

        UpdateUI();
    }

    private void UpdateUI()
    {
        // detruire les enfants dans heartlayout 
        /*
        foreach (Transform child in transform)
        {
            Destroy(child);
        }
        */
        
        //en instancier le bon nombre de complet pour les paires de vies 

        // puis un demi pour la vie impaire

        //puis instancier des coeurs bleus pour chaque bouclier 
    }
}
