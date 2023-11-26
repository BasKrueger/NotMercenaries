using System.Collections.Generic;

namespace Model
{
    public static class CardLibrary
    {
        private static List<AbstractMercenary> GetMercs(int id)
        {
            var mercs = new List<AbstractMercenary>();

            mercs.Add(new Antonidas(id));
            mercs.Add(new CarielRome(id));
            mercs.Add(new AnduinWrynn(id));
            mercs.Add(new Xuen(id));
            mercs.Add(new Yrel(id));
            mercs.Add(new Alextrasza(id));

            return mercs;
        }

        private static List<AbstractAbility> GetAbilities(int id)
        {
            var abilities = new List<AbstractAbility>();

            abilities.Add(new Fireball(id));
            abilities.Add(new FlameStrike(id));
            abilities.Add(new FireballStorm(id));

            abilities.Add(new CrusadersBlow(id));
            abilities.Add(new Taunt(id));
            abilities.Add(new SealOfLight(id));

            abilities.Add(new HolyNova(id));
            abilities.Add(new Penance(id));
            abilities.Add(new Salvation(id));

            abilities.Add(new EqualizingStrike(id));
            abilities.Add(new Pounce(id));
            abilities.Add(new TigerLightning(id));

            abilities.Add(new WrathOfTheLightbound(id));
            abilities.Add(new VindicatorsFury(id));
            abilities.Add(new RadientLight(id));

            abilities.Add(new DragonBreath(id));
            abilities.Add(new FlameBuffet(id));
            abilities.Add(new DragonQueensGambit(id));

            return abilities;
        }

        private static List<AbstractBuff> GetBuffs(int id)
        {
            var buffs = new List<AbstractBuff>();

            buffs.Add(new TauntBuff(id));
            buffs.Add(new SealOfLightBuff(id));

            buffs.Add(new SalvationBuff(id));

            buffs.Add(new XuenAttackBuff(id));
            buffs.Add(new XuenAttackDebuff(id));
            buffs.Add(new TigerBuff(id));

            buffs.Add(new LightBuff(id));

            return buffs;
        }


        public static AbstractMercenary GetMercenary(int id, string name)
        {
            foreach(var merc in GetMercs(id))
            {
                if(merc.name == name) return merc;
            }

            return null;
        }

        public static AbstractAbility GetAbility(int id, string name)
        {
            foreach (var ability in GetAbilities(id))
            {
                if (ability.name == name) return ability;
            }

            return null;
        }

        public static AbstractBuff GetBuff(int id, string name)
        {
            foreach (var buff in GetBuffs(id))
            {
                if (buff.name == name) return buff;
            }

            return null;
        }
    }
}