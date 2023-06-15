using Microsoft.AspNetCore.Mvc;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace ThomasGrant.Utility
{
    public static class ListController
    {
        private static Random random = new Random();
        public static List<IPublishedContent> Shuffle(List<IPublishedContent> content)
        {
            int n = content.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                IPublishedContent value = content[k];
                content[k] = content[n];
                content[n] = value;
            }

            return content;
        }
    }
}
