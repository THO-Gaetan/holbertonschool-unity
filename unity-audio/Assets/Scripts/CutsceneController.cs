using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    Animator animator;
    bool isCutScenePlaying = false;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject timerCanvas;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void StartOfCutscene()
    {
        isCutScenePlaying = true;
        DisablePlayerController();
        DisableTimerCanvas();
    }

    void EndOfCutscene()
    {
        Debug.Log(isCutScenePlaying);
        animator.SetBool("isCutSceneStoping", isCutScenePlaying);
        EnablePlayerCamera();
        EnablePlayerController();
        EnableTimerCanvas();
        gameObject.SetActive(false);
    }

    void DisablePlayerController() => playerController.enabled = false;

    void DisableTimerCanvas() => timerCanvas.SetActive(false);

    void EnablePlayerCamera() => playerCamera.SetActive(true);

    void EnablePlayerController() => playerController.enabled = true;

    void EnableTimerCanvas() => timerCanvas.SetActive(true);
}
