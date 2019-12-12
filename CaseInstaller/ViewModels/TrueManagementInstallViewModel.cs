using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using CaseInstaller.Models;
using System.Collections.ObjectModel;

namespace CaseInstaller.ViewModels
{
    public class TrueManagementInstallViewModel : CaseInstallerBase
    {
        private readonly IRegionManager regionManager;

        private string previousVisible = "Hidden";

        public string PreviousVisible
        {
            get { return previousVisible; }
            set { SetProperty(ref previousVisible, value); }
        }


        public DelegateCommand<string> InstallCommand { get; private set; }

        public DelegateCommand WindowLoadCommand { get; set; }

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

        
        public TrueManagementInstallViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.InstallCommand = new DelegateCommand<string>(Navigate);
            this.WindowLoadCommand = new DelegateCommand(LoadAction);
        }

        private void LoadAction()
        {

            this.ProcessList = new ObservableCollection<InstallationSteps>(Process());
            
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {

                var ProcessListTemp = this.ProcessList;              
                ProcessListTemp.Where(count => count.Process == "Initial Configurations").Select(s => s.Progresses = 2).ToList();
                ProcessListTemp.Where(count => count.Process == "Configuration Settings").Select(s => s.Progresses = 1).ToList();
                this.ProcessList = new ObservableCollection<InstallationSteps>(ProcessListTemp);
                this.PreviousVisible = "Visible";              
                this.regionManager.RequestNavigate("ContentRegion", navigatePath);
  
            }


        }

    }
}
