using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseInstaller.ViewModels
{
    class TrueManagementLegalViewModel:CaseInstallerBase
    {

        #region Properties
        public string legalText1= "1. Copy the jar & config file to a Standard install location is                                                             “C:/TruCareSuite/TrucareInsights” or “C:/TruCare Insights”." ;

        public string legalText2 = "2. Stops any currently runnning Trucare Insights services.";

        public string legalText3 = "3.Then uses NSSM(or W/e we bundle with the installer) to configure a service to run the jar.";

        public string legalText4 = "4. Starts the TruCare Insights services.";

        public string legalText21 = "1. Verify TruCare exists and is a compatible version.";

        public string legalText22 = "2. Instal/Overwrite analytics views compatible with the version of TruCare.";

        public string legalText23 = "3.Run new migrations on the TruCare Insights database.";

        public bool accept = false;

        public bool reject = false; 

        public bool acceptchange = false;

        public bool previouscheck=true;

        public string LegalText1 {
            get { return legalText1; }
            set { SetProperty(ref legalText1, value); }
        }

        public string LegalText2
        {
            get { return legalText2; }
            set { SetProperty(ref legalText2, value); }
        }

        public string LegalText3
        {
            get { return legalText3; }
            set { SetProperty(ref legalText3, value); }
        }

        public string LegalText4
        {
            get { return legalText4; }
            set { SetProperty(ref legalText4, value); }
        }


        public string LegalText21
        {
            get { return legalText21; }
            set { SetProperty(ref legalText21, value); }
        }


        public string LegalText22
        {
            get { return legalText22; }
            set { SetProperty(ref legalText22, value); }
        }


        public string LegalText23
        {
            get { return legalText23; }
            set { SetProperty(ref legalText23, value); }
        }

        public bool Accept
        {
            get { return accept; }
            set
            {
                accept = value;
                this.RaisePropertyChanged("Accept");
            }
        }

        public bool Rejected
        {
            get { return reject; }

            set
            {
                reject = value;
                this.RaisePropertyChanged("Rejected");
            }
        }

        public bool AcceptChecked {
            get { return acceptchange; }
            set
            {
                acceptchange = value;
                this.RaisePropertyChanged("AcceptChecked");
            }
        }

        public bool PreviousChecked
        {

            get { return previouscheck; }
            set
            {
                previouscheck = value;
                this.RaisePropertyChanged("PreviousChecked");
            }

        }

        #endregion
        private readonly IRegionManager regionManager;

        
        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand WindowLoadCommand { get; set; }

        public DelegateCommand AcceptCheckCommand { get; set; }

        public DelegateCommand RejectCheckCommand { get; set; }

        public DelegateCommand<string> AcceptCommand { get; private set; }

        public TrueManagementLegalViewModel(IRegionManager regionManager)
        {
            
            
            this.regionManager = regionManager;
            this.GoBackCommand = new DelegateCommand(GoBack);
            this.AcceptCommand = new DelegateCommand<string>(Navigate);
            //this.WindowLoadCommand = new DelegateCommand(LoadAction);
            this.AcceptCheckCommand = new DelegateCommand(LegalAccepted);
            this.RejectCheckCommand = new DelegateCommand(LegalRejected);
        }

        private void LegalRejected()
        {
            this.Accept = false;
            this.PreviousChecked = true;
            if (this.Accept==false)
            {
                this.AcceptChecked = false;

            }

        }

        private void LegalAccepted()
        {
            this.Accept = true;
            this.PreviousChecked = false;
            if (this.Accept == true)
            {
                this.AcceptChecked = true;
                
            }

        }

        

        public void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                this.regionManager.RequestNavigate("ContentRegion", navigatePath);
                
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = @" -executionpolicy unrestricted C:\Users\amal.babu\Documents\FirstScript.ps1";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
        }
    }
}
