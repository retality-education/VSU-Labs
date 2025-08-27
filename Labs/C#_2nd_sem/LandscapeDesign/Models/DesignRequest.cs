// DesignRequest.cs
using System.Collections;
using System.Collections.Generic;

namespace LandscapeDesign.Models
{
    internal class DesignRequest : IEnumerable<AreaChange>
    {
        public List<AreaChange> AreaChanges { get; }

        public DesignRequest(List<AreaChange> areaChanges)
        {
            AreaChanges = areaChanges;
        }
        public IEnumerator<AreaChange> GetEnumerator()
        {
            return AreaChanges.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}