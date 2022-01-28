using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    private State currentState;

    public ARSessionOrigin sessionOrigin;
    public GameObject aRSession;
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    [HideInInspector] public ARPlane gamePlane = null;

    public GameObject UICanvas;
    [HideInInspector] public List<GameObject> UIList;

    public TargetManager targetManager;
    public ScoreManager scoreManager;
    public AmmoLauncher ammoLauncher;

    [HideInInspector] public int score;

    private void Awake()
    {
        foreach (Transform child in UICanvas.transform)
            UIList.Add(child.gameObject);
    }

    void Start()
    {
        SetState(new PlaneTrackingState(this));
    }

    void Update()
    {
        if (currentState != null)
            currentState.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        Debug.Log($"Setting state to {state}");
        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
