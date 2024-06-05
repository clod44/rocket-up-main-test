using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime = 0f;
    public bool isGameStarted = false;

    private VolumeExpoLerper volumeExpoLerper;
    private CameraController cameraController;
    private Transform playerTransform;

    [SerializeField]
    private Vector2 meteoriteSpawnPosition = new Vector2(0.0f, 15.0f);

    [SerializeField]
    private Vector2 meteoriteSpawnArea = new Vector2(15.0f, 5.0f);
    public GameObject meteoritePrefab;
    [SerializeField]
    private float meteoriteSpawnRate = 2f;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (FindObjectOfType<GameManager>() == null)
        {
            GameObject managerObj = new GameObject("GameManager");
            managerObj.AddComponent<GameManager>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        volumeExpoLerper = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<VolumeExpoLerper>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        volumeExpoLerper.ChangeFromTo(10f, 0f, 1f, () =>
        {
            InvokeRepeating("SpawnMeteorite", 2.0f, 1f / meteoriteSpawnRate);
        });
    }


    public void SpawnMeteorite()
    {
        if (playerTransform == null) return;
        Vector3 pos = playerTransform.position + new Vector3(meteoriteSpawnPosition.x, meteoriteSpawnPosition.y, 0.0f) +
            new Vector3(
                Random.Range(
                    -meteoriteSpawnArea.x,
                    meteoriteSpawnArea.x
                ) * 0.5f,
                Random.Range(
                    -meteoriteSpawnArea.y,
                    meteoriteSpawnArea.y
                ) * 0.5f,
                0.0f);
        Instantiate(meteoritePrefab, pos, Quaternion.identity);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //onselected gizmo draw the rectangel area of the meteorite spawn area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(meteoriteSpawnPosition.x, meteoriteSpawnPosition.y, 0.0f), meteoriteSpawnArea);
    }

}
