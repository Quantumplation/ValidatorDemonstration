using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorDemo.Data;

namespace ValidatorDemo
{
    public class UnitOfWork
    {
        private Dictionary<Type, Dictionary<int, Persistable>> data;
        private Agent agent;
        public UnitOfWork(Agent a)
        {
            agent = a;
            data = new Dictionary<Type, Dictionary<int, Persistable>>
            {
                {
                    typeof(Building), new Dictionary<int, Persistable>
                    {
                        { 1, new Building
                            {
                                Id = 1,
                                floorIds = new [] { 5, 6, 7 }
                            }
                        },
                        { 2, new Building
                            {
                                Id = 2,
                                floorIds = new [] { 8, 9 }
                            }
                        },
                        { 3, new Building { Id = 3 }},
                        { 4, new Building
                            {
                                Id = 4,
                                floorIds = new [] { 10 }
                            }},
                    }
                },
                {
                    typeof(Floor), new Dictionary<int, Persistable>
                    {
                        {5, new Floor { Id = 5, ParentId = 1 }},
                        {6, new Floor { Id = 6, ParentId = 1 }},
                        {7, new Floor { Id = 7, ParentId = 1 }},
                        {8, new Floor { Id = 8, ParentId = 2 }},
                        {9, new Floor { Id = 9, ParentId = 2 }},
                        {10, new Floor { Id = 10, ParentId = 4 }},
                    }
                }
            };
            foreach (var d in data.Values.SelectMany(t => t.Values))
                d.UnitOfWork = this;
        }

        public IEnumerable<T> Get<T>(params int[] id) where T : Persistable
        {
            var resultSet = id.Select(x => data[typeof (T)][x]).OfType<T>().ToList();
            var forbiddenSet = new List<T>();
            foreach (var validator in agent.GetValidators())
            {
                validator.GetFilter(resultSet, forbiddenSet);
            }
            foreach (var validator in agent.GetValidators())
            {
                validator.GetFuzz(resultSet);
            }
            return resultSet;
        }
    }
}
