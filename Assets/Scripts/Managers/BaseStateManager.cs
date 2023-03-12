using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class BaseStateManager : MonoBehaviour
    {
        protected abstract void SignUpState();
        protected abstract void RemoveAllState();
        protected abstract void CheckAvailableState();
        public enum Controller
        {
            TurnOff,
            TurnOn
        }
        public Controller value;
        
    }
}
