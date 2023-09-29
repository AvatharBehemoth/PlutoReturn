using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stateman
{
    public class state<T> :MonoBehaviour
    {
        public virtual void Enter(T owner) { }
        public virtual void Execute() { }
        public virtual void Exit() { }
    }
}
