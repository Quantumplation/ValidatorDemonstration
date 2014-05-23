using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorDemo.Data;

namespace ValidatorDemo
{
    public abstract class Validator
    {
        public abstract void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems) where T : Persistable;

        public abstract void GetFuzz<T>(ICollection<T> items) where T : Persistable;
    }

    public class DenyAllValidator : Validator
    {
        public override void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems)
        {
            throw new Exception("DENIED");
        }

        public override void GetFuzz<T>(ICollection<T> items)
        {
        }
    }

    public class DenyAllOfType<T> : Validator where T : Persistable
    {
        public override void GetFilter<T1>(ICollection<T1> allowedItems, ICollection<T1> filteredItems)
        {
            if(typeof(T) == typeof(T1))
                throw new Exception("DENIED");
        }

        public override void GetFuzz<T1>(ICollection<T1> items)
        {
            if (typeof(T) == typeof(T1))
                throw new Exception("DENIED");
        }
    }

    public class FilterAllValidator : Validator
    {
        public override void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems)
        {
            foreach (var item in allowedItems)
                filteredItems.Add(item);
            allowedItems.Clear();
        }

        public override void GetFuzz<T>(ICollection<T> items)
        {
        }
    }

    public class FilterEvenValidator : Validator
    {
        public override void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems)
        {
            foreach (var item in allowedItems)
            {
                if (item.Id%2 == 0)
                {
                    filteredItems.Add(item);
                }
            }

            foreach (var item in filteredItems)
            {
                allowedItems.Remove(item);
            }
        }

        public override void GetFuzz<T>(ICollection<T> items)
        {
        }
    }

    public class HasPermissionsValidator : Validator
    {
        public static HashSet<int> HasPermissionsTo = new HashSet<int>();

        public override void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems)
        {
            foreach (var item in allowedItems)
            {
                if (HasPermissionsTo.Contains(item.Id))
                    filteredItems.Add(item);
            }
            foreach (var item in filteredItems)
                allowedItems.Remove(item);
        }

        public override void GetFuzz<T>(ICollection<T> items)
        {
        }
    }

    public class FuzzBuildingNameValidator : Validator
    {
        public override void GetFilter<T>(ICollection<T> allowedItems, ICollection<T> filteredItems)
        {
        }

        public override void GetFuzz<T>(ICollection<T> items)
        {
            if (typeof (T) == typeof (Building))
            {
                foreach (var item in items.OfType<Building>())
                {
                    item.Name = "Fuzzed Name";
                }
            }
        }
    }
}
