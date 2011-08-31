﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using MyToolkit.Phone;

namespace MyToolkit.Converters
{
	public class ImageBackgroundColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return new SolidColorBrush(!String.IsNullOrEmpty((String)value) ? Colors.Transparent : Resources.PhoneInactiveColor); 
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
