using System;

namespace JCGAttendanceSystem.RFID
{
    internal sealed class RfidTagEventArgs : EventArgs
    {
        public RfidTagEventArgs(string tag) { Tag = tag; }
        public string Tag { get; }
    }

    internal interface IRfidReader : IDisposable
    {
        event EventHandler<RfidTagEventArgs> TagScanned;
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}
