using UnityEngine;

public class LoadLevel2 : MonoBehaviour
{
    void Update()
    {
        if(InputManager.GetKeyDown(InputKey.P))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
        }
    }
}
