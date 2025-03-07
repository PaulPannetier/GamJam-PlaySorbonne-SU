using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHearts = 8;

    [SerializeField] private int HP = 6;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Image[] halfHearts;

    // Update is called once per frame
    public void UpdateHealth()
    {

        
        for(int heart_i = 0; heart_i < halfHearts.Length; heart_i++)
        {
            // Le nombre de coeurs entiers
            int hearts = HP / 2;
            // Le nombre de demi-coeurs
            int r = currentHealth % 2;

            if(heart_i < hearts)
            {
                
                halfHearts[i].enabled = false;
            }
            else
            {   

                hearts[i].enabled = false;
            }
        }
    }
}
