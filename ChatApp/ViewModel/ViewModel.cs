using ChatApp.Commands;
using ChatApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class ViewModel : INotifyPropertyChanged

    {
        #region MainWindow

        #region Properties
        public string ContactName { get; set; }
        public Uri ContactPhoto { get; set; }
        public string LastSeen { get; set; }

        #region Search Chats
        protected bool _isSearchBoxOpen { get; set; }
        public bool IsSearchBoxOpen
        {
            get => _isSearchBoxOpen;
            set
            {
                if (_isSearchBoxOpen == value)
                    return;

                _isSearchBoxOpen = value;

                if (_isSearchBoxOpen == false)
                    SearchText = string.Empty;

                OnPropertyChanged("SearchText");
                OnPropertyChanged("IsSearchBoxOpen");
            }
        }
        protected string LastSearchText { get; set; }
        protected string mSearchText { get; set; }
        public string SearchText
        {

            get => mSearchText;

            set
            {
                if (mSearchText == value)
                    return;

                //Update
                mSearchText = value;

                // if search text is empty restore message
                if (string.IsNullOrEmpty(SearchText))
                {
                    Search();
                }



            }
        }

       
    

    #endregion

    #endregion

    #region Logics

    public void OpenSearchBox() => IsSearchBoxOpen = true;

        public void CloseSearchBox() => IsSearchBoxOpen = false;

        public void ClearSearchBox()
        {
            if (!string.IsNullOrEmpty(SearchText))
                SearchText = string.Empty;
            else
                CloseSearchBox();
        }
        
        protected void Search()
        {
            // To avoid re searching same text again
            if (string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText) || string.Equals(LastSearchText, SearchText))
                return;
            //if searchbox is empty
            if (string.IsNullOrEmpty(SearchText) || Chats == null || Chats.Count <= 0)
            {
                FilteredChats = new ObservableCollection<ChatListData>(Chats ?? Enumerable.Empty<ChatListData>());
                OnPropertyChanged("FilteredChats");

                FilteredPinnedChats = new ObservableCollection<ChatListData>(PinnedChats ?? Enumerable.Empty<ChatListData>());
                OnPropertyChanged("FilteredPinnedChats");

                //Update Last Search Text
                LastSearchText = SearchText;

                return;
            }

            // now , to find all chats thay contain the text in our searchbox

            //if that chat is in normal unpinned chat list find there..

            FilteredChats = new ObservableCollection<ChatListData>(
                Chats.Where(
                    chat => chat.ContactName.ToLower().Contains(SearchText) // If ContactName contains SearchText the add it in filteredChat List
                    ||
                    chat.Message != null && chat.Message.ToLower().Contains(SearchText)// If Message contains SearchText the add it in filteredChat List
                    ));
            OnPropertyChanged("FilteredChats");

            FilteredPinnedChats = new ObservableCollection<ChatListData>(
             PinnedChats.Where(
                 pinnedchat => pinnedchat.ContactName.ToLower().Contains(SearchText) // If ContactName contains SearchText the add it in filteredChat List
                 ||
                 pinnedchat.Message != null && pinnedchat.Message.ToLower().Contains(SearchText)// If Message contains SearchText the add it in filteredChat List
                 ));
            OnPropertyChanged("FilteredPinnedChats");
            //Update Last Search Text
            LastSearchText = SearchText;
            //else if not found un normal unpinned chat list find in pinned chat list
        }
        #endregion

        #region Command
       

        /// <summary>
        /// Open Search Command
        /// </summary>

        protected ICommand _openSearchCommand;
        public ICommand OpenSearchCommand
        {
            get
            {
                if (_openSearchCommand == null)
                    _openSearchCommand = new CommandViewModel(OpenSearchBox);

                return _openSearchCommand;
            }
            set
            {
                _openSearchCommand = value;
            }
        }
        /// <summary>
        /// Close Search Command
        /// </summary>
        protected ICommand _closesearchCommand;
        public ICommand CloseSearchCommand
        {
            get
            {
                if (_closesearchCommand == null)
                    _closesearchCommand = new CommandViewModel(CloseSearchBox);
                return _closesearchCommand;
            }
            set
            {
                _closesearchCommand = value;
            }
        }
        /// <summary>
        /// Clear Search Command
        /// </summary>
        protected ICommand _clearSearchCommand;
        public ICommand ClearSearchCommand
        {
            get
            {
                if (_clearSearchCommand == null)
                    _clearSearchCommand = new CommandViewModel(ClearSearchBox);
                return _clearSearchCommand;
            }
            set
            {
                _clearSearchCommand = value;
            }
        }
        /// <summary>
        ///  Search Command
        /// </summary>
        protected ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                    _searchCommand = new CommandViewModel(Search);
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
            }
        }
        #endregion
        #endregion

        #region StatusThumbs
        #region Properties
        public ObservableCollection<StatusDataModel> statusThumbsCollection { get; set; }
        #endregion

        #region Logics
        void LoadStatusThumbs()
        {
            // Bindeemos nuestra Collection al itemsControl
            statusThumbsCollection = new ObservableCollection<StatusDataModel>()
            {

                new StatusDataModel
                {
                     /*Ya que queremos mantener el primer estado
                     en blanco para que el usuario agregue su propio estado */
                    IsMeAddStatus=true,

                },
                new StatusDataModel
                {
                    ContactName="Jose",
                    ContactPhoto=new Uri("/Assets/1.png",UriKind.RelativeOrAbsolute),
                    StatusImage=new Uri("/Assets/5.jpg",UriKind.RelativeOrAbsolute),
                    IsMeAddStatus=false,
                    //To-Do: Status Message
                    //StatusMessage="",
                },new StatusDataModel
                {
                    ContactName="Steve",
                    ContactPhoto=new Uri("/Assets/2.jpg",UriKind.RelativeOrAbsolute),
                    StatusImage=new Uri("/Assets/8.jpg",UriKind.RelativeOrAbsolute),
                    IsMeAddStatus=false,
                    //To-Do: Status Message
                    //StatusMessage="",
                },new StatusDataModel
                {
                    ContactName="Mairan",
                    ContactPhoto=new Uri("/Assets/3.jpg",UriKind.RelativeOrAbsolute),
                    StatusImage=new Uri("/Assets/5.jpg",UriKind.RelativeOrAbsolute),
                    IsMeAddStatus=false,
                    //To-Do: Status Message
                    //StatusMessage="",
                },
                new StatusDataModel
                {
                    ContactName="Jhon",
                    ContactPhoto=new Uri("/Assets/6.jpg",UriKind.RelativeOrAbsolute),
                    StatusImage=new Uri("/Assets/7.png",UriKind.RelativeOrAbsolute),
                    IsMeAddStatus=false,
                    //To-Do: Status Message
                    //StatusMessage="",
                },
            };
            OnPropertyChanged("statusThumbsCollection");
        }
        #endregion
        #endregion

        #region ChatList
        #region Properties

        public ObservableCollection<ChatListData> mChats { get; set; }
        public ObservableCollection<ChatListData> Chats
        {
            get => mChats;
            set
            {
                // To change the list
                if (mChats == value)
                {
                    return;
                }

                //To update de List
                mChats = value;

                //Updating filtered chats to match
                FilteredChats = new ObservableCollection<ChatListData>(mChats);
                OnPropertyChanged("Chats");
                OnPropertyChanged("FilteredChats");
            }
        }
        public ObservableCollection<ChatListData> mPinnedChats { get; set; }
        public ObservableCollection<ChatListData> PinnedChats
        {
            get => mPinnedChats;
            set
            {
                // To change the list
                if (mPinnedChats == value)
                {
                    return;
                }

                //To update de List
                mPinnedChats = value;

                //Updating filtered chats to match
                FilteredPinnedChats = new ObservableCollection<ChatListData>(mPinnedChats);
                OnPropertyChanged("PinnedChats");
                OnPropertyChanged("FilteredPinnedChats");
            }
        }
        protected ObservableCollection<ChatListData> _archivedChats { get; set; }
        public ObservableCollection<ChatListData> ArchivedChats
        {
            get => _archivedChats;
            set
            {
                _archivedChats = value;
                OnPropertyChanged();
            }
        }
        // Filtrar Chat List, Pinned Chats y Archive chats
        public ObservableCollection<ChatListData> FilteredChats { get; set; }
        public ObservableCollection<ChatListData> FilteredPinnedChats { get; set; }
        #endregion

        #region Logics
        void LoadChats()
        {
            Chats = new ObservableCollection<ChatListData>()
            {
                new ChatListData
                {
                    ContactName="Jose",
                    ContactPhoto=new Uri("/Assets/2.jpg",UriKind.RelativeOrAbsolute),
                    Message="Hey, Whats UpPPP121212",
                    LastMessageTime="Tue, 12:58 PM",
                    ChatIsSelected=true,
                },
                 new ChatListData
                {
                    ContactName="Steve",
                    ContactPhoto=new Uri("/Assets/3.jpg",UriKind.RelativeOrAbsolute),
                    Message="Hey, Whats Up",
                    LastMessageTime="Tue, 12:58 PM",
                    ChatIsSelected=true,
                },
                  new ChatListData
                {
                    ContactName="Mike",
                    ContactPhoto=new Uri("/Assets/5.jpg",UriKind.RelativeOrAbsolute),
                    Message="Hey, Whats Up",
                    LastMessageTime="Tue, 12:58 PM",
                    ChatIsSelected=true,
                },
                   new ChatListData
                {
                    ContactName="Juan",
                    ContactPhoto=new Uri("/Assets/7.png",UriKind.RelativeOrAbsolute),
                    Message="Hey, Whats Up",
                    LastMessageTime="Tue, 12:58 PM",
                    ChatIsSelected=true,
                },
                    new ChatListData
                {
                    ContactName="Federico",
                    ContactPhoto=new Uri("/Assets/8.jpg",UriKind.RelativeOrAbsolute),
                    Message="Hey, Whats Up",
                    LastMessageTime="Tue, 12:58 PM",
                    ChatIsSelected=true,
                },
            };
            OnPropertyChanged();
        }
        #endregion

        #region Command
        // Para tomar el ContactName del chat seleccionado, para eso podemos abrir la correspondiente conversacion
        protected ICommand _getSelectedChatCommand;
        public ICommand GetSelectedChatCommand => _getSelectedChatCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is ChatListData v)
            {
                // Tomando ContactName del Chat Seleccionado
                ContactName = v.ContactName;
                OnPropertyChanged("ContactName");
                // Tomando ContactPhoto del Chat Seleccionado
                ContactPhoto = v.ContactPhoto;
                OnPropertyChanged("ContactPhoto");

                LoadConversation(v);
            }
        });


        //To Pin Chat on Pin Button Click
        protected ICommand _pinChatCommand;
        public ICommand PinChatCommand => _pinChatCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is ChatListData v)
            {
                if (!FilteredPinnedChats.Contains(v))
                {

                    PinnedChats.Add(v);
                    FilteredPinnedChats.Add(v);
                    v.ChatIsPinned = true;
                    OnPropertyChanged("PinnedChats");
                    OnPropertyChanged("FilteredPinnedChats");

                    Chats.Remove(v);
                    FilteredChats.Remove(v);
                    OnPropertyChanged("Chats");
                    OnPropertyChanged("FilteredChats");

                    // Remember , Chat will be removed from Pinned Lisr when Archived... and Vice Versa
                    if (ArchivedChats != null)
                    {
                        if (ArchivedChats.Contains(v))
                        {
                            ArchivedChats.Remove(v);
                            v.ChatIsArchived = false;
                        }
                    }

                }
            }
        });

        protected ICommand _unPinChatCommand;
        public ICommand UnPinChatCommand => _unPinChatCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is ChatListData v)
            {
                if (!FilteredChats.Contains(v))
                {
                    Chats.Add(v);
                    FilteredChats.Add(v);
                    OnPropertyChanged("Chats");
                    OnPropertyChanged("FilteredChats");

                    //Remove selected pinned chat list
                    PinnedChats.Remove(v);
                    FilteredPinnedChats.Remove(v);
                    OnPropertyChanged("PinnedChats");
                    OnPropertyChanged("FilteredPinnedChats");
                    v.ChatIsPinned = false;


                }
            }
        });

        protected ICommand _archivedChatCommand;
        public ICommand ArchivedChatCommand => _archivedChatCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is ChatListData v)
            {
                if (!ArchivedChats.Contains(v))
                {
                    // Remember , Chat will be removed from Pinned Lisr when Archived... and Vice Versa


                    // Remove Chat from Pinned & Unpinned Chat List
                    Chats.Remove(v);
                    FilteredChats.Remove(v);

                    PinnedChats.Remove(v);
                    FilteredPinnedChats.Remove(v);

                    //Add chat in ArchiveList
                    ArchivedChats.Add(v);
                    v.ChatIsPinned = false;
                    v.ChatIsArchived = true;

                    //Update List
                    OnPropertyChanged("Chats");
                    OnPropertyChanged("FilteredChats");
                    OnPropertyChanged("PinnedChats");
                    OnPropertyChanged("FilteredPinnedChats");
                    OnPropertyChanged("ArchivedChats");


                }
            }
        });

        // UnArchive Chat Command
        protected ICommand _unArchivedChatCommand;
        public ICommand UnArchivedChatCommand => _unArchivedChatCommand ??= new RelayCommand(parameter =>
        {
            if (parameter is ChatListData v)
            {
                if (!FilteredChats.Contains(v) && !Chats.Contains(v))
                {

                    Chats.Add(v);
                    FilteredChats.Add(v);

                }
                //Add chat in ArchiveList
                ArchivedChats.Remove(v);
                v.ChatIsPinned = false;
                v.ChatIsArchived = false;


                OnPropertyChanged("FilteredChats");
                OnPropertyChanged("Chats");
                OnPropertyChanged("ArchivedChats");
            }
        });
        #endregion

        #endregion

        #region Conversations
        #region Properties
        protected bool _isSearchConversationBoxOpen { get; set; }
        public bool IsSearchConversationBoxOpen
        {
            get => _isSearchConversationBoxOpen;
            set
            {
                if (_isSearchConversationBoxOpen == value)
                    return;

                _isSearchConversationBoxOpen = value;

                if (_isSearchConversationBoxOpen == false)
                    SearchConversationText = string.Empty;

                OnPropertyChanged("SearchConversationText");
                OnPropertyChanged("IsSearchConversationBoxOpen");
            }
        }
        protected ObservableCollection<ChatConversation> mConversations;
        public ObservableCollection<ChatConversation> Conversations
        {
            get => mConversations;
            set
            {
                // To change the list
                if (mConversations == value)
                    return;


                //To update de List
                mConversations = value;

                //Updating filtered chats to match
                FilteredConversations = new ObservableCollection<ChatConversation>(mConversations);
                OnPropertyChanged("Conversations");
                OnPropertyChanged("FilteredConversations");


            }
        }
        /// <summary>
        /// Filter Conversations
        /// </summary>

        public ObservableCollection<ChatConversation> FilteredConversations { get; set; }

        //Usaremos este messageText para transferir el valor del mensaje mandado a nuestro body de conversacion
        protected string messageText;
        public string MessageText
        {
            get => messageText;
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");

            }
        }
        protected string LastSearchConversationText;
        protected string mSearchConversationText;
        public string SearchConversationText
        {
            get => mSearchConversationText;
            set
            {

                if (mSearchConversationText == value)
                    return;

                mSearchConversationText = value;

                if (string.IsNullOrEmpty(SearchConversationText))
                {
                    SearchInConversation();
                }
            }
        }
        #endregion 

        #region Logics
        public void OpenConversationSearchBox() => IsSearchConversationBoxOpen = true;

        public void CloseConversationSearchBox() => IsSearchConversationBoxOpen = false;

        public void ClearConversationSearchBox()
        {
            if (!string.IsNullOrEmpty(SearchConversationText))
                SearchConversationText = string.Empty;
            else
                CloseConversationSearchBox();
        }

        
        void LoadConversation(ChatListData chat)

        {


            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
            if (Conversations == null)
                Conversations = new ObservableCollection<ChatConversation>();

            Conversations.Clear();
            FilteredConversations.Clear();

            using (SqlCommand com = new SqlCommand("SELECT * FROM conversations WHERE ContactName = @ContactName ", connection))
            {
                com.Parameters.AddWithValue("@ContactName", chat.ContactName);

                using (SqlDataReader reader = com.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        // Para formatear
                        string MsgReceivedOn = !string.IsNullOrEmpty(reader["MsgReceivedOn"].ToString()) ?
                                Convert.ToDateTime(reader["MsgReceivedOn"].ToString()).ToString("MM dd, hh:mm tt") : "";

                        string MsgSentOn = !string.IsNullOrEmpty(reader["MsgSentOn"].ToString()) ?
                                Convert.ToDateTime(reader["MsgSentOn"].ToString()).ToString("MM dd, hh:mm tt") : "";

                        var conversation = new ChatConversation()
                        {

                            ContactName = reader["ContactName"].ToString(),
                            ReceivedMessage = reader["ReceivedMsgs"].ToString(),
                            LastSeen = reader["LastOnline"].ToString(),
                            MsgReceivedOn = reader["MsgReceivedOn"].ToString(),
                            SentMessage = reader["SentMsgs"].ToString(),
                            MsgSentOn = reader["MsgSentOn"].ToString(),
                            IsMessageReceived = string.IsNullOrEmpty(reader["ReceivedMsgs"].ToString()) ? false : true
                        };
                        Conversations.Add(conversation);
                        OnPropertyChanged("Conversations");
                        FilteredConversations.Add(conversation);
                        OnPropertyChanged("FilteredConversations");
                    }
                }
            }


        }

        void SearchInConversation()
        {
            if ((string.IsNullOrEmpty(LastSearchConversationText) && string.IsNullOrEmpty(SearchConversationText) || string.Equals(LastSearchConversationText, SearchConversationText)))
                return;
            if (string.IsNullOrEmpty(SearchConversationText) || Conversations == null || Conversations.Count == 0)
            {
                FilteredConversations = new ObservableCollection<ChatConversation>(Conversations ?? Enumerable.Empty<ChatConversation>());
                OnPropertyChanged("FilteredConversations");

                LastSearchConversationText = SearchConversationText;

                return;
            }
            // now , to find all Conversations thay contain the text in our searchbox

            FilteredConversations = new ObservableCollection<ChatConversation>(
                Conversations.Where(chat => chat.ReceivedMessage.ToLower().Contains(SearchConversationText) || chat.SentMessage.ToLower().Contains(SearchConversationText)));
            OnPropertyChanged("FilteredConversations");


            //Update Last Search Text
            LastSearchConversationText = SearchConversationText;
            //else if not found un normal unpinned chat list find in pinned chat list

        }

        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrador\source\repos\ChatApp\ChatApp\Database\Database1.mdf;Integrated Security=True");
        #endregion

        #region Commands
        protected ICommand _openConversationSearchCommand;
        public ICommand OpenConversationSearchCommand
        {
            get
            {
                if (_openConversationSearchCommand == null)
                    _openConversationSearchCommand = new CommandViewModel(OpenConversationSearchBox);

                return _openConversationSearchCommand;
            }
            set
            {
                _openConversationSearchCommand = value;
            }
        }

        protected ICommand _closeConversationSearchCommand;
        public ICommand CloseConversationSearchCommand
        {
            get
            {
                if (_closeConversationSearchCommand == null)
                    _closeConversationSearchCommand = new CommandViewModel(CloseConversationSearchBox);
                return _closeConversationSearchCommand;
            }
            set
            {
                _closeConversationSearchCommand = value;
            }
        }

        protected ICommand _clearConversationSearchCommand;
        public ICommand ClearConversationSearchCommand
        {
            get
            {
                if (_clearConversationSearchCommand == null)
                    _clearConversationSearchCommand = new CommandViewModel(ClearConversationSearchBox);
                return _clearConversationSearchCommand;
            }
            set
            {
                _clearConversationSearchCommand = value;
            }
        }

        protected ICommand _searchConversationCommand;
        public ICommand SearchConversationCommand
        {
            get
            {
                if (_searchConversationCommand == null)
                    _searchConversationCommand = new CommandViewModel(SearchInConversation);
                return _searchConversationCommand;
            }
            set
            {
                _searchConversationCommand = value;
            }
        }
        #endregion
        #endregion

        #region ContactInfo

        #region Properties
        protected bool _isContactInfoOpen;
        public bool IsContactInfoOpen
        {
            get=> _isContactInfoOpen;
            set
            { 
                _isContactInfoOpen = value;
                OnPropertyChanged("IsContactInfoOpen");
                OnPropertyChanged();
            }
        }

        #endregion
        #region Logics
        public void OpenContactInfo()=> IsContactInfoOpen = true;
        public void CloseContactInfo() => IsContactInfoOpen = false;


        #endregion
        #region Commands
        protected ICommand _openContactInfoCommand;
        public ICommand OpenContactInfoCommand
        {
            get
            {
                if (_openContactInfoCommand == null)
                    _openContactInfoCommand = new CommandViewModel(OpenContactInfo);

                return _openContactInfoCommand;
            }
            set
            {
                _openContactInfoCommand = value;
            }
        }
        protected ICommand _closeContactInfoCommand;
        public ICommand CloseContactInfoCommand
        {
            get
            {
                if (_closeContactInfoCommand == null)
                    _closeContactInfoCommand = new CommandViewModel(CloseContactInfo);

                return _closeContactInfoCommand;
            }
            set
            {
                _closeContactInfoCommand = value;
            }
        }
        #endregion
        #endregion

        public ViewModel()
        {
            
            LoadStatusThumbs();
            LoadChats();
            PinnedChats = new ObservableCollection<ChatListData>();
            ArchivedChats = new ObservableCollection<ChatListData>();
        }



        // Permite notificar a la UI cada vez que cambias una propiedad en el ViewModel
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
