using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtsWmsCS_6S1InfeedMissionGeneration
{
    public partial class AtsWmsCS_6S1InfeedMissionGeneration : ServiceBase
    {

        static string className = "AtsWmsCS_6S1InfeedMissionGeneration";
        private static readonly ILog Log = LogManager.GetLogger(className);

        public AtsWmsCS_6S1InfeedMissionGeneration()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Debug("OnStart :: AtsWmsTataA1InfeedMissionGeneration in OnStart....");

                try
                {
                    XmlConfigurator.Configure();
                    try
                    {
                        AtsWmsCS_6S1InfeedMissionGenerationTaskThread();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("OnStart :: Exception occured while AtsWmsTataA1InfeedMissionGenerationTaskThread  threads task :: " + ex.Message);
                    }
                    Log.Debug("OnStart :: AtsWmsTataA1InfeedMissionGenerationTaskThread in OnStart ends..!!");
                    //XmlConfigurator.Configure();
                    //Thread staThread = new Thread(new ThreadStart(AtsWmsCS_6S1InfeedMissionGenerationTaskThread));
                    //staThread.SetApartmentState(ApartmentState.STA);
                    //staThread.Start();
                    //Log.Debug("OnStart :: AtsWmsDispatchOrderServiceTaskThread in OnStart ends..!!");
                }
                catch (Exception ex)
                {
                    Log.Error("OnStart :: Exception occured in OnStart :: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error("OnStart :: Exception occured in OnStart :: " + ex.Message);
            }
        }

        public async void AtsWmsCS_6S1InfeedMissionGenerationTaskThread()
        {
            await Task.Run(() =>
            {
                try
                {
                    AtsWmsCS_6S1InfeedMissionGenerationDetails AtsWmsCS_6S1InfeedMissionGenerationDetailsInstance = new AtsWmsCS_6S1InfeedMissionGenerationDetails();
                    AtsWmsCS_6S1InfeedMissionGenerationDetailsInstance.startOperation();
                }
                catch (Exception ex)
                {
                    Log.Error("TestService :: Exception in AtsWmsTataA1InfeedMissionGenerationTaskThread :: " + ex.Message);
                }

            });
        }

        //public void AtsWmsCS_6S1InfeedMissionGenerationTaskThread()
        //{
        //    try
        //    {
        //        AtsWmsCS_6S1InfeedMissionGenerationDetails AtsWmsCS_6S1InfeedMissionGenerationDetailsInstance = new AtsWmsCS_6S1InfeedMissionGenerationDetails();
        //        AtsWmsCS_6S1InfeedMissionGenerationDetailsInstance.startOperation();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("TestService :: Exception in AtsWmsDispatchOrderServiceTaskThread :: " + ex.Message);
        //    }
        //}
        protected override void OnStop()
        {
        }
    }
}
