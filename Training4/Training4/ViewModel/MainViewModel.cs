using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using Training4.Model;

namespace Training4.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ProductVM> Products { get; set; }
        Server server;
        Client client;
        bool isConnected = false;
        bool isServer = false;
        const string ip = "127.0.0.1";
        const int port = 10100;
        public RelayCommand ClientBtnClick { get; set; }
        public RelayCommand ServerBtnClick { get; set; }
        public RelayCommand AddBtnClick { get; set; }
        
        #region Properties
        private string newID;

        public string NewID
        {
            get { return newID; }
            set { newID = value; RaisePropertyChanged(); }
        }

        private string newName;

        public string NewName
        {
            get { return newName; }
            set { newName = value; RaisePropertyChanged(); }
        }

        private string newPrice;

        public string NewPrice
        {
            get { return newPrice; }
            set { newPrice = value; RaisePropertyChanged(); }
        }

        private string newType;

        public string NewType
        {
            get { return newType; }
            set { newType = value; RaisePropertyChanged(); }
        }

        public string[] Types { get { return ProductVM.Types; } }

        #endregion


        public MainViewModel()
        {
            Products = new ObservableCollection<ProductVM>();

            ClientBtnClick = new RelayCommand(()=>
            {
                StartClient(ip, port, GuiRefresh);
            },()=>{ return !isServer && !isConnected; });


            ServerBtnClick = new RelayCommand(() =>
            {
                StartServer(ip, port, GuiRefresh);
            }, () => { return !isServer && !isConnected; });


            AddBtnClick = new RelayCommand(() =>
            {
                NewObject();
            }, () => { return CanExecuteAddBtnClick(); });
        }

        #region Methods
        private void StartServer(string ip, int port, Action<String> GuiRefresh)
        {
            server = new Server(ip, port, GuiRefresh);
            isServer = true;
            isConnected = true;
        }

        private void StartClient(string ip, int port, Action<String> GuiRefresh)
        {
            client = new Client(ip, port, GuiRefresh);
            isConnected = true;
        }

        private void NewObject()
        {
            string newObjectToString = "@" + newID + ":" + newName + ":" + newPrice + ":" + newType;

            if (isServer)
            {
                //tell server to broadcast the string
                server.Broadcast(null, newObjectToString);
            }
            else
            {
                //tell client to send msg to server, which has to broadcast the string
                client.Send(newObjectToString);
            }

            NewID = "";
            NewName = "";
            NewPrice = "";
            NewType = "";
        }

        private void GuiRefresh(string newProduct)
        {
            string[] raw;
            string[] productArray;
            App.Current.Dispatcher.Invoke(() => 
            {
                raw = newProduct.Split('@');
                productArray = raw[1].Split(':');

                Products.Add(new ProductVM(productArray[0], productArray[1], Int32.Parse(productArray[2]), productArray[3]));

            });
        }

        private bool CanExecuteAddBtnClick()
        {
            if (isConnected)
            {
                if(NewID.Length > 0 && NewName.Length > 0 && NewPrice.Length > 0 && NewType.Length > 0) return true;
            }
            return false;
        }
        #endregion

    }
}