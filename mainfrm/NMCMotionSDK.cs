//=============================================================================
//
// MMCE Motion SDK
// RSAutomation.co.,LTD.
// 2014-03-14
// Revision History
//
//=============================================================================

using System.Runtime.InteropServices;
using System.Text;

static class NMCDEF
{
    public const uint MAX_EEPROM_SIZE        = 0xFFFF;  //16bit max (64k)
    public const uint ECAT_MAX_NAME          = 0xFF;    //255
    public const uint MAX_BOARD_CNT          = 4;
    public const uint MAX_AXIS_CNT           = 64;
    public const uint MAX_AXIS_ID            = 65535;   //Ver_0c010012_6
    public const uint MAX_NODE_CNT           = 256;
    public const uint MAX_PHYSICAL_ADDR      = 65535;   //Ver_0c010012_3
    public const uint MAX_MASTER_ID          = 10;
    public const uint MAX_ERR_LEN            = 128;
    public const uint MAX_PROFILE_ITEM_COUNT = 6;
    public const uint MAX_AXES_IN_GROUP      = 8;       //v12.1.0.45
    public const uint MAX_AXES_GROUP_CNT     = 32;
    public const uint MAX_AXES_STATUS_SIZE   = 8;
    public const uint MAX_ALL_STATUS_SIZE    = 1536;	//Ver_0c010019_1 : 계산식 = (MAX_AXIS_CNT + MAX_AXES_GROUP_CNT)

	public const uint MAX_LOGICAL_AXIS_COUNT   = 64;	//v12.1.0.41
	public const uint MAX_LOGICAL_DEVICE_COUNT = 1024;  //v12.1.0.41
    public const uint MAX_DEVICE_NAME_SIZE     = 256;   //v12.1.0.64

    public const ushort INVALID_BOARD    = 0xFFFF;
    public const ushort INVALID_AXIS     = 0xFFFF;
    public const ushort INVALID_MASTERID = 0xFFFF;
    public const uint   INVALID_FLASH    = 0xFFFFFFFF;

    //Axis Parameter Definition
    public const uint COMMANDED_POSITION        = 1;
    public const uint SW_LIMIT_POS              = 2;
    public const uint SW_LIMIT_NEG              = 3;
    public const uint ENABLE_LIMIT_POS          = 4;
    public const uint ENABLE_LIMIT_NEG          = 5;
    public const uint ENABLE_POS_LAG_MONITORING = 6;
    public const uint MAX_POSITION_LAG          = 7;
    public const uint MAX_VELOCITY_SYSTEM       = 8;
    public const uint MAX_VELOCITY_APPL         = 9;
    public const uint ACTUAL_VELOCITY           = 10;
    public const uint COMMANDED_VELOCITY        = 11;
    public const uint MAX_ACCELERATION_SYSTEM   = 12;
    public const uint MAX_ACCELERATION_APPL     = 13;
    public const uint MAX_DECELERATION_SYSTEM   = 14;
    public const uint MAX_DECELERATION_APPL     = 15;
    public const uint MAX_JERK                  = 16;
    public const uint ACTUAL_POSITION           = 1001;
    public const uint PROFILE_DACCEL            = 1100;
	
    public const uint IOMAP_HEADER_SIZE = 28; //DSP 0.13 이상에서 변경됨 2014.08.26 (32 - 4)
    public const uint IOPAGE_SIZE = 4096;
}

namespace NMCMotionSDK
{

    /// <summary>
    /// Library Class
    /// </summary>
    /// 
    public partial class NMCSDKLib
    {
        //DLL name should be specified what you have.
        public const string NMC_DLL_NAME = @"C:\Program Files\RS Automation\NMC\MotionSDK\NMC_Motion.dll";

        //axis_source status
        public enum MC_STATUS : uint
        {
			MC_OK                                   			=	0x00000000,
			
			//Device Driver Error Defines 
			MC_ERROR_HW_NOT_INSTALLED							=	0x000000DC,
			MC_ERROR_DD_SEND_ERROR								=	0x000000DD,
			MC_ERROR_DD_READ_ERROR								=	0x000000DE,
			MC_DD_ERROR_SEND									=	0x000000DF,
			MC_DD_ERROR_RECV									=	0x000000E0,
			MC_DD_OPEN_FAIL										=	0x000000E6,
			MC_DD_NOT_OPENED									=	0x000000E7,
			MC_DD_CONN_FAIL										=	0x000000E8,
			MC_DD_CLIENT_START_FAIL								=	0x000000E9,
			MC_DD_OK											=	0x00000000,
			MC_CN_NOT_CONNECTED									=	0x000000F0,
			MC_CN_CONNECTED										=	0x000000F1,
			MC_CN_CONNERROR										=	0x000000F2,

			//PLCOpen Motion Command Response Error Defines (F/W)
			MC_INVALID_SYSTEM_STATE								=	0x00010000, 
			MC_UNSUPPORT_CMD									=	0x00020000,
            MC_INVALID_AXIS_STATE_NOT_HOMING_MODE               =   0x00020047, //v12.1.0.42 
            MC_UNSUPPORTED_CMD_AXIS_HOMING                      =   0x00024148, //v12.2.0.1
	        MC_UNSUPPORTED_CMD_INVALID_ETHERCAT_CYCLE_TIME      =   0x00024354, //v12.2.0.10
            MC_UNSUPPORTED_CMD_INVALID_ETHERCAT_OPERATION_MODE  =   0x0002454D, //v12.2.0.1
            MC_UNSUPPORTED_CMD_BOARD_CATALOG                    =   0x00024743, //v12.2.0.1
            MC_UNSUPPORTED_CMD_GANTRY_ENABLING                  =   0x00024745, //v12.2.0.1
            MC_UNSUPPORTED_CMD_GANTRY_HOMING                    =   0x00024748, //v12.2.0.1
            MC_UNSUPPORTED_CMD_GANTRY_YAW_UNSTABLE              =   0x00024759, //v12.2.0.1
            MC_UNSUPPORTED_CMD_BOARD_CATALOG_IO                 =   0x00024943, //v12.2.0.3
            MC_UNSUPPORTED_CMD_INVALID_REF_AXIS_STATUS		    =   0x00025241, //v12.2.0.10
            MC_UNSUPPORTED_CMD_SLAVE_HOMING                     =   0x00025348, //v12.2.0.1
            MC_UNSUPPORTED_CMD_SLAVE_HOME_MODE                  =   0x0002534D, //v12.2.0.1
            MC_UNSUPPORTED_CMD_TORQUE_LIMIT                     =   0x0002544C, //v12.2.0.10
			MC_INVALID_PARAM									=	0x00030000,
			MC_INVALID_PARAM_1									=	0x00030001,
			MC_INVALID_PARAM_2									=	0x00030002,
			MC_INVALID_PARAM_3									=	0x00030003,
			MC_INVALID_PARAM_4									=	0x00030004,
			MC_INVALID_PARAM_5									=	0x00030005,
			MC_INVALID_PARAM_6									=	0x00030006,
			MC_INVALID_PARAM_7									=	0x00030007,
			MC_INVALID_PARAM_8									=	0x00030008,
			MC_INVALID_PARAM_9									=	0x00030009,
			MC_INVALID_PARAM_10									=	0x0003000A,
            MC_INVALID_GANTRY_CONDITION                         =   0x00030018, //v12.2.0.0
			MC_INVALID_SIZE										=	0x00040000,
			MC_INVALID_AXIS_NUM									=	0x00050000,
            MC_GANTRY_SLAVE_AXIS_CONFIG                         =   0x00050017, //v12.2.0.0
			MC_NOT_ENOUGH_RESOURCE								=	0x00060000,
			MC_LIMIT_ERROR_PARAM								=	0x00070000,
			MC_LIMIT_ERROR_PARAM_1								=	0x00070001,
			MC_LIMIT_ERROR_PARAM_2								=	0x00070002,
			MC_LIMIT_ERROR_PARAM_3								=	0x00070003,
			MC_LIMIT_ERROR_PARAM_4								=	0x00070004,
			MC_LIMIT_ERROR_PARAM_5								=	0x00070005,
			MC_LIMIT_ERROR_PARAM_6								=	0x00070006,
			MC_LIMIT_ERROR_PARAM_7								=	0x00070007,
			MC_LIMIT_ERROR_PARAM_8								=	0x00070008,
			MC_LIMIT_ERROR_PARAM_9								=	0x00070009,
			MC_LIMIT_ERROR_PARAM_10								=	0x0007000A,
			MC_INVALID_DEVICE_STATE								=	0x00080000,
			MC_INVALID_DEVICE_STATE_ERROR						=	0x00080001,
            MC_INVALID_DEVICE_STATE_GANTRY_OPERATION_MODE       =   0x00080017, //v12.2.0.0
            MC_GANTRY_UNMATCHED_ETHERCAT_CYCLE_TIME             =   0x00080025, //v12.2.0.1
			MC_INVALID_AXIS_STATE_DISABLED						=	0x00090000,
			MC_INVALID_AXIS_STATE_STANDSTILL					=	0x00090001, //v12.1.0.42
			MC_INVALID_AXIS_STATE_DISCRETE_MOTION				=	0x00090002, //v12.1.0.42
			MC_INVALID_AXIS_STATE_CONTINUOUS_MOTION				=	0x00090003, //v12.1.0.42
			MC_INVALID_AXIS_STATE_SYNC_MOTION					=	0x00090004,
			MC_INVALID_AXIS_STATE_HOMING						=	0x00090005, //v12.1.0.42
			MC_INVALID_AXIS_STATE_STOPPING						=	0x00090006,
			MC_INVALID_AXIS_STATE_ERRORSTOP						=	0x00090007,
			MC_INVALID_AXIS_STATE_MODE_CHANGE					=	0x00090008, //v12.1.0.42
            MC_MASKED_IO_MASTER_AXIS                            =   0x00090016, //v12.2.0.0         
            MC_INVALID_AXIS_STATE_EC_ENABLED                    =   0x00090020,	//v12.2.0.0
			MC_INVALID_AXIS_CONFIG								=	0x000A0000,
            MC_INVALID_CONFIG_AXIS_TYPE                         =   0x000A0002, //v12.2.0.0            
            MC_INVALID_AXIS_CONFIG_POS_LIMIT_SWITCH				=	0x000A0006, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_NEG_LIMIT_SWITCH				=	0x000A0009, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_HOME_SWITCH					=	0x000A000B, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_Z_PHASE_INPUT				=	0x000A000D, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_HOME_SENSOR					=	0x000A0010,
			MC_INVALID_AXIS_CONFIG_MARK_PULSE					=	0x000A0012,
            MC_AXIS_CONFIG_JOYSTICK                             =   0x000A001F,	//v12.2.0.0
            MC_INVALID_INPUT_SHAPING_FREQUENCY                  =   0x000A0020, //v12.2.0.0
            MC_INVALID_INPUT_SHAPING_DAMPIN_RATIO               =   0x000A0021, //v12.2.0.0
            MC_UNSUPPORTED_INPUT_SHAPING_MODE_IN_THIS_API       =   0x000A0022, //v12.2.0.0
            MC_UNSUPPORTED_AXIS_TYPE_IN_THIS_API                =   0x000A0023, //v12.2.0.0
            MC_INVALID_AXIS_CONFIG_HOME_TYPE					=	0x000A0032, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_HOME_FLAG_HANDLE				=	0x000A003A, //v12.1.0.42
			MC_INVALID_AXIS_CONFIG_HOMING_MODE					=	0x000A0064,	
			MC_GEARING_RULE_VIOLATION                           =	0x000B0000,	
			MC_LIMIT_POSITION_OVER								=	0x000C0000,	
			MC_POS_HW_LIMIT_POSITION_OVER						=	0x000C0001,	//v12.1.0.42
			MC_NEG_HW_LIMIT_POSITION_OVER						=	0x000C0002,	//v12.1.0.42
			MC_POS_SW_LIMIT_POSITION_OVER						=	0x000C0004, //v12.1.0.42
			MC_NEG_SW_LIMIT_POSITION_OVER						=	0x000C0008, //v12.1.0.42
			MC_INVALID_AXES_GROUP_NUM							=	0x000D0000,
			MC_AXIS_ALREADY_ASSIGNED							=	0x000E0002, //v12.1.0.42
			MC_IDENT_ALREADY_ASSIGNED							=	0x000E0004, //v12.1.0.42
			MC_AXES_GROUP_INVALID_STATE							=	0x000F0000,	
			MC_GROUP_INVALID_STATE_MOVING						=	0x000F0002,	//v12.1.0.42
			MC_GROUP_INVALID_STATE_HOMING						=	0x000F0003,	//v12.1.0.42
			MC_GROUP_INVALID_STATE_STOPPING						=	0x000F0004,	//v12.1.0.42
			MC_GROUP_INVALID_STATE_ERRORSTOP					=	0x000F0005,	//v12.1.0.42
			MC_AXIS_IN_SINGLE_MOTION_STATE						=	0x00100000,	
			MC_1ST_AXIS_IN_MOTION_STATE							=	0x00100001,	//v12.1.0.42
			MC_2ND_AXIS_IN_MOTION_STATE							=	0x00100002,	//v12.1.0.42
			MC_3RD_AXIS_IN_MOTION_STATE							=	0x00100003,	//v12.1.0.42
			MC_4TH_AXIS_IN_MOTION_STATE							=	0x00100004,	//v12.1.0.42
			MC_5TH_AXIS_IN_MOTION_STATE							=	0x00100005,	//v12.1.0.42
			MC_6TH_AXIS_IN_MOTION_STATE							=	0x00100006,	//v12.1.0.42
			MC_7TH_AXIS_IN_MOTION_STATE							=	0x00100007,	//v12.1.0.42
			MC_8TH_AXIS_IN_MOTION_STATE							=	0x00100008,	//v12.1.0.42
			MC_GROUP_MEMBER_EMPTY								=	0x00110000,
            MC_GROUP_MEMBER_EMPTY_1                             =   0x00110001, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_2                             =   0x00110002, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_3                             =   0x00110003, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_4                             =   0x00110004, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_5                             =   0x00110005, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_6                             =   0x00110006, //v12.2.0.1
            MC_GROUP_MEMBER_EMPTY_7                             =   0x00110007, //v12.2.0.1
			MC_1ST_AXIS_IN_GROUP_LIMIT_OVER 					=	0x00120000, //v12.1.0.42
			MC_2ND_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120001,	//v12.1.0.42
			MC_3RD_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120002,	//v12.1.0.42
			MC_4TH_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120003,	//v12.1.0.42
			MC_5TH_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120004,	//v12.1.0.42
			MC_6TH_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120005,	//v12.1.0.42
			MC_7TH_AXIS_IN_GROUP_LIMIT_OVER						=	0x00120006,	//v12.1.0.42
			MC_8TH_AXIS_IN_GROUP_LIMIT_OVER 					=	0x00120007,	//v12.1.0.42
			MC_GROUP_CMD_SIZE_ERROR							    =	0x00130000,	
			MC_GROUP_CMD_PARAMETER_SIZE_ERROR					=	0x00130003,	//v12.1.0.42
			MC_GROUP_MEMBER_NOT_ALLOCATED_X						=	0x00140000,	//v12.1.0.42
			MC_GROUP_MEMBER_NOT_ALLOCATED_Y						=	0x00140001,	//v12.1.0.42
            MC_GROUP_NOT_ALLOCATED_ROTATION_AXIS                =   0x00140003, //v12.2.0.0
            MC_GROUP_NOT_ALLOCATED_TILT_AXIS                    =   0x00140004, //v12.2.0.0            
			MC_AXIS_IN_GROUP_MOTION								=	0x00150000,
            MC_GANTRY_ASSIGNED_NOT                              =   0x001A0000, //v12.2.0.0
            MC_GANTRY_SLAVE_AXIS                                =   0x00170000, //v12.2.0.0
            MC_MASKED_IO_SLAVE_AXIS                             =   0x00170016, //v12.2.0.0            
            MC_GANTRY_ASSIGNED_ALREADY_AXIS                     =   0x00180000, //v12.2.0.0
            MC_ALREADY_ENABLED_GANTRY_ID                        =   0x00180047, //v12.2.0.0
            MC_CHECK_LASTINDEX_ON_EC_DATA_FILE                  =   0x001B0000, //v12.2.0.0	
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_1ST_COMMAND_POSITION =   0x001C0001, //v12.2.0.0
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_2ND_COMMAND_POSITION =   0x001C0002, //v12.2.0.0
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_3RD_COMMAND_POSITION =   0x001C0003, //v12.2.0.0
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_1ST_ACTUAL_POSITION  =   0x001D0001, //v12.2.0.0
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_2ND_ACTUAL_POSITION  =   0x001D0002, //v12.2.0.0
            MC_CHECK_EC_DATA_FILE_FOCUS_ON_3RD_ACTUAL_POSITION  =   0x001D0003, //v12.2.0.0
            MC_CAN_NOT_CHANGE_EC_MODE_BY_INVALID_AMP_STATE      =   0x001E0019, //v12.2.0.0
            MC_ALREADY_EC_MODE_ENABLE                           =   0x001E0020, //v12.2.0.0
            MC_ALREADY_EC_MODE_DISABLE                          =   0x001E0021, //v12.2.0.0
            MC_EC_DIMENSION_MISMATCH                            =   0x001E0022, //v12.2.0.0
            MC_EC_TABLE_EMPTIED                                 =   0x001E0023, //v12.2.0.0
            MC_EC_TABLE_INDEX_MISMATCH                          =   0x001E0024, //v12.2.0.0
            MC_IO_INVALID_STATE_EMPTY_MASKED_IO                 =   0x00200000, //v12.1.0.66
            MC_IO_INVALID_STATE_ALREADY_MASKED_IO               =   0x0020000E, //v12.1.0.66
            MC_IO_INVALID_STATE_REQUEST_UNMASK_IO               =   0x00200016, //v12.1.0.66
            MC_NOT_ENOUGH_EC_MEMORY                             =   0x00240000, //v12.2.0.0
            MC_ALREADY_DISABLED_GANTRY_ID                       =   0x00250047, //v12.2.0.0
            MC_INVALID_GANTRY_STATE_NOT_COMPLETE_MOTION         =   0x00260000, //v12.2.0.0
            MC_GANTRY_IN_HOMING_SEQUENCE                        =   0x00270000, //v12.2.0.0
            MC_INVALID_GANTRY_STATE_DO_NOT_CHANGE_RATIO         =   0x00280001, //v12.2.0.1
            MC_INVALID_GANTRY_STATE_EC_ENABLED                  =   0x00280020, //v12.2.0.0
            MC_INVALID_GANTRY_STATE_ENABLED_TORQUE_LIMIT		=   0x00280026, //v12.2.0.10
	        MC_INVALID_GANTRY_STATE_ALIGN_SW_LIMIT				=   0x00280027, //v12.2.0.10
	        MC_INVALID_AXIS_ID_ENTER_GANTRY_SALVE_AXIS_ID		=   0x00290000, //v12.2.0.10
	        MC_INVALID_PARAMETER_ATTRIBUTE						=   0x002A0000, //v12.2.0.10            
            MC_GROUP_EC_MODE_ENABLED                            =   0x11010020, //v12.2.0.0
            MC_GANTRY_EC_MODE_ENABLED                           =   0x12000020, //v12.2.0.0

			//Motion Library Error Defines
			MC_FAIL												=	0xE00C0001,
			MC_ERROR											=	0xE00C0002,
			MC_IOMAPING_ERR										=	0xE00C0003,
			MC_COMMINIT_ERR										=	0xE00C0004,
			MC_COMM_EVENT_INIT_ERR								=	0xE00C0005,
			MC_READ_ENI_NODE_ERR								=	0xE00C0006,
			MC_INVALID_AXIS_ERR									=	0xE00C0007,
			MC_INVALID_BOARD_ERR								=	0xE00C0008,
			MC_XML_PARSING_ERR									=	0xE00C0009,
			MC_XML_ITEM_COUNT_MISMATCH							=	0xE00C000A,
			MC_NO_BOARD_INSTALLED								=	0xE00C000B,
			MC_INVALID_DOWNLOAD_FILE_TYPE						=	0xE00C000C,
			MC_OPEN_ENI_ERR										=	0xE00C000D,
			MC_FILE_OPEN_FAIL									=	0xE00C000E,
			MC_NO_MATCHING_DOWNLOADINFORMATION					=	0xE00C000F,
			MC_NONE_OP											=	0xE00C0010,
			MC_FAIL_GEN_DOWNLOAD_FILE							=	0xE00C0011,
			MC_REG_KEY_READ_FAIL								=	0xE00C0012,
			MC_NOT_ALLOWED_IN_THIS_MASTER_MODE					=	0xE00C0014,
			MC_MASTERID_OUT_OF_RANGE							=	0xE00C0015,			
			MC_BOARDNO_OUT_OF_RANGE								=	0xE00C0016,
			MC_AXISNO_OUT_OF_RANGE								=	0xE00C0017,
			MC_BOARDCNT_OUT_OF_RANGE							=	0xE00C0018,
			MC_RETURN_SIZE_NOT_EQUAL							=	0xE00C001A,
			MC_MASTERID_DUPLICATION_ERR							=	0xE00C001B,
			MC_PARAM_ERROR_FILE_IS_NULL							=	0xE00C001C,
			MC_NO_MATCHING_BOARDID_FOUND						=	0xE00C001D,
			MC_NOT_READY_NETWORK_CONFIGURATION                  =   0xE00C001E,
			MC_INVALID_MASTERID_ERR								=	0xE00C001F,
			MC_MASTER_MODE_CHANGE_NOT_ALLOWED					=	0xE00C0020,
			MC_MASTER_REQUEST_PARAM_ERROR   					=	0xE00C0021,
			MC_MASTER_INVALID_STATE         					=	0xE00C0022,  
			MC_NOT_MOTION_LIBRAY_INITIALIZED                 	=	0xE00C0023, //2014.08.22
			MC_IOMANAGER_NOT_RUNNING		                 	=	0xE00C0024, //2014.08.22
			MC_ANOTHER_PROGRAM_IS_USING_NMC_LIBRARY             =	0xE00C0025, //2014.10.02
			MC_SLAVE_ITEM_MISMATCH		                        =	0xE00C0026, 
			MC_SLAVE_ITEM_COUNT_MISMATCH				        =	0xE00C0027, 
			MC_AXIS_COUNT_OUT_OF_RANGE							=   0xE00C0028, //v12.1.0.46 
            MC_SLAVE_DEVICEID_OUT_OF_RANGE                      =   0xE00C0029, //v12.1.0.63
            MC_FM_TRIG_COUNT_OUT_OF_RANGE                       =   0xE00C002A, //v12.1.0.64
            MC_FM_IDXNO_OUT_OF_RANGE                            =   0xE00C002B, //v12.1.0.64
            MC_MODULE_INFO_FILE_OPEN_FAIL                       =   0xE00C002C, //v12.1.0.64
            MC_ID_IDX_TYPE_OUT_OF_RANGE                         =   0xE00C002D, //v12.1.0.66
            MC_WAIT_TIME_OUT_OF_RANGE                           =   0xE00C0030, //v12.1.0.67	
            MC_SELF_HOLD_IO_NOT_FOUND                           =   0xE00C0031, //v12.1.0.67
            MC_INVALID_SLAVE_SAFEOP_STATE                       =   0xE00C0032, //v12.1.0.67
            MC_AXIS_TYPE_OUT_OF_RANGE                           =   0xE00C0040, //v12.2.0.0
            MC_EC_TYPE_OUT_OF_RANGE                             =   0xE00C0041, //v12.2.0.0
            MC_EC_MODE_OUT_OF_RANGE                             =   0xE00C0042, //v12.2.0.0
            MC_EC_MAPID_OUT_OF_RANGE                            =   0xE00C0043, //v12.2.0.0
            MC_EC_LAST_INDEX_OUT_OF_RANGE                       =   0xE00C0044, //v12.2.0.0
            MC_BOARD_CATALOG_MISMATCH_ERROR                     =   0xE00C0045, //v12.2.0.10

			//Comm Library Error Defines
			MC_PCICIP_GEN_10    								=	0xCC100000,
			COMM_CONNECTION_ESTABLISHED                     	=	0xED000001,
			COMM_CONN_CONFIG_FAILED_INVALID_NETWORK_PATH    	=	0xED000002,
			COMM_CONN_CONFIG_FAILED_NO_RESPONSE             	=	0xED000003,
			COMM_CONN_CONFIG_FAILED_ERROR_RESPONSE          	=	0xED000004,
			COMM_CONNECTION_TIMED_OUT                       	=	0xED000005,
			COMM_CONNECTION_CLOSED                          	=	0xED000006,
			COMM_INCOMING_CONNECTION_RUN_IDLE_FLAG_CHANGED  	=	0xED000007,
			COMM_ASSEMBLY_NEW_INSTANCE_DATA                 	=	0xED000008,
			COMM_ASSEMBLY_NEW_MEMBER_DATA                   	=	0xED000009,
			COMM_CONNECTION_NEW_INPUT_SCANNER_DATA          	=	0xED00000A,
			COMM_CONNECTION_VERIFICATION						=	0xED00000B,
			COMM_CONNECTION_RECONFIGURED                    	=	0xED00000C,
			COMM_REQUEST_RESPONSE_RECEIVED                  	=	0xED000064,
			COMM_REQUEST_FAILED_INVALID_NETWORK_PATH        	=	0xED000065,
			COMM_REQUEST_TIMED_OUT                          	=	0xED000066,
			COMM_CLIENT_OBJECT_REQUEST_RECEIVED             	=	0xED000067,
			COMM_NEW_CLASS3_RESPONSE                        	=	0xED000068,
			COMM_CLIENT_PCCC_REQUEST_RECEIVED               	=	0xED000069,
			COMM_NEW_LIST_IDENTITY_RESPONSE						=	0xED00006A,
			COMM_ID_RESET										=	0xED00006B,
			COMM_BACKPLANE_REQUEST_RECEIVED						=	0xED00006C,
			COMM_OUT_OF_MEMORY                              	=	0xED0000C8,
			COMM_UNABLE_INTIALIZE_WINSOCK                   	=	0xED0000C9,
			COMM_UNABLE_START_THREAD                        	=	0xED0000CA,
			COMM_ERROR_USING_WINSOCK                        	=	0xED0000CB,
			COMM_ERROR_SETTING_SOCKET_TO_NONBLOCKING        	=	0xED0000CC,
			COMM_ERROR_SETTING_TIMER                        	=	0xED0000CD,
			COMM_SESSION_COUNT_LIMIT_REACHED                	=	0xED0000CE,
			COMM_CONNECTION_COUNT_LIMIT_REACHED             	=	0xED0000CF,
			COMM_PENDING_REQUESTS_LIMIT_REACHED             	=	0xED0000D0,
			COMM_PENDING_REQUEST_GROUPS_LIMIT_REACHED       	=	0xED0000D1,
			COMM_ERROR_UNABLE_START_MODBUS						=	0xED0000D2,
			COMM_ERROR_HW_NOT_INSTALLED							=	0xED0000DC,
			COMM_ERROR_DD_SEND_ERROR							=	0xED0000DD,
			COMM_ERROR_DD_READ_ERROR							=	0xED0000DE,
			COMM_DD_ERROR_SEND									=	0xED0000DF,
			COMM_DD_ERROR_RECV									=	0xED0000E0,
			COMM_DD_OPEN_FAIL									=	0xED0000E6,
			COMM_DD_NOT_OPENED									=	0xED0000E7,
			COMM_DD_CONN_FAIL									=	0xED0000E8,
			COMM_DD_CLIENT_START_FAIL							=	0xED0000E9,
			COMM_DD_OK											=	0xED000000,
			COMM_CN_NOT_CONNECTED								=	0xED0000F0,
			COMM_CN_CONNECTED									=	0xED0000F1,
			COMM_CN_CONNERROR									=	0xED0000F2,
			//
			COMM_ERROR_SUCCESS									=	0xEE000000,
			COMM_ERROR_FAILURE									=	0xEE010000,
			COMM_EXT_ERR_DUPLICATE_FWD_OPEN						=	0xEE010100,
			COMM_EXT_ERR_CLASS_TRIGGER_INVALID					=	0xEE010103,
			COMM_EXT_ERR_OWNERSHIP_CONFLICT						=	0xEE010106,
			COMM_EXT_ERR_CONNECTION_NOT_FOUND					=	0xEE010107,
			COMM_EXT_ERR_INVALID_CONN_TYPE						=	0xEE010108,
			COMM_EXT_ERR_INVALID_CONN_SIZE						=	0xEE010109,
			COMM_EXT_ERR_DEVICE_NOT_CONFIGURED					=	0xEE010110,
			COMM_EXT_ERR_RPI_NOT_SUPPORTED						=	0xEE010111,
			COMM_EXT_ERR_CONNECTION_LIMIT_REACHED				=	0xEE010113,
			COMM_EXT_ERR_VENDOR_PRODUCT_CODE_MISMATCH			=	0xEE010114,
			COMM_EXT_ERR_PRODUCT_TYPE_MISMATCH					=	0xEE010115,
			COMM_EXT_ERR_REVISION_MISMATCH						=	0xEE010116,
			COMM_EXT_ERR_INVALID_CONN_POINT						=	0xEE010117,
			COMM_EXT_ERR_INVALID_CONFIG_FORMAT					=	0xEE010118,
			COMM_EXT_ERR_NO_CONTROLLING_CONNECTION				=	0xEE010119,
			COMM_EXT_ERR_TARGET_CONN_LIMIT_REACHED				=	0xEE01011A,
			COMM_EXT_ERR_RPI_SMALLER_THAN_INHIBIT				=	0xEE01011B,
			COMM_EXT_ERR_CONNECTION_TIMED_OUT					=	0xEE010203,
			COMM_EXT_ERR_UNCONNECTED_SEND_TIMED_OUT				=	0xEE010204,
			COMM_EXT_ERR_PARAMETER_ERROR						=	0xEE010205,
			COMM_EXT_ERR_MESSAGE_TOO_LARGE						=	0xEE010206,
			COMM_EXT_ERR_UNCONN_ACK_WITHOUT_REPLY				=	0xEE010207,
			COMM_EXT_ERR_NO_BUFFER_MEMORY_AVAILABLE				=	0xEE010301,
			COMM_EXT_ERR_BANDWIDTH_NOT_AVAILABLE				=	0xEE010302,
			COMM_EXT_ERR_TAG_FILTERS_NOT_AVAILABLE				=	0xEE010303,
			COMM_EXT_ERR_REAL_TIME_DATA_NOT_CONFIG				=	0xEE010304,
			COMM_EXT_ERR_PORT_NOT_AVAILABLE						=	0xEE010311,
			COMM_EXT_ERR_LINK_ADDR_NOT_AVAILABLE				=	0xEE010312,
			COMM_EXT_ERR_INVALID_SEGMENT_TYPE_VALUE				=	0xEE010315,
			COMM_EXT_ERR_PATH_CONNECTION_MISMATCH				=	0xEE010316,
			COMM_EXT_ERR_INVALID_NETWORK_SEGMENT				=	0xEE010317,
			COMM_EXT_ERR_INVALID_LINK_ADDRESS					=	0xEE010318,
			COMM_EXT_ERR_SECOND_RESOURCES_NOT_AVAILABLE			=	0xEE010319,
			COMM_EXT_ERR_CONNECTION_ALREADY_ESTABLISHED			=	0xEE01031A,
			COMM_EXT_ERR_DIRECT_CONN_ALREADY_ESTABLISHED		=	0xEE01031B,
			COMM_EXT_ERR_MISC									=	0xEE01031C,
			COMM_EXT_ERR_REDUNDANT_CONNECTION_MISMATCH			=	0xEE01031D,
			COMM_EXT_ERR_NO_MORE_CONSUMER_RESOURCES				=	0xEE01031E,
			COMM_EXT_ERR_NO_TARGET_PATH_RESOURCES				=	0xEE01031F,
			COMM_EXT_ERR_VENDOR_SPECIFIC						=	0xEE010320,
			COMM_ERROR_NO_RESOURCE								=	0xEE020000,
			COMM_ERROR_INVALID_PARAMETER_VALUE					=	0xEE030000,
			COMM_ERROR_INVALID_SEG_TYPE							=	0xEE040000,
			COMM_ERROR_INVALID_DESTINATION						=	0xEE050000,
			COMM_ERROR_PARTIAL_DATA								=	0xEE060000,
			COMM_ERROR_CONN_LOST								=	0xEE070000,
			COMM_ERROR_BAD_SERVICE								=	0xEE080000,
			COMM_ERROR_BAD_ATTR_DATA							=	0xEE090000,
			COMM_ERROR_ATTR_LIST_ERROR							=	0xEE0A0000,
			COMM_ERROR_ALREADY_IN_REQUESTED_MODE				=	0xEE0B0000,
			COMM_ERROR_OBJECT_STATE_CONFLICT					=	0xEE0C0000,
			COMM_ERROR_OBJ_ALREADY_EXISTS						=	0xEE0D0000,
			COMM_ERROR_ATTR_NOT_SETTABLE						=	0xEE0E0000,
			COMM_ERROR_PERMISSION_DENIED						=	0xEE0F0000,
			COMM_ERROR_DEV_IN_WRONG_STATE						=	0xEE100000,
			COMM_ERROR_REPLY_DATA_TOO_LARGE						=	0xEE110000,
			COMM_ERROR_FRAGMENT_PRIMITIVE						=	0xEE120000,
			COMM_ERROR_NOT_ENOUGH_DATA							=	0xEE130000,
			COMM_ERROR_ATTR_NOT_SUPPORTED						=	0xEE140000,
			COMM_ERROR_TOO_MUCH_DATA							=	0xEE150000,
			COMM_ERROR_OBJ_DOES_NOT_EXIST						=	0xEE160000,
			COMM_ERROR_NO_FRAGMENTATION							=	0xEE170000,
			COMM_ERROR_DATA_NOT_SAVED							=	0xEE180000,
			COMM_ERROR_DATA_WRITE_FAILURE						=	0xEE190000,
			COMM_ERROR_REQUEST_TOO_LARGE						=	0xEE1A0000,
			COMM_ERROR_RESPONSE_TOO_LARGE						=	0xEE1B0000,
			COMM_ERROR_MISSING_LIST_DATA						=	0xEE1C0000,
			COMM_ERROR_INVALID_LIST_STATUS						=	0xEE1D0000,
			COMM_ERROR_SERVICE_ERROR							=	0xEE1E0000,
			COMM_ERROR_VENDOR_SPECIFIC							=	0xEE1F0000,
			COMM_ERROR_INVALID_PARAMETER						=	0xEE200000,
			COMM_ERROR_WRITE_ONCE_FAILURE						=	0xEE210000,
			COMM_ERROR_INVALID_REPLY							=	0xEE220000,
			COMM_ERROR_BAD_KEY_IN_PATH							=	0xEE250000,
			COMM_ERROR_BAD_PATH_SIZE							=	0xEE260000,
			COMM_ERROR_UNEXPECTED_ATTR							=	0xEE270000,
			COMM_ERROR_INVALID_MEMBER							=	0xEE280000,
			COMM_ERROR_MEMBER_NOT_SETTABLE						=	0xEE290000,
			COMM_ERROR_UNKNOWN_MODBUS_ERROR						=	0xEE2B0000,
			COMM_ERROR_HW_NOT_INSTALLED1						=	0xEE2C0000,
			COMM_ERROR_ENCAP_PROTOCOL							=	0xEE6A0000,
			COMM_ERROR_STILL_PROCESSING							=	0xEEFF0000,
			MC_DOWNLOAD_FAIL_DUE_TO_ANOTHER_PROGRAM_IS_RUNNING	=	0xEE800000, //Ver_0c010012_1
        }
        public enum EcState : uint
        {
            eST_UNKNOWN   = 0,
            eST_INIT      = 0x01,	//Ver_0c010016_2
            eST_PREOP     = 0x02,	//Ver_0c010016_2
            eST_BOOTSTRAP = 0x03,   //v12.1.0.48
            eST_SAFEOP    = 0x04,	//Ver_0c010016_2
            eST_OP        = 0x08,	//Ver_0c010016_2
            eST_ACKERR    = 0x10	//Ver_0c010016_2
        }
        public enum EcMstMode : uint
        {
            eMM_IDLE = 0,
            eMM_SCAN,
            eMM_RUN,
            eMM_INTRANSITION,
            eMM_ERR,
            eMM_LINKBROKEN,
			eMM_FREE_RUN, //v12.1.0.54
        }
        public enum EcScanMode : uint
        {
            SCAN_ALL = 0,
            SCAN_SINGLE,
        }
        public enum EcScanSts : uint
        {
            SCAN_NONE = 0,
            SCAN_BUSY,
            SCAN_DONE
        }
        public enum IOBufMode : ushort
        {
            BUF_OUT = 0,
            BUF_IN = 1,
        }
        public enum MC_AXISSTATUS : uint
        {
            mcErrorStop = 0x00000001,
            mcDisabled = 0x00000002,
            mcStopping = 0x00000004,
            mcStandStill = 0x00000008,
            mcDiscreteMotion = 0x00000010,
            mcContinuousMotion = 0x00000020,
            mcSynchroMotion = 0x00000040,
            mcHoming = 0x00000080,
            mcReserved_as_8 = 0x00000100,
            mcReserved_as_9 = 0x00000200,
            mcConstantVelocity = 0x00000400,
            mcAccelerating = 0x00000800,
            mcDecelerating = 0x00001000,
            mcDirectionPositive = 0x00002000,
            mcDirectionNegative = 0x00004000,
            mcLimitSwitchNeg = 0x00008000,
            mcLimitSwitchPos = 0x00010000,
            mcHomeAbsSwitch = 0x00020000,
            mcLimitSwitchPosEvent = 0x00040000,
            mcLimitSwitchNegEvent = 0x00080000,
            mcDriveFault = 0x00100000,
            mcSensorStop = 0x00200000,
            mcReadyForPowerOn = 0x00400000,
            mcPowerOn = 0x00800000,
            mcIsHomed = 0x01000000,
            mcAxisWarning = 0x02000000,
            mcMotionComplete = 0x04000000,
            mcGearing = 0x08000000,
            mcGroupMotion = 0x10000000,
            mcBufferFull = 0x20000000,
            mcReserved_as_30 = 0x40000000,
            mcReserved_as_31 = 0x80000000,
        }

        //MC_ReadStatus
        public enum MC_Status : uint
        {
            mcASErrorStop = 0x00000001,
            mcASDisabled = 0x00000002,
            mcASStopping = 0x00000004,
            mcASStandStill = 0x00000008,
            mcASDiscreteMotion = 0x00000010,
            mcASContinuousMotion = 0x00000020,
            mcASSynchroMotion = 0x00000040,
            mcASHoming = 0x00000080,
        }

        //MC_ReadMotionState
        public enum MC_MOTIONSTATE : uint
        {
            mcMSConstantVelocity = 0x00000001,
            mcMSAccelerating = 0x00000002,
            mcMSDecelerating = 0x00000004,
            mcMSDirectionPositive = 0x00000008,
            mcMSDirectionNegative = 0x00000010,
        }
        //MC_ReadAxisInfo
        public enum MC_AXISINFO : uint
        {
            mcAIHomeAbsSwitch = 0x00000001,
            mcAILimitSwitchPos = 0x00000002,
            mcAILimitSwitchNeg = 0x00000004,
            mcAIReserved3 = 0x00000008,
            mcAIReserved4 = 0x00000010, 
            mcAIReadyForPowerOn = 0x00000020,
            mcAIPowerOn = 0x00000040,
            mcAIIsHomed = 0x00000080,
            mcAIAxisWarining = 0x00000100,
            mcAIMotionComplete = 0x00000200,
            mcAIGearing = 0x00000400,
            mcAIGroupMotion = 0x00000800,
            mcAIBufferFull = 0x00001000,
            mcAIReseved13 = 0x00002000,
        }
        public enum MC_AXISERROR : uint
        {
            mcAxis_NO_ERROR = 0x0000,
            mcAxis_DEVICE_ERROR = 0x0001,
            mcAxis_INVALID_AXIS_STATE = 0x0002,
            mcAxis_PARAMETER_INVALID = 0x0003,
            mcAxis_UNSUPPORT_CMD_REQUEST = 0x0004,
            mcAxis_CMD_REQUEST_FORMAT_WRONG = 0x0005,
            mcAxis_RESOURCE_ERROR = 0x0006,
            mcAxis_CONFIG_INVALID = 0x0007,
            mcAxis_POSITION_FOLLOWING_ERROR = 0x0008,
            mcAxis_VELOCITY_FOLLOWING_ERROR = 0x0009,
            mcAxis_SYSTEM_MAX_VELOCITY_OVER_ERROR = 0x000A,
            mcAxis_SYSTEM_MAX_ACCEL_OVER_ERROR = 0x000B,
            mcAxis_SYSTEM_MAX_DECEL_OVER_ERROR = 0x000C,
            mcAxis_SYSYEM_MAX_JERK_OVER_ERROR = 0x000D,
            mcAxis_MALFUNCTION_ERROR = 0x000E,
            mcAxis_GEARING_RULE_VIOLATION = 0x000F,
            mcAxis_HW_LIMIT_REACHED_WARNING = 0x8001,
            mcAxis_SW_LIMIT_REACHED_WARNING = 0x8002,
        }
        public struct MC_AxisErrorInfo
        {
            ushort ErrorId;
            ushort ErrorInfo0;
            ushort ErrorInfo1;
        }
        public struct MC_AxesGroupRawDataStatus
        {
            byte Mode;				    //Raw Data Mode
            byte Enabled;				//Enabled Flag
            uint EmptyBufferCount;	    //Empty Buffer Count
            ushort InBufferIndex;		//In Buffer Index
            ushort OutBufferIndex;		//Out Buffer Index
            uint CurrentRawDataID;	    //Current Out RawDataSet ID
            byte CoordSystem;			//Reserved
        }
        public enum MC_SOURCE : uint
        {
            mcSetValue = 0, //Synchronization on master set value
            mcActualValue,  //Synchronization on master actual value
			mcSetValueFixedGear = 0x10,
			mcActualValueFixedGear = 0x11,
        }
        public enum MC_EXECUTION_MODE : uint
        {
            mcImmediately = 0,
            mcQueued,
        }
        public enum MC_BUFFER_MODE : uint
        {
            //0:Aborting, 1:Buffered, 2:BlendingLow, 3:BlendingPrevious, 4:BlendingNext, 5:BlendingHigh
            mcAborting = 0,
            mcBuffered,
            mcBlendingLow,
            mcBlendingPrevious,
            mcBlendingNext,
            mcBlendingHigh,
            mcBufferedBlendingLow,
            mcBufferedBlendingPrevious,
            mcBufferedBlendingNext,
            mcBufferedBlendingHigh,
        }
        public enum MC_DIRECTION : uint
        {
            // 0:Positive Direction, 1:Shortest_way, 2:Negative_Direction, 3:Current_Direction
            mcPositiveDirection = 0,
            mcShortestWay,
            mcNegativeDirection,
            mcCurrentDirection,
        }
        public enum MC_GearStatus : ushort
        {
            mcGearActive = 0x0001,
            mcGearIn = 0x0002,
            mcGearReserved2 = 0x0004,
            mcGearReserved3 = 0x0008,
            mcGearReserved4 = 0x0010,
            mcGearReserved5 = 0x0020,
            mcGearReserved6 = 0x0040,
            mcGearReserved7 = 0x0080,
            mcGearReserved8 = 0x0100,
            mcGearReserved9 = 0x0200,
            mcGearReserved10 = 0x0400,
            mcGearReserved11 = 0x0800,
            mcGearReserved12 = 0x1000,
            mcGearReserved13 = 0x2000,
            mcGearReserved14 = 0x4000,
            mcGearReserved15 = 0x8000,
        }
        public enum AxisStopType : uint
        {
            IMMEDIATE = 0,
            DECEL
        }
		//Do not use!!! (Old Version)
        public enum MC_AXIS_DIRECTION : uint
        {
            CW = 0,
            CCW
        }
		//New Version
		public enum MC_HOME_AXIS_DIRECTION : uint
		{
			HOMING_DIR_CCW = 0,
			HOMING_DIR_CW
		}		
        public enum MC_AXIS_CONTROL : uint
        {
            OL_PULSE_DIRECTION = 0,
            OL_TWO_PULSE,
            OL_QUDARATURE_PULSE,
            CL_VELOCITY_LEVEL_ANALOG,
            CL_TORQUE_LEVEL_ANALOG,
        }
        public enum MC_AXIS_I_MODE : uint
        {
            IN_STANDING = 0,
            ALWAYS,
        }
        public enum BinFileType : uint
        {
            BIN_BOOT = 1,
            BIN_A8OS = 2,
            BIN_TM = 3,
            BIN_DSPOS = 5,
            BIN_ENI = 6
        }
        public enum SWVerType : ushort
        {
            SW_VER_MOTION = 0,
            SW_VER_PCICIP,
            SW_VER_DDSDK,
            SW_VER_DD,
        }
        public enum MC_CoordSystem : byte
        {
            mcACS = 1,
            mcMCS,
            mcPCS,
        }
        public enum MC_TRANSITION_MODE : byte
        {
            mcTMNone = 0,			//Insert no transition curve (default mode)
            mcTMStartVelocity,		//Transition with given start velocity
            mcTMConstantVelocity,	//Transition with given constant velocity
            mcTMCornerDistance,		//Transition with given corner distance
            mcTMMaxCornerDeviation, //Transition with given maximum corner deviation
            //5 - 9 Reserved by PLCopen
            //10 - Supplier specific modes
            mcTMProfileContinue = 11, //RSA Transiton Mode (Special) //v12.1.0.71
        }
        public enum MC_CIRC_MODE : byte
        {
            mcBORDER = 0,
            mcCENTER,
            mcRADIUS,
            mcCENTER_ANGLE = 10,
            mcBORDER_ANGLE = 11,
            mcSPIRAL   = 20, //v12.1.0.71
            mcCYLINDER = 21, //v12.1.0.71  
            mcSPHERE   = 22, //v12.1.0.71
        }
        public enum MC_CIRC_PATHCHOICE : byte
        {
            mcClockWise = 0,
            mcCounterClockWise,
        }
        public enum MC_RAW_DATA_MODE : byte
        {
            mcPositionCmdMode = 0,
            mcVelocityCmdMode,
            mcTorqueCmdMode,
        }
        public enum MC_SAVE_MODE : byte
        {
            mcSMAuto = 0,
            mcSMIndex,
        }
        public enum MC_GroupStatus : uint
        {
            GroupMoving = 0x00000001,
            GroupHoming = 0x00000002,
            GroupErrorStop = 0x00000004,
            GroupStandby = 0x00000008,
            GroupStopping = 0x00000010,
            GroupDisabled = 0x00000020,
            ConstantVelocity = 0x00000040,
            Accelerating = 0x00000080,
            Decelerating = 0x00000100,
            InPosition = 0x00000200,
        }	
	    //v12.2.0.1
        public enum MC_GantryStatus : uint
        {
            mcGantry_MotionCompleted = 0x00000001, //Bit 00
            mcGantry_Fault = 0x00000002, //Bit 01
            mcGantry_Warning = 0x00000004, //Bit 02
            mcGantry_FollowingError = 0x00000008, //Bit 03
            mcGantry_YawError = 0x00000010, //Bit 04
            mcGantry_YawStable = 0x00000020, //Bit 05
            mcGantry_AmpOff = 0x00000040, //Bit 06
            mcGantry_Moving = 0x00000080, //Bit 07
            mcGantry_Buffering = 0x00000100, //Bit 08
            mcGantry_IsHomed = 0x00000200, //Bit 09
            mcGantry_Homing = 0x00000400, //Bit 10
            mcGantry_PosHWLimitMaster = 0x00000800, //Bit 11
            mcGantry_NegHWLimitMaster = 0x00001000, //Bit 12
            mcGantry_PosSWLimitMaster = 0x00002000, //Bit 13
            mcGantry_NegSWLimitMaster = 0x00004000, //Bit 14
            mcGantry_PosHWLimitSlave = 0x00008000, //Bit 15
            mcGantry_NegHWLimitSlave = 0x00010000, //Bit 16
            mcGantry_PosSWLimitSlave = 0x00020000, //Bit 17
            mcGantry_NegSWLimitSlave = 0x00040000, //Bit 18
            mcGantry_HWLimitEventMaster = 0x00080000, //Bit 19
            mcGantry_HWLimitEventSlave = 0x00100000, //Bit 20
            mcGantry_InputShapingZV = 0x00200000, //Bit 21
            mcGantry_InputShapingZVD = 0x00400000, //Bit 22
            mcGantry_Reserved_23 = 0x00800000, //Bit 23
            mcGantry_Reserved_24 = 0x01000000, //Bit 24
            mcGantry_Reserved_25 = 0x02000000, //Bit 25
            mcGantry_Reserved_26 = 0x04000000, //Bit 26
            mcGantry_Reserved_27 = 0x08000000, //Bit 27
            mcGantry_Reserved_28 = 0x10000000, //Bit 28
            mcGantry_Reserved_29 = 0x20000000, //Bit 29
            mcGantry_Reserved_30 = 0x40000000, //Bit 30
            mcGantry_Reserved_31 = 0x80000000, //Bit 31
        }
		public enum MC_ParamID : uint
		{
			mcpCommandedPosition          		= 1,
			mcpSWLimitPos_  					= 2,
			mcpSWLimitNeg_  					= 3,
			mcpEnableLimitPos_  				= 4,
			mcpEnableLimitNeg_  				= 5,
			mcpEnablePosLagMonitoring  			= 6,
			mcpMaxPositionLag_  				= 7,
			mcpMaxVelocitySystem_  				= 8,
			mcpMaxVelocityAppl_  				= 9,
			mcpActualVelocity  					= 10,
			mcpCommandedVelocity  				= 11,
			mcpMaxAccelerationSystem_  			= 12,
			mcpMaxAccelerationAppl  			= 13,
			mcpMaxDecelerationSystem_  			= 14,
			mcpMaxDecelerationAppl  			= 15,
			mcpMaxJerkSystem  					= 16,
			mcpMaxJerkAppl_  					= 17,
			mcpActualPosition					= 1000,
			mcpCommandedAccel					= 1001,
			mcpActualAccel						= 1002,
			mcpCommandedJerk					= 1003,
			mcpActualJerk						= 1004,
			mcpTotalBufferCount					= 1010,
			mcpAvailableBufferCount				= 1011,
            mcpECCommandPosition                = 1501, //v12.2.0.0
            mcpECActualPosition                 = 1502, //v12.2.0.0
			mcpAxisType							= 2002,
			mcpModuloAxis						= 2003,
			mcpModuloValue						= 2004,
			mcpEnableHWLimitPos					= 2010,
			mcpHWLimitPosInputNum				= 2011,
			mcpHWLimitPosActLevel				= 2012,
			mcpEnableHWLimitNeg					= 2013,
			mcpHWLimitNegInputNum				= 2014,
			mcpHWLimitNegActLevel				= 2015,
			mcpHomeInputNum						= 2016,
			mcpHomeActLevel						= 2017,
			mcpMarkerInputNum					= 2018,
			mcpMarkerActLevel					= 2019,
			mcpInputActLevel					= 2020,
			mcpEnableLimitPos					= 2030,
			mcpSWLimitPos						= 2031,
			mcpEnableLimitNeg					= 2032,
			mcpSWLimitNeg						= 2033,
			mcpMaxVelocityAppl					= 2034,
			mcpMaxAccelAppl						= 2035,
			mcpMaxDecelAppl						= 2036,
			mcpMaxJerkAppl						= 2037,
			mcpMaxVelocitySystem				= 2038,
			mcpMaxAccelerationSystem			= 2039,
			mcpMaxDecelerationSystem			= 2040,
			mcpmcpMaxJerkSystem					= 2041,
			mcpmcpEStopType						= 2060,
			mcpEStopDecel						= 2061,
			mcpEStopJerk						= 2062,
			mcpInvertCmdDir						= 2070,
			mcpCmdScaleFactor					= 2071,
			mcpFeedbackMode						= 2072,
			mcpInvertFeedbackDir				= 2073,
			mcpFeedbackScaleFactor				= 2074,
			mcpPositionFeedbackFilter			= 2075,
			mcpVelocityFeedbackFilter			= 2076,
			mcpAccelerationFeedbackFilter	    = 2077,
			mcpStartVelocityOffset				= 2078,
			mcpStopVelocityOffset				= 2079,
			mcpInPositionCheckType				= 2080,
			mcpInPositionWindowSize				= 2081,
			mcpInVelocityWindowSize				= 2082,
			mcpEnablePositionLagMonit			= 2083,
			mcpMaxPositionLag					= 2084,
			mcpPositionLagCalMethod				= 2085,
			mcpEnableVelocityLagMonit			= 2086,
			mcpMaxVelocityLag					= 2087,
			mcpVelocityLagCalMethod				= 2088,
			mcpHomingType						= 2100,
			mcpHomingDir						= 2101,
			mcpHomingVelocity					= 2102,
			mcpHomingAcceleration				= 2103,
			mcpHomingDeceleration				= 2104,
			mcpHomingJerk						= 2105,
			mcpHomingCreepVelocity				= 2106,
			mcpHomePositionOffset				= 2107,
			mcpHomeCompleteFlagHandle			= 2108,
			//v12.1.0.33
			mcpSensor0StopEnable 				= 2109,
			mcpSensor0StopMode	 				= 2110,
			mcpSensor0StopIOOffset 				= 2111,
			mcpSensor0StopIOSize 				= 2112,
			mcpSensor0StopIOBit 				= 2113,
			mcpSensor0StopPosOffset 			= 2114,
			mcpSensor1StopEnable 				= 2115,
			mcpSensor1StopMode	 				= 2116,
			mcpSensor1StopIOOffset 				= 2117,
			mcpSensor1StopIOSize 				= 2118,
			mcpSensor1StopIOBit 				= 2119,
			mcpSensor1StopPosOffset 			= 2120,
			mcpSensor2StopEnable 				= 2121,
			mcpSensor2StopMode	 				= 2122,
			mcpSensor2StopIOOffset 				= 2123,
			mcpSensor2StopIOSize 				= 2124,
			mcpSensor2StopIOBit 				= 2125,
			mcpSensor2StopPosOffset 			= 2126,
			mcpSensor3StopEnable 				= 2127,
			mcpSensor3StopMode	 				= 2128,
			mcpSensor3StopIOOffset 				= 2129,
			mcpSensor3StopIOSize 				= 2130,
			mcpSensor3StopIOBit 				= 2131,
			mcpSensor3StopPosOffset 			= 2132,
			mcpSensor4StopEnable 				= 2133,
			mcpSensor4StopMode	 				= 2134,
			mcpSensor4StopIOOffset 				= 2135,
			mcpSensor4StopIOSize 				= 2136,
			mcpSensor4StopIOBit 				= 2137,
			mcpSensor4StopPosOffset 			= 2138,
            //v12.1.0.40
            mcpPositiveLimitErrorStop           = 2139,
            mcpNegativeLimitErrorStop           = 2140,
			//
			mcpSensor0MovingVelocity			= 2141,
			mcpSensor1MovingVelocity			= 2142,
			mcpSensor2MovingVelocity			= 2143,
			mcpSensor3MovingVelocity			= 2144,
			mcpSensor4MovingVelocity			= 2145,
            //v12.1.0.67
            mcpPositionOffset                   = 2146,
            mcpPositionOffsetValue              = 2147,
            mcpMaintainGearStateEnable          = 2148,
            mcpClampingVelocity                 = 2149,
            //v12.2.0.2
            mcpMaxTorqueMode                    = 2150,
		}	
    }
}

namespace NMCMotionSDK
{
    //MMC Library APIs
    public partial class NMCSDKLib
    {
        //===============================================================================
        //-------------------------------------------------------------------------------
        //  
        //
        // System APIs - Initialization, General & Utiltiy Functions
        //
        //
        //-------------------------------------------------------------------------------
        #region Public Function
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Init();
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MasterInit(ushort MasterID);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MasterRUN(ushort MasterID);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MasterSTOP(ushort MasterID);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetSWVersion(ushort Type, ref ushort Major, ref ushort Minor);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetErrorMessage(uint ErrorCode, uint Size, StringBuilder ErrorMessage);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetMasterMap(ushort[] MasterMap /*MAX_BOARD_CNT*/, ref ushort MasterCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetMasterCount(ref ushort MasterCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetBoardScanNo(ushort BoardID, ref ushort BoardScanNo);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SetSystemPerformance(ushort Level);
		
        //v12.1.0.55
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetNodeFailureID(
                                    ushort BoardID,          //_in Board ID  
                                    ref ushort NodeFailureID //_out Node FailureID
                                    );

        //v12.1.0.58
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_RestoreSWEmergency(
                                    ushort BoardID,        //_in BoardID
                                    ref byte RestoreStatus //_out Restore Status
                                    );
		
        //v12.1.0.67
       [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SetIOSelfHoldFunc(
	  								ushort BoardID,	    //_in BoardID
									ushort WaitTime     //_in Wait Time Before Slave SafeOP Check (Range: 0 ~ 100000 ms)
									);

        //v12.1.0.68
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_EniFileDownload(
                                    ushort BoardID,             //_in BoardID
                                    StringBuilder sNvsFileName, //_in eMMC.nvs Full Path File Name ex) c:\\test\\eMMC.nvs
                                    StringBuilder sEniFileName  //_in ENI.xml Full Path File Name ex) c:\\test\\ENI.xml
                                    ); 

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Master APIs
        //
        //
        //-------------------------------------------------------------------------------
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetCurMode(ushort BoardID, ref byte MasterMode);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetBoardID(ushort MasterScanNo, ref ushort MasterID);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetOSRevision(ushort BoardID, ref byte Major, ref byte Minor);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetMCRevision(ushort BoardID, ref byte Major, ref byte Minor);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetSDOData(
                                    ushort BoardID,
                                    ushort SlaveNo,
                                    ushort SDOIndex,
                                    byte SubIndex,
                                    uint DataSize,
                                    ref uint respDataSize,
                                    byte[] bDataArray
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetSDODataEcatAddr(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort SDOIndex,
                                    byte SubIndex,
                                    uint DataSize,
                                    ref uint respDataSize,
                                    byte[] bDataArray
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterSetSDOData(
                                    ushort BoardID,
                                    ushort SlaveNo,
                                    ushort SDOIndex,
                                    byte SubIndex,
                                    uint DataSize,
                                    ref uint respDataSize,
                                    byte[] bDataArray
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterSetSDODataEcatAddr(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort SDOIndex,
                                    byte SubIndex,
                                    uint DataSize,
                                    ref uint respDataSize,
                                    byte[] bDataArray
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetLastError(
                                    ushort BoardID,
                                    ref uint pSequenceNo,
                                    ref uint pErrorCode,
                                    byte[] ExtErrorInfo
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterClearError(
                                    ushort BoardID
                                    );

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Master APIs - Device Info.
        //
        //
        //-------------------------------------------------------------------------------
        //Master Get Axes Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetAxesCount(ushort BoardID, ref ushort TotalAxisCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetAxesID(ushort BoardID, ushort[] AxisIDArray);

        //Master Get Device Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetDeviceCount(ushort BoardID, ref ushort TotalDeviceCount);        
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGetDeviceID(ushort BoardID, ushort[] DeviceIDArray);

        //Master Get Digital Input Info. (Module Info. Update v12.1.0.64)
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DI_DeviceCount(ushort BoardID, ref ushort DeviceCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DI_DeviceID(ushort BoardID, ushort[] DeviceIDArray);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DI_DeviceTotalChSize(ushort BoardID, ushort DeviceID, ref ushort DeviceTotalChSize);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DI_DeviceName(ushort BoardID, ushort DeviceID, byte[] DeviceNameArray); //Device Name Array [256]

        //Master Get Digital Output Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DO_DeviceCount(ushort BoardID, ref ushort DeviceCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DO_DeviceID(ushort BoardID, ushort[] DeviceIDArray);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DO_DeviceTotalChSize(ushort BoardID, ushort DeviceID, ref ushort DeviceTotalChSize);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_DO_DeviceName(ushort BoardID, ushort DeviceID, byte[] DeviceNameArray); //Device Name Array [256]

        //Master Get Analog Input Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AI_DeviceCount(ushort BoardID, ref ushort DeviceCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AI_DeviceID(ushort BoardID, ushort[] DeviceIDArray);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AI_DeviceTotalChSize(ushort BoardID, ushort DeviceID, ref ushort DeviceTotalChSize);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AI_DeviceName(ushort BoardID, ushort DeviceID, byte[] DeviceNameArray); //Device Name Array [256]

        //Master Get Analog Output Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AO_DeviceCount(ushort BoardID, ref ushort DeviceCount);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AO_DeviceID(ushort BoardID, ushort[] DeviceIDArray);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AO_DeviceTotalChSize(ushort BoardID, ushort DeviceID, ref ushort DeviceTotalChSize);
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterGet_AO_DeviceName(ushort BoardID, ushort DeviceID, byte[] DeviceNameArray); //Device Name Array [256]

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // Master APIs - Device I/O Info.
        // v12.2.0.10
        //
        //-----------------------------------------------------------------------------
        //Digital Input - I/O Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DI_IO_Count(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									ref ushort Count	      //_out Count
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DI_IO_Name(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									byte[,] NameArray         //_out Name Array [,256]
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DI_IO_Data(
									ushort BoardID,			  //_in BoardID
									ushort DeviceID,		  //_in DeviceID (EcatAddr)
									ushort[] BitSizeArray,	  //_out Bit Size Array
									ushort[] RawIOOffsetArray //_out Raw IOOffset Array
									);

        //Digital Output - I/O Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DO_IO_Count(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									ref ushort Count	      //_out Count
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DO_IO_Name(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									byte[,] NameArray         //_out Name Array [,256]
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_DO_IO_Data(
									ushort BoardID,			  //_in BoardID
									ushort DeviceID,		  //_in DeviceID (EcatAddr)
                                    ushort[] BitSizeArray,	  //_out Bit Size Array								
                                    ushort[] RawIOOffsetArray //_out Raw IOOffset Array
									);

        //Analog Input - I/O Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AI_IO_Count(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									ref ushort Count	      //_out Count
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AI_IO_Name(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									byte[,] NameArray         //_out Name Array [,256]
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AI_IO_Data(
									ushort BoardID,			  //_in BoardID
									ushort DeviceID,		  //_in DeviceID (EcatAddr)
                                    ushort[] BitSizeArray,	  //_out Bit Size Array
                                    ushort[] RawIOOffsetArray //_out Raw IOOffset Array
									);

        //Analog Output - I/O Info.
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AO_IO_Count(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
									ref ushort Count	      //_out Count
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AO_IO_Name(
									ushort BoardID,		      //_in BoardID
									ushort DeviceID,	      //_in DeviceID (EcatAddr)
                                    byte[,] NameArray         //_out Name Array [,256]
									);

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS	MasterGet_AO_IO_Data(
									ushort BoardID,			  //_in BoardID
									ushort DeviceID,		  //_in DeviceID (EcatAddr)
                                    ushort[] BitSizeArray,	  //_out Bit Size Array
                                    ushort[] RawIOOffsetArray //_out Raw IOOffset Array
									);

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Slave APIs
        //
        //
        //-------------------------------------------------------------------------------
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS SlaveGetAliasNo(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ref ushort AliasID
                                    );
        //Ver_0c010012_8
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS SlaveSetAliasNo(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort AliasID
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS SlaveGetCurState(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ref byte data
                                    );

        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MasterECatDirectAccess(
                                    ushort BoardID,
                                    byte EcatCmd,
                                    ushort Adp,
                                    ushort Ado,
                                    ushort ReqDataSize,
                                    byte[] bReqDataArray,
                                    ref ushort RespDataSize,
                                    byte[] bRespDataArray,
                                    ref ushort WC
                                    );

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Slave APIs - Slave Home
        //
        //
        //-------------------------------------------------------------------------------
        //5.1.1.68 MC_SetHomeFlag [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SetHomeFlag(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    ushort EcatAddr         //Ethercat Address
                                    );

        //5.1.1.69 MC_GetHomeFlag [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetHomeFlag(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    ref uint pHomeFlag      //Home Flag
                                    );

        //5.1.1.70 MC_ModeChange [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ModeChange(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    byte Mode,  	        //Mode(6:hm,8:csp)
                                    byte ReservedZero       //MC_BUFFER_MODE BufferMode (Reserved)
                                    );

        //5.1.1.71 MC_SlaveHomeSet  [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SlaveHomeSet(
                                    ushort BoardID,         //BoardID
                                    ushort EcatAddr,        //Ethercat Address
                                    int Offset,		        //Home Offset 
                                    byte Method,            //Method
                                    uint SpeedSwitch,       //Speed during search for switch
                                    uint SpeedZero,         //Speed during search for zero
                                    uint Acceleration,      //Acceleration
                                    byte ReservedZero       //MC_BUFFER_MODE BufferMode (Reserved)
                                    );

        //5.1.1.72 MC_SlaveHome [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SlaveHome(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    byte Start,             //0:Stop,1:Start
                                    byte ReservedZero       //MC_BUFFER_MODE BufferMode (Reserved)
                                    );

        //5.1.1.73 MC_SlaveHomeHalt [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SlaveHomeHalt(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    byte ReservedZero       //MC_BUFFER_MODE BufferMode (Reserved)
                                    );

        //5.1.1.74 MC_ReadSlaveHomeStatus [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadSlaveHomeStatus(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    ref uint HomeStatus     //Response Data(Bit0:Homing Complete, Bit1:Homing Error)
                                    );

        //5.1.1.75 MC_ReadSlaveModeStatus [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadSlaveModeStatus(
                                    ushort BoardID,         //BoardID
                                    ushort AxisID,          //Axis number
                                    ref byte ModeStatus     //Response Data(6:hm,8:csp)
                                    );

        //===============================================================================
        //---------------------------------------------------------------------------
        //
        //
        // I/O APIs
        //
        //
        //---------------------------------------------------------------------------
        //MC_GetIOState
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GetIOState(
                                    ushort BoardID,
                                    uint type,
                                    ref uint State
                                    );

        //MC_IO_WRITE 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_RAW_WRITE(
                                    ushort BoardID,
                                    uint Offset,
                                    uint Size,
                                    byte[] DataArray
                                    );
        //MC_IO_WRITE 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_WRITE(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    uint Offset,
                                    uint Size,
                                    byte[] DataArray
                                    );
        //MC_IO_WRITE_BIT
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_WRITE_BIT(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    uint Offset,
                                    byte bitOffset,
                                    bool data
                                    );
        //MC_IO_WRITE_BIT
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_RAW_WRITE_BIT(
                                    ushort BoardID,
                                    uint Offset,
                                    byte bitOffset,
                                    bool data
                                    );
        //MC_IO_WRITE_BYTE
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_WRITE_BYTE(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    uint Offset,
                                    byte data
                                    );
        // MC_IO_WRITE_WORD
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_WRITE_WORD(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    uint Offset,
                                    ushort data
                                    );
        //MC_IO_WRITE_DWORD
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_WRITE_DWORD(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    uint Offset,
                                    ushort data
                                    );

        //MC_IO_READ
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_READ(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort BufferInOut,
                                    uint Offset,
                                    uint Size,
                                    byte[] DataArray
                                    );
        //MC_IO_READ
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_RAW_READ(
                                    ushort BoardID,
                                    ushort BufferInOut,
                                    uint Offset,
                                    uint Size,
                                    byte[] DataArray
                                    );
        //MC_IO_READ_BIT
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_READ_BIT(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort BufferInOut,
                                    uint Offset,
                                    byte BitOffset,
                                    ref bool data
                                    );
        //MC_IO_READ_BIT
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_RAW_READ_BIT(
                                    ushort BoardID,
                                    ushort BufferInOut,
                                    uint Offset,
                                    byte BitOffset,
                                    ref bool data
                                    );
        //MC_IO_READ_BYTE
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_READ_BYTE(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort BufferInOut,
                                    uint Offset,
                                    ref byte data
                                    );
        //MC_IO_READ_WORD
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_READ_WORD(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort BufferInOut,
                                    uint Offset,
                                    ref ushort data
                                    );
        //MC_IO_READ_DWORD
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_READ_DWORD(
                                    ushort BoardID,
                                    ushort EcatAddr,
                                    ushort BufferInOut,
                                    uint Offset,
                                    ref uint data   //v12.2.0.0 (ushort -> uint)
                                    );

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // I/O APIs - Buffering I/O Control
        //
        //
        //-----------------------------------------------------------------------------
        //v12.1.0.66
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_IO_MaskDigitalOutputBit(
									ushort BoardID,			//_in BoardID
									ushort AxisID,			//_in AxisID
									byte   IDIndexType,		//_in ID Index Type (AxisID:1, GroupID:2, GantryID:3)
									ushort EcatAddr,		//_in EcatAddr
									ushort SlaveOffset,		//_in Slave Offset				
									byte   OutputBitNumber,	//_in Mask Output Bit Number (Max Range: 0~31)
									bool   OutputValue		//_in Mask Output Bit Value (등록시 Output 초기 출력값 설정) (Set:1, Clear:0)
									);
         //v12.1.0.66 
         [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
         public static extern MC_STATUS MC_IO_UnMaskDigitalOutputBit(
                                    ushort BoardID,			//_in BoardID
                                    ushort EcatAddr,		//_in EcatAddr
                                    ushort SlaveOffset,		//_in Slave Offset
                                    byte OutputBitNumber	//_in UnMask Output Bit Number (Max Range: 0~31)
                                    );

         //v12.1.0.66 
         [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
         public static extern MC_STATUS MC_IO_GetMaskedDigitalOutputBit(
                                     ushort BoardID,		//_in BoardID
                                     ushort EcatAddr,		//_in EcatAddr
                                     ushort SlaveOffset,	//_in Slave Offset								
                                     byte OutputBitNumber,	//_in Output Bit Number (Max Range: 0~31)
                                     ref ushort AxisID,		//_out Masked AxisID
                                     ref byte IDIndexType	//_out Masked ID Index Type (AxisID:1, GroupID:2, GantryID:3)
                                     );

         //v12.1.0.66 
         [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
         public static extern MC_STATUS  MC_IO_GetMaskedDigitalOutput(
									ushort   BoardID,			//_in BoardID
									ref byte MaskCount,		    //_out Masked Count	
									ushort[] EcatAddr,			//_out Masked EcatAddr[9]
									ushort[] SlaveOffset,		//_out Masked Slave Offset[9]
									byte[]   OutputBitNumber,   //_out Masked Output Bit Number[9]
									ushort[] AxisID,		    //_out Masked AxisID[9]
									byte[]   IDIndexType		//_out Masked ID Index Type[9] (AxisID:1, GroupID:2, GantryID:3)
									);
         //v12.1.0.66  
         [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
         public static extern MC_STATUS  MC_IO_WriteDigitalOutputBit(
									ushort BoardID,			//_in BoardID
									ushort EcatAddr,		//_in EcatAddr
									ushort SlaveOffset,		//_in Slave Offset
									byte   OutputBitNumber,	//_in Output Bit Number (Max Range: Bit 0~31)
									bool   Value			//_in Output Bit Value (Set:1, Clear:0)
									);

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Motion APIs - Single Axis Motion
        //
        //
        //-------------------------------------------------------------------------------
        //5.1.1.1
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Power(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    bool Enable         //Axis enable : 0:Disable, 1:Enable
                                    );

        //5.1.1.2
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveAbsolute(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    double Position,    //Target Position
                                    double Velocity,    //Max Velocity
                                    double Accel,       //Max Acceleration
                                    double Decel,       //Max Deceleration
                                    double Jerk,        //Max Jerk
                                    MC_DIRECTION Dir,   //0:Positive Direction, 1:Shortest_way, 2:Negative_Direction, 3:Current_Direction
                                    MC_BUFFER_MODE BufferMode //Buffer Mode : 0:Aborting, 1:Buffered, 2:BlendingLow, 3:BlendingPrevious, 4:BlendingNext, 5:BlendingHigh
                                    );

        //5.1.1.3
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveRelative(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    double Distance,    //Distance
                                    double Velocity,    //Max Velocity
                                    double Accel,       //Max Acceleration
                                    double Decel,       //Max Deceleration
                                    double Jerk,        //Max Jerk
                                    MC_BUFFER_MODE BufferMode //Buffer Mode : 0:Aborting, 1:Buffered, 2:BlendingLow, 3:BlendingPrevious, 4:BlendingNext, 5:BlendingHigh
                                    );

        //5.1.1.6
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveVelocity(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    double Velocity,    //Max Velocity
                                    double Accel,       //Max Acceleration
                                    double Decel,       //Max Deceleration
                                    double Jerk,        //Max Jerk
                                    MC_DIRECTION Dir,   //0:Positive Direction, 1:Shortest_way, 2:Negative_Direction, 3:Current_Direction
                                    MC_BUFFER_MODE BufferMode //Buffer Mode : 0:Aborting, 1:Buffered, 2:BlendingLow, 3:BlendingPrevious, 4:BlendingNext, 5:BlendingHigh
                                    );

        //5.1.1.7
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Home(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    double Position,    //Position 
                                    MC_BUFFER_MODE BufferMode
                                    );

        //5.1.1.8
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Stop(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    bool Execute,       //Excute
                                    double Decel,       //Max Deceleration
                                    double Jerk         //Max Jerk
                                    );

        //5.1.1.9 MCReadStatus
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadStatus(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    ref uint pStatus
                                    );

        //5.1.1.10
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadAxisError(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    ref ushort pErrorID,
                                    ref ushort pErrorInfo,
                                    ref ushort pErrorInfoExt
                                    );

        //5.1.1.11
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Reset(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID       //Axis number
                                    );

        //5.1.1.12 MCReadParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    ref double pValue
                                    );

        //5.1.1.12 MCReadBoolParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadBoolParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    ref bool pValue
                                    );

        //5.1.1.12 MCReadIntParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadIntParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    ref uint pValue
                                    );

        //5.1.1.13 MCWriteParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    double dValue
                                    );

        //5.1.1.13 MCWriteBoolParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteBoolParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    bool Value
                                    );

        //5.1.1.13 MCWriteIntParameter
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteIntParameter(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number,
                                    uint ParameterNum,  //PN
                                    uint dwValue
                                    );

        //5.1.1.14 MCReadActualPosition
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadActualPosition(
                                    ushort BoardID,      //BoardID
                                    ushort AxisID,       //Axis number
                                    ref double pPosition //Position Acutal Value
                                    );

        //5.1.1.21 MC_GearIn
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GearIn(
                                    ushort BoardID,
                                    ushort MasterAxisID,
                                    ushort SlaveAxisID,
                                    uint RatioNumerator,
                                    uint RatioDenominator,
                                    MC_SOURCE MasterValueSource,
                                    double Acceleration,
                                    double Deceleration,
                                    double Jerk,
                                    MC_BUFFER_MODE BufferMode
                                    );

        //5.1.1.22 MC_GearOut
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GearOut(
                                    ushort BoardID,
                                    ushort SlaveAxisID
                                    );

        //5.1.1.24
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_TouchProbe(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint TriggerInput,
                                    bool WindowOnly,
                                    double FirstPosition,
                                    double LastPosition
                                    );

        //5.1.1.25
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_AbortTrigger(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint TriggerInput
                                    );

        //5.1.1.26
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadDigitalInput(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint InputNumber,
                                    ref bool pValue
                                    );

        //5.1.1.27
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadDigitalOutput(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint OutputNumber,
                                    ref bool pValue
                                    );

        //5.1.1.28
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteDigitalOutput(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint OutputNumber,
                                    bool Value
                                    );

        //5.1.1.29 MCSetPosition
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_SetPosition(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    double Position,
                                    bool Relative,
                                    MC_EXECUTION_MODE Mode
                                    );

        //5.1.1.31 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadActualVelocity(
                                    ushort BoardID,      //BoardID
                                    ushort AxisID,       //Axis number
                                    ref double pVelocity //Velocity Acutal Value
                                    );

        //5.1.1.37
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_Halt(
                                    ushort BoardID,
                                    ushort AxisID,
                                    double Deceleration,
                                    double Jerk,
                                    MC_BUFFER_MODE BufferMode
                                    );

        //5.1.1.38
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_TriggerMonitor(
                                    ushort BoardID,
                                    ushort AxisID,
                                    uint TriggerInput,
                                    ref bool pDone,
                                    ref double pRecordedPosition,
                                    ref bool pProbeActive
                                    );

        //5.1.1.39 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadMotionState(
                                    ushort BoardID,       //BoardID
                                    ushort AxisID,        //Axis number
                                    ref uint pMotionState //MC_MOTIONSTATE
                                    );
        //5.1.1.40 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadAxisInfo(
                                    ushort BoardID,     //BoardID
                                    ushort AxisID,      //Axis number
                                    ref uint pAxisInfo  //MC_AXISINFO
                                    );

        //5.1.1.41 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadAxisStatus(
                                    ushort BoardID,      //BoardID
                                    ushort AxisID,       //Axis number
                                    ref uint pAxisStatus //MC_AXISSTATUS
                                    );

        //5.1.1.42 MC_GearMonitor
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GearMonitor(
                                    ushort BoardID,
                                    ushort AxisID,
                                    ref ushort pStatus
                                    );

        //5.1.1.43 MC_ReadProfileData
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadProfileData(
                                    ushort BoardID,
                                    ushort AxisID,
                                    ref uint TickCount,
                                    ref double Position,
                                    ref double Velocity,
                                    ref double Accel,
                                    ref double Jerk,
                                    ref double ActPos,
                                    ref double ActVel
                                    );

        //5.1.1.77 MC_ReadCommandedPosition [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadCommandedPosition(
                                    ushort BoardID,		 //BoardID
                                    ushort AxisID,		 //Axis number
                                    ref double pPosition //Commmanded Position Value
                                    );

        //5.1.1.78 MC_ReadCommandedVelocity [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadCommandedVelocity(
                                    ushort BoardID,		 //BoardID
                                    ushort AxisID,		 //Axis number
                                    ref double pVelocity //Commmanded Velocity Value
                                    );

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Motion APIs - Coordinated Motion
        //
        //
        //-------------------------------------------------------------------------------
        //5.1.1.44 MC_AddAxisToGroup [32]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_AddAxisToGroup(
                                    ushort BoardID,
                                    ushort AxisID,
                                    ushort AxesGroupNo,
                                    ushort IDInGroup
                                    );

        //5.1.1.45 MC_RemoveAxisFromGroup [33]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_RemoveAxisFromGroup(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ushort IDInGroup
                                    );

        //5.1.1.46 MC_UngroupAllAxes [34]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_UngroupAllAxes(
                                    ushort BoardID,
                                    ushort AxesGroupNo
                                    );

        //5.1.1.47 MC_GroupReadConfiguration [35]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadConfiguration(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ushort IDInGroup,
                                    MC_CoordSystem CoordSystem,
                                    ref ushort AxisNo
                                    );

        //5.1.1.48 MC_GroupEnable [36]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupEnable(
                                    ushort BoardID,
                                    ushort AxesGroupNo
                                    );

        //5.1.1.49 MC_GroupDisable [37]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupDisable(
                                    ushort BoardID,
                                    ushort AxesGroupNo
                                    );

        //5.1.1.50 MC_MoveLinearAbsolute [43]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveLinearAbsolute(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ushort PositionCount,
                                    double[] PositionData,
                                    double Velocity,
                                    double Acceleration,
                                    double Deceleration,
                                    double Jerk,
                                    MC_CoordSystem CoordSystem,        //Coordination System 을 지정한다. //ACS, MCS, PCS
                                    MC_BUFFER_MODE BufferMode,         //Buffer Mode 를 지정한다.
                                    MC_TRANSITION_MODE TransitionMode, //Transition Mode 를 지정한다. 
                                    ushort TransitionParameterCount,   //TransitionParameter 의 Count 를 명시한다.
                                    double[] TransitionParameter       //TransitionParameterCount 에 명시된 크기 만큼의 Data 를 입력한다.
                                    );

        //5.1.1.51 MC_GroupHalt [42]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupHalt(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    double Deceleration,
                                    double Jerk,
                                    MC_BUFFER_MODE BufferMode //Buffer Mode 를 지정한다.
                                    );

        //5.1.1.52 MC_GroupStop [41]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupStop(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    bool Execute,
                                    double Deceleration,
                                    double Jerk
                                    );

        //5.1.1.53 MC_MoveCircularAbsolute2D [44]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveCircularAbsolute2D(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    MC_CIRC_MODE CircMode,
                                    MC_CIRC_PATHCHOICE PathChoice,
                                    double[] AuxPoint,
                                    double[] EndPoint,
                                    double Angle,
                                    double Velocity,
                                    double Acceleration,
                                    double Deceleration,
                                    double Jerk,
                                    MC_CoordSystem CordSystem,
                                    MC_BUFFER_MODE BufferMode,
                                    MC_TRANSITION_MODE TransitionMode,
                                    ushort TransitionParamCount,
                                    double[] TransitionParameter
                                    );

        //v12.1.0.71 MC_MoveCircularAbsolute3D
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MoveCircularAbsolute3D(
                                    ushort BoardID,                     //_in BoardID
                                    ushort AxesGroupNo,                 //_in Axes Group No
                                    MC_CIRC_MODE CircMode,              //_in Circ Mode (mcSPIRAL = 20, mcCYLINDER = 21, mcSPHERE = 22)                      
                                    double[] OriginPoint,               //_in Origin Point[3]
                                    double Angle,                       //_in Angle
                                    double[] Displacement,              //_in Displacement[4]
                                    double Velocity,                    //_in Velocity
                                    double Acceleration,                //_in Acceleration
                                    double Deceleration,                //_in Deceleration
                                    double Jerk,                        //_in Jerk
                                    MC_CoordSystem CordSystem,          //_in Cord System (Reserved)
                                    MC_BUFFER_MODE BufferMode,          //_in BufferMode
                                    MC_TRANSITION_MODE TransitionMode,  //_in TransitionMode (mcTMProfileContinue = 11, :: RSA Transition Mode)
                                    ushort TransitionParamCount,        //_in TransitionParamCount
                                    double[] TransitionParameter        //_in TransitionParameter
                                    );

        //v12.1.0.71 MC_ReadGroupStatus
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadGroupStatus(
                                    ushort BoardID,         //_in BoardID
                                    ushort AxesGroupNo,     //_in Axes Group No
                                    ref uint pGroupStatus   //_out pGroupStatus (MMCE Board)
                                    );

        //5.1.1.54 MC_GroupReadStatus [38]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadStatus(
                                    ushort BoardID,         //_in BoardID
                                    ushort AxesGroupNo,     //_in Axes Group No
                                    ref uint pGroupStatus   //_out pGroupStatus (Shared Memory)
                                    );

        //5.1.1.55 MC_GroupReadError [39]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadError(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ref ushort pErrorID,
                                    ref ushort pErrorInfo0,
                                    ref ushort pErrorInfo1
                                    );

        //5.1.1.56 MC_GroupReset [40]
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReset(
                                    ushort BoardID,
                                    ushort AxesGroupNo
                                    );

        //5.1.1.57 MC_GroupReadProfileData []
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadProfileData (
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ref ushort AxisCount,
                                    ref uint TimeTick,
                                    double[,] ProfileDataArray
                                    );

        //5.1.1.58 MC_GroupReadInfo []
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadInfo (
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ref ushort LastIdentNum,
                                    ushort[] IdentAxisNumList
                                    );

        //5.1.1.59 MC_ReadAllStatus []
        //[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        //public static extern MC_STATUS MC_ReadAllStatus (
        //                          ushort BoardID,
        //                          uint StatusData[MAX_ALL_STATUS_SIZE]
        //                          );

        //5.1.1.60 MC_GroupSetRawDataMode []
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupSetRawDataMode(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    MC_RAW_DATA_MODE Mode,
                                    bool Enable,				//Raw data mode enable : 0:Disable, 1:Enable
                                    byte ReservedZero1,			//MC_COORDSYSTEM CoordSystem,
                                    byte ReservedZero2			//MC_BUFFER_MODE BufferMode
                                    );

        ////5.1.1.61 MC_GroupReadRawDataStatus [] : 
        //[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        //public static extern MC_STATUS MC_GroupReadRawDataStatus(
        //                          ushort BoardID,
        //                          ushort AxesGroupNo,
        //                          MC_AxesGroupRawDataStatus *AxesGroupRawDataStatus
        //                          );

        //5.1.1.62 MC_GroupClearRawData [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupClearRawData(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ushort ReservedZero1,
                                    ushort ReservedZero2
                                    );

        //5.1.1.63 MC_GroupSetRawData [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupSetRawData(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    MC_SAVE_MODE SaveMode,
                                    ushort IndexNum,
                                    ushort RawDataSize,
                                    ushort RawDataCount,
                                    byte[] RawDataArray,
                                    ref ushort StoredNum
                                    );

        //5.1.1.64 MC_ReadRemainBuffer [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadRemainBuffer(
                                    ushort BoardID,
                                    ushort AxesNo,
                                    ref uint BufferSize
                                    );

        //5.1.1.65 MC_GroupReadRemainBuffer [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadRemainBuffer(
                                    ushort BoardID,
                                    ushort AxesGroupNo,
                                    ref uint BufferSize
                                    );

        //5.1.1.79 MC_GroupWriteParameter [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupWriteParameter(
                                    ushort BoardID,		 //_in BoardID
                                    ushort GroupNum,     //_in GroupNum
                                    uint ParameterNum,   //_in ParameterNum
                                    double Value         //_in Value
                                    );

        //5.1.1.80 MC_GroupReadParameter [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GroupReadParameter(
                                    ushort BoardID,		 //_in BoardID
                                    ushort GroupNum,     //_in GroupNum
                                    uint ParameterNum,   //_in ParameterNum
                                    ref double pValue    //_out Value
                                    );

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
		// Motion APIs - Multi Motion
        //
        //
        //-------------------------------------------------------------------------------
		//5.1.1.66 MC_MoveAbsoluteMultiAxis [] : //v12.1.0.40
		[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_MoveAbsoluteMultiAxis( 
				                    ushort BoardID,					//Board ID
				                    ushort AxisCount,				//Number of Axis
				                    ushort[] AxisArray,			    //Array of Axis ID
				                    double[] PositionArray,		    //Target Position Array
				                    double Velocity,				//Max Velocity
				                    double Acceleration,			//Max Acceleration
				                    double Deceleration,			//Max Deceleration
				                    double Jerk,					//Max Jerk
				                    MC_DIRECTION[] DirectionArray,  //Array of Moving Direction for each Axis(0,1,2,3) 
				                    byte ErrorStopMode				//Mode of multi-axes Motion Stop when Error occurred in one(or more) of multi-axes.
		                            );								//0:Remainder axes do not Stop (Continue their Motion), 1:Remainder axes also Stop with E-Stop Parameters.		

		//5.1.1.67 MC_MoveRelativeMultiAxis [] : //v12.1.0.40
		[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_MoveRelativeMultiAxis(
				                    ushort BoardID,					//Board ID
				                    ushort AxisCount,				//Number of Axis
				                    ushort[] AxisArray, 			//Array of Axis ID
				                    double[] PositionArray, 		//Target Position Array
				                    double Velocity, 				//Max Velocity
				                    double Acceleration, 			//Max Acceleration
				                    double Deceleration, 			//Max Deceleration
				                    double Jerk,					//Max Jerk
				                    byte ErrorStopMode				//Mode of multi-axes Motion Stop when Error occurred in one(or more) of multi-axes.
	        	                    );								//0:Remainder axes do not Stop (Continue their Motion), 1:Remainder axes also Stop with E-Stop Parameters.

		//5.1.1.67 MC_HaltMultiAxis [] : //v12.1.0.42 
		[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_HaltMultiAxis(
				                    ushort BoardID,					//Board ID
				                    ushort AxisCount,				//Number of Axis
				                    ushort[] AxisArray, 			//Array of Axis ID
				                    byte ErrorStopMode				//Mode of multi-axes Motion Stop when Error occurred in one(or more) of multi-axes.
		                            );								//0:Remainder axes do not Stop (Continue their Motion), 1:Remainder axes also Stop with E-Stop Parameters.
        					
		//5.1.1.76 MC_ReadMultiAxisStatus [] : 
		[DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_ReadMultiAxisStatus( 
				                    ushort BoardID,		  //Board ID
				                    ushort AxisCount,	  //Number of Axis
				                    ushort[] AxisArray,   //Array of Axis ID
				                    ref uint  Status	  //Axis Status
		                            );

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // Motion APIs - Gantry Motion
        //
        //
        //-----------------------------------------------------------------------------
        //Gantry
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_GantryEnable(
									ushort BoardID,		 //_in BoardID
									ushort GantryID,	 //_in GantryID (Range:0~1)
									ushort MasterAxisID, //_in Master AxisID
									ushort SlaveAxisID,	 //_in Slave AxisID
									double VirtualRatio, //_in Virtual Ratio (Master/Slave Mode - Master(0), Virtual Axis Mode : 1 ~ 50)
									double ErrorRange	 //_in Error Range (Yaw Error Range - Yaw Control Disable : 0, Yaw Control Enable : 1 ~ )
									);
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_GantryDisable(
									ushort BoardID,     //_in BoardID
									ushort GantryID     //_in GantryID (Range:0~1)
									);
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_GantryReadConfiguration(
									ushort BoardID,			//_in BoardID
                                    ushort[] MasterAxisID,	//_out MasterAxisID[4] (array[2], array[3] Reserved)
                                    ushort[] SlaveAxisID,	//_out SlaveAxisID[4]  (array[2], array[3] Reserved)
                                    double[] VirtualRatio,	//_out Virtual Ratio[4] (Master/Slave Mode - Master(0), Virtual Axis Mode : 1~50) (array[2], array[3] Reserved)
                                    double[] ErrorRange	    //_out Error Range[4] (Yaw Error Range - Yaw Control Disable : 0, Yaw Control Enable : 1 ~ ) (array[2], array[3] Reserved)
									);
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_GantryReadStatus(
									ushort BoardID,	    //_in BoardID
									ushort GantryID,    //_in GantryID (Range:0~1)
									ref uint Status     //_out Status
									);
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_GantryHome(
									ushort BoardID,			//_in BoardID
									ushort MasterAxisID,	//_in Master AxisID
									ushort HomeMethod,		//_in Home Method
									double HomingVel,		//_in Homin Vel
									double HomingAcc,		//_in Homin Acc
									double HomingDec,		//_in Homin Dec
									double HomingJerk,		//_in Homin Jerk
									double HomingCreepVel,	//_in Homing Creep Vel
									byte   HomingDirection,	//_in Homing Direction (1:CW, 0:CCW)
									double AlignOffset		//_in Align Offset
									);

        //v12.2.0.2
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_ChangeGantryAlign(
                                    ushort BoardID,				//_in BoardID
                                    ushort GantrySlaveAxisID,	//_in Gantry Slave AxisID (*Only Gantry Slave AxisID, Virtual Axis Mode)
                                    double AlignPosition,		//_in Align Position
                                    double Velocity,			//_in Velocity
                                    double Acceleration, 		//_in Acceleration
                                    double Deceleration, 		//_in Deceleration
                                    double Jerk,				//_in Jerk
                                    byte   DisplayMode			//_in DisplayMode //0 : Yaw Error Display (Master Axis - Slave Axis)
									);											  //1 : Yaw Diff. Display (Virtual Axis - Slave Axis)	
                                                                                  //2 : Yaw Error Display (Slave Axis - Master Axis)
                                                                                  //3 : Yaw Diff. Display (Slave Axis - Virtual Axis)

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // Motion APIs - Direct Output
        //
        //
        //-----------------------------------------------------------------------------
        //Pdo Output
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_DirectOutput(
									ushort BoardID,		//_in BoardID
									ushort AxisID,		//_in AxisID
									double OutputValue	//_in Output Value
									);

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // Motion APIs - Error Compensation
        //
        //
        //-----------------------------------------------------------------------------
        //Error Compensation
        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ChangeErrorCompensationMode(
                                    ushort BoardID,		    //_in BoardID
                                    ushort AxisID,		    //_in AxisID (AxisID:1~65535 / Gantry:0~1 / Group:0~15)
                                    byte Type,		        //_in Type (Axis:0, Gantry:1, Group:2)
                                    ushort Mode,		    //_in Mode (EC Data Mode Enable:1, EC Data Mode Disable:0)
                                    byte ECMapID		    //_in ECMapID (Range:0~3) (Table1 ~ Table4)	
									);

        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ReadErrorCompensationStatus(
                                    ushort BoardID,		    //_in BoardID
                                    byte ECMapID,		    //_in ECMapID (Range:0~3) (Table1 ~ Table4)
                                    ref ushort AxisID,      //_out AxisID (AxisID:1~65535 / Gantry:0~1 / Group:0~15)
                                    ref byte Type,		    //_out Type (Axis:0, Gantry:1, Group-2D:2, Group-3D:3)
                                    ref byte Status		    //_out Mode Status (Disabled:0, Enabled:1, Disabling:2, Enabling:3)
									);

        //v12.2.0.0
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern MC_STATUS MC_WriteErrorCompensationDataFile(
                                    ushort BoardID,			 //_in BoardID
                                    ushort ECType,			 //_in ECType (0:1D Axis or Gantry, 1:2D Group, 2:3D Group)
                                    byte ECMapID,			 //_in ECMapID (Range:0~3) (Table1 ~ Table4)
                                    StringBuilder sFullPathFileName //_in WriteFile FullPathFileName
									);

        //=============================================================================
        //-----------------------------------------------------------------------------
        //
        //
        // Motion APIs - etc. Application
        //
        //
        //-----------------------------------------------------------------------------
        //v12.2.0.3
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_ChangeTorqueLimitPositionEvent(
                                    ushort BoardID,				//_in BoardID
                                    ushort AxisID,				//_in AxisID
                                    double EventPosition,		//_in Event Position
                                    double Width,				//_in Width
                                    ushort NormalTorque, 		//_in Normal Torque
                                    ushort EventTorque 		    //_in Event Torque
                                    );

        //v12.2.0.4
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_MovePositionEvent(
                                    ushort BoardID,				//_in BoardID
                                    ushort AxisID,				//_in Command AxisID
                                    ushort RefAxisID,			//_in Monitor AxisID (Trigger Source AxisID)
                                    double EventPosition,		//_in Event Trigger Position
                                    byte EventEdge,			    //_in Event Trigger Edge (0 : Rising, 1 : Falling)
                                    byte EventMotion,			//_in Event Motion (0 : Absolute, 1 : Relative)
                                    double Position,            //_in Command Position
                                    double Velocity,			//_in Command Velocity
                                    double Acceleration,		//_in Command Acceleration
                                    double Deceleration,		//_in Command Deceleration
                                    double Jerk,				//_in Command Jerk
                                    MC_BUFFER_MODE BufferMode	//_in Buffer Mode (Reserved 0 : Aborting)
									);

        //===============================================================================
        //-------------------------------------------------------------------------------
        //
        //
        // Function Module APIs - Special
        //
        //
        //-------------------------------------------------------------------------------        
        //5.1.1.81 MC_WriteIntervalTrigParameterFM [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteIntervalTrigParameterFM(
                                    ushort BoardID,		    //_in BoardID
				                    ushort EcatAddr,		//_in Ethercat Address
				                    ushort AxisID,		    //_in AxisID
				                    double StartPosition,	//_in Start Position
				                    double EndPosition,		//_in End Position
				                    ushort IntervalPeriod,	//_in Interval Period
				                    ushort PulseWidth		//_in Pulse Width
                                    );

        //5.1.1.82 MC_WriteIntervalTrigEnableFM [] : 
        [DllImport(NMC_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern MC_STATUS MC_WriteIntervalTrigEnableFM(
                                    ushort BoardID,		    //_in BoardID
				                    ushort EcatAddr,		//_in Ethercat Address
				                    ushort AxisID,		    //_in AxisID
                                    bool Enable             //_in Configuration Enable True/False
                                    );      

        #endregion
    }
}