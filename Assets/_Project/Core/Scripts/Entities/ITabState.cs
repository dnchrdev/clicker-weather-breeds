namespace _Project.Core.Scripts.Entities
{
    public interface ITabState
    {
        TabType Type { get; }
        public void Enter();
        public void Tick(float deltaTime);
        public void Exit();
    }
}