namespace Model
{
    public class AIPlayer : Player
    {
        public void PlayMercsAI()
        {
            while(bank.Count > 0 && inPlay.Count < 0)
            {
                ActionManager.UseNow(new PlayMercAction(null, this, bank[UnityEngine.Random.Range(0, bank.Count)]));
            }
        }

        public void PrepareAbilitiesAI()
        {
            foreach(var merc in inPlay)
            {
                var ability = merc.abilities[UnityEngine.Random.Range(0, merc.abilities.Count)];

                merc.PreparePlay(this, ability, Game.GetRandomValidTarget(ability.targets, this));
            }
        }
    }
}
