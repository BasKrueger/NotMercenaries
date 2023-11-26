using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Model
{
    public static class CardManager
    {
        private static List<AbstractMercenary> activeMercenaries = new List<AbstractMercenary>();
        private static List<AbstractAbility> activeAbilities = new List<AbstractAbility>();
        private static List<AbstractBuff> activeBuffs = new List<AbstractBuff>();

        public static AbstractMercenary CreateMercenary(string name)
        {
            var merc = CardLibrary.GetMercenary(GetUniqueID(), name);
            activeMercenaries.Add(merc);
            return merc;
        }

        public static AbstractAbility CreateAbility(AbstractMercenary owner, string name)
        {
            var ability = CardLibrary.GetAbility(GetUniqueID(), name);
            activeAbilities.Add(ability);
            return ability;
        }

        public static AbstractBuff CreateBuff(string name)
        {
            var buff = CardLibrary.GetBuff(GetUniqueID(), name);
            activeBuffs.Add(buff);
            return buff;
        }

        public static void InvokeCards(string methodName, params object[] args)
        {
            foreach(var m in activeMercenaries)
            {
                if (m.boardPosition != BoardPosition.InPlay) continue;

                try
                {
                    m.GetType().GetMethod(methodName).Invoke(m, args);
                }
                catch (Exception e) { }

                m.InvokeAbilities(methodName, args);
                m.InvokeBuffs(methodName, args);
            }
        }

        public static void UpdateMercs()
        {
            foreach(var m in activeMercenaries)
            {
                m.NormalUpdate();
            }
        }

        public static T GetCard<T>(int id)
        {
            var cards = new List<AbstractCard>();
            cards.AddRange(activeMercenaries);
            cards.AddRange(activeAbilities);
            cards.AddRange(activeBuffs);

            foreach (var card in cards)
            {
                if (card.id == id && card is T)
                {
                    object value = (object)card;
                    return (T)value;
                }
            }

            return default(T);
        }

        public static void Reset()
        {
            activeMercenaries.Clear();
            activeAbilities.Clear();
            activeBuffs.Clear();
        }

        private static int GetUniqueID()
        {
            int id = -1;
            System.Random rng = new System.Random();
            do
            {
                id = rng.Next();
            }
            while (activeMercenaries.ContainsID(id) || activeAbilities.ContainsID(id) || activeBuffs.ContainsID(id));
            return id;
        }
    }
}
