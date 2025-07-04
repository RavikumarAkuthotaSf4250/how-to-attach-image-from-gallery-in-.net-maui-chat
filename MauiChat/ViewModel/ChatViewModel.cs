using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiChat
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<object> messages;

        private Author currentAuthor;

        private Author Harrison = new Author() { Name = "Harrison", Avatar = "harrison.png" };

        private Author Margaret = new Author() { Name = "Margaret", Avatar = "margaret.png" };

        private Author Stevan = new Author() { Name = "Steven", Avatar = "steven.png" };

        #endregion

        #region Constructor
        public ChatViewModel()
        {
            this.PopupItems = new ObservableCollection<Model>()
            {
               new Model(){ ImageSource = "\ue76c", Text="Image" },
               new Model(){ ImageSource = "\ue7a1", Text="Audio" },
               new Model(){ ImageSource = "\ue797", Text="Document" },
               new Model(){ ImageSource = "\ue712", Text="Folder" },
               new Model(){ ImageSource = "\ue774", Text="Event" },
            };
            
            this.messages = new ObservableCollection<object>();
            this.currentAuthor = new Author() { Name = "Nancy", Avatar = "nancy.png" };
            this.GenerateMessages();
        }

        #endregion

        #region Public Properties
        public ObservableCollection<object> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
            }
        }

        public Author CurrentUser
        {
            get
            {
                return this.currentAuthor;
            }
            set
            {
                this.currentAuthor = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public ObservableCollection<Model> PopupItems { get; set; }


        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Private Methods

        private void GenerateMessages()
        {
            this.messages.Add(new TextMessage()
            {
                Author = currentAuthor,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to launch a new mobile application.",
                DateTime = DateTime.Now
            });

            this.messages.Add(new TextMessage()
            {
                Author = this.Margaret,
                Text = "Oh! That's great",
                DateTime = DateTime.Now
            });

            this.messages.Add(new TextMessage()
            {
                Author = this.Harrison,
                Text = "That is good news.",
                DateTime = DateTime.Now
            });

            this.messages.Add(new TextMessage()
            {
                Author = this.Stevan,
                Text = "What kind of application is it and when are we going to launch?.",
                DateTime = DateTime.Now
            });
        }

        #endregion

        #region Callbacks

        private void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}