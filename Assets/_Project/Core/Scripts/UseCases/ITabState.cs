using _Project.Core.Scripts.Entities;

namespace _Project.Core.Scripts.UseCases
{
    public interface ITabState
    {
        TabType Type { get; }
        public void Enter();
        public void Tick(float dt);
        public void Exit();
    }
}