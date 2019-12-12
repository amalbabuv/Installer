using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Threading;

namespace CaseInstaller.ViewModels
{
    public class ProgressControlViewModel:BindableBase
    {

        #region PROPERTIES
        private int progressCount = 0;
        private string progressPercentage = "0%";
        private string buttonVisible = "Hidden";
        private string installprogress = "Installing...";
        CaseInstallerLogger installerLogger;



        public string InstallationPercentage
        {
            get
            {
                return progressPercentage;
            }
            set
            {
                progressPercentage = value;
                this.RaisePropertyChanged("InstallationPercentage");

            }
        }


        public int ProgressValue
        {
            get
            {
                return progressCount;
            }
            set
            {
                progressCount = value;
                this.RaisePropertyChanged("ProgressValue");

            }
        }

        public string InstallationProgress
        {
            get
            {
                return installprogress;
            }
            set
            {
                installprogress = value;
                this.RaisePropertyChanged("InstallationProgress");

            }
        }


        public string ButtonVisible
        {
            get
            {
                return buttonVisible;
            }
            set
            {
                buttonVisible = value;
                this.RaisePropertyChanged("ButtonVisible");

            }
        }

        #endregion

        IRegionManager regionManager;

        // DispatcherTimer Progresstimer = new DispatcherTimer();
        public DelegateCommand WindowLoadCommand { get; set; }

        public DelegateCommand ModifyCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="installerLogger"></param>
        public ProgressControlViewModel(IRegionManager regionManager, CaseInstallerLogger installerLogger) 
        {
            this.installerLogger = installerLogger;
            this.regionManager = regionManager;
            this.WindowLoadCommand = new DelegateCommand(LoadProgress);
            this.ModifyCommand = new DelegateCommand(FinalPage);
        }

        
        private void FinalPage()
        {
            this.regionManager.RequestNavigate("ContentRegion","TrueManagementFinal");
        }

       

        /// <summary>
        ///Loading the values to the progressbar
        /// </summary>
        private void LoadProgress()
        {

            // this.ProgressValue = 80;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (s, ea) =>
            {
                this.ProgressValue++;
                string percent = (this.ProgressValue / 10).ToString();
                this.InstallationPercentage = percent+"%";
                if (ProgressValue == 1000)
                {
                    timer.Stop();
                    this.ButtonVisible = "Visible";
                    this.InstallationProgress = "Completed";
                    this.installerLogger.Log("Installed", Prism.Logging.Category.Debug, Prism.Logging.Priority.High);

                }
            };
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);  
            timer.Start();
        }
    }
}
