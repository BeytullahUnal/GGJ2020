using Utilities.Publisher_Subscriber_System;

namespace Utilities.EventManager {
    public static class GameEventManager
    {
        public delegate void OnGameStart();
        public static event OnGameStart OnGameStartEvent;

        public static void RaiseOnGameStartEvent() => OnGameStartEvent?.Invoke();

        public delegate void OnGameEnd();
        public static event OnGameEnd OnGameEndEvent;

        public static void RaiseOnGameEndEvent() => OnGameEndEvent?.Invoke();
    }
}
