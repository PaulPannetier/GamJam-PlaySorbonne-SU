using UnityEngine;

public class SceneInit : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private GameObject prefabScene;
    [SerializeField] private GameObject playerPrefab;

    void Awake()
    {
        if (prefabScene == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        mainCamera = Camera.main;

        // init les présent (0, 0, 0) et futur (100, 0, 0)
        GameObject presentScene = Instantiate(prefabScene, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(prefabScene, new Vector3(100f, 0f, 0f), Quaternion.identity);

        // init bonne taille de caméra
        mainCamera.orthographicSize = 10f;

        // init player
        ScenePrefabVars scriptInstance = presentScene.GetComponent<ScenePrefabVars>();

        if (scriptInstance != null)
        {
            GameObject[] spawnPoints = scriptInstance.spawnPoints;
            GameObject randomSpawn = spawnPoints.GetRandom();

            GameObject player = Instantiate(playerPrefab, randomSpawn.transform.position, Quaternion.identity);
            PlayerWatch scriptInstance2 = player.GetComponent<PlayerWatch>();
        }
    }
}
