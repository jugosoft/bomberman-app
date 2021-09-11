using System.Collections.Generic;
using System.Windows.Forms;

namespace GXA
{
    class KeyboardState
    {
        public KeyboardState()
        {
            downKeys.Clear();
        }

        static private KeyboardState instance;

        static public KeyboardState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KeyboardState();                    
                }
                return instance;
            }
        }
        
        private IList<Keys> downKeys = new List<Keys>();

        public bool GetKeyStateIsDown(Keys key)
        {
            return downKeys.Contains(key);
        }

        public void PressKey(Keys key)
        {
            if (!downKeys.Contains(key))
            {
                downKeys.Add(key);
            }
        }

        public void ReleaseKey(Keys key)
        {
            if (downKeys.Contains(key))
            {
                downKeys.Remove(key);
            }
        }
    }
}
