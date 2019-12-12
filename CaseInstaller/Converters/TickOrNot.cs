using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CaseInstaller.Converters
{
    class TickOrNot:IValueConverter
    {
        
        
          public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
          {

               string uri=string.Empty;


               if (value.ToString() == "2")
               {

                   uri = "C:\\Users\\amal.babu\\Pictures\\tick.jpg";

                   return uri;

               }

               else if (value.ToString() == "1")
               {
                   uri = "C:\\Users\\amal.babu\\Pictures\\depositphotos_124582638-stock-illustration-empty-white-paper-plate-vector.jpg";
                   return uri;
               }

               else
               {
                   
                   return uri;
               }
          }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

       
    }
}
