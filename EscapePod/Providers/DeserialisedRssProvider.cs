using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using EscapePod.Logging;
using EscapePod.Model;

namespace EscapePod.Providers
{
    public interface IDeserialisedRssProvider
    {
        rss Deserialise(string podcastLink);
    }

    public class DeserialisedRssProvider : IDeserialisedRssProvider
    {
        private readonly ILogger _logger;

        public DeserialisedRssProvider(ILogger logger)
        {
            if(logger == null) throw new ArgumentNullException("logger");

            _logger = logger;
        }

        public rss Deserialise(string podcastLink)
        {
            Stream stream;
            using (WebClient webClient = new WebClient())
            {
                string xmlLink = ToHttp(podcastLink);
                stream = webClient.OpenRead(xmlLink);
                
                if (stream == null)
                {
                    _logger.Error("No stream found for URL '{0}'", xmlLink);
                    throw new Exception("No stream found for URL");
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(rss));
            rss Rss;
            try
            {
                Rss = (rss)serializer.Deserialize(stream);
            }
            catch (InvalidOperationException e)
            {
                string error = String.Format("Unable to parse RSS feed.{0}{0}{1}", Environment.NewLine, e);
                _logger.Error(error);
                throw new Exception(error);
            }          
            
            return Rss;
        }

        private static string ToHttp(string link)
        {
            if (link.StartsWith("http"))
            {
                return link;
            }

            return String.Format("{0}{1}", "http://", link);
        }
    }
}
