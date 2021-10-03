using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IDamagable
    {
        void TakeDamage(int _damage);
    }
    
    public interface IPoolable
    {
        void OnFixedUpdate();
        void OnEnableObject();
        void OnDisableObject();
    }
}