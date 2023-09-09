using System;

namespace FarmerSimulator.Domain.Models
{
    public abstract class Herb
    {
        protected PlantState _currentState = PlantState.Planted;
        public PlantState CurrentState
        {
            get => _currentState;
            private set
            {
                _currentState = value;
                StateChanged?.Invoke(_currentState);
            }
        }
        public event Action<PlantState>? StateChanged;

        public abstract long TimeToGrow { get; protected set; }
        public abstract double Cost { get; protected set; }
        protected abstract void Update();
        protected abstract void OnPlant();
        protected abstract void OnGrew();

        public void Plant()
        {
            
        }
    }
}