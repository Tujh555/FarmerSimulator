using System;

namespace FarmerSimulator.Domain.Models
{
    public abstract class Herb
    {
        private PlantState _currentState = PlantState.Planted;
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
    }
}