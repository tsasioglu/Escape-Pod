using EscapePod.Logging;
using EscapePod.Model;
using EscapePod.Providers;
using EscapePod.ViewModels;

namespace EscapePod.Mocks
{
    public class EpisodesViewModelMock : EpisodesViewModel
    {
        public EpisodesViewModelMock() : base(new Logger(), new DeserialisedRssProvider(new Logger()), new DirectoryProvider())
        {
            Rss = new rss
            {
                version = 1m,
                lang = "lang",
                channel = new rssChannel
                {
                    author = "author",
                    category = new category
                    {
                        text = "category",
                    },
                    copyright = "copywrite",
                    description = "description",
                    generator = "generator",
                    keyword = "anjunabeats, above and beyond",
                    owner = new owner
                    {
                        email = "email",
                        name = "name",
                    },
                    link = "http://static.aboveandbeyond.nu/grouptherapy/podcast.xml",
                    title = "Above & Beyond: Group Therapy",
                    subtitle = "subtitle",
                    language = "en",
                    summary = "Hailing from Hungary, Myon & Shane 54 have achieved extraordinary success in the international dance music scene, having established a solid reputation for themselves with notable releases including numerous Beatport number ones and consistent rankings among DJ Mag's Top 100 DJs. They produce for some of the biggest names in music, are constant fixtures in set lists of some of world’s best known DJs, and regularly feature in a multitude of EDM radio show playlists. Myon & Shane 54 also host their own weekly radio show “International Departures”, frequently one of the most popular music podcasts around due in large part to their shameless, all-encompassing approach to mash-ups. In addition, their extensive tour diary “International Departures TV” provides an in depth view of life on the road for the well-traveled duo.",
                    item = new Episode[]
                  {
                      new Episode()
                      {
                          author = "Above & Beyond",
                          author1 = "author1",
                          description = "description",
                          duration = "duration",
                          keywords = "keywords",
                          enclosure = new rssChannelItemEnclosure
                          {
                              length = 1,
                              type = "type",
                              url = "url",
                          },
                          subtitle = "subtitle",
                          title = "Anjunadeep 06 mini-mix (available to pre-order now)",
                          summary = "summary",
                          link = "www.anjunadeep.com",
                          guid = "Anjunadeep 06",
                          @explicit = "no",
                          pubDate = "29 August 2014",
                      },
                     new Episode()
                      {
                          author = "Above & Beyond",
                          author1 = "author1",
                          description = "01. Time Takers - Things You Want 02. Nibc & Life So Far - You Let Me Go (LKiD Remix) 03. Jerome Isma-Ae - Overdrive 04. Pryda - Axis 05. Helena feat Shawnee Taylor - Levity (Freaky Flavour Remix) 06. Reunity feat Danyka Nadeau - Come Closer 07. Boom Jinx & Meredith Call - The Dark Track Of The Moment: 08. Myon & Shane 54 with Kyler England - Summer Of Love (Club Mix) 09. Above & Beyond - Blue Sky Action (Club Mix) 10. Cosmic Gate & Kristina Antuna - Alone 11. Michael Jackson - Slave To The Rhythm (Audien Remix) Blast From The Past: 12. Nic Chagall - This Moment",
                          duration = "duration",
                          keywords = "keywords",
                          enclosure = new rssChannelItemEnclosure
                          {
                              length = 1,
                              type = "type",
                              url = "url",
                          },
                          subtitle = "subtitle",
                          title = "#093 Group Therapy Radio with Above & Beyond",
                          summary = "01. Time Takers - Things You Want 02. Nibc & Life So Far - You Let Me Go (LKiD Remix) 03. Jerome Isma-Ae - Overdrive 04. Pryda - Axis 05. Helena feat Shawnee Taylor - Levity (Freaky Flavour Remix) 06. Reunity feat Danyka Nadeau - Come Closer 07. Boom Jinx & Meredith Call - The Dark Track Of The Moment: 08. Myon & Shane 54 with Kyler England - Summer Of Love (Club Mix) 09. Above & Beyond - Blue Sky Action (Club Mix) 10. Cosmic Gate & Kristina Antuna - Alone 11. Michael Jackson - Slave To The Rhythm (Audien Remix) Blast From The Past: 12. Nic Chagall - This Moment",
                          link = "www.anjunadeep.com",
                          guid = "Anjunadeep 06",
                          @explicit = "no",
                          pubDate = "22 November 2014",
                      },
                  }
                },
            };

            Episodes = new EpisodeViewModel[]
            {
                new EpisodeViewModel(new DirectoryProvider(), Rss, Rss.channel.item[0]) { Progress = 50 },
                new EpisodeViewModel(new DirectoryProvider(), Rss, Rss.channel.item[1]),
                
            };
        }

        public static EpisodeViewModel[] Create
        {
            get { return new EpisodesViewModelMock().Episodes; }
        }

    }
}
