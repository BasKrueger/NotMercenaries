using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameView {
    GameObject gameObject { get; }
    IEnumerator OnGameStateChanged(DTO.GameState state); 
}

public class Game : MonoBehaviour
{
    private static Game instance;

    [SerializeField]
    private List<GameObject> customViewUpdateOrder;

    public static Model.Game model { get; private set; }
    private List<DTO.GameState> stateHistory = new List<DTO.GameState>();
    private List<DTO.GameState> storedStates = new List<DTO.GameState>();

    private void Awake()
    {
        if (instance != null) Destroy(this);

        instance = this;
        model = new Model.Game();
    }

    private void Start()
    {
        model.Reset();
        StartCoroutine(UpdateGameStates());
    }

    private void OnEnable() => model.StateChanged += OnGameStateChanged;
    private void OnDisable() => model.StateChanged -= OnGameStateChanged;

    private void OnGameStateChanged(DTO.GameState state)
    {
        storedStates.Add(state);
        stateHistory.Add(state);
    }

    private IEnumerator UpdateGameStates()
    {
        while (true)
        {
            while(storedStates.Count > 0)
            {
                DTO.GameState currentState = storedStates.First();
                List<IGameView> listeners = new List<IGameView>();
                foreach(var listener in FindObjectsOfType<MonoBehaviour>(true).OfType<IGameView>())
                {
                    listeners.Add(listener);
                }

                foreach (var priorityListener in customViewUpdateOrder)
                {
                    yield return StartCoroutine(priorityListener.GetComponent<IGameView>().OnGameStateChanged(currentState));
                    listeners.Remove(priorityListener.GetComponent<IGameView>());
                }

                foreach (var listener in listeners)
                {
                    yield return StartCoroutine(listener.OnGameStateChanged(currentState));
                }

                storedStates.Remove(currentState);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public static void RequestStateUpdate(IGameView view)
    {
        instance.StartCoroutine(view.OnGameStateChanged(instance.stateHistory[instance.stateHistory.Count - 1]));
    }


    public void Restart()
    {
        storedStates.Clear();
        stateHistory.Clear();

        model.Reset();
    }
}
