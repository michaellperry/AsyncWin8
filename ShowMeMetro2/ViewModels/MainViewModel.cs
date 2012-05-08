using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShowMeMetro;
using ShowMeMetro2.Models;
using Windows.Foundation.Collections;

namespace ShowMeMetro2.ViewModels
{
    public class MainViewModel
    {
        private ShowsFile _showsFile = new ShowsFile();
        private IObservableVector<object> _shows = new ObservableVector<object>();

        public IObservableVector<object> Shows
        {
            get
            {
                return _shows;
            }
        }

        public async void LoadShows()
        {
            ShowsDocument document;
            if (await _showsFile.GetFileExistsAsync())
            {
                document = await _showsFile.ReadDocumentAsync();
            }
            else
            {
                document = await GetSampleDataAsync();
                await _showsFile.WriteDocumentAsync(document);
            }
            foreach (var show in document.Shows)
                _shows.Add(show);
        }

        private static async Task<ShowsDocument> GetSampleDataAsync()
        {
            await Task.Delay(2000);
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
