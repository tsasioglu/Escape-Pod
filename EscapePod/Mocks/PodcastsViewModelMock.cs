using System.Collections.ObjectModel;
using EscapePod.Logging;
using EscapePod.Model;
using EscapePod.Providers;
using EscapePod.ViewModels;

namespace EscapePod.Mocks
{
    public class PodcastsViewModelMock : PodcastsViewModel
    {
        public PodcastsViewModelMock() : base(new DeserialisedRssProvider(new Logger()))
        {
            Podcasts = new ObservableCollection<Podcast>()
            {
                new Podcast
                {
                    Title = "Above & Beyond: Group Therapy",
                    Url = "http://static.aboveandbeyond.nu/grouptherapy/podcast.xml",
                },

                new Podcast
                {
                    Title = "The Gareth Emery Podcast",
                    Url = "http://www.galexmusic.com/podcast/gareth.xml",
                },

                new Podcast
                {
                    Title = "The Gareth Emery Podcast Long Ass Title Long Ass Title Long Ass Title Long Ass Title",
                    Url = "http://www.galexmusic.com/podcast/gareth.xml_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link_long_ass_link",
                },
            };
        }
    }
}
