using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QYPlugin
{
    public static class Robot
    {
        private static class DllEvents
        {
            [DllExport]
            public static string AppInfo() => "\"{\"ret\":1,\"apiver\":2018,\"appid\":\"" + PluginInfo.appid + "\",\"name\":\"" + PluginInfo.name + "\",\"version\":\"" + PluginInfo.version + "\",\"version_id\":" + PluginInfo.versionId.ToString() + ",\"author\":\"" + PluginInfo.author + "\",\"description\":\"" + PluginInfo.description + "\",\"event\":[{\"id\":1001,\"type\":1001,\"name\":\"QY启动事件\",\"function\":\"_eventStartup\",\"priority\":30000},{\"id\":1002,\"type\":1002,\"name\":\"QY关闭事件\",\"function\":\"_eventExit\",\"priority\":30000},{\"id\":1003,\"type\":1003,\"name\":\"应用已被启用\",\"function\":\"_eventEnable\",\"priority\":30000},{\"id\":1004,\"type\":1004,\"name\":\"应用将被停用\",\"function\":\"_eventDisable\",\"priority\":30000},{\"id\":1,\"type\":166,\"name\":\"私聊消息处理\",\"function\":\"_eventPrivateMsg\",\"priority\":30000},{\"id\":2,\"type\":82,\"name\":\"群消息处理\",\"function\":\"_eventGroupMsg\",\"priority\":30000},{\"id\":4,\"type\":101,\"name\":\"推送消息事件\",\"function\":\"_eventPushMsgEvent\",\"priority\":30000},{\"id\":5,\"type\":102,\"name\":\"群管理变动事件处理\",\"function\":\"_eventSystem_GroupAdmin\",\"priority\":30000},{\"id\":6,\"type\":103,\"name\":\"群成员减少事件处理\",\"function\":\"_eventSystem_GroupMemberDecrease\",\"priority\":30000},{\"id\":7,\"type\":104,\"name\":\"群成员增加事件处理\",\"function\":\"_eventSystem_GroupMemberIncrease\",\"priority\":30000},{\"id\":8,\"type\":201,\"name\":\"好友事件处理\",\"function\":\"_eventFriendEvent\",\"priority\":30000},{\"id\":9,\"type\":301,\"name\":\"好友添加请求处理\",\"function\":\"_eventRequest_AddFriend\",\"priority\":30000},{\"id\":10,\"type\":302,\"name\":\"群添加请求处理\",\"function\":\"_eventRequest_AddGroup\",\"priority\":30000}],\"menu\":[{\"name\":\"设置\",\"function\":\"_menuSet\"}],\"auth\":[101,102,103,104,105,110,111,112,113,114,115]}\"";
            [DllExport]
            public static int _eventStartup() => 0;
            [DllExport]
            public static int _eventExit() => 0;
            [DllExport]
            public static int _eventDisable() => 0;
            [DllExport]
            public static int _eventEnable() => 0;
            [DllExport]
            public static int Initialize(int ac, int pt)
            {
                AuthCode = ac;
                ProtocolType = pt;
                return 0;
            }
            [DllExport]
            public static int _menuSet()
            {
                new SetForm().Show();
                return 0;
            }
            [DllExport]
            public static int _eventPrivateMsg(long QQID, int subType, long sendTime, long fromQQ, long fromID, string fromInfo, string msg, string info, int test)
            {
                return 0;
            }
            [DllExport]
            public static int _eventGroupMsg(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, string fromInfo, string msg, string info, int test)
            {
                return 0;
            }
            [DllExport]
            public static int _eventPushMsgEvent(long QQID, int subType, long sendTime, long fromID, long fromQQ, long fromQQID, string msg, string info, int test)
            {
                return 0;
            }
            [DllExport]
            public static int _eventSystem_GroupAdmin(long QQID, int subType, long sendTime, long fromGroup, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport]
            public static int _eventSystem_GroupMemberDecrease(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport]
            public static int _eventSystem_GroupMemberIncrease(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport]
            public static int _eventFriendEvent(long QQID, int subType, long sendTime, long fromQQ, string info)
            {
                return 0;
            }
            [DllExport]
            public static int _eventRequest_AddFriend(long QQID, int subType, long sendTime, long fromQQ, string source, string msg, string responseFlag)
            {
                return 0;
            }
            [DllExport]
            public static int _eventRequest_AddGroup(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, string source, string msg, string responseFlag)
            {
                return 0;
            }
        }
        private static int AuthCode { get; set; }
        private static int ProtocolType { get; set; }

        /// <summary>
        /// 获取应用数据目录，末尾带“\”
        /// <para>返回如：D:\robot\plugin\com.contract.testapp\</para>
        /// <para>应用的所有数据、配置【必须】存放于此目录，避免给用户带来困扰。</para>
        /// </summary>
        public static string AppDirectory => Marshal.PtrToStringAnsi(QY_getAppDirectory(AuthCode));
        [DllImport("QYOffer.dll")]
        private static extern IntPtr QY_getAppDirectory(int authCode);


    }
}
