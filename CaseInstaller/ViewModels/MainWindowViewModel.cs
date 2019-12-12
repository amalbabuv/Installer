using Prism.Commands;
using Prism.Regions;
using Prism.Logging;
using System.IO;
using System.Windows;
using CaseInstaller.Models;
using System.Collections.ObjectModel;
using Prism.Events;
using CaseInstaller.Events;
using System;

namespace CaseInstaller.ViewModels
{
    public class MainWindowViewModel : CaseInstallerBase
    {
        private readonly IRegionManager regionManager;

        public CaseInstallerLogger loggerFacade;
        public string buttonVisible;
        public string ButtonVisible 
        {
            get { return buttonVisible; }
            set
            { 
                buttonVisible = value;
                this.RaisePropertyChanged("ButtonVisible");
            }
        }

        public bool check = false;
        public bool CheckedAny
        {
            get { return check; }
            set
            {
                check = value;
                this.RaisePropertyChanged("CheckedAny");

            }

        }
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> WindowLoadCommand { get; private set; }
        public DelegateCommand<string> CloseCommand { get; private set; }

        private ObservableCollection<InstallationSteps> compList;      
        public ObservableCollection<InstallationSteps> ProcessList
        {
            get { return compList; }
            set
            {
                compList = value;
                this.RaisePropertyChanged("ProcessList");
            }
        }
        public MainWindowViewModel(IRegionManager regionManager,CaseInstallerLogger loggerFacade,IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.loggerFacade = loggerFacade ;    
            this.loggerFacade.Log("Debug",Category.Debug,Priority.High);
            this.WindowLoadCommand= new DelegateCommand<string>(Navigate);
            this.CloseCommand = new DelegateCommand<string>(PerformCloseAction);
            this.NavigateCommand = new DelegateCommand<string>(NavigateNext);
            eventAggregator.GetEvent<RadioEvents>().Subscribe(EventReceived,true);

        }

        private void NavigateNext(string path)
        {
            this.regionManager.RequestNavigate("ContentRegion",path);
            this.ButtonVisible = "Collapsed";
        }

        private void EventReceived(bool enable)
        {
            this.CheckedAny = enable;
        }

        string path = @"D:\DotNET\test";
        private void Navigate(string navigatePath)
        {
            this.regionManager.RequestNavigate("SidePanelRegion","SidePanel");
            if (!Directory.Exists(path))
            { 

                navigatePath = "TrueManagementInstall";
                if (navigatePath != null)
                {
                    this.regionManager.RequestNavigate("ContentRegion", navigatePath);
                    

                    //Directory.CreateDirectory(path);
                }
            }
            else
            {
                navigatePath = "TrueManagementSetting";
                if (navigatePath != null)
                {
                    this.regionManager.RequestNavigate("ContentRegion", navigatePath);
                }
            }
        }
        private void PerformCloseAction(object Parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}




