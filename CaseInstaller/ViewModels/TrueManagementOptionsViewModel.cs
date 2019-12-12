using CaseInstaller.Crypto;
using CaseInstaller.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace CaseInstaller.ViewModels
{
    class TrueManagementOptionsViewModel:CaseInstallerBase 
    {
        private readonly IRegionManager regionManager;
       
        public DelegateCommand GoBackCommand { get; private set; }

        public DelegateCommand<string> WindowLoadCommand { get; private set; }

        public DelegateCommand<object> InstallCommand { get; private set; }

        private string trueurl;
        private string authorization;
        private string sqlserver;
        private string sqluser;
        private object sqlpassword;


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
                 new InstallationSteps {Process="Initial Configurations" , Progresses=2 },
                 new InstallationSteps {Process="Configuration Settings" , Progresses=1 },
                 new InstallationSteps {Process="Legal Agreement" , Progresses=0 },
                 new InstallationSteps {Process="Summary" , Progresses=0 }
            };
            return processes;
        }

        //Constructor
        public TrueManagementOptionsViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            this.InstallCommand = new DelegateCommand<object>(createxml);

            this.WindowLoadCommand = new DelegateCommand<string>(LoadData);

            this.GoBackCommand = new DelegateCommand(GoBack);
        }


        public string Authorization
        {
            get { return authorization; }
            set
            {
                SetProperty(ref authorization, value);
            }
        }

        public string TrueURL
        {
            get { return trueurl; }
            set
                 {
                    SetProperty(ref trueurl, value);
                 }
        }

        public string SQL_Server
        {
            get { return sqlserver; }
            set
            {
                SetProperty(ref sqlserver, value);
            }
        }

        public string SQL_User
        {
            get { return sqluser; }
            set
            {
                SetProperty(ref sqluser, value);
            }
        }

        public object SQL_Password
        {
            get { return sqlpassword; }
            set
            {
                SetProperty(ref sqlpassword, value);
            }
        }


        /// <summary>
        /// Serialize to XML
        /// </summary>
        /// <param name="serialiseData"></param>
        public void createxml(object pword)
        {
            var passwordBox = pword as PasswordBox;
            string password = passwordBox.Password;
            OptionData optionData = new OptionData();
            optionData.TrueCare_Url = this.TrueURL ;
            optionData.OAuth2_ClientID = this.Authorization;
            optionData.MSSQL_Server = this.SQL_Server;
            optionData.MSSQL_User = this.SQL_User;
            optionData.MSSQL_Password = EncAndDec.Encrypt(password) ; 

            GenericXmlOps<OptionData> serializer = new GenericXmlOps<OptionData>();
            string xml = serializer.Serialize(optionData);
            CaseInstallerBase caseInstallerBase = new CaseInstallerBase();

            this.regionManager.RequestNavigate("ContentRegion", "TrueManagementLegal");

            //string navigatePath = "TrueManagementLegal";
            //if (navigatePath != null)
            //{
            //    this.regionManager.RequestNavigate("ContentRegion", navigatePath);
            //}

            //var ProcessListTemp = this.ProcessList;
            //ProcessListTemp.Where(count => count.Process == "Initial Configurations").Select(s => s.Progresses = 2).ToList();
            //ProcessListTemp.Where(count => count.Process == "Configuration Settings").Select(s => s.Progresses = 2).ToList();
            //ProcessListTemp.Where(count => count.Process == "Legal Agreement").Select(s => s.Progresses = 1).ToList();
            //this.ProcessList = new ObservableCollection<InstallationSteps>(ProcessListTemp);
        }

        
        /// <summary>
        /// Deserialize from XML
        /// </summary>
        /// <param name="DeserialiseData"></param>
        public void LoadData(object deserialiseData)
        {
            this.ProcessList = new ObservableCollection<InstallationSteps>(Process());

            if (File.Exists(@"D:\\DotNET\\test\\CaseInstaller.xml"))
            {
                GenericXmlOps<OptionData> serializer = new GenericXmlOps<OptionData>();
                var deserilizedObject = serializer.Deserialize();
                this.TrueURL = deserilizedObject.TrueCare_Url;
                this.Authorization = deserilizedObject.OAuth2_ClientID;
                this.SQL_Server = deserilizedObject.MSSQL_Server;
                this.SQL_User = deserilizedObject.MSSQL_User;
                string password = EncAndDec.Decrypt(deserilizedObject.MSSQL_Password);
                this.SQL_Password = password;


            }
            else
            {
                this.TrueURL = String.Empty;
                this.Authorization = String.Empty;
                this.SQL_Server = String.Empty;
                this.SQL_User = String.Empty;
                this.SQL_Password = String.Empty;
            }
        }

       
    }
}
