using System;

namespace WalkingMan
{
    public interface IUserKeyInputProxy
    {
        event Action<bool> KeyOnChange;
        void GetKey();
    }
}
