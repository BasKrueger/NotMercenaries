using Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace Model
{
    public static class Extensions
    {
        public static T Random<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static AbstractMercenary GetById(this List<AbstractMercenary> list, int id)
        {
            foreach (var model in list)
            {
                if (model.id == id)
                {
                    return model;
                }
            }

            return null;
        }

        public static bool ContainsID(this List<AbstractMercenary> list, int id)
        {
            foreach (var model in list)
            {
                if (model.id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsID(this List<AbstractAbility> list, int id)
        {
            foreach (var model in list)
            {
                if (model.id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsID(this AbstractAbility[] list, int id)
        {
            foreach (var model in list)
            {
                if (model.id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsID(this List<AbstractBuff> list, int id)
        {
            foreach (var model in list)
            {
                if (model.id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

namespace DTO
{
    public static class DTOExtensions
    {
        public static DTO.MercenaryState GetById(this List<DTO.MercenaryState> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return model;
                }
            }

            return null;
        }

        public static bool ContainsID(this List<DTO.MercenaryState> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static DTO.AbilityState GetById(this List<DTO.AbilityState> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return model;
                }
            }

            return null;
        }

        public static bool ContainsID(this List<DTO.AbilityState> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

namespace View
{
    public static class ViewExtension
    {
        public static View.Mercenary GetById(this List<View.Mercenary> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return model;
                }
            }

            return null;
        }

        public static bool ContainsID(this List<View.Mercenary> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static View.AbstractCard GetById(this List<View.AbstractCard> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return model;
                }
            }

            return null;
        }

        public static bool ContainsID(this List<View.AbstractCard> list, int id)
        {
            foreach (var model in list)
            {
                if (model.Id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

public static class IIDExtensions
{
    public static bool ContainsID(this List<IID> list, int id)
    {
        foreach (var merc in list)
        {
            if (merc.Id == id)
            {
                return true;
            }
        }

        return false;
    }

    public static T GetById<T>(this List<IID> list, int id)
    {
        foreach (var merc in list)
        {
            if (merc.Id == id)
            {
                return (T)merc;
            }
        }

        return default(T);
    }

}
