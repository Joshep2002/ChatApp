using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class ChatListData:INotifyPropertyChanged
    { 
        public string ContactName { get; set; }
        public byte[] ContactPhoto { get; set; }
        protected string message { get; set; }
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }
        public string lastMessageTime { get; set; }
        public string LastMessageTime
        {
            get { return lastMessageTime; }
            set
            {
                lastMessageTime = value;
                OnPropertyChanged("LastMessageTime");
            }
        }
        public bool ChatIsSelected { get; set; }
        public bool ChatIsPinned { get; set; }
        public bool ChatIsArchived { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
