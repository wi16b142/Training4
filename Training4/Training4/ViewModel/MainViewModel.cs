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

        private int newPrice;

        public int NewPrice
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
                //add new product
            }, () => { return CanExecuteAddBtnClick(); });
        }

        #region Methods
        private void StartServer(string ip, int port, Action<String> GuiRefresh)
        {
            
        }

        private void StartClient(string ip, int port, Action<String> GuiRefresh)
        {

        }

        private void GuiRefresh(string newProduct)
        {
            //set current thread
            //tell server to broadcast the new product and refresh own gui
        }

        private bool CanExecuteAddBtnClick()
        {
            //check if fields are filled and if connected.
            return true;
        }
        #endregion

    }
}