using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Canvas config")]
    [SerializeField] Canvas startGameCanvasPrefab;
    [SerializeField] Canvas endScoreCanvasPrefab;

    Canvas startGameCanvas;
    Canvas endScoreCanvas;

    bool isGameStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        if(!(FindObjectsOfType<GameController>().Length > 1))
        {
            DontDestroyOnLoad(gameObject);

            //startGameCanvas = Instantiate(startGameCanvasPrefab, transform);
            //endScoreCanvas = Instantiate(endScoreCanvasPrefab, transform);

            LoadCanvas();

            startGameCanvas.enabled = true;
            endScoreCanvas.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCanvas()
    {
        var canvasObjects = FindObjectsOfType<Canvas>();
        startGameCanvas = System.Array.Find(canvasObjects, c => c.name == "StartCanvas");
        endScoreCanvas = System.Array.Find(canvasObjects, c => c.name == "EndScoreCanvas");
    }

    void StartGame()
    {
        if (isGameStarted) return;
        isGameStarted = true;

        startGameCanvas.enabled = false;
        endScoreCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startGameCanvas)
        {
            LoadCanvas();
            startGameCanvas.enabled = false;
            endScoreCanvas.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            endScoreCanvas.enabled = true;
            isGameStarted = false;
            StartCoroutine(ReloadSceneWhenJumpButtonPressed());
        }
    }

    private IEnumerator ReloadSceneWhenJumpButtonPressed()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        SceneManager.LoadScene(0);
    }
}
