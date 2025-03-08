using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    
    public void Reload()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
