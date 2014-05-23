using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidatorDemo.Data;

namespace ValidatorDemo
{
    public abstract class Agent
    {
        public abstract IEnumerable<Validator> GetValidators();
    }

    public class SimpleAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield break;
        }
    }

    public class DenyAllAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new DenyAllValidator();
        }
    }

    public class DenyFloorsAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new DenyAllOfType<Floor>();
        }
    }

    public class FilterAllAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new FilterAllValidator();
        }
    }

    public class FilterEvenAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new FilterEvenValidator();
        }
    }

    public class PermissionsAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new HasPermissionsValidator();
        }
    }

    public class OddFuzzAgent : Agent
    {
        public override IEnumerable<Validator> GetValidators()
        {
            yield return new FilterEvenValidator();
            yield return new FuzzBuildingNameValidator();
        }
    }
}
