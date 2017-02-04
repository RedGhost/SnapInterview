using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPrefetch
{
    public class Content
    {
        public static int MinWeight = 0;

        public int PrefetchDistance;
        public string ContentUrl;
        public List<Content> Neighbors;

        public Content()
        {
            PrefetchDistance = MinWeight;
            Neighbors = new List<Content>();
        }

        public override int GetHashCode()
        {
            return ContentUrl.GetHashCode();
        }
    }
}
