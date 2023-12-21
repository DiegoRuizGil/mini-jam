using UnityEngine.Events;

namespace GameEvents
{
    public interface IGameEventListener<T>
    {
        void OnEventRaise(T item);

        void AddAction(UnityAction<T> call);
    }
}

