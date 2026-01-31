namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public interface IGameRules
    {
        public void Update(float deltaTime);
        public bool Result();
    }
}
