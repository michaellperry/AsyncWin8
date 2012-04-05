using System;
using System.Collections.Generic;
using ShowMeMetro.Models;

namespace ShowMeMetro.ViewModels
{
    public class MainViewModel
    {
        private ShowsFile _showsFile = new ShowsFile();
        private List<Show> _shows;

        public IEnumerable<Show> Shows
        {
            get
            {
                if (_shows == null)
                    LoadShows();

                return _shows;
            }
        }

        private void LoadShows()
        {
            ShowsDocument document;
            if (_showsFile.FileExists)
            {
                document = _showsFile.ReadDocument();
            }
            else
            {
                document = GetSampleData();
                _showsFile.WriteDocument(document);
            }
            _shows = document.Shows;
        }

        private static ShowsDocument GetSampleData()
        {
            return new ShowsDocument
            {
                Shows = new List<Show>
                {
                    new Show
                    {
                        Name = "THE TOUR 2012: KISS AND MÖTLEY CRÜE",
                        Venue = "Gexa Energy Pavilion",
                        SaleTime = DateTime.Parse("3/23/12"),
                        ShowTime = DateTime.Parse("8/4/12")
                    },
                    new Show
                    {
                        Name = "Michael Jackson THE IMMORTAL World Tour",
                        Venue = "American Airlines Center",
                        SaleTime = DateTime.Parse("3/26/12"),
                        ShowTime = DateTime.Parse("6/26/12")
                    },
                    new Show
                    {
                        Name = "Drake: The Club Paradise Tour",
                        Venue = "Gexa Energy Pavilion",
                        SaleTime = DateTime.Parse("3/23/12"),
                        ShowTime = DateTime.Parse("5/16/12")
                    },
                    new Show
                    {
                        Name = "Avicii",
                        Venue = "Fort Worth Conv Ctr Arena",
                        SaleTime = DateTime.Parse("3/26/12"),
                        ShowTime = DateTime.Parse("5/18/12")
                    },
                    new Show
                    {
                        Name = "Mesquite Rodeo & Real.texas.festival",
                        Venue = "Mesquite Arena",
                        SaleTime = DateTime.Parse("3/23/12"),
                        ShowTime = DateTime.Parse("4/27/12")
                    },
                    new Show
                    {
                        Name = "Yo Gotti",
                        Venue = "House of Blues Dallas",
                        SaleTime = DateTime.Parse("3/23/12"),
                        ShowTime = DateTime.Parse("6/8/12")
                    },
                    new Show
                    {
                        Name = "One Direction",
                        Venue = "Gexa Energy Pavilion",
                        SaleTime = DateTime.Parse("3/24/12"),
                        ShowTime = DateTime.Parse("6/23/12")
                    },
                    new Show
                    {
                        Name = "Sons of Bill",
                        Venue = "House of Blues Dallas",
                        SaleTime = DateTime.Parse("3/23/12"),
                        ShowTime = DateTime.Parse("5/31/12")
                    }
                }
            };
        }
    }
}
