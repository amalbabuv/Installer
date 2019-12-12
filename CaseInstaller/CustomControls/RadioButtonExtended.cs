using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace CaseInstaller.CustomControls
{
    public class RadioButtonExtended: RadioButton
    {


        //public string CustomIcon
        //{
        //    get { return (string)GetValue(CustomIconProperty); }
        //    set { SetValue(CustomIconProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CustomIconProperty =
        //    DependencyProperty.Register("CustomIcon", typeof(RadioButtonExtended), typeof(string), new PropertyMetadata(string.Empty));



        //public string RecivedCount
        //{
        //    get { return this.GetValue(RecivedCountProperty).ToString(); }
        //    set { this.SetValue(RecivedCountProperty, value); }
        //}



        public string SubText
        {
           get { return (string)GetValue(SubTextProperty); }
            set { SetValue(SubTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubTextProperty =
            DependencyProperty.Register("SubText", typeof(string), typeof(RadioButtonExtended), new PropertyMetadata(string.Empty));


    }


   
}


