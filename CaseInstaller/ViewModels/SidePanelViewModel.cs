using CaseInstaller.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseInstaller.ViewModels
{
    public class SidePanelViewModel:BindableBase
    {
        private ObservableCollection<InstallationSteps> compList;

        public DelegateCommand WindowLoadCommand { get; set; }
        public ObservableCollection<InstallationSteps>ProcessList
        {
            get { return compList; }
            set
            {
                compList = value;
                this.RaisePropertyChanged("ProcessList");
            }
        }
        public SidePanelViewModel()
        {
            this.WindowLoadCommand = new DelegateCommand(Loading);
        }

        private void Loading()
        {
            var processes = new List<InstallationSteps>
            {
                 new InstallationSteps {Process="Initial Configurations" , Progresses=0 },
                 new InstallationSteps {Process="Configuration  Settings" , Progresses=0 },
                 new InstallationSteps {Process="Legal Agreement" , Progresses=0 },
                 new InstallationSteps {Process="Summary" , Progresses=0 }
            };
            this.ProcessList = new ObservableCollection<InstallationSteps>(processes);
        }
    }
}
