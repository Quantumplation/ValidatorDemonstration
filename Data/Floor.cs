using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorDemo.Data
{
    public class Floor : Persistable
    {
        internal int ParentId { get; set; }

        public Building Parent
        {
            get { return UnitOfWork.Get<Building>(ParentId).Single(); }
        }
    }
}
