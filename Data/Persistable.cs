using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorDemo.Data
{
    public class Persistable
    {
        public int Id { get; set; }
        internal UnitOfWork UnitOfWork { get; set; }
    }
}
