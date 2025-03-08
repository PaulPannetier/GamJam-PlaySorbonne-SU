using UnityEngine;
using UnityEngine.Audio;

public class StartMenuManager : MonoBehaviour
{
  
  string  lvl1 = "Level1";

  private void Awake()
  {
    //UnityEngine.SceneManagement.SceneLoader.Instance.LoadStartMenuScene();
    AudioManager.instance.PlaySound("Main Theme", 1f);
    Debug.Log("AudioManager.instance.PlaySound(\"Main Theme\", 1f);");
  }
  public void PlayGame()
  {
    //UnityEngine.SceneManagement.SceneLoader.Instance.LoadGameScene(lvl1);
  }
    public void QuitGame()
    {   
        Debug.Log("Quit");
        Application.Quit();
    }
}
