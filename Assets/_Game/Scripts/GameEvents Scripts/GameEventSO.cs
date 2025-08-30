using System.Collections.Generic;
using UnityEngine;


public class GameEventSO<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> listeners = new();

    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public void RegisterListener(IGameEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }
}

// For parameterless events, use Unit as the type
[CreateAssetMenu(fileName = "New_GameEvent", menuName = "Game Data/Game Event")]
public class GameEventSO : GameEventSO<GameEventNullData>
{
    public void Raise()
    {
        Raise(GameEventNullData.Default);
    }
}
