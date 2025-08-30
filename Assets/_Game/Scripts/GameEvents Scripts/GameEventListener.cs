using UnityEngine;
using UnityEngine.Events;

public class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
{
    [SerializeField] private GameEventSO<T> _gameEvent;
    [SerializeField] private UnityEvent<T> _response;

    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        _response.Invoke(data);
    }
}

public class GameEventListener : GameEventListener<GameEventNullData>
{

}
