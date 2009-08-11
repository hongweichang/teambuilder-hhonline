using System;

namespace HHOnline.Framework
{
    public class HHEventArgs : EventArgs
    {
        private ObjectState _state;

        public ObjectState State
        {
            get
            {
                return _state;
            }
        }

        public HHEventArgs(ObjectState state)
        {
            _state = state;
        }

        public HHEventArgs()
            : this(ObjectState.None)
        { }
    }
}
