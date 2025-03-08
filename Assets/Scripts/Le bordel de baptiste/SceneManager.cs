using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private GameObject[] prefabScenes;
    [SerializeField] private GameObject[] prefabScenesFuture;
    [SerializeField] private GameObject playerPrefab;

    void Awake()
    {
        int toChoose = Random.Rand(0, prefabScenes.Length);
        GameObject prefabScene = prefabScenes[toChoose];
        GameObject prefabSceneFuture = prefabScenesFuture[toChoose];

        if (prefabScene == null)
        {
            Debug.LogError("Prefab not found in Resources folder!");
            return;
        }
        mainCamera = Camera.main;

        // init les présent (0, 0, 0) et futur (100, 0, 0)
        GameObject presentScene = Instantiate(prefabScene, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(prefabSceneFuture, new Vector3(100f, 0f, 0f), Quaternion.identity);

        // init bonne taille de caméra
        mainCamera.orthographicSize = 10f;

        // init player
        ScenePrefabVars scriptInstance = presentScene.GetComponent<ScenePrefabVars>();

        if (scriptInstance != null)
        {
            GameObject[] spawnPoints = scriptInstance.spawnPoints;
            GameObject randomSpawn = spawnPoints.GetRandom();
            Debug.Log(randomSpawn.transform);

            GameObject player = Instantiate(playerPrefab, randomSpawn.transform.position, Quaternion.identity);
            PlayerWatch scriptInstance2 = player.GetComponent<PlayerWatch>();
        }
    }
}
