using System;
using EscapePod.Model;

namespace EscapePod.ExtensionMethods
{
    public static class EpisodeExtensions
    {
        public static string GetUrl(this Episode episode)
        {
            if (episode.enclosure != null && !String.IsNullOrWhiteSpace(episode.enclosure.url))
            {
                return episode.enclosure.url;
            }

            if (episode.link != null)
            {
                return episode.link;
            }

            return String.Empty;
        }
    }
}