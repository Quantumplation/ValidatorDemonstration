using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorDemo.Data
{
    public class Building : Persistable
    {
        public Building()
        {
        }

        private string name;
        public string Name
        {
            get { return name ?? (name = Id + " Metrotech"); }
            set { name = value; }
        }

        internal ICollection<int> floorIds; 
        public ICollection<Floor> Floors
        {
            get { return UnitOfWork.Get<Floor>(floorIds.ToArray()).ToList(); }
        } 
    }
}
