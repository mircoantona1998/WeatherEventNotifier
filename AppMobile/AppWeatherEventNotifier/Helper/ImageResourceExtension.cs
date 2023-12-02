using AppWeatherEventNotifier.Helper.Interfaces;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace AppWeatherEventNotifier.Helper
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            string imgSource;
            if (Source == null)
            {
                return null;
            }
            var path = DependencyService.Get<IConfigurationInterface>().GetIconPath();
            if (Source.Contains("custom"))
            {
                var a = Source.Split("custom.");
                imgSource = string.Concat(path, ".", a[1]);
            }
            else
                imgSource = string.Concat("AppWeatherEventNotifier.Resources.", Source);
            ImageSource imageSource = ImageSource.FromResource(imgSource, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }
        public static ImageSource SetImage(string imageName)
        {
            var imgSource = string.Concat("AppWeatherEventNotifier.Resources.", imageName);
            return ImageSource.FromResource(imgSource, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
        public static ImageSource SetCustomImage(string imageName)
        {
            var path = DependencyService.Get<IConfigurationInterface>().GetIconPath();
            var imgSource = string.Concat(path, ".", imageName);
            return ImageSource.FromResource(imgSource, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }
}
