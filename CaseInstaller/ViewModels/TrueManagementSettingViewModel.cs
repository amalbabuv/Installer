using Prism.Commands;
using Prism.Regions;
using System.IO;
using CaseInstaller.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Prism.Events;
using CaseInstaller.Events;

namespace CaseInstaller.ViewModels
{
    class TrueManagementSettingViewModel : CaseInstallerBase
    {
        private readonly IRegionManager regionManager;
        private readonly DialogueService dialogservice;

        public bool check=false;

        public bool CheckedAny
        { 
            get { return check; }
            set 
            {
                check = value;
                this.RaisePropertyChanged("CheckedAny");
            
            }
           
        }

        public DelegateCommand ModifyCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand UninstallCommand { get; private set; }

        IEventAggregator eventAggregator;
        public DelegateCommand WindowLoadCommand { get; set; }

        public DelegateCommand<string> NavigateCommand { get; set; }

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

        public List<InstallationSteps> Process()
        {
            var processes = new List<InstallationSteps>
            {
                 new InstallationSteps {Process="Initial Configurations" , Progresses=1 },
                 new InstallationSteps {Process="Configuration Settings" , Progresses=0 },
                 new InstallationSteps {Process="Legal Agreement" , Progresses=0 },
                 new InstallationSteps {Process="Summary" , Progresses=0 }
            };
            return processes;
        }

        public TrueManagementSettingViewModel(IRegionManager regionManager,DialogueService dialogService, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.dialogservice = dialogService;
            this.eventAggregator = eventAggregator;
            this.ModifyCommand = new DelegateCommand(Modify);
            this.UpdateCommand = new DelegateCommand(Update);
            this.UninstallCommand = new DelegateCommand(uninstall);
            this.WindowLoadCommand = new DelegateCommand(LoadAction);
            this.NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatepath)
        {
            this.regionManager.RequestNavigate("ContentRegion",navigatepath);
        }
        public bool checkedornot = true;
        private void Update()
        {
            //this.CheckedAny = true;

            

            this.eventAggregator.GetEvent<RadioEvents>().Publish(checkedornot);
        }

        private void LoadAction()
        {
            this.ProcessList = new ObservableCollection<InstallationSteps>(Process());
        }

        private void Modify()
        {
            this.eventAggregator.GetEvent<RadioEvents>().Publish(checkedornot);
            //this.CheckedAny = true;
            //if (navigatePath != null)
            //{
            //    this.regionManager.RequestNavigate("ContentRegion", navigatePath);
            //}
        }


        //string path = @"D:\DotNET\test";
        private void uninstall()
        {
            this.eventAggregator.GetEvent<RadioEvents>().Publish(checkedornot);
            //this.CheckedAny = true;

            //if (Directory.Exists(path))
            //{
            //   if (navigatePath != null)
            //   {
            //       dialogservice.ShowMessage();
            //       if (dialogservice.Confirm == true)
            //       {
            //           this.regionManager.RequestNavigate("ContentRegion", navigatePath);
            //           Directory.Delete(path,true);
            //       }

            //   }
            //}
        }
    }
}
