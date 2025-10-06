using AtsWmsCS_6S1InfeedMissionGeneration.ats_tata_metallics_dbDataSetTableAdapters;
using log4net;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static AtsWmsCS_6S1InfeedMissionGeneration.ats_tata_metallics_dbDataSet;

namespace AtsWmsCS_6S1InfeedMissionGeneration
{
    class AtsWmsCS_6S1InfeedMissionGenerationDetails
    {
        #region Global Variables
        static string className = "AtsWmsCS_6S1InfeedMissionGenerationDetails";
        private static readonly ILog Log = LogManager.GetLogger(className);
        private System.Timers.Timer TataA1InfeedMissionGenTimer = null;
        int areaId = 1;
        Boolean sameMaterialRackNotFound = false;
        string palletPresentOnPickup = "";
        string palletCodeOnPickUp = "";
        bool sameMaterialRackNotFoundS2 = false;
        int checkId = 0;
        string checkPoint = "";
        #endregion

        #region DataTables
        ats_wms_infeed_mission_runtime_detailsDataTable ats_wms_infeed_mission_runtime_detailsDataTableDT = null;
        ats_wms_infeed_mission_runtime_detailsDataTable ats_wms_infeed_mission_runtime_detailsDataTablein_progressDT = null;
        ats_wms_infeed_mission_runtime_detailsDataTable ats_wms_infeed_mission_runtime_detailsDataTableRejectIn_progressCheck = null;
        ats_wms_master_plc_connection_detailsDataTable ats_wms_master_plc_connection_detailsDataTableDT = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableDT = null;
        ats_wms_mapping_floor_area_detailsDataTable ats_wms_mapping_floor_area_detailsDataTableDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableDT = null;
        ats_wms_master_rack_detailsDataTable ats_wms_master_rack_detailsDataTableDT = null;
        ats_wms_current_stock_detailsDataTable ats_wms_current_stock_detailsDataTableDT = null;
        ats_wms_master_shift_detailsDataTable ats_wms_master_shift_detailsDataTableDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTablePositionAllocatedInRackDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableEmptyRackDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableFrontPosLockDT = null;
        ats_wms_master_position_detailsDataTable ats_wms_master_position_detailsDataTableDeadCellDT = null;
        ats_wms_outfeed_mission_runtime_detailsDataTable ats_wms_outfeed_mission_runtime_detailsDataTableDT = null;
        ats_wms_transfer_pallet_mission_runtime_detailsDataTable ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = null;
        ats_wms_master_product_variant_detailsDataTable ats_wms_master_product_variant_detailsDataTableDT = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableDT1 = null;
        ats_wms_infeed_mission_check_detailsDataTable ats_wms_infeed_mission_check_detailsDataTableDT = null;
        ats_wms_current_stock_detailsDataTable ats_wms_current_stock_detailsDataTableDTDuplicateCheck = null;
        ats_wms_current_stock_detailsDataTable ats_wms_current_stock_detailsDataTableDTCheckingDummyPallet = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableDTCheckDuplicate = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT = null;
        ats_wms_master_pallet_informationDataTable ats_wms_master_pallet_informationDataTableEmptyStatusDT = null;
        #endregion

        #region TableAdapters
        ats_wms_infeed_mission_runtime_detailsTableAdapter ats_wms_infeed_mission_runtime_detailsTableAdapterInstance = new ats_wms_infeed_mission_runtime_detailsTableAdapter();
        ats_wms_master_plc_connection_detailsTableAdapter ats_wms_master_plc_connection_detailsTableAdapterInstance = new ats_wms_master_plc_connection_detailsTableAdapter();
        ats_wms_master_pallet_informationTableAdapter ats_wms_master_pallet_informationTableAdapterInstance = new ats_wms_master_pallet_informationTableAdapter();
        ats_wms_mapping_floor_area_detailsTableAdapter ats_wms_mapping_floor_area_detailsTableAdapterInstance = new ats_wms_mapping_floor_area_detailsTableAdapter();
        ats_wms_master_position_detailsTableAdapter ats_wms_master_position_detailsTableAdapterInstance = new ats_wms_master_position_detailsTableAdapter();
        ats_wms_master_position_detailsTableAdapter ats_wms_master_position_detailsTableAdapterSeconPositionInstance = new ats_wms_master_position_detailsTableAdapter();
        ats_wms_master_position_detailsTableAdapter ats_wms_master_position_detailsTableAdapterDeadCellTableAdapterInstance = new ats_wms_master_position_detailsTableAdapter();
        ats_wms_master_rack_detailsTableAdapter ats_wms_master_rack_detailsTableAdapterInstance = new ats_wms_master_rack_detailsTableAdapter();
        ats_wms_current_stock_detailsTableAdapter ats_wms_current_stock_detailsTableAdapterInstance = new ats_wms_current_stock_detailsTableAdapter();
        ats_wms_master_shift_detailsTableAdapter ats_wms_master_shift_detailsTableAdapterInstance = new ats_wms_master_shift_detailsTableAdapter();
        ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance = new ats_wms_transfer_pallet_mission_runtime_detailsTableAdapter();
        ats_wms_outfeed_mission_runtime_detailsTableAdapter ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance = new ats_wms_outfeed_mission_runtime_detailsTableAdapter();
        ats_wms_master_product_variant_detailsTableAdapter ats_wms_master_product_variant_detailsTableAdapterInstance = new ats_wms_master_product_variant_detailsTableAdapter();
        ats_wms_infeed_mission_check_detailsTableAdapter ats_wms_infeed_mission_check_detailsTableAdapterInstance = new ats_wms_infeed_mission_check_detailsTableAdapter();
        ats_wms_ccm_buffer_detailsTableAdapter ats_wms_ccm_buffer_detailsTableAdapterInstance = new ats_wms_ccm_buffer_detailsTableAdapter();
        #endregion

        #region PLC PING VARIABLE   
        string IP_ADDRESS = "";
        private Ping pingSenderForThisConnection = null;
        private PingReply replyForThisConnection = null;
        private Boolean pingStatus = false;
        private int serverPingStatusCount = 0;
        #endregion

        #region KEPWARE VARIABLES

        /* Kepware variable*/

        OPCServer ConnectedOpc = new OPCServer();


        Array OPCItemIDs = Array.CreateInstance(typeof(string), 100);
        Array ItemServerHandles = Array.CreateInstance(typeof(Int32), 100);
        Array ItemServerErrors = Array.CreateInstance(typeof(Int32), 100);
        Array ClientHandles = Array.CreateInstance(typeof(Int32), 100);
        Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), 100);
        Array AccessPaths = Array.CreateInstance(typeof(string), 100);
        Array ItemServerValues = Array.CreateInstance(typeof(string), 100);
        OPCGroup CS6InfeedOPC;
        // object kDIR;
        //object lDIR;
        object kDIR;
        object lDIR;

        // Connection string
        static string plcServerConnectionString = null;

        #endregion

        public void startOperation()
        {
            try
            {
                //Timer 
                TataA1InfeedMissionGenTimer = new System.Timers.Timer();
                //Running the function after 1 sec 
                TataA1InfeedMissionGenTimer.Interval = (1000);
                //to reset timer after completion of 1 cycle
                TataA1InfeedMissionGenTimer.AutoReset = true;
                //Enabling the timer
                TataA1InfeedMissionGenTimer.Enabled = true;
                //Timer Start
                TataA1InfeedMissionGenTimer.Start();
                //After 1 sec timer will elapse and DataFetchDetailsOperation function will be called 
                TataA1InfeedMissionGenTimer.Elapsed += new System.Timers.ElapsedEventHandler(AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation);
            }
            catch (Exception ex)
            {
                Log.Error("startOperation :: Exception Occure in TataA1InfeedMissionGenTimer" + ex.Message);
            }
        }

        public void AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation(object sender, EventArgs args)
        {
            try
            {
                Log.Debug("2");
                try
                {
                    TataA1InfeedMissionGenTimer.Stop();
                }
                catch (Exception ex)
                {
                    Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occure while stopping the timer :: " + ex.Message + "StackTrace  :: " + ex.StackTrace);
                }

                try
                {
                    //Fetching PLC data from DB by sending PLC connection IP address
                    try
                    {
                        ats_wms_master_plc_connection_detailsDataTableDT = ats_wms_master_plc_connection_detailsTableAdapterInstance.GetData();
                        IP_ADDRESS = ats_wms_master_plc_connection_detailsDataTableDT[0].PLC_CONNECTION_IP_ADDRESS;
                        Log.Debug("2.1.1 :: IP_ADDRESS ::" + IP_ADDRESS);
                    }
                    catch (Exception ex)
                    {

                        Log.Error("a1MasterGiveMissionOperation :: Exception Occure while reading machine datasource connection IP_ADDRESS :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                    }
                    ats_wms_master_plc_connection_detailsDataTableDT = ats_wms_master_plc_connection_detailsTableAdapterInstance.GetDataByPLC_CONNECTION_IP_ADDRESS(IP_ADDRESS);
                    Log.Debug("2.1");
                }
                catch (Exception ex)
                {
                    Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception Occure while reading machine datasource connection details from the database :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                }


                // Check PLC Ping Status
                try
                {
                    //Checking the PLC ping status by a method
                    pingStatus = checkPlcPingRequest();
                    Log.Debug("2.2");
                }
                catch (Exception ex)
                {
                    Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception while checking plc ping status :: " + ex.Message + " stactTrace :: " + ex.StackTrace);
                }

                if (pingStatus == true)
                //if (true)
                {
                    try
                    {
                        Log.Debug("3");
                        //checking if the PLC data from DB is retrived or not
                        if (ats_wms_master_plc_connection_detailsDataTableDT != null && ats_wms_master_plc_connection_detailsDataTableDT.Count != 0)
                        //if (true)
                        {
                            try
                            {
                                plcServerConnectionString = ats_wms_master_plc_connection_detailsDataTableDT[0].PLC_CONNECTION_URL;
                            }
                            catch (Exception ex)
                            {
                                Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while getting plcServerConnectionString ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                            }
                            try
                            {
                                //Calling the connection method for PLC connection
                                OnConnectPLC();
                            }
                            catch (Exception ex)
                            {
                                Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception while connecting to plc :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                            }

                            try
                            {
                                // Check the PLC connected status
                                if (ConnectedOpc.ServerState.ToString().Equals("1"))
                                //if (true)
                                {
                                    Log.Debug("4");
                                    //Bussiness logic


                                    try
                                    {
                                        Log.Debug("5");

                                        try
                                        {
                                            Log.Debug("Getting Infeed mission check details");
                                            ats_wms_infeed_mission_check_detailsDataTableDT = ats_wms_infeed_mission_check_detailsTableAdapterInstance.GetData();

                                            if (ats_wms_infeed_mission_check_detailsDataTableDT != null && ats_wms_infeed_mission_check_detailsDataTableDT.Count > 0)
                                            {
                                                Log.Debug("Found mission details count :: " + ats_wms_infeed_mission_check_detailsDataTableDT.Count);
                                                for (int x = 0; x < ats_wms_infeed_mission_check_detailsDataTableDT.Count; x++)
                                                {
                                                    Thread.Sleep(2000);
                                                    Log.Debug("Looking for infeed mission MISSION_LOCATION :: " + ats_wms_infeed_mission_check_detailsDataTableDT[x].MISSION_LOCATION + " :: INFEED_MISSION_CHECK_ID :: " + ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID);


                                                    checkId = ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID;
                                                    checkPoint = ats_wms_infeed_mission_check_detailsDataTableDT[x].MISSION_LOCATION;

                                                    try
                                                    {
                                                        Log.Debug("Checking if the mission is already ready or inprogress for coreshop :: " + ats_wms_infeed_mission_check_detailsDataTableDT[x].MISSION_LOCATION);
                                                        ats_wms_infeed_mission_runtime_detailsDataTablein_progressDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByCORESHOPandINFEED_MISSION_STATUSOrINFEED_MISSION_STATUS1(ats_wms_infeed_mission_check_detailsDataTableDT[x].MISSION_LOCATION, "READY", "IN_PROGRESS");

                                                        if (ats_wms_infeed_mission_runtime_detailsDataTablein_progressDT != null && ats_wms_infeed_mission_runtime_detailsDataTablein_progressDT.Count == 0)
                                                        {
                                                            #region checkPalletPresent
                                                            Log.Debug("IN checkPalletPresent with checkPoint :: " + checkPoint + " :: checkId :: " + checkId);

                                                            if ((ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID == 1) || (ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID == 2) || (ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID == 3) || (ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID == 4))
                                                            {
                                                                Log.Debug("A1");
                                                                //ats_wms_infeed_mission_check_detailsDataTableDT = ats_wms_infeed_mission_check_detailsTableAdapterInstance.GetDataByINFEED_MISSION_CHECK_ID(ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID);

                                                                //if (ats_wms_infeed_mission_check_detailsDataTableDT != null && ats_wms_infeed_mission_check_detailsDataTableDT.Count > 0)
                                                                {
                                                                    Log.Debug("A2");
                                                                    palletPresentOnPickup = readTag(ats_wms_infeed_mission_check_detailsDataTableDT[x].PALLET_PRESENT_ON_PICKUP_TAG);

                                                                    if (palletPresentOnPickup.Equals("True"))
                                                                    {
                                                                        Log.Debug("A3");
                                                                        palletCodeOnPickUp = readTag(ats_wms_infeed_mission_check_detailsDataTableDT[x].PALLET_CODE_ON_PICKUP_TAG);

                                                                        if (palletCodeOnPickUp.Length == 4)
                                                                        {
                                                                            Log.Debug("A4");
                                                                            Log.Debug("Checking pallet information details is available from DB");
                                                                            ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnPickUp);

                                                                            if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                                                                            {
                                                                                Log.Debug("A5");
                                                                                try
                                                                                {

                                                                                    int lodingstationworkdone = ats_wms_master_pallet_informationDataTableDT[0].LOADING_STATION_WORKDONE;

                                                                                    if (lodingstationworkdone == 1)
                                                                                    {


                                                                                        // from rework station empty pallet might come so checking pallet status not 3 or equal to 3
                                                                                        Log.Debug("A6");
                                                                                        Log.Debug("10 Checking if the id is already generated in the infeed mission");
                                                                                        if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 0 && (ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID != 3 || ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID == 3))
                                                                                        {
                                                                                            Log.Debug("11 Infeed mission is not generated");


                                                                                            try

                                                                                            {
                                                                                                bool isMissionGenarated = false;
                                                                                                Log.Debug("12 Searching active floors for the area Id :: " + areaId);
                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_IS_ACTIVE(areaId, 1);
                                                                                                if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                                                                                                {
                                                                                                    Log.Debug("13 Found active floors count :: " + ats_wms_mapping_floor_area_detailsDataTableDT.Count);
                                                                                                    for (int i = 0; i < ats_wms_mapping_floor_area_detailsDataTableDT.Count; i++)
                                                                                                    {
                                                                                                        Log.Debug("14 Searching empty position on the floor id :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);


                                                                                                        Log.Debug("inside racklist");
                                                                                                        List<int> rackList = null;
                                                                                                        try
                                                                                                        {
                                                                                                            rackList = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDistinctRack_IDwherePRODUCT_VARIENT_CODEAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_VARIANT_CODE, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                            //ats_wms_current_stock_detailsDataTableDT = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDISTRACK_IDWherePRODUCT_VARIANT_CODEAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_VARIANT_CODE, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);

                                                                                                        }
                                                                                                        catch (Exception ex)
                                                                                                        {
                                                                                                            Log.Error("generateTheMission :: Exception occured while reading rack details :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                                                                                        }

                                                                                                        if (rackList != null && rackList.Count > 0)
                                                                                                        //if (ats_wms_current_stock_detailsDataTableDT != null && ats_wms_current_stock_detailsDataTableDT.Count > 0)
                                                                                                        {
                                                                                                            Log.Debug("getting distinct rack " + rackList.Count);


                                                                                                            for (int j = 0; j < rackList.Count; j++)
                                                                                                            {

                                                                                                                List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                int currentRack = rackList[j];
                                                                                                                if (!rackListColumn1.Contains(currentRack))
                                                                                                                {


                                                                                                                    //
                                                                                                                    ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDDesc(rackList[j], 1, 0, 1);
                                                                                                                    //ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList[j], 1, 0, 1);

                                                                                                                    Log.Debug("getting empty positions");

                                                                                                                    if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count > 0)
                                                                                                                    {
                                                                                                                        ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDLessThan(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                        Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                        if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                        {
                                                                                                                            ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                            if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                            {
                                                                                                                                Log.Debug("getting active rack");
                                                                                                                                try
                                                                                                                                {

                                                                                                                                    ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                    ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                    ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                }
                                                                                                                                catch (Exception ex)
                                                                                                                                {
                                                                                                                                    Log.Error("exception occured while checking inprogress missions" + ex.Message + ex.StackTrace);
                                                                                                                                }



                                                                                                                                if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count == 0 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count == 0)
                                                                                                                                {
                                                                                                                                    ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                    if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                    {
                                                                                                                                        if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                        {
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                Log.Debug("22 Inserting data in infeed table");

                                                                                                                                                string shiftName = "";
                                                                                                                                                int shiftId = 0;
                                                                                                                                                DateTime now = DateTime.Now;
                                                                                                                                                ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                {
                                                                                                                                                    shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                    shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                    //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                    if (data != null && data.Count>0)
                                                                                                                                                    {
                                                                                                                                                        shiftName = data[0].SHIFT_NAME;
                                                                                                                                                        shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                try
                                                                                                                                                {

                                                                                                                                                    Thread.Sleep(1000);
                                                                                                                                                    //Inserting data in infeed mission table
                                                                                                                                                    Log.Debug("Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                    Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                    Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE);
                                                                                                                                                    Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);
                                                                                                                                                    Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME);
                                                                                                                                                    Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);
                                                                                                                                                    Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME);
                                                                                                                                                    Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                    Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME);
                                                                                                                                                    Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE);
                                                                                                                                                    Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                    Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                    Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST1_POSITION_NAME);
                                                                                                                                                    Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST1_POSITION_NUMBER_IN_RACK);
                                                                                                                                                    //Log.Debug("SERIAL_NUMBER :: " + ats_wms_master_pallet_informationDataTableDT[0].SERIAL_NUMBER);

                                                                                                                                                    Log.Debug("AAAA");
                                                                                                                                                    ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID,
                                                                                                                                                      ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_ID,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_NAME,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].CORE_SIZE,
                                                                                                                                                    0,
                                                                                                                                                     ats_wms_master_pallet_informationDataTableDT[0].QUANTITY,
                                                                                                                                                     ats_wms_master_pallet_informationDataTableDT[0].CORESHOP,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_NAME,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_SIDE,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].ST1_POSITION_NUMBER_IN_RACK,
                                                                                                                                                    //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                    //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                    shiftId,
                                                                                                                                                    shiftName,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_NAME,
                                                                                                                                                    DateTime.Now,
                                                                                                                                                    null,
                                                                                                                                                    null,
                                                                                                                                                    "READY",
                                                                                                                                                    0,
                                                                                                                                                    "NA",
                                                                                                                                                    "NA",
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_TYPE_ID, 1

                                                                                                                                                           );
                                                                                                                                                Log.Debug("23 Data inserted in infeed mission table :: ");
                                                                                                                                                }
                                                                                                                                                catch (Exception ex)
                                                                                                                                                {

                                                                                                                                                    Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                }

                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    //update is infeed mission generated in pallet information table 
                                                                                                                                                    ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                    Log.Debug("24 Is mission generated value updated in pallet information table:: ");
                                                                                                                                                }
                                                                                                                                                catch (Exception ex)
                                                                                                                                                {
                                                                                                                                                    Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                }

                                                                                                                                                try

                                                                                                                                                {
                                                                                                                                                    //update position is allocated in master position table
                                                                                                                                                    ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                                                }
                                                                                                                                                catch (Exception ex)
                                                                                                                                                {
                                                                                                                                                    Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                }

                                                                                                                                                isMissionGenarated = true;
                                                                                                                                                break;

                                                                                                                                            }
                                                                                                                                            catch (Exception ex)
                                                                                                                                            {
                                                                                                                                                Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }


                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    //else
                                                                                                                    ////as the floor 1 does not have the same material rack checking next floor id in loop
                                                                                                                    //{
                                                                                                                    //    continue;
                                                                                                                    //}
                                                                                                                    else
                                                                                                                    {
                                                                                                                        sameMaterialRackNotFound = true;
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            sameMaterialRackNotFound = true;
                                                                                                        }
                                                                                                        //else
                                                                                                        if (sameMaterialRackNotFound == true)
                                                                                                        {
                                                                                                            Log.Debug("120");
                                                                                                            //find empty rack and assign the mission
                                                                                                            List<int> rackList1 = null;
                                                                                                            rackList1 = ats_wms_master_position_detailsTableAdapterInstance.GetDataByDistinctRACK_IDWhereFLOOR_IDAndAREA_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID, 1, 0, 1, 0).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                            Log.Debug("121");
                                                                                                            if (rackList1 != null && rackList1.Count > 0)
                                                                                                            {

                                                                                                                Log.Debug("122");
                                                                                                                for (int k = 0; k < rackList1.Count; k++)
                                                                                                                {

                                                                                                                    List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                    int currentRack = rackList1[k];
                                                                                                                    if (!rackListColumn1.Contains(currentRack))
                                                                                                                    {



                                                                                                                        ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDDesc(rackList1[k], 1, 0, 1);
                                                                                                                        // ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList1[k], 1, 0, 1);
                                                                                                                        if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count == 2)
                                                                                                                        {
                                                                                                                            Log.Debug("123");


                                                                                                                            ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                            if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                            {
                                                                                                                                ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                {
                                                                                                                                    if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                    {
                                                                                                                                        try
                                                                                                                                        {
                                                                                                                                            Log.Debug("42 Inserting data in infeed table");

                                                                                                                                            string shiftName = "";
                                                                                                                                            int shiftId = 0;
                                                                                                                                            DateTime now = DateTime.Now;
                                                                                                                                            ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                            if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                            {
                                                                                                                                                shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                //Log.Debug("Shift name: " + shiftName);
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                if (data != null && data.Count > 0)
                                                                                                                                                {
                                                                                                                                                    shiftName = data[0].SHIFT_NAME;
                                                                                                                                                    shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByFLOOR_ID(ats_wms_master_position_detailsDataTableDT[0].FLOOR_ID);


                                                                                                                                                Thread.Sleep(1000);
                                                                                                                                                //Inserting data in infeed mission table
                                                                                                                                                Log.Debug("Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE);
                                                                                                                                                Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_ID);
                                                                                                                                                Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME);
                                                                                                                                                Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID);
                                                                                                                                                Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME);
                                                                                                                                                Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_NAME);
                                                                                                                                                Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_SIDE);
                                                                                                                                                Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NAME);
                                                                                                                                                Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK);

                                                                                                                                                Log.Debug("BBBB");

                                                                                                                                                ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_ID,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_NAME,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].CORE_SIZE,
                                                                                                                                                    0,
                                                                                                                                                     ats_wms_master_pallet_informationDataTableDT[0].QUANTITY,
                                                                                                                                                     ats_wms_master_pallet_informationDataTableDT[0].CORESHOP,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID,
                                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_NAME,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_SIDE,
                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                    ats_wms_master_position_detailsDataTableDT[0].ST1_POSITION_NUMBER_IN_RACK,
                                                                                                                                                    //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                    //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                    shiftId,
                                                                                                                                                    shiftName,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID,
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_NAME,
                                                                                                                                                    DateTime.Now,
                                                                                                                                                    null,
                                                                                                                                                    null,
                                                                                                                                                    "READY",
                                                                                                                                                    0,
                                                                                                                                                    "NA",
                                                                                                                                                    "NA",
                                                                                                                                                    ats_wms_master_pallet_informationDataTableDT[0].PALLET_TYPE_ID, 1

                                                                                                                                                    );
                                                                                                                                            Log.Debug("23 Data inserted in infeed mission table :: ");
                                                                                                                                            }
                                                                                                                                            catch (Exception ex)
                                                                                                                                            {

                                                                                                                                                Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                            }

                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                //update is infeed mission generated in pallet information table 
                                                                                                                                                ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                Log.Debug("24 Is mission generated value updated in pallet information table:: ");
                                                                                                                                            }
                                                                                                                                            catch (Exception ex)
                                                                                                                                            {
                                                                                                                                                Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                            }

                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                //update position is allocated in master position table
                                                                                                                                                ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                Log.Debug("25 Position is allocated value update in master position table::  :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                            }
                                                                                                                                            catch (Exception ex)
                                                                                                                                            {
                                                                                                                                                Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                            }

                                                                                                                                            isMissionGenarated = true;
                                                                                                                                            break;

                                                                                                                                        }
                                                                                                                                        catch (Exception ex)
                                                                                                                                        {
                                                                                                                                            Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }

                                                                                                                        }

                                                                                                                        else
                                                                                                                        {
                                                                                                                            continue;
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }

                                                                                                        if (isMissionGenarated)
                                                                                                        {
                                                                                                            break;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            continue;
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception ex)
                                                                                            {
                                                                                                Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while getting active floor details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Log.Debug("Infeed mission is already generated");
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Log.Debug("Loading Station Workdone is not a Zero");
                                                                                    }
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                    Log.Error("Exception occured while getting is infeed generated count 0 :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (ats_wms_infeed_mission_check_detailsDataTableDT[x].INFEED_MISSION_CHECK_ID == 5)
                                                            {


                                                                {
                                                                    palletPresentOnPickup = readTag(ats_wms_infeed_mission_check_detailsDataTableDT[x].PALLET_PRESENT_ON_PICKUP_TAG);

                                                                    if (palletPresentOnPickup.Equals("True"))
                                                                    {
                                                                        palletCodeOnPickUp = readTag(ats_wms_infeed_mission_check_detailsDataTableDT[x].PALLET_CODE_ON_PICKUP_TAG);

                                                                        if (palletCodeOnPickUp.Length == 4)
                                                                        {


                                                                            {
                                                                                Log.Debug("6 :: pallet present :: " + palletPresentOnPickup);
                                                                                Log.Debug("7 :: Checking pallet code at pallet pickup position");
                                                                                try
                                                                                {

                                                                                    string palletCodeOnStackerPickupPosition = "";

                                                                                    palletCodeOnStackerPickupPosition = palletCodeOnPickUp;

                                                                                    //TEST_TABLEDataTableDT = TEST_TABLETableAdapterInstance.GetData();

                                                                                    //palletCodeOnStackerPickupPosition = TEST_TABLEDataTableDT[0].PALLET_CODE;

                                                                                    Log.Debug("8 :: .atsWmsBatteryA1InfeedMissionGenerationOperation :: Scanned Pallet Code :: " + palletCodeOnStackerPickupPosition);

                                                                                    //Checking if the pallet code is in standard format
                                                                                    if (palletCodeOnStackerPickupPosition.Length == 4)
                                                                                    {
                                                                                        Log.Debug("9 :: Pallet code is in standard form");
                                                                                        try
                                                                                        {
                                                                                            ats_wms_current_stock_detailsDataTableDTDuplicateCheck = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByPALLET_CODE(palletCodeOnStackerPickupPosition);

                                                                                            if (ats_wms_current_stock_detailsDataTableDTDuplicateCheck != null && ats_wms_current_stock_detailsDataTableDTDuplicateCheck.Count > 0)
                                                                                            {
                                                                                                Log.Debug("9.0.1 :: Duplicate Pallet Code is found in current stock");

                                                                                                Log.Debug("9.0.2 :: Writing in error alert tag");

                                                                                                writeTag("ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_ALERT", "Duplicate Pallet Code");

                                                                                                //writeTag("ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_ALERT", "1");

                                                                                                Log.Debug("9.0.3 :: Written in ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_ALERT");

                                                                                                List<string> dummyPalletList = new List<string> { "9989", "9988", "9987", "9986", "9985", "9984", "9983", "9982", "9981", "9980" };

                                                                                                for (int p = 0; p < dummyPalletList.Count; p++)
                                                                                                {
                                                                                                    Log.Debug("In dummyPalletList :: 1 :: dummyPalletList");

                                                                                                    ats_wms_master_pallet_informationDataTableDTCheckDuplicate = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(dummyPalletList[p]);

                                                                                                    if (ats_wms_master_pallet_informationDataTableDTCheckDuplicate != null && ats_wms_master_pallet_informationDataTableDTCheckDuplicate.Count > 0)
                                                                                                    {

                                                                                                        ats_wms_current_stock_detailsDataTableDTCheckingDummyPallet = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByPALLET_CODE(dummyPalletList[p]);
                                                                                                        Log.Debug("In dummyPalletList :: 2 :: dummyPalletList :: ");

                                                                                                        if (ats_wms_current_stock_detailsDataTableDTCheckingDummyPallet != null && ats_wms_current_stock_detailsDataTableDTCheckingDummyPallet.Count > 0)
                                                                                                        {
                                                                                                            continue;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Log.Debug("Dummy pallet code not found in current stock :: " + dummyPalletList[p]);
                                                                                                            ats_wms_current_stock_detailsTableAdapterInstance.UpdateQueryPALLET_CODE(dummyPalletList[p], ats_wms_current_stock_detailsDataTableDTDuplicateCheck[0].POSITION_ID);
                                                                                                            //palletCodeOnStackerPickupPosition = dummyPalletList[p];
                                                                                                            writeTag("ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_ALERT", "Duplicate Pallet Code");
                                                                                                            Log.Debug("Dummy pallet code :: " + dummyPalletList[p] + ":: Assign to POSITION_ID :: " + ats_wms_current_stock_detailsDataTableDTDuplicateCheck[0].POSITION_ID);
                                                                                                            break;
                                                                                                        }

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Log.Debug("2.1 :: Pallet Details not present in the DB.... Inserting New Information");
                                                                                                        Log.Debug("ats_wms_master_pallet_informationDataTableDTCheckDuplicate[0].PALLET_TYPE_ID "+ ats_wms_master_pallet_informationDataTableDTCheckDuplicate[0].PALLET_TYPE_ID);
                                                                                                        //pallet details are not into database already inserting new 
                                                                                                        ats_wms_master_pallet_informationTableAdapterInstance.Insert(dummyPalletList[p], 0, "NA", "0", 0, "NA", "NA", "NA", 0,
                                                                                                           "NA", "NA", 0, ats_wms_master_pallet_informationDataTableDTCheckDuplicate[0].PALLET_TYPE_ID, 3, "EMPTY", 0, 0, 0, 0, 0, DateTime.Now, 1, "master_user", 0, "0", 0, 0);

                                                                                                    }

                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Log.Debug(" No duplicate pallet code found");
                                                                                                writeTag("ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_ALERT", "0");
                                                                                            }

                                                                                            string palletStatusFromPLC = "";
                                                                                            palletStatusFromPLC = readTag("ATS.WMS_STACKER_2.STACKER_2_PICKUP_POSITION_EMPTY_PALLET_STATUS");

                                                                                            if (palletStatusFromPLC.Equals("3"))
                                                                                            {
                                                                                                Log.Debug("9.1.1 :: Checking pallet information details is available from DB for pallet code :: " + palletCodeOnStackerPickupPosition);
                                                                                                ats_wms_master_pallet_informationDataTableEmptyStatusDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnStackerPickupPosition);
                                                                                                if (ats_wms_master_pallet_informationDataTableEmptyStatusDT != null && ats_wms_master_pallet_informationDataTableEmptyStatusDT.Count > 0)
                                                                                                {
                                                                                                    if (ats_wms_master_pallet_informationDataTableEmptyStatusDT[0].PALLET_STATUS_ID != 3 && ats_wms_master_pallet_informationDataTableEmptyStatusDT[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                    {


                                                                                                        ats_wms_master_pallet_informationTableAdapterInstance.UpdatePalletDateWherePALLET_INFORMATION_ID(palletCodeOnStackerPickupPosition,
                                                                                                            0,
                                                                                                            "NA",
                                                                                                            "NA",
                                                                                                            0,
                                                                                                            "NA",
                                                                                                            "NA", "NA",
                                                                                                            0, "NA",
                                                                                                            0, 3, "EMPTY",
                                                                                                            0, 0, DateTime.Now.ToString(),
                                                                                                            0, "NA",
                                                                                                            0, "NA", 0, "NA", 0, ats_wms_master_pallet_informationDataTableEmptyStatusDT[0].PALLET_TYPE_ID, 0, 0, 0, ats_wms_master_pallet_informationDataTableEmptyStatusDT[0].PALLET_INFORMATION_ID);

                                                                                                        Log.Debug("Data Updated for empty pallet");
                                                                                                    }
                                                                                                }
                                                                                            }


                                                                                            Log.Debug("9.1 :: Checking pallet information details is available from DB for pallet code :: " + palletCodeOnStackerPickupPosition);
                                                                                            ats_wms_master_pallet_informationDataTableDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnStackerPickupPosition);
                                                                                            if (ats_wms_master_pallet_informationDataTableDT != null && ats_wms_master_pallet_informationDataTableDT.Count > 0)
                                                                                            {



                                                                                                try
                                                                                                {
                                                                                                    Log.Debug("10 :: Checking if Infeed Mission is already generated");
                                                                                                    if (ats_wms_master_pallet_informationDataTableDT[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                    {
                                                                                                        Log.Debug("11 :: Infeed mission is not generated");
                                                                                                        //ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUS1("READY", "IN_PROGRESS");
                                                                                                        ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUS1AndSTACKER_ID("READY", "IN_PROGRESS", 2);
                                                                                                        if (ats_wms_infeed_mission_runtime_detailsDataTableDT != null && ats_wms_infeed_mission_runtime_detailsDataTableDT.Count == 0)
                                                                                                        {

                                                                                                            //}

                                                                                                            // for recall pallet
                                                                                                            int palletStatusAtPickUp = ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID;
                                                                                                            Boolean checkPosition = true;
                                                                                                            if (checkPalletStatus(checkPosition, palletStatusAtPickUp))
                                                                                                            {



                                                                                                                //if (ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID == 6)
                                                                                                                //{
                                                                                                                Log.Debug("1 :: Recall pallet Infeed mission is not generated :: pallet status :: " + palletStatusAtPickUp);

                                                                                                                bool isMissionGenaratedForRecall = false;
                                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndFLOOR_IDAndINFEED_IS_ACTIVE(1, 6, 1);



                                                                                                                if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                                                                                                                {
                                                                                                                    Log.Debug("2 :: ");
                                                                                                                    ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByAREA_IDAndFLOOR_IDAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_EMPTYAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(1, 6, 0, 1, 1, 0);

                                                                                                                    if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count > 0)
                                                                                                                    {
                                                                                                                        Log.Debug("3 :: ");



                                                                                                                        for (int a = 0; a < ats_wms_master_position_detailsDataTableDT.Count; a++)
                                                                                                                        {
                                                                                                                            Log.Debug("4 :: Position ID :: " + ats_wms_master_position_detailsDataTableDT[a].POSITION_ID);

                                                                                                                            ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[a].RACK_ID, 1);

                                                                                                                            if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                            {

                                                                                                                                Log.Debug("5 :: rack ID :: " + ats_wms_master_position_detailsDataTableDT[a].RACK_ID);


                                                                                                                                ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDGreaterThan(ats_wms_master_position_detailsDataTableDT[a].RACK_ID, ats_wms_master_position_detailsDataTableDT[a].POSITION_ID);

                                                                                                                                Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                                if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                                {

                                                                                                                                    try
                                                                                                                                    {

                                                                                                                                        ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_position_detailsDataTableDT[a].RACK_ID);
                                                                                                                                        ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_position_detailsDataTableDT[a].RACK_ID);
                                                                                                                                        ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_position_detailsDataTableDT[a].RACK_ID);
                                                                                                                                    }
                                                                                                                                    catch (Exception ex)
                                                                                                                                    {
                                                                                                                                        Log.Error("exception occured while checking inprogress missions" + ex.Message + ex.StackTrace);
                                                                                                                                    }


                                                                                                                                    if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count == 0 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null
                                                                                                                                                           && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count == 0
                                                                                                                                                           && ats_wms_infeed_mission_runtime_detailsDataTableDT != null && ats_wms_infeed_mission_runtime_detailsDataTableDT.Count == 0)
                                                                                                                                    {
                                                                                                                                        Log.Debug("6 :: rack ID :: No Ready of inprogress mission found ");

                                                                                                                                        {
                                                                                                                                            Log.Debug("6 :: There are no Ready or inprogress Missions in Runtime");
                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                            if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                            {
                                                                                                                                                if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                                {
                                                                                                                                                    Log.Debug("7 :: Infeed is not generated for Respective Pallet");
                                                                                                                                                    try
                                                                                                                                                    {
                                                                                                                                                        Log.Debug("8 :: Calculating Shift Details");

                                                                                                                                                        string shiftName = "";
                                                                                                                                                        int shiftId = 0;
                                                                                                                                                        DateTime now = DateTime.Now;
                                                                                                                                                        ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                        if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                        {
                                                                                                                                                            shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                            shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                            //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                            if (data != null && data.Count > 0)
                                                                                                                                                            {
                                                                                                                                                                shiftName = data[0].SHIFT_NAME;
                                                                                                                                                                shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        try
                                                                                                                                                        {

                                                                                                                                                            Thread.Sleep(1000);
                                                                                                                                                            //Inserting data in infeed mission table
                                                                                                                                                            Log.Debug("9 :: Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                            Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                            Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE);
                                                                                                                                                            Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_ID);
                                                                                                                                                            Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME);
                                                                                                                                                            Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID);
                                                                                                                                                            Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME);
                                                                                                                                                            Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                            Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME);
                                                                                                                                                            Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE);
                                                                                                                                                            Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                            Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[a].POSITION_ID);
                                                                                                                                                            Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[a].ST2_POSITION_NAME);
                                                                                                                                                            Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[a].ST2_POSITION_NUMBER_IN_RACK);
                                                                                                                                                            //Log.Debug("SERIAL_NUMBER :: " + ats_wms_master_pallet_informationDataTableDT[0].SERIAL_NUMBER);
                                                                                                                                                            Log.Debug("CCCC");

                                                                                                                                                            ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID,
                                                                                                                                                              ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE,
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_ID,
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_NAME,
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].CORE_SIZE,
                                                                                                                                                            0,
                                                                                                                                                             ats_wms_master_pallet_informationDataTableDT1[0].QUANTITY,
                                                                                                                                                             "NA",
                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[a].AREA_ID,
                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME,
                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID,
                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME,
                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME,
                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE,
                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[a].POSITION_ID,
                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[a].NOMENCLATURE,
                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[a].ST2_POSITION_NUMBER_IN_RACK,
                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                            shiftId,
                                                                                                                                                            shiftName,
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_ID,
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_NAME,
                                                                                                                                                            DateTime.Now,
                                                                                                                                                            null,
                                                                                                                                                            null,
                                                                                                                                                            "READY",
                                                                                                                                                            0,
                                                                                                                                                            "NA",
                                                                                                                                                            "NA",
                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_TYPE_ID,
                                                                                                                                                            2);
                                                                                                                                                        Log.Debug("10 :: Data inserted in infeed mission table :: ");
                                                                                                                                                        }
                                                                                                                                                        catch (Exception ex)
                                                                                                                                                        {

                                                                                                                                                            Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                        }

                                                                                                                                                        try
                                                                                                                                                        {
                                                                                                                                                            //update is infeed mission generated in pallet information table 
                                                                                                                                                            ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                            Log.Debug("11 :: Is mission generated value updated in pallet information table:: ");
                                                                                                                                                        }
                                                                                                                                                        catch (Exception ex)
                                                                                                                                                        {
                                                                                                                                                            Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                        }

                                                                                                                                                        try

                                                                                                                                                        {
                                                                                                                                                            //update position is allocated in master position table
                                                                                                                                                            ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[a].POSITION_ID);
                                                                                                                                                            Log.Debug("12 :: Updated Position Status in Master Position Details table");

                                                                                                                                                        }
                                                                                                                                                        catch (Exception ex)
                                                                                                                                                        {
                                                                                                                                                            Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                        }

                                                                                                                                                        Log.Debug("13 :: isMissionGenaratedForRecall " + isMissionGenaratedForRecall);
                                                                                                                                                        isMissionGenaratedForRecall = true;
                                                                                                                                                        Log.Debug("14 :: isMissionGenaratedForRecall " + isMissionGenaratedForRecall);
                                                                                                                                                        break;

                                                                                                                                                    }
                                                                                                                                                    catch (Exception ex)
                                                                                                                                                    {
                                                                                                                                                        Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }

                                                                                                                                    }
                                                                                                                                }

                                                                                                                            }

                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        Log.Debug("No Empty cell found in Floor 6");
                                                                                                                        checkPosition = false;
                                                                                                                        checkPalletStatus(checkPosition, palletStatusAtPickUp);
                                                                                                                    }
                                                                                                                }

                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                Log.Debug(" Pallet Status is not 6");


                                                                                                                if (!checkPalletStatus(checkPosition, palletStatusAtPickUp))
                                                                                                                {

                                                                                                                    if (palletStatusAtPickUp == 7)
                                                                                                                    {


                                                                                                                        Log.Debug(" ***********   Pallet Status found 7   *********************");




# region abcd
                                                                                                                        ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnStackerPickupPosition);

                                                                                                                        if (ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT != null && ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT.Count > 0)

                                                                                                                        {
                                                                                                                            Log.Debug(" In new Logic/Flow");

                                                                                                                            GenerateInfeedMission(ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT[0], areaId);
                                                                                                                        }

#endregion





                                                                                                                    }
                                                                                                                    else if (palletStatusAtPickUp != 1)
                                                                                                                    {
                                                                                                                        Log.Debug(" ***********   Pallet Status not 1   *********************");

                                                                                                                        


                                                                                                                        ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnStackerPickupPosition);

                                                                                                                        if (ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT != null && ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT.Count > 0)
                                                                                                                        {
                                                                                                                            if (ats_wms_master_pallet_informationDataTableIsMissionGeneratedDT[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                            {

                                                                                                                                try

                                                                                                                                {
                                                                                                                                    bool isMissionGenarated = false;
                                                                                                                                    Log.Debug("12 :: isMissionGenarated :: " + isMissionGenarated);
                                                                                                                                    Log.Debug("12 :: Searching active floors for the area Id :: " + areaId);
                                                                                                                                    ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_IS_ACTIVE(areaId, 1);
                                                                                                                                    if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                                                                                                                                    {
                                                                                                                                        Log.Debug("13 :: Found active floors count :: " + ats_wms_mapping_floor_area_detailsDataTableDT.Count);
                                                                                                                                        for (int i = 0; i < ats_wms_mapping_floor_area_detailsDataTableDT.Count; i++)
                                                                                                                                        {
                                                                                                                                            Log.Debug("14 :: Searching empty position on the floor id :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);


                                                                                                                                            Log.Debug("14.1 :: inside racklist");
                                                                                                                                            List<int> rackList = null;
                                                                                                                                            try
                                                                                                                                            {
                                                                                                                                                rackList = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDistinctRack_IDwherePALLET_STATUS_IDAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                                                                //ats_wms_current_stock_detailsDataTableDT = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDISTRACK_IDWherePRODUCT_VARIANT_CODEAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_VARIANT_CODE, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);

                                                                                                                                            }
                                                                                                                                            catch (Exception ex)
                                                                                                                                            {
                                                                                                                                                Log.Error("generateTheMission :: Exception occured while reading rack details :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                                                                                                                            }

                                                                                                                                            if (rackList != null && rackList.Count > 0)
                                                                                                                                            //if (ats_wms_current_stock_detailsDataTableDT != null && ats_wms_current_stock_detailsDataTableDT.Count > 0)
                                                                                                                                            {
                                                                                                                                                Log.Debug("14.2 : getting distinct rack " + rackList.Count);



                                                                                                                                                for (int j = 0; j < rackList.Count; j++)
                                                                                                                                                {
                                                                                                                                                    //date 24-12-2024
                                                                                                                                                    List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                                                    int currentRack = rackList[j];
                                                                                                                                                    if (!rackListColumn1.Contains(currentRack))
                                                                                                                                                    {


                                                                                                                                                        ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackList[j], 1, 0, 1, 0);
                                                                                                                                                        //ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList[j], 1, 0,1);

                                                                                                                                                        Log.Debug("14.3 :: getting empty positions");

                                                                                                                                                        if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count > 0)
                                                                                                                                                        {

                                                                                                                                                            Log.Debug("position Id :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                            Log.Debug("ats_wms_master_position_detailsDataTableDT[0].RACK_ID :: " + ats_wms_master_position_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                            Log.Debug("getting empty positions");
                                                                                                                                                            Log.Debug("getting empty positions");

                                                                                                                                                            ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDGreaterThan(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                                                            Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                                                            if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                                                            {
                                                                                                                                                                ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                                                                if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                                                                {
                                                                                                                                                                    Log.Debug("14.4 :: getting active rack");
                                                                                                                                                                    try
                                                                                                                                                                    {

                                                                                                                                                                        ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                        ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                        ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                    }
                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                    {
                                                                                                                                                                        Log.Error("exception occured while checking inprogress missions" + ex.Message + ex.StackTrace);
                                                                                                                                                                    }



                                                                                                                                                                    if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count == 0 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null
                                                                                                                                                                        && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count == 0
                                                                                                                                                                        && ats_wms_infeed_mission_runtime_detailsDataTableDT != null && ats_wms_infeed_mission_runtime_detailsDataTableDT.Count == 0)
                                                                                                                                                                    {
                                                                                                                                                                        Log.Debug("15 :: There are no Ready or inprogress Missions in Runtime");
                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                                                        if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                                                        {
                                                                                                                                                                            if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                                                            {
                                                                                                                                                                                Log.Debug("16 :: Infeed is not generated for Respective Pallet :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE);
                                                                                                                                                                                Log.Debug("16 :: Infeed is not generated for Respective Pallet info ID :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    Log.Debug("17 :: Calculating Shift Details");

                                                                                                                                                                                    string shiftName = "";
                                                                                                                                                                                    int shiftId = 0;
                                                                                                                                                                                    DateTime now = DateTime.Now;
                                                                                                                                                                                    ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                                                    if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                                                    {
                                                                                                                                                                                        shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                                                        shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                                                        //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                                                        if (data != null && data.Count > 0)
                                                                                                                                                                                        {
                                                                                                                                                                                            shiftName = data[0].SHIFT_NAME;
                                                                                                                                                                                            shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    try
                                                                                                                                                                                    {

                                                                                                                                                                                        Thread.Sleep(1000);
                                                                                                                                                                                        //Inserting data in infeed mission table
                                                                                                                                                                                        Log.Debug("18 :: Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                                                        Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                        Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE);
                                                                                                                                                                                        Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);
                                                                                                                                                                                        Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME);
                                                                                                                                                                                        Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);
                                                                                                                                                                                        Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME);
                                                                                                                                                                                        Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                                        Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME);
                                                                                                                                                                                        Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE);
                                                                                                                                                                                        Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                                                        Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                        Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NAME);
                                                                                                                                                                                        Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK);
                                                                                                                                                                                        //Log.Debug("SERIAL_NUMBER :: " + ats_wms_master_pallet_informationDataTableDT[0].SERIAL_NUMBER);
                                                                                                                                                                                        Log.Debug("FFFF");

                                                                                                                                                                                        ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID,
                                                                                                                                                                                          ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE,
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_ID,
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_NAME,
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].CORE_SIZE,
                                                                                                                                                                                        0,
                                                                                                                                                                                         ats_wms_master_pallet_informationDataTableDT[0].QUANTITY,
                                                                                                                                                                                         "NA",
                                                                                                                                                                                        ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                                                        ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME,
                                                                                                                                                                                        ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID,
                                                                                                                                                                                        ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME,
                                                                                                                                                                                        ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                                                        ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME,
                                                                                                                                                                                        ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE,
                                                                                                                                                                                        ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                                                        ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                                                        ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                                                        ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK,
                                                                                                                                                                                        //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                                                        //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                                                        shiftId,
                                                                                                                                                                                        shiftName,
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID,
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_NAME,
                                                                                                                                                                                        DateTime.Now,
                                                                                                                                                                                        null,
                                                                                                                                                                                        null,
                                                                                                                                                                                        "READY",
                                                                                                                                                                                        0,
                                                                                                                                                                                        "NA",
                                                                                                                                                                                        "NA",
                                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT[0].PALLET_TYPE_ID,
                                                                                                                                                                                        2
                                                                                                                                                                                               );
                                                                                                                                                                                    Log.Debug("19 :: Data inserted in infeed mission table :: ");
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {

                                                                                                                                                                                        Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        //update is infeed mission generated in pallet information table 
                                                                                                                                                                                        ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                        Log.Debug("20 :: Is mission generated value updated in pallet information table:: ");
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    try

                                                                                                                                                                                    {
                                                                                                                                                                                        //update position is allocated in master position table
                                                                                                                                                                                        ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                        Log.Debug("21 :: Updated Position Status in Master Position Details table");

                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    isMissionGenarated = true;
                                                                                                                                                                                    break;

                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception ex)
                                                                                                                                                                                {
                                                                                                                                                                                    Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }


                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                        //else
                                                                                                                                                        ////as the floor 1 does not have the same material rack checking next floor id in loop
                                                                                                                                                        //{
                                                                                                                                                        //    continue;
                                                                                                                                                        //}
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            sameMaterialRackNotFoundS2 = true;
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                sameMaterialRackNotFoundS2 = true;
                                                                                                                                            }
                                                                                                                                            //else
                                                                                                                                            if (sameMaterialRackNotFoundS2 == true)
                                                                                                                                            {
                                                                                                                                                Log.Debug("120");
                                                                                                                                                //find empty rack and assign the mission
                                                                                                                                                List<int> rackList1 = null;
                                                                                                                                                rackList1 = ats_wms_master_position_detailsTableAdapterInstance.GetDataByDistinctRACK_IDWhereFLOOR_IDAndAREA_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID, 1, 0, 1, 0).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                                                                Log.Debug("121");
                                                                                                                                                if (rackList1 != null && rackList1.Count > 0)
                                                                                                                                                {

                                                                                                                                                    Log.Debug("122");
                                                                                                                                                    for (int k = 0; k < rackList1.Count; k++)
                                                                                                                                                    {

                                                                                                                                                        //date 31-12-2024
                                                                                                                                                        List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                                                        int currentRack = rackList1[k];
                                                                                                                                                        if (!rackListColumn1.Contains(currentRack))
                                                                                                                                                        {



                                                                                                                                                            ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackList1[k], 1, 0, 1, 0);
                                                                                                                                                            // ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList1[k], 1, 0, 1);
                                                                                                                                                            if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count == 2)
                                                                                                                                                            {
                                                                                                                                                                Log.Debug("123");


                                                                                                                                                                ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDGreaterThan(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                                                                Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                                                                if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                                                                {
                                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                                                                    if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                                                                    {
                                                                                                                                                                        ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                                                        if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                                                        {
                                                                                                                                                                            if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                                                            {
                                                                                                                                                                                try
                                                                                                                                                                                {
                                                                                                                                                                                    Log.Debug("42 Inserting data in infeed table");

                                                                                                                                                                                    string shiftName = "";
                                                                                                                                                                                    int shiftId = 0;
                                                                                                                                                                                    DateTime now = DateTime.Now;
                                                                                                                                                                                    ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                                                    if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                                                    {
                                                                                                                                                                                        shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                                                        shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                                                        //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                                                        if (data != null && data.Count > 0)
                                                                                                                                                                                        {
                                                                                                                                                                                            shiftName = data[0].SHIFT_NAME;
                                                                                                                                                                                            shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                    try
                                                                                                                                                                                    {

                                                                                                                                                                                        ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByFLOOR_ID(ats_wms_master_position_detailsDataTableDT[0].FLOOR_ID);

                                                                                                                                                                                        Thread.Sleep(1000);
                                                                                                                                                                                        //Inserting data in infeed mission table
                                                                                                                                                                                        Log.Debug("Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                                                        Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                        Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE);
                                                                                                                                                                                        Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_ID);
                                                                                                                                                                                        Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME);
                                                                                                                                                                                        Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID);
                                                                                                                                                                                        Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME);
                                                                                                                                                                                        Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                                        Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_NAME);
                                                                                                                                                                                        Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_SIDE);
                                                                                                                                                                                        Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                                                        Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                        Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NAME);
                                                                                                                                                                                        Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK);

                                                                                                                                                                                        Log.Debug("GGGG");

                                                                                                                                                                                        ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PALLET_CODE,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_ID,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_NAME,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].CORE_SIZE,
                                                                                                                                                                                            0,
                                                                                                                                                                                             ats_wms_master_pallet_informationDataTableDT[0].QUANTITY,
                                                                                                                                                                                             "NA",
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK,
                                                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                                                            shiftId,
                                                                                                                                                                                            shiftName,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_ID,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PALLET_STATUS_NAME,
                                                                                                                                                                                            DateTime.Now,
                                                                                                                                                                                            null,
                                                                                                                                                                                            null,
                                                                                                                                                                                            "READY",
                                                                                                                                                                                            0,
                                                                                                                                                                                            "NA",
                                                                                                                                                                                            "NA",
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT[0].PALLET_TYPE_ID,
                                                                                                                                                                                            2
                                                                                                                                                                                            );
                                                                                                                                                                                    Log.Debug("23 Data inserted in infeed mission table :: ");
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {

                                                                                                                                                                                        Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        //update is infeed mission generated in pallet information table 
                                                                                                                                                                                        ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                        Log.Debug("24 Is mission generated value updated in pallet information table:: ");
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        //update position is allocated in master position table
                                                                                                                                                                                        ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                        Log.Debug("25 Position is allocated value update in master position table::  :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                    }

                                                                                                                                                                                    isMissionGenarated = true;
                                                                                                                                                                                    break;

                                                                                                                                                                                }
                                                                                                                                                                                catch (Exception ex)
                                                                                                                                                                                {
                                                                                                                                                                                    Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }

                                                                                                                                                            }

                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                continue;
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }

                                                                                                                                            if (isMissionGenarated)
                                                                                                                                            {
                                                                                                                                                break;
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                continue;
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception ex)
                                                                                                                                {
                                                                                                                                    Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting active floor details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                }

                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Log.Debug("1 :: Infeed mission is already generated");
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else if (palletStatusAtPickUp == 1)
                                                                                                                    {

                                                                                                                        Log.Debug(" Generating Mission for Accidental Pallet which has status 1 loaded");



                                                                                                                        {
                                                                                                                            Log.Debug(" ***********   Pallet Status is 1   *********************");
                                                                                                                           
                                                                                                                          
                                                                                                                                try
                                                                                                                                {
                                                                                                                                    var delete_entry_from_CCM_Buffer = ats_wms_ccm_buffer_detailsTableAdapterInstance.GetDataByPALLET_CODEAndIS_BUFFER_DELETED(palletCodeOnStackerPickupPosition, 0);



                                                                                                                                    try
                                                                                                                                    {
                                                                                                                                        if (delete_entry_from_CCM_Buffer != null && delete_entry_from_CCM_Buffer.Count > 0)
                                                                                                                                        {
                                                                                                                                            ats_wms_ccm_buffer_detailsTableAdapterInstance.UpdateIS_BUFFER_DELETEDWhereBUFFER_ID(1, delete_entry_from_CCM_Buffer[0].BUFFER_ID);
                                                                                                                                            Log.Debug(" Entry of Pallet is deleted form BUffer :: Pallet Code " + delete_entry_from_CCM_Buffer[0].PALLET_CODE);
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception ex)
                                                                                                                                    {

                                                                                                                                    Log.Error("Error in deleting buffer from CCM Buffer Table");
                                                                                                                                    }
                                                                                                                                }
                                                                                                                                catch (Exception ex)
                                                                                                                                {

                                                                                                                                Log.Error("Error in finding Pallet in CCM BUffer Table");
                                                                                                                            }
                                                


                                                                                                                            var ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_CODEAndPALLET_INFORMATION_IDOrderByDesc(palletCodeOnStackerPickupPosition);

                                                                                                                            if (ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet != null && ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet.Count > 0)
                                                                                                                            {
                                                                                                                                if (ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                {

                                                                                                                                    try

                                                                                                                                    {
                                                                                                                                        bool isMissionGenaratedForAccidentalPalletLoaded = false;
                                                                                                                                        Log.Debug("12 :: isMissionGenarated :: " + isMissionGenaratedForAccidentalPalletLoaded);
                                                                                                                                        Log.Debug("12 :: Searching active floors for the area Id :: " + areaId);
                                                                                                                                        ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_IS_ACTIVE(areaId, 1);
                                                                                                                                        if (ats_wms_mapping_floor_area_detailsDataTableDT != null && ats_wms_mapping_floor_area_detailsDataTableDT.Count > 0)
                                                                                                                                        {
                                                                                                                                            Log.Debug("13 :: Found active floors count :: " + ats_wms_mapping_floor_area_detailsDataTableDT.Count);
                                                                                                                                            for (int i = 0; i < ats_wms_mapping_floor_area_detailsDataTableDT.Count; i++)
                                                                                                                                            {
                                                                                                                                                Log.Debug("14 :: Searching empty position on the floor id :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);


                                                                                                                                                Log.Debug("14.1 :: inside racklist");
                                                                                                                                                List<int> rackList = null;
                                                                                                                                                try
                                                                                                                                                {
                                                                                                                                                    rackList = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDistinctRack_IDwherePALLET_STATUS_IDAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet[0].PALLET_STATUS_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                                                                    //ats_wms_current_stock_detailsDataTableDT = ats_wms_current_stock_detailsTableAdapterInstance.GetDataByDISTRACK_IDWherePRODUCT_VARIANT_CODEAndFLOOR_IDAndAREA_ID(ats_wms_master_pallet_informationDataTableDT[0].PRODUCT_VARIANT_CODE, ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);

                                                                                                                                                }
                                                                                                                                                catch (Exception ex)
                                                                                                                                                {
                                                                                                                                                    Log.Error("generateTheMission :: Exception occured while reading rack details :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                                                                                                                                                }

                                                                                                                                                if (rackList != null && rackList.Count > 0)
                                                                                                                                                //if (ats_wms_current_stock_detailsDataTableDT != null && ats_wms_current_stock_detailsDataTableDT.Count > 0)
                                                                                                                                                {
                                                                                                                                                    Log.Debug("14.2 : getting distinct rack " + rackList.Count);



                                                                                                                                                    for (int j = 0; j < rackList.Count; j++)
                                                                                                                                                    {
                                                                                                                                                        //date 24-12-2024
                                                                                                                                                        List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                                                        int currentRack = rackList[j];
                                                                                                                                                        if (!rackListColumn1.Contains(currentRack))
                                                                                                                                                        {


                                                                                                                                                            ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackList[j], 1, 0, 1, 0);
                                                                                                                                                            //ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterSeconPositionInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList[j], 1, 0,1);

                                                                                                                                                            Log.Debug("14.3 :: getting empty positions");

                                                                                                                                                            if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count > 0)
                                                                                                                                                            {

                                                                                                                                                                Log.Debug("position Id :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                Log.Debug("ats_wms_master_position_detailsDataTableDT[0].RACK_ID :: " + ats_wms_master_position_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                Log.Debug("getting empty positions");
                                                                                                                                                                Log.Debug("getting empty positions");

                                                                                                                                                                ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDGreaterThan(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                                                                Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                                                                if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                                                                {
                                                                                                                                                                    ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                                                                    if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                                                                    {
                                                                                                                                                                        Log.Debug("14.4 :: getting active rack");
                                                                                                                                                                        try
                                                                                                                                                                        {

                                                                                                                                                                            ats_wms_outfeed_mission_runtime_detailsDataTableDT = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance.GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                            ats_wms_infeed_mission_runtime_detailsDataTableDT = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                            ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance.GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                        }
                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                        {
                                                                                                                                                                            Log.Error("exception occured while checking inprogress missions" + ex.Message + ex.StackTrace);
                                                                                                                                                                        }



                                                                                                                                                                        if (ats_wms_outfeed_mission_runtime_detailsDataTableDT != null && ats_wms_outfeed_mission_runtime_detailsDataTableDT.Count == 0 && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT != null
                                                                                                                                                                            && ats_wms_transfer_pallet_mission_runtime_detailsDataTableDT.Count == 0
                                                                                                                                                                            && ats_wms_infeed_mission_runtime_detailsDataTableDT != null && ats_wms_infeed_mission_runtime_detailsDataTableDT.Count == 0)
                                                                                                                                                                        {
                                                                                                                                                                            Log.Debug("15 :: There are no Ready or inprogress Missions in Runtime");
                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableIsMissionGeneratedDTAccidentalLoadedPallet[0].PALLET_INFORMATION_ID);

                                                                                                                                                                            if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                                                            {
                                                                                                                                                                                if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                                                                {
                                                                                                                                                                                    Log.Debug("16 :: Infeed is not generated for Respective Pallet :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE);
                                                                                                                                                                                    Log.Debug("16 :: Infeed is not generated for Respective Pallet info ID :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Debug("17 :: Calculating Shift Details");

                                                                                                                                                                                        string shiftName = "";
                                                                                                                                                                                        int shiftId = 0;
                                                                                                                                                                                        DateTime now = DateTime.Now;
                                                                                                                                                                                        ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                                                        if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                                                        {
                                                                                                                                                                                            shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                                                            shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                                                            //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                                                            if (data != null && data.Count > 0)
                                                                                                                                                                                            {
                                                                                                                                                                                                shiftName = data[0].SHIFT_NAME;
                                                                                                                                                                                                shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        try
                                                                                                                                                                                        {

                                                                                                                                                                                            Thread.Sleep(1000);
                                                                                                                                                                                            //Inserting data in infeed mission table
                                                                                                                                                                                            Log.Debug("18 :: Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                                                            Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                            Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE);
                                                                                                                                                                                            Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID);
                                                                                                                                                                                            Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME);
                                                                                                                                                                                            Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID);
                                                                                                                                                                                            Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME);
                                                                                                                                                                                            Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                                            Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME);
                                                                                                                                                                                            Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE);
                                                                                                                                                                                            Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                                                            Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                            Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NAME);
                                                                                                                                                                                            Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK);
                                                                                                                                                                                            //Log.Debug("SERIAL_NUMBER :: " + ats_wms_master_pallet_informationDataTableDT[0].SERIAL_NUMBER);
                                                                                                                                                                                            Log.Debug("FFFF");

                                                                                                                                                                                            ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID,
                                                                                                                                                                                              ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_ID,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_NAME,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].CORE_SIZE,
                                                                                                                                                                                            0,
                                                                                                                                                                                             ats_wms_master_pallet_informationDataTableDT1[0].QUANTITY,
                                                                                                                                                                                             "NA",
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_NAME,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID,
                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_NAME,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE,
                                                                                                                                                                                            ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                                                            ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK,
                                                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                                                            //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                                                            shiftId,
                                                                                                                                                                                            shiftName,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_ID,
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_NAME,
                                                                                                                                                                                            DateTime.Now,
                                                                                                                                                                                            null,
                                                                                                                                                                                            null,
                                                                                                                                                                                            "READY",
                                                                                                                                                                                            0,
                                                                                                                                                                                            "NA",
                                                                                                                                                                                            "NA",
                                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1[0].PALLET_TYPE_ID,
                                                                                                                                                                                            2
                                                                                                                                                                                                   );
                                                                                                                                                                                        Log.Debug("19 :: Data inserted in infeed mission table :: ");
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {

                                                                                                                                                                                            Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            //update is infeed mission generated in pallet information table 
                                                                                                                                                                                            ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                            Log.Debug("20 :: Is mission generated value updated in pallet information table:: ");
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {
                                                                                                                                                                                            Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        try

                                                                                                                                                                                        {
                                                                                                                                                                                            //update position is allocated in master position table
                                                                                                                                                                                            ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                            Log.Debug("21 :: Updated Position Status in Master Position Details table");

                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {
                                                                                                                                                                                            Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        isMissionGenaratedForAccidentalPalletLoaded = true;
                                                                                                                                                                                        break;

                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }


                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                            //else
                                                                                                                                                            ////as the floor 1 does not have the same material rack checking next floor id in loop
                                                                                                                                                            //{
                                                                                                                                                            //    continue;
                                                                                                                                                            //}
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                sameMaterialRackNotFoundS2 = true;
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    sameMaterialRackNotFoundS2 = true;
                                                                                                                                                }
                                                                                                                                                //else
                                                                                                                                                if (sameMaterialRackNotFoundS2 == true)
                                                                                                                                                {
                                                                                                                                                    Log.Debug("120");
                                                                                                                                                    //find empty rack and assign the mission
                                                                                                                                                    List<int> rackList1 = null;
                                                                                                                                                    rackList1 = ats_wms_master_position_detailsTableAdapterInstance.GetDataByDistinctRACK_IDWhereFLOOR_IDAndAREA_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(ats_wms_mapping_floor_area_detailsDataTableDT[i].FLOOR_ID, ats_wms_mapping_floor_area_detailsDataTableDT[i].AREA_ID, 1, 0, 1, 0).Select(o => o.RACK_ID).Distinct().ToList();
                                                                                                                                                    Log.Debug("121");
                                                                                                                                                    if (rackList1 != null && rackList1.Count > 0)
                                                                                                                                                    {

                                                                                                                                                        Log.Debug("122");
                                                                                                                                                        for (int k = 0; k < rackList1.Count; k++)
                                                                                                                                                        {

                                                                                                                                                            //date 31-12-2024
                                                                                                                                                            List<int> rackListColumn1 = new List<int> { 1, 27, 53, 79, 105 };

                                                                                                                                                            int currentRack = rackList1[k];
                                                                                                                                                            if (!rackListColumn1.Contains(currentRack))
                                                                                                                                                            {



                                                                                                                                                                ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackList1[k], 1, 0, 1, 0);
                                                                                                                                                                // ats_wms_master_position_detailsDataTableDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEOrderByPOSITION_IDAsc(rackList1[k], 1, 0, 1);
                                                                                                                                                                if (ats_wms_master_position_detailsDataTableDT != null && ats_wms_master_position_detailsDataTableDT.Count == 2)
                                                                                                                                                                {
                                                                                                                                                                    Log.Debug("123");


                                                                                                                                                                    ats_wms_master_position_detailsDataTableFrontPositionEmptyDT = ats_wms_master_position_detailsTableAdapterInstance.GetDataByRACK_IDAndPOSITION_IDGreaterThan(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);

                                                                                                                                                                    Log.Debug("ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count :: " + ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count);


                                                                                                                                                                    if (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT != null && ((ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count > 0 && ats_wms_master_position_detailsDataTableFrontPositionEmptyDT[0].POSITION_IS_ALLOCATED == 0) || (ats_wms_master_position_detailsDataTableFrontPositionEmptyDT.Count == 0)))
                                                                                                                                                                    {
                                                                                                                                                                        ats_wms_master_rack_detailsDataTableDT = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(ats_wms_master_position_detailsDataTableDT[0].RACK_ID, 1);

                                                                                                                                                                        if (ats_wms_master_rack_detailsDataTableDT != null && ats_wms_master_rack_detailsDataTableDT.Count > 0)
                                                                                                                                                                        {
                                                                                                                                                                            ats_wms_master_pallet_informationDataTableDT1 = ats_wms_master_pallet_informationTableAdapterInstance.GetDataByPALLET_INFORMATION_ID(ats_wms_master_pallet_informationDataTableDT[0].PALLET_INFORMATION_ID);

                                                                                                                                                                            if (ats_wms_master_pallet_informationDataTableDT1 != null && ats_wms_master_pallet_informationDataTableDT1.Count > 0)
                                                                                                                                                                            {
                                                                                                                                                                                if (ats_wms_master_pallet_informationDataTableDT1[0].IS_INFEED_MISSION_GENERATED == 0)
                                                                                                                                                                                {
                                                                                                                                                                                    try
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Debug("42 Inserting data in infeed table");

                                                                                                                                                                                        string shiftName = "";
                                                                                                                                                                                        int shiftId = 0;
                                                                                                                                                                                        DateTime now = DateTime.Now;
                                                                                                                                                                                        ats_wms_master_shift_detailsDataTableDT = ats_wms_master_shift_detailsTableAdapterInstance.GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(now.ToString("HH:mm:ss"), 0);
                                                                                                                                                                                        if (ats_wms_master_shift_detailsDataTableDT != null && ats_wms_master_shift_detailsDataTableDT.Count > 0)
                                                                                                                                                                                        {
                                                                                                                                                                                            shiftName = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME;
                                                                                                                                                                                            shiftId = ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID;
                                                                                                                                                                                            //Log.Debug("Shift name: " + shiftName);
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            var data = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                                                                                                                                                                                            if (data != null && data.Count > 0)
                                                                                                                                                                                            {
                                                                                                                                                                                                shiftName = data[0].SHIFT_NAME;
                                                                                                                                                                                                shiftId = data[0].SHIFT_NUMBER;
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                        try
                                                                                                                                                                                        {

                                                                                                                                                                                            ats_wms_mapping_floor_area_detailsDataTableDT = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByFLOOR_ID(ats_wms_master_position_detailsDataTableDT[0].FLOOR_ID);

                                                                                                                                                                                            Thread.Sleep(1000);
                                                                                                                                                                                            //Inserting data in infeed mission table
                                                                                                                                                                                            Log.Debug("Inserting data in infeed mission runtime details table as follows :: ");
                                                                                                                                                                                            Log.Debug("PALLET_INFORMATION_ID :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                            Log.Debug("PALLET_CODE :: " + ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE);
                                                                                                                                                                                            Log.Debug("AREA_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_ID);
                                                                                                                                                                                            Log.Debug("AREA_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME);
                                                                                                                                                                                            Log.Debug("FLOOR_ID :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID);
                                                                                                                                                                                            Log.Debug("FLOOR_NAME :: " + ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME);
                                                                                                                                                                                            Log.Debug("RACK_ID :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_ID);
                                                                                                                                                                                            Log.Debug("RACK_NAME :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_NAME);
                                                                                                                                                                                            Log.Debug("RACK_SIDE :: " + ats_wms_master_rack_detailsDataTableDT[0].S1_RACK_SIDE);
                                                                                                                                                                                            Log.Debug("RACK_COLUMN :: " + ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN);
                                                                                                                                                                                            Log.Debug("POSITION_ID :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                            Log.Debug("POSITION_NAME :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NAME);
                                                                                                                                                                                            Log.Debug("POSITION_NUMBER_IN_RACK :: " + ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK);

                                                                                                                                                                                            Log.Debug("GGGG");

                                                                                                                                                                                            ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PALLET_CODE,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_ID,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PRODUCT_NAME,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].CORE_SIZE,
                                                                                                                                                                                                0,
                                                                                                                                                                                                 ats_wms_master_pallet_informationDataTableDT1[0].QUANTITY,
                                                                                                                                                                                                 "NA",
                                                                                                                                                                                                ats_wms_master_position_detailsDataTableDT[0].AREA_ID,
                                                                                                                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT[0].AREA_NAME,
                                                                                                                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_ID,
                                                                                                                                                                                                ats_wms_mapping_floor_area_detailsDataTableDT[0].FLOOR_NAME,
                                                                                                                                                                                                ats_wms_master_rack_detailsDataTableDT[0].RACK_ID,
                                                                                                                                                                                                ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_NAME,
                                                                                                                                                                                                ats_wms_master_rack_detailsDataTableDT[0].S2_RACK_SIDE,
                                                                                                                                                                                                ats_wms_master_rack_detailsDataTableDT[0].RACK_COLUMN,
                                                                                                                                                                                                ats_wms_master_position_detailsDataTableDT[0].POSITION_ID,
                                                                                                                                                                                                ats_wms_master_position_detailsDataTableDT[0].NOMENCLATURE,
                                                                                                                                                                                                ats_wms_master_position_detailsDataTableDT[0].ST2_POSITION_NUMBER_IN_RACK,
                                                                                                                                                                                                //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_ID,
                                                                                                                                                                                                //ats_wms_master_shift_detailsDataTableDT[0].SHIFT_NAME,
                                                                                                                                                                                                shiftId,
                                                                                                                                                                                                shiftName,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_ID,
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PALLET_STATUS_NAME,
                                                                                                                                                                                                DateTime.Now,
                                                                                                                                                                                                null,
                                                                                                                                                                                                null,
                                                                                                                                                                                                "READY",
                                                                                                                                                                                                0,
                                                                                                                                                                                                "NA",
                                                                                                                                                                                                "NA",
                                                                                                                                                                                                ats_wms_master_pallet_informationDataTableDT1[0].PALLET_TYPE_ID,
                                                                                                                                                                                                2
                                                                                                                                                                                                );
                                                                                                                                                                                        Log.Debug("23 Data inserted in infeed mission table :: ");
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {

                                                                                                                                                                                            Log.Error("exception occured while inserting data infeed mission runtime details table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            //update is infeed mission generated in pallet information table 
                                                                                                                                                                                            ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, ats_wms_master_pallet_informationDataTableDT1[0].PALLET_INFORMATION_ID);
                                                                                                                                                                                            Log.Debug("24 Is mission generated value updated in pallet information table:: ");
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {
                                                                                                                                                                                            Log.Error("exception occured while updating infeed mission generation in pallet information table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        try
                                                                                                                                                                                        {
                                                                                                                                                                                            //update position is allocated in master position table
                                                                                                                                                                                            ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                            Log.Debug("25 Position is allocated value update in master position table::  :: " + ats_wms_master_position_detailsDataTableDT[0].POSITION_ID);
                                                                                                                                                                                        }
                                                                                                                                                                                        catch (Exception ex)
                                                                                                                                                                                        {
                                                                                                                                                                                            Log.Error("exception occured while updating position is allocated in master position table" + ex.Message + ex.StackTrace);
                                                                                                                                                                                        }

                                                                                                                                                                                        isMissionGenaratedForAccidentalPalletLoaded = true;
                                                                                                                                                                                        break;

                                                                                                                                                                                    }
                                                                                                                                                                                    catch (Exception ex)
                                                                                                                                                                                    {
                                                                                                                                                                                        Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting shift and inserting infeed details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }

                                                                                                                                                                }

                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    continue;
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }

                                                                                                                                                if (isMissionGenaratedForAccidentalPalletLoaded)
                                                                                                                                                {
                                                                                                                                                    break;
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    continue;
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                    catch (Exception ex)
                                                                                                                                    {
                                                                                                                                        Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting active floor details :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                                                    }

                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    Log.Debug("1 :: Infeed mission is already generated");
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }





                                                                                                                    }

                                                                                                                }
                                                                                                            }
                                                                                                        }

                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Log.Debug("Infeed mission is already generated");
                                                                                                    }
                                                                                                }
                                                                                                catch (Exception ex)
                                                                                                {

                                                                                                    //Log.Error("Exception occured while getting is infeed generated count 0");
                                                                                                    Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Exception occured while getting is infeed generated count 0 :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                /////PALLET TYPE ID REMAINING!!!!!!
                                                                                                Log.Debug("2.1 :: Pallet Details not present in the DB.... Inserting New Information");
                                                                                                //pallet details are not into database already inserting new 
                                                                                                ats_wms_master_pallet_informationTableAdapterInstance.Insert(palletCodeOnStackerPickupPosition, 0, "NA", "0", 0, "NA", "NA", "NA", 0,
                                                                                                   "NA", "NA", 0, 0
                                                                                                   , 3, "EMPTY", 0, 0, 0, 0, 0, DateTime.Now, 1, "master_user", 0, "0", 0, 0);
                                                                                            }
                                                                                            // }
                                                                                            //}


                                                                                        }
                                                                                        catch (Exception ex)
                                                                                        {
                                                                                            Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Pallet information details are not present :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                        }
                                                                                    }

                                                                                    else
                                                                                    {
                                                                                        Log.Debug("Pallet code is not in the standard form");
                                                                                    }
                                                                                }
                                                                                catch (Exception ex)
                                                                                {
                                                                                    Log.Error("atsWmsBatteryA1InfeedMissionGenerationOperation :: Eception occured while getting pallet present value :: " + ex.Message + "StackTrace :: " + ex.StackTrace);
                                                                                }
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occure while getting inprogrees and ready mission details of respective core shop :: " + ex.Message + "StackTrace  :: " + ex.StackTrace);
                                                    }

                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Log.Error("Exception occurred while reading pallet present :: " + ex.StackTrace);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occure while getting pallet present value :: " + ex.Message + "StackTrace  :: " + ex.StackTrace);
                                    }


                                }
                                else
                                {
                                    //Reconnect to plc
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while checking server state is 1 ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                            }
                        }
                        else
                        {
                            //Reconnect to plc, Check Ip address, url
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("AtsWmsCS_6S1InfeedMissionGenerationDetailsOperation :: Exception occured while checking PLC connection details ::" + ex.Message + " StackTrace:: " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Error("startOperation :: Exception occured while stopping timer :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
            }
            finally
            {
                try
                {
                    //Starting the timer again for the next iteration
                    TataA1InfeedMissionGenTimer.Start();
                }
                catch (Exception ex1)
                {
                    Log.Error("startOperation :: Exception occured while stopping timer :: " + ex1.Message + " stackTrace :: " + ex1.StackTrace);
                }
            }

        }




        public bool checkPalletStatus(bool checkPosition, int palletStatusAtPickUp)
        {
            if (checkPosition && palletStatusAtPickUp == 6)
            {
                Log.Debug("Pallet Status in if :: " + palletStatusAtPickUp);
                return true;
            }
            else
            {
                Log.Debug("Pallet Status in else :: " + palletStatusAtPickUp);
                return false;
            }
        }



        private void InsertInfeedMission(dynamic pallet, dynamic floor, dynamic rack, dynamic pos)
        {
            try
            {
                Log.Debug("InsertInfeedMission :: 1 :: InsertInfeedMission START :: Preparing to fetch shift data...");

                var CurrentDateTime = DateTime.Now;
                var shiftData = ats_wms_master_shift_detailsTableAdapterInstance
                    .GetDataByCurrentShiftDataByCurrentTimeAndSHIFT_IS_DELETED(CurrentDateTime.ToString("HH:mm:ss"), 0);

                var shift = shiftData?.FirstOrDefault();
                if (shift == null)
                {
                    Log.Debug("InsertInfeedMission :: 2 :: Primary shift not found, trying fallback shift...");
                    var fallback = ats_wms_master_shift_detailsTableAdapterInstance.GetShiftDataByStartTimeGreaterThanEndTimeAndSHIFT_IS_DELETED(0);
                    shift = fallback?.FirstOrDefault();
                }

                if (shift == null)
                {
                    Log.Debug("InsertInfeedMission :: 3 :: No shift data found. Aborting mission insert.");
                    return;
                }

                Log.Debug("InsertInfeedMission :: 4 :: Shift data obtained. Sleeping for mission stability...");
                Thread.Sleep(1000);

               


                Log.Debug("InsertInfeedMission :: 5 :: Preparing data to insert into infeed mission runtime table:");
                Log.Debug("------------------------------------------------------------------");
                Log.Debug($"PalletID      : {pallet.PALLET_INFORMATION_ID}");
                Log.Debug($"PalletCode    : {pallet.PALLET_CODE}");
                Log.Debug($"ProductID     : {pallet.PRODUCT_ID}");
                Log.Debug($"ProductName   : {pallet.PRODUCT_NAME}");
                Log.Debug($"CoreSize      : {pallet.CORE_SIZE}");
                Log.Debug($"Quantity      : {pallet.QUANTITY}");
                Log.Debug($"AreaID        : {pos.AREA_ID}");
                Log.Debug($"AreaName      : {floor.AREA_NAME}");
                Log.Debug($"FloorID       : {floor.FLOOR_ID}");
                Log.Debug($"FloorName     : {floor.FLOOR_NAME}");
                Log.Debug($"RackID        : {rack.RACK_ID}");
                Log.Debug($"RackName      : {rack.S2_RACK_NAME ?? rack.S1_RACK_NAME}");
                Log.Debug($"RackSide      : {rack.S2_RACK_SIDE ?? rack.S1_RACK_SIDE}");
                Log.Debug($"RackColumn    : {rack.RACK_COLUMN}");
                Log.Debug($"PositionID    : {pos.POSITION_ID}");
                Log.Debug($"PositionName  : {pos.NOMENCLATURE ?? pos.ST2_POSITION_NAME}");
                Log.Debug($"PositionNo.   : {pos.ST2_POSITION_NUMBER_IN_RACK}");
                Log.Debug($"ShiftID       : {shift.SHIFT_ID}");
                Log.Debug($"ShiftName     : {shift.SHIFT_NAME}");
                Log.Debug($"Status        : READY");

                Log.Debug($"InsertInfeedMission :: 6 :: [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Inserting infeed mission for PalletCode: {pallet.PALLET_CODE}");

                Log.Debug("------------------------------------------------------------------");

                ats_wms_infeed_mission_runtime_detailsTableAdapterInstance.Insert(
                    pallet.PALLET_INFORMATION_ID,
                    pallet.PALLET_CODE,
                    pallet.PRODUCT_ID,
                    pallet.PRODUCT_NAME,
                    pallet.CORE_SIZE,
                    0,
                    pallet.QUANTITY,
                    "NA",
                    pos.AREA_ID,
                    floor.AREA_NAME,
                    floor.FLOOR_ID,
                    floor.FLOOR_NAME,
                    rack.RACK_ID,
                    rack.S2_RACK_NAME ?? rack.S1_RACK_NAME,
                    rack.S2_RACK_SIDE ?? rack.S1_RACK_SIDE,
                    rack.RACK_COLUMN,
                    pos.POSITION_ID,
                    pos.NOMENCLATURE ?? pos.ST2_POSITION_NAME,
                    pos.ST2_POSITION_NUMBER_IN_RACK,
                    shift.SHIFT_ID,
                    shift.SHIFT_NAME,
                    pallet.PALLET_STATUS_ID,
                    pallet.PALLET_STATUS_NAME,
                    CurrentDateTime,
                    null,
                    null,
                    "READY",
                    0,
                    "NA",
                    "NA",
                    pallet.PALLET_TYPE_ID,
                    2
                );

                Log.Debug("InsertInfeedMission :: 7 :: Updating pallet and position states...");

                ats_wms_master_pallet_informationTableAdapterInstance.UpdateIS_INFEED_MISSION_GENERATEDWherePALLET_INFORMATION_ID(1, pallet.PALLET_INFORMATION_ID);
                ats_wms_master_position_detailsTableAdapterInstance.UpdatePOSITION_IS_ALLOCATEDWherePOSITION_ID(1, pos.POSITION_ID);

                Log.Debug($"InsertInfeedMission :: 8 :: [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] InsertInfeedMission SUCCESS :: Mission inserted for PalletCode: {pallet.PALLET_CODE}, PositionID: {pos.POSITION_ID}");
            }
            catch (Exception ex)
            {
                Log.Error($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] InsertInfeedMission ERROR :: {ex.Message} :: PalletCode: {pallet?.PALLET_CODE}");
            }
        }


        private bool TryFindMissionInEmptyRack(dynamic pallet, dynamic floor)
        {
            bool missionInserted = false;
            try
            {
                Log.Debug("Starting TryFindMissionInEmptyRack logic...");

                var rackColumnList = new List<int> { 1, 27, 53, 79, 105 };

                var activeFloors = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_IS_ACTIVE(areaId, 1);
                Log.Debug($"Found active floors count :: {activeFloors.Count}");

                // Phase 1: Try prioritized racks across all floors
                Log.Debug("Trying prioritized racks across all floors...");
                foreach (var fl in activeFloors)
                {
                    var rackData = ats_wms_master_position_detailsTableAdapterInstance
                        .GetDataByDistinctRACK_IDWhereFLOOR_IDAndAREA_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(
                            (int)fl.FLOOR_ID, (int)fl.AREA_ID, 1, 0, 1, 0);

                    List<int> rackList = new List<int>();
                    if (rackData != null)
                    {
                        foreach (var item in rackData)
                        {
                            rackList.Add(item.RACK_ID);
                        }
                        rackList = rackList.Distinct().OrderBy(id => id).ToList();
                    }

                    var primaryRacks = rackList.Where(id => rackColumnList.Contains(id)).ToList();
                    foreach (var rackId in primaryRacks)
                    {
                        missionInserted = TryInsertInfeedMission(pallet, fl, rackId);
                        if (missionInserted)
                        {
                            Log.Debug("Mission inserted using prioritized rack logic.");
                            return true;
                        }
                    }
                }

                // Phase 2: Try fallback racks across all floors
                Log.Debug("Trying fallback racks across all floors...");
                foreach (var fl in activeFloors)
                {
                    var rackData = ats_wms_master_position_detailsTableAdapterInstance
                        .GetDataByDistinctRACK_IDWhereFLOOR_IDAndAREA_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCH(
                            (int)fl.FLOOR_ID, (int)fl.AREA_ID, 1, 0, 1, 0);

                    List<int> rackList = new List<int>();
                    if (rackData != null)
                    {
                        foreach (var item in rackData)
                        {
                            rackList.Add(item.RACK_ID);
                        }
                        rackList = rackList.Distinct().OrderBy(id => id).ToList();
                    }

                    var fallbackRacks = rackList.Where(id => !rackColumnList.Contains(id)).ToList();
                    foreach (var rackId in fallbackRacks)
                    {
                        missionInserted = TryInsertInfeedMission(pallet, fl, rackId);
                        if (missionInserted)
                        {
                            Log.Debug("Mission inserted using fallback rack logic.");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TryFindMissionInEmptyRack :: {ex.Message} :: PalletCode: {pallet?.PALLET_CODE}");
            }

            return false;
        }




        private bool TryInsertInfeedMission(dynamic pallet, dynamic floor, int rackId)
        {
            try
            {
                var emptyPositions = ats_wms_master_position_detailsTableAdapterInstance
                    .GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackId, 1, 0, 1, 0);

                if (emptyPositions == null || emptyPositions.Count == 0)
                {
                    Log.Debug($"No empty positions found in RackID: {rackId}.");
                    return false;
                }

                foreach (var pos in emptyPositions)
                {
                    Log.Debug($"Evaluating PositionID: {pos.POSITION_ID} in RackID: {rackId}");

                    var frontPositions = ats_wms_master_position_detailsTableAdapterInstance
                        .GetDataByRACK_IDAndPOSITION_IDGreaterThan(pos.RACK_ID, pos.POSITION_ID);

                    var hasFrontClear = frontPositions == null || frontPositions.Count == 0 || frontPositions[0].POSITION_IS_ALLOCATED == 0;
                    Log.Debug($"Checked front clearance for PositionID {pos.POSITION_ID}: {(hasFrontClear ? "CLEAR" : "BLOCKED")}");

                    if (!hasFrontClear)
                    {
                        Log.Debug("Front position is not clear. Skipping.");
                        continue;
                    }

                    var rackDetails = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(pos.RACK_ID, 1);
                    if (rackDetails == null || rackDetails.Count == 0)
                    {
                        Log.Debug("Rack details not found or rack is inactive. Skipping.");
                        continue;
                    }

                    var rack = rackDetails[0];

                    var infeedMissions = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance
                        .GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                    var outfeedMissions = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance
                        .GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                    var transferMissions = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance
                        .GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                    if ((infeedMissions?.Count ?? 0) > 0 ||
                        (outfeedMissions?.Count ?? 0) > 0 ||
                        (transferMissions?.Count ?? 0) > 0)
                    {
                        Log.Debug("One or more missions in progress for this rack. Skipping.");
                        continue;
                    }

                    var latestPallet = ats_wms_master_pallet_informationTableAdapterInstance
    .GetDataByPALLET_INFORMATION_ID(pallet.PALLET_INFORMATION_ID);

                    if (latestPallet == null)
                    {
                        Log.Debug($"Latest pallet data could not be retrieved for PalletCode: {pallet.PALLET_CODE}. Aborting insert.");
                        return false;
                    }
                    Log.Debug("latestPallet :: " + latestPallet[0].IS_INFEED_MISSION_GENERATED);
                    Log.Debug("latestPallet.IS_INFEED_MISSION_GENERATED :: " + latestPallet[0].IS_INFEED_MISSION_GENERATED);
                    if (Convert.ToInt32(latestPallet[0].IS_INFEED_MISSION_GENERATED) == 1)
                    {
                        Log.Debug($"Skipping insert — mission already generated (live check) for PalletCode: {pallet.PALLET_CODE}");
                        return false;
                    }


                    Log.Debug("No conflicting missions. Proceeding to insert mission.");
                    InsertInfeedMission(pallet, floor, rack, pos);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TryInsertInfeedMission :: {ex.Message} :: RackID: {rackId}, PalletCode: {pallet?.PALLET_CODE}");
            }

            return false;
        }





        private bool TryFindMissionWithSameMaterial(dynamic pallet, dynamic floor)
        {
            bool missionInserted = false;
            try
            {
                Log.Debug("Starting TryFindMissionWithSameMaterial logic...");

                var rackData = ats_wms_current_stock_detailsTableAdapterInstance
                    .GetDataByDistinctRack_IDwherePALLET_STATUS_IDAndFLOOR_IDAndAREA_ID((int)pallet.PALLET_STATUS_ID, (int)floor.FLOOR_ID, (int)floor.AREA_ID);

                Log.Debug("Fetched rackData based on pallet status, floor, and area IDs.");

                List<int> rackList = new List<int>();
                if (rackData != null)
                {
                    foreach (var item in rackData)
                    {
                        rackList.Add(item.RACK_ID);
                    }
                    rackList = rackList.Distinct().OrderBy(id => id).ToList();
                    Log.Debug($"Constructed and sorted distinct rackList: [{string.Join(", ", rackList)}]");
                }

                if (rackList == null || rackList.Count == 0)
                {
                    Log.Debug("rackList is empty. Returning false.");
                    return false;
                }

                var primaryRacks = new List<int> { 1, 27, 53, 79, 105 };

                foreach (var rackId in primaryRacks)
                {
                    Log.Debug($"Processing RackID: {rackId}");

                    if (!rackList.Contains(rackId))
                    {
                        Log.Debug($"RackID {rackId} is not part of rackList with same material. Skipping.");
                        continue;
                    }

                    var emptyPositions = ats_wms_master_position_detailsTableAdapterSeconPositionInstance
                        .GetDataByRACK_IDAndPOSITION_IS_EMPTYAndPOSITION_IS_ALLOCATEDAndPOSITION_IS_ACTIVEAndIS_MANUAL_DISPATCHOrderByPOSITION_IDAsc(rackId, 1, 0, 1, 0);

                    if (emptyPositions == null || emptyPositions.Count == 0)
                    {
                        Log.Debug("No empty positions found in this rack. Skipping.");
                        continue;
                    }

                    foreach (var pos in emptyPositions)
                    {
                        Log.Debug($"Evaluating PositionID: {pos.POSITION_ID} in RackID: {rackId}");

                        var frontPositions = ats_wms_master_position_detailsTableAdapterInstance
                            .GetDataByRACK_IDAndPOSITION_IDGreaterThan(pos.RACK_ID, pos.POSITION_ID);

                        var hasFrontClear = frontPositions == null || frontPositions.Count == 0 || frontPositions[0].POSITION_IS_ALLOCATED == 0;
                        Log.Debug($"Checked front clearance for PositionID {pos.POSITION_ID}: {(hasFrontClear ? "CLEAR" : "BLOCKED")}");

                        if (!hasFrontClear)
                        {
                            Log.Debug("Front position is not clear. Skipping.");
                            continue;
                        }

                        var rackDetails = ats_wms_master_rack_detailsTableAdapterInstance.GetDataByRACK_IDAndRACK_IS_ACTIVE(pos.RACK_ID, 1);
                        if (rackDetails == null || rackDetails.Count == 0)
                        {
                            Log.Debug("Rack details not found or rack is inactive. Skipping.");
                            continue;
                        }

                        var rack = rackDetails[0];

                        var infeedMissions = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance
                            .GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                        var outfeedMissions = ats_wms_outfeed_mission_runtime_detailsTableAdapterInstance
                            .GetDataByOUTFEED_MISSION_STATUSOrOUTFEED_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                        var transferMissions = ats_wms_transfer_pallet_mission_runtime_detailsTableAdapterInstance
                            .GetDataByTRANSFER_MISSION_STATUSOrTRANSFER_MISSION_STATUSAndRACK_ID("READY", "IN_PROGRESS", rack.RACK_ID);

                        if ((infeedMissions?.Count ?? 0) > 0 ||
                            (outfeedMissions?.Count ?? 0) > 0 ||
                            (transferMissions?.Count ?? 0) > 0)
                        {
                            Log.Debug("One or more missions in progress for this rack. Skipping.");
                            continue;
                        }

                        Log.Debug("No conflicting missions. Proceeding to insert mission.");
                        InsertInfeedMission(pallet, floor, rack, pos);
                        Log.Debug("Mission inserted with same material logic.");
                        missionInserted = true;
                        break;
                    }

                    if (missionInserted)
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TryFindMissionWithSameMaterial :: {ex.Message} :: PalletCode: {pallet?.PALLET_CODE}");
            }

            return missionInserted;
        }






        private void GenerateInfeedMission(dynamic pallet, int areaId)
        {
            try
            {
                Log.Debug("GenerateInfeedMission START :: Checking pallet state...");

                if (pallet == null)
                {
                    Log.Debug("Pallet is null. Aborting mission generation.");
                    return;
                }

                if (Convert.ToInt32(pallet.IS_INFEED_MISSION_GENERATED) == 1) 
                {
                    Log.Debug($"Pallet already has infeed mission generated :: PalletCode: {pallet.PALLET_CODE}");
                    return;
                }

                var rejectInProgress = ats_wms_infeed_mission_runtime_detailsTableAdapterInstance
                    .GetDataByINFEED_MISSION_STATUSOrINFEED_MISSION_STATUS1AndSTACKER_IDAndPALLET_STATUS_ID("READY", "IN_PROGRESS", 2, 7);

                if (Convert.ToInt32(pallet.PALLET_TYPE_ID) == 2 && rejectInProgress.Count > 0)
                {
                    Log.Debug("Reject pallet already has a mission in progress. Aborting.");
                    return;
                }

                Log.Debug($"Searching active floors for the area Id :: {areaId}");

                var activeFloors = ats_wms_mapping_floor_area_detailsTableAdapterInstance.GetDataByAREA_IDAndINFEED_IS_ACTIVE(areaId, 1);
                Log.Debug($"Found active floors count :: {activeFloors.Count}");

                // First: Try all floors for same material
                foreach (var floor in activeFloors)
                {
                    Log.Debug($"[SAME MATERIAL] Searching mission for FloorID: {floor.FLOOR_ID}");

                    var found = TryFindMissionWithSameMaterial(pallet, floor);
                    if (found)
                    {
                        Log.Debug("Mission generated using same material logic.");
                        return;
                    }
                }

                // Second: Try all floors for empty racks (fallback)
                foreach (var floor in activeFloors)
                {
                    Log.Debug($"[EMPTY RACK] Searching mission for FloorID: {floor.FLOOR_ID}");

                    var found = TryFindMissionInEmptyRack(pallet, floor);
                    if (found)
                    {
                        Log.Debug("Mission generated using empty rack fallback logic.");
                        return;
                    }
                }

                Log.Debug("Mission generation completed with no suitable racks found.");
            }
            catch (Exception ex)
            {
                Log.Error($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] GenerateInfeedMission ERROR :: {ex.Message} :: PalletCode: {pallet?.PALLET_CODE}");
            }
        }




        #region Ping funcationality

        public Boolean checkPlcPingRequest()
        {
            //Log.Debug("IprodPLCMachineXmlGenOperation :: Inside checkServerPingRequest");

            try
            {
                try
                {
                    pingSenderForThisConnection = new Ping();
                    replyForThisConnection = pingSenderForThisConnection.Send(IP_ADDRESS);
                }
                catch (Exception ex)
                {
                    Log.Error("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Exception occured while sending ping request :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                    replyForThisConnection = null;
                }

                if (replyForThisConnection != null && replyForThisConnection.Status == IPStatus.Success)
                {
                    //Log.Debug("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Ping success :: " + replyForThisConnection.Status.ToString());
                    return true;
                }
                else
                {
                    //Log.Debug("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Ping failed. ");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("checkPlcPingRequest :: for IP :: " + IP_ADDRESS + " Exception while checking ping request :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region Read and Write PLC tag

        [HandleProcessCorruptedStateExceptions]
        public string readTag(string tagName)
        {

            try
            {
                //Log.Debug("IprodPLCCommunicationOperation :: Inside readTag.");

                // Set PLC tag
                OPCItemIDs.SetValue(tagName, 1);
                //Log.Debug("readTag :: Plc tag is configured for plc group.");

                // remove all group
                ConnectedOpc.OPCGroups.RemoveAll();
                //Log.Debug("readTag :: Remove all group.");

                // Kepware configuration                
                CS6InfeedOPC = ConnectedOpc.OPCGroups.Add("AtsWmsCS_6S1InfeedMissionGenerationDetailsGroup");
                CS6InfeedOPC.DeadBand = 0;
                CS6InfeedOPC.UpdateRate = 500;
                CS6InfeedOPC.IsSubscribed = true;
                CS6InfeedOPC.IsActive = true;
                CS6InfeedOPC.OPCItems.AddItems(1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
                //Log.Debug("readTag :: Kepware properties configuration is complete.");

                // Read tag
                CS6InfeedOPC.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 1, ref
                   ItemServerHandles, out ItemServerValues, out ItemServerErrors, out kDIR, out lDIR);

                //Log.Debug("readTag ::  tag name :: " + tagName + " tag value :: " + Convert.ToString(ItemServerValues.GetValue(1)));

                if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("True"))
                {
                    Log.Debug("readTag :: Found and Return True");
                    return "True";
                }
                else if (Convert.ToString(ItemServerValues.GetValue(1)).Equals("False"))
                {
                    Log.Debug("readTag :: Found and Return False");
                    return "False";
                }
                else
                {
                    Log.Debug("readTag :: Found read value :: " + (ItemServerValues.GetValue(1)));
                    return Convert.ToString(ItemServerValues.GetValue(1));

                }

            }
            catch (Exception ex)
            {
                Log.Error("readTag :: Exception while reading plc tag :: " + tagName + " :: " + ex.Message);
                OnConnectPLC();
            }

            Log.Debug("readTag :: Return False.. retun null.");

            return "False";
        }

        [HandleProcessCorruptedStateExceptions]
        public Boolean writeTag(string tagName, string tagValue)
        {

            try
            {
                Log.Debug("IprodGiveMissionToStacker :: Inside writeTag.");

                // Set PLC tag
                OPCItemIDs.SetValue(tagName, 1);
                //Log.Debug("writeTag :: Plc tag is configured for plc group.");

                // remove all group
                ConnectedOpc.OPCGroups.RemoveAll();
                //Log.Debug("writeTag :: Remove all group.");

                // Kepware configuration                  
                CS6InfeedOPC = ConnectedOpc.OPCGroups.Add("AtsWmsCS_6S1InfeedMissionGenerationDetailsGroup");
                CS6InfeedOPC.DeadBand = 0;
                CS6InfeedOPC.UpdateRate = 500;
                CS6InfeedOPC.IsSubscribed = true;
                CS6InfeedOPC.IsActive = true;
                CS6InfeedOPC.OPCItems.AddItems(1, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);
                //Log.Debug("writeTag :: Kepware properties configuration is complete.");

                // read plc tags
                CS6InfeedOPC.SyncRead((short)OPCAutomation.OPCDataSource.OPCDevice, 1, ref
                   ItemServerHandles, out ItemServerValues, out ItemServerErrors, out kDIR, out lDIR);

                // Add tag value
                ItemServerValues.SetValue(tagValue, 1);

                // Write tag
                CS6InfeedOPC.SyncWrite(1, ref ItemServerHandles, ref ItemServerValues, out ItemServerErrors);

                return true;

            }
            catch (Exception ex)
            {
                Log.Error("writeTag :: Exception while writing mission data in the plc tag :: " + tagName + " :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
                OnConnectPLC();
            }

            return false;

        }

        #endregion

        #region Connect and Disconnect PLC

        private void OnConnectPLC()
        {

            Log.Debug("OnConnectPLC :: inside OnConnectPLC");

            try
            {
                // Connection url
                if (!((ConnectedOpc.ServerState.ToString()).Equals("1")))
                {
                    ConnectedOpc.Connect(plcServerConnectionString, "");
                    Log.Debug("OnConnectPLC :: PLC connection successful and OPC server state is :: " + ConnectedOpc.ServerState.ToString());
                }
                else
                {
                    Log.Debug("OnConnectPLC :: Already connected with the plc.");
                }

            }
            catch (Exception ex)
            {
                Log.Error("OnConnectPLC :: Exception while connecting to plc :: " + ex.Message + " stackTrace :: " + ex.StackTrace);
            }
        }

        private void OnDisconnectPLC()
        {
            Log.Debug("inside OnDisconnectPLC");

            try
            {
                ConnectedOpc.Disconnect();
                Log.Debug("OnDisconnectPLC :: Connection with the plc is disconnected.");
            }
            catch (Exception ex)
            {
                Log.Error("OnDisconnectPLC :: Exception while disconnecting to plc :: " + ex.Message);
            }

        }


        #endregion
    }
}


