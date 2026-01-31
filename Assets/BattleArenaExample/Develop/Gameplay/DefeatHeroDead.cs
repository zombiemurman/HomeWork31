namespace Assets.BattleArenaExample.Develop.Gameplay
{
    public class DefeatHeroDead : IGameRules
    {
        private Character _hero;

        public DefeatHeroDead(Character hero)
        {
            _hero = hero;
        }

        public void Update(float deltaTime)
        {
        }

        public bool Result()
        {
            return _hero.Health <= 0;
        }
    }
}
