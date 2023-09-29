using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;


namespace stateman
{
    //statestack architecture adapted from gamedev experiments "make a game like pokemon in unity" #101
    public class statemachine<T>
    {
        public state<T> CurrentState { get; private set; }
        public Stack<state<T>> StateStack { get; private set; }

        T owner;
        public statemachine(T owner) 
        {
            this.owner = owner;
            StateStack= new Stack<state<T>>();
        }

        public void Execute()
        {
            CurrentState?.Execute();
        }

        public void Push(state<T> newstate) 
        {
            StateStack.Push(newstate);
            CurrentState= newstate;
            CurrentState.Enter(owner);
        }

        public void Pop() 
        {
            StateStack.Pop();
            CurrentState.Exit();
            CurrentState= StateStack.Peek();
        }

        public void ChangeState(state<T> newstate) 
        {
        if (CurrentState != null) 
        {
            StateStack.Pop();
            CurrentState.Exit();
        }

        StateStack.Push(newstate);
        CurrentState = newstate;
        CurrentState.Enter(owner);
        }
    }
}