using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ShowMeSilverlight.Models
{
    public class Show
    {
        public string Name { get; set; }
        public DateTime ShowTime { get; set; }
        public string Venue { get; set; }
        public DateTime SaleTime { get; set; }
    }
}
