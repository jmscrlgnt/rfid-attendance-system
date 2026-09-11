using System;

namespace JCGAttendanceSystem.RFID
{
    internal sealed class NullRfidReader : IRfidReader
    {
#pragma warning disable 67
        public event EventHandler<RfidTagEventArgs> TagScanned;
#pragma warning restore 67
        public bool IsRunning { get; private set; }
        public void Start() { IsRunning = true; }
        public void Stop() { IsRunning = false; }
        public void Dispose() { Stop(); }
    }
}
