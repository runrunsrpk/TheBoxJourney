using System;

namespace NumGates.TestBattle
{
    public interface IUIBaseManagement
    {
        public Action<int> OnClickSelection { get; set; }
        public Action<int, bool> OnClickPosition { get; set; }
    }
}

