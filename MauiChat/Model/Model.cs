using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiChat
{
    public class Model : INotifyPropertyChanged
    {
        #region Fields

        private string? imageSource;
        private string? text;

        #endregion

        #region Properties

        public string? ImageSource
        {
            get { return imageSource; }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    RaisePropertyChanged(nameof(ImageSource));
                }
            }
        }
        public string? Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    RaisePropertyChanged(nameof(Text));
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Callback

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }

    public class ImageModel
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }

        #endregion
    }

}
