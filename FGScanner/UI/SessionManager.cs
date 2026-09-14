using System;
using System.Windows.Forms;

namespace FGScanner.UI
{
    public enum SessionEndReason
    {
        Manual,
        IdleTimeout
    }

    public sealed class SessionEndedEventArgs : EventArgs
    {
        public SessionEndedEventArgs(SessionEndReason reason) => Reason = reason;

        public SessionEndReason Reason { get; }
    }

    /// <summary>Tracks keyboard and mouse activity across all application windows.</summary>
    public static class SessionManager
    {
        private static readonly TimeSpan IdleTimeout = TimeSpan.FromMinutes(30);
        private static readonly ActivityMessageFilter ActivityFilter = new();
        private static Timer _idleTimer;
        private static DateTime _lastActivityUtc;
        private static bool _isActive;

        public static event EventHandler<SessionEndedEventArgs> SessionEnded;

        public static void Start()
        {
            StopTimer();
            _lastActivityUtc = DateTime.UtcNow;

            if (!_isActive)
            {
                Application.AddMessageFilter(ActivityFilter);
                _isActive = true;
            }

            _idleTimer = new Timer { Interval = 15_000 };
            _idleTimer.Tick += IdleTimer_Tick;
            _idleTimer.Start();
        }

        public static void SignOut(SessionEndReason reason)
        {
            if (!_isActive)
            {
                return;
            }

            Stop();
            SessionEnded?.Invoke(null, new SessionEndedEventArgs(reason));
        }

        public static void Stop()
        {
            StopTimer();

            if (_isActive)
            {
                Application.RemoveMessageFilter(ActivityFilter);
                _isActive = false;
            }
        }

        private static void RecordActivity()
        {
            if (_isActive)
            {
                _lastActivityUtc = DateTime.UtcNow;
            }
        }

        private static void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (_isActive && DateTime.UtcNow - _lastActivityUtc >= IdleTimeout)
            {
                SignOut(SessionEndReason.IdleTimeout);
            }
        }

        private static void StopTimer()
        {
            if (_idleTimer == null)
            {
                return;
            }

            _idleTimer.Stop();
            _idleTimer.Tick -= IdleTimer_Tick;
            _idleTimer.Dispose();
            _idleTimer = null;
        }

        private sealed class ActivityMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message message)
            {
                switch (message.Msg)
                {
                    case 0x0100: // WM_KEYDOWN
                    case 0x0104: // WM_SYSKEYDOWN
                    case 0x0200: // WM_MOUSEMOVE
                    case 0x0201: // WM_LBUTTONDOWN
                    case 0x0204: // WM_RBUTTONDOWN
                    case 0x0207: // WM_MBUTTONDOWN
                    case 0x020A: // WM_MOUSEWHEEL
                    case 0x0245: // WM_POINTERUPDATE
                    case 0x0246: // WM_POINTERDOWN
                        RecordActivity();
                        break;
                }

                return false;
            }
        }
    }
}
