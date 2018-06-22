using SimpleCM.Tools;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleCM.Data
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                long weight = (long)value;
                DateTime datetime = Util.GetTimeFromMillis(weight);


                return string.Format("{0}年{1}月{2}日", datetime.Year, datetime.Month, datetime.Day);
            }
            catch
            {
                return "";
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                long weight = (long)value;
                return weight + "";
            }
            catch
            {
                return "";
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BillNoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                BillNote bill = (BillNote)value;
                return bill.Tostring();
            }
            catch
            {
                return "";
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
