using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentPrefetch
{
    class Program
    {
        static void Main(string[] args)
        {
            var startContent = new Content();

            var fetched = GetContentToPrefetch(startContent, 0, 2, 1, new HashSet<Content>() { startContent });
        }

        public static List<Content> GetContentToPrefetch(Content current, int distance, int requirePrefetchDistance, int optionalPrefetchDistance, HashSet<Content> visited)
        {
            if (requirePrefetchDistance == 0  && optionalPrefetchDistance == 0)
            {
                return new List<Content>();
            }
            else if(requirePrefetchDistance == 0)
            {
                var returnList = new List<Content>();
                foreach(var content in current.Neighbors)
                {
                    if (!visited.Contains(content) && content.PrefetchDistance >= distance)
                    {
                        visited.Add(content);
                        returnList.Add(content);
                        returnList.AddRange(GetContentToPrefetch(content, distance+1, 0, optionalPrefetchDistance - 1, visited));
                    }
                }
                return returnList;
            }
            else
            {
                var returnList = new List<Content>();
                foreach (var content in current.Neighbors)
                {
                    if (!visited.Contains(content))
                    {
                        visited.Add(content);
                        returnList.Add(content);
                        returnList.AddRange(GetContentToPrefetch(content, distance+1, requirePrefetchDistance-1, optionalPrefetchDistance, visited));
                    }
                }
                return returnList;
            }
        }
    }
}
