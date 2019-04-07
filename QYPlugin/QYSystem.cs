using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// 建议用户不要更改此文件，除非你确信你看得懂这里面所有代码并且你的操作没有问题
namespace QYPlugin
{
    /// <summary>
    /// 执行 QY 机器人相关操作
    /// </summary>
    public static class Robot
    {
        private static class DllEvents
        {
            [DllExport(CallingConvention.StdCall)]
            public static string AppInfo()
            {
                return "{\"ret\":1,\"apiver\":2018,\"appid\":\"" + PluginInfo.appid + "\",\"name\":\"" + PluginInfo.name + "\",\"version\":\"" + PluginInfo.version + "\",\"version_id\":" + PluginInfo.versionId.ToString() + ",\"author\":\"" + PluginInfo.author + "\",\"description\":\"" + PluginInfo.description + "\",\"event\":[{\"id\":1001,\"type\":1001,\"name\":\"QY启动事件\",\"function\":\"_eventStartup\",\"priority\":30000},{\"id\":1002,\"type\":1002,\"name\":\"QY关闭事件\",\"function\":\"_eventExit\",\"priority\":30000},{\"id\":1003,\"type\":1003,\"name\":\"应用已被启用\",\"function\":\"_eventEnable\",\"priority\":30000},{\"id\":1004,\"type\":1004,\"name\":\"应用将被停用\",\"function\":\"_eventDisable\",\"priority\":30000},{\"id\":1,\"type\":166,\"name\":\"私聊消息处理\",\"function\":\"_eventPrivateMsg\",\"priority\":30000},{\"id\":2,\"type\":82,\"name\":\"群消息处理\",\"function\":\"_eventGroupMsg\",\"priority\":30000},{\"id\":4,\"type\":101,\"name\":\"推送消息事件\",\"function\":\"_eventPushMsgEvent\",\"priority\":30000},{\"id\":5,\"type\":102,\"name\":\"群管理变动事件处理\",\"function\":\"_eventSystem_GroupAdmin\",\"priority\":30000},{\"id\":6,\"type\":103,\"name\":\"群成员减少事件处理\",\"function\":\"_eventSystem_GroupMemberDecrease\",\"priority\":30000},{\"id\":7,\"type\":104,\"name\":\"群成员增加事件处理\",\"function\":\"_eventSystem_GroupMemberIncrease\",\"priority\":30000},{\"id\":8,\"type\":201,\"name\":\"好友事件处理\",\"function\":\"_eventFriendEvent\",\"priority\":30000},{\"id\":9,\"type\":301,\"name\":\"好友添加请求处理\",\"function\":\"_eventRequest_AddFriend\",\"priority\":30000},{\"id\":10,\"type\":302,\"name\":\"群添加请求处理\",\"function\":\"_eventRequest_AddGroup\",\"priority\":30000}],\"menu\":[{\"name\":\"设置\",\"function\":\"_menuSet\"}],\"auth\":[101,102,103,104,105,111,112,113,114,115]}";
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventStartup() => 0;
            [DllExport(CallingConvention.StdCall)]
            public static int _eventExit() => 0;
            [DllExport(CallingConvention.StdCall)]
            public static int _eventDisable() => 0;
            [DllExport(CallingConvention.StdCall)]
            public static int _eventEnable() => 0;
            [DllExport(CallingConvention.StdCall)]
            public static int Initialize(int ac, int pt)
            {
                AuthCode = ac;
                ProtocolType = pt;
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _menuSet()
            {
                new SetForm().Show();
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventPrivateMsg(long QQID, int subType, long sendTime, long fromQQ, long fromID, string fromInfo, string msg, string info, int test)
            {
                switch(subType)
                {
                    case 11:
                        QYEvents.FriendMsg(new FriendMsgArgs(fromQQ, msg));
                        break;
                    case 2:
                        QYEvents.GroupTmpMsg(new GroupTmpMsgArgs(fromQQ, fromID, msg));
                        break;
                }
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventGroupMsg(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, string fromInfo, string msg, string info, int test)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventPushMsgEvent(long QQID, int subType, long sendTime, long fromID, long fromQQ, long fromQQID, string msg, string info, int test)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventSystem_GroupAdmin(long QQID, int subType, long sendTime, long fromGroup, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventSystem_GroupMemberDecrease(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventSystem_GroupMemberIncrease(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventFriendEvent(long QQID, int subType, long sendTime, long fromQQ, string info)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventRequest_AddFriend(long QQID, int subType, long sendTime, long fromQQ, string source, string msg, string responseFlag)
            {
                return 0;
            }
            [DllExport(CallingConvention.StdCall)]
            public static int _eventRequest_AddGroup(long QQID, int subType, long sendTime, long fromGroup, long fromQQ, string source, string msg, string responseFlag)
            {
                return 0;
            }
        }

        private class Unpack // 根据易语言 SDK 的 Unpack 类来写的
        {
            public Unpack(byte[] bin)
            {
                MemoryStream ms = new MemoryStream(bin);
                br = new BinaryReader(ms);
            }
            public Unpack(string base64)
            {
                byte[] bytes = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(bytes);
                br = new BinaryReader(ms);
            }        

            private BinaryReader br;

            public int NextInt
            {
                get
                {
                    byte[] buffer = br.ReadBytes(4);
                    Array.Reverse(buffer);
                    return BitConverter.ToInt32(buffer, 0);
                }
            }
            public long NextLong
            {
                get
                {
                    byte[] buffer = br.ReadBytes(8);
                    Array.Reverse(buffer);
                    return BitConverter.ToInt64(buffer, 0);
                }
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

        /// <summary>
        /// 获取当前登录的 QQ 号
        /// </summary>
        public static string LoginQQ => LongQQ.ToString();
        private static long LongQQ
        {
            get
            {
                string base64 = Marshal.PtrToStringAnsi(QY_getLoginQQList(AuthCode));
                Unpack u = new Unpack(base64);
                long qq = 0;
                if (u.NextInt > 0)
                    qq = u.NextLong;
                return qq;
            }
        }
        [DllImport("QYOffer.dll")]
        private static extern IntPtr QY_getLoginQQList(int authCode);

        /// <summary>
        /// 获取当前登录 QQ 的昵称
        /// </summary>
        public static string LoginNick => Marshal.PtrToStringAnsi(QY_getLoginNick(AuthCode, LongQQ));
        [DllImport("QYOffer.dll")]
        private static extern IntPtr QY_getLoginNick(int authCode, long qqID);

        /// <summary>
        /// 获取当前框架登录 QQ 的在线状态
        /// </summary>
        public static OnlineStatus Status => (OnlineStatus)QY_getFrameAccountState(AuthCode, LongQQ);
        [DllImport("QYOffer.dll")]
        private static extern int QY_getFrameAccountState(int authCode, long qqID);

        /// <summary>
        /// 用于机器人框架发送消息
        /// </summary>
        public static class Send
        {
            /// <summary>
            /// 发送好友消息 
            /// </summary>
            /// <param name="qq">收件人 QQ</param>
            /// <param name="msg">消息内容</param>
            /// <returns>是否成功</returns>
            public static bool Friend(string qq, string msg)
            {
                bool res = long.TryParse(qq, out long targ);
                if (!res)
                    return false;
                int ret = QY_sendFriendMsg(AuthCode, LongQQ, targ, msg);
                return ret == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_sendFriendMsg(int authCode, long qqID, long target, string msg);

            /// <summary>
            /// 发送群消息 
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="msg">消息内容</param>
            /// <returns>是否成功</returns>
            public static bool Group(string group, string msg)
            {
                bool res = long.TryParse(group, out long targ);
                if (!res)
                    return false;
                int ret = QY_sendGroupMsg(AuthCode, LongQQ, targ, msg);
                return ret == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_sendGroupMsg(int authCode, long qqID, long target, string msg);

            /// <summary>
            /// 发送群临时消息
            /// </summary>
            /// <param name="group">收件人所在群号</param>
            /// <param name="qq">收件人 QQ</param>
            /// <param name="msg">待发送消息内容</param>
            /// <returns>是否成功</returns>
            public static bool GTmp(string group, string qq, string msg)
            {
                bool res = long.TryParse(group, out long targ);
                if (!res)
                    return false;
                res = long.TryParse(qq, out long targqq);
                if (!res)
                    return false;
                int ret = QY_sendGroupTmpMsg(AuthCode, LongQQ, targ, targqq, msg);
                return ret == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_sendGroupTmpMsg(int authCode, long qqID, long target, long targqq, string msg);

        }

        /// <summary>
        /// 在框架中记录日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="eventt">事件</param>
        /// <param name="color">颜色</param>
        public static void Log(string content, string eventt = "日志", LogColor color = LogColor.dimGray) => QY_addLog(AuthCode, 0, (int)color, eventt, content);
        [DllImport("QYOffer.dll")]
        private static extern int QY_addLog(int authCode, long qqID, int a, string b, string c);

        /// <summary>
        /// 点赞，用一次赞一下，每天可赞五十下
        /// </summary>
        /// <param name="target">目标</param>
        /// <returns>结果的枚举</returns>
        public static LikeResult Like(string target)
        {
            bool isok = long.TryParse(target, out long t);
            if (!isok)
                return LikeResult.notQQ;
            int res = QY_sendLikeFavorite(AuthCode, LongQQ, t);
            switch (res)
            {
                case 0:
                    return LikeResult.success;
                case 1:
                    return LikeResult.notAllowed;
                case 51:
                    return LikeResult.full;
            }
            return LikeResult.other;
        }
        [DllImport("QYOffer.dll")]
        private static extern int QY_sendLikeFavorite(int authCode, long qqID, long targ);

    }

    /// <summary>
    /// 表示机器人的在线状态
    /// </summary>
    public enum OnlineStatus
    {
        /// <summary>
        /// 表示机器人当前离线
        /// </summary>
        offline = 0,
        /// <summary>
        /// 表示机器人当前在线
        /// </summary>
        online = 1
    }
    /// <summary>
    /// 日志输出的字体颜色
    /// </summary>
    public enum LogColor
    {
        /// <summary>
        /// 暗淡的灰色
        /// <para>6908265
        /// #696969
        /// 105,105,105</para>
        /// </summary>
        dimGray = 0,
        /// <summary>
        /// 纯黑
        /// <para>0         #000000  0,0,0</para>
        /// </summary>
        black = 10,
        /// <summary>
        /// 深紫罗兰的蓝色
        /// <para>14822282  #8A2BE2  138,43,226</para>
        /// </summary>
        blueViolet = 11,
        /// <summary>
        /// 棕色
        /// <para>2763429   #A52A2A  165,42,42</para>
        /// </summary>
        brown = 12,
        /// <summary>
        /// 纯蓝
        /// <para>16711680  #0000FF  0,0,255</para>
        /// </summary>
        blue = 13,
        /// <summary>
        /// 纯绿
        /// <para>32768     #008000  0,128,0</para>
        /// </summary>
        green = 14,
        /// <summary>
        /// 深橙色
        /// <para>36095     #FF8C00  255,140,0</para>
        /// </summary>
        darkOrange = 20,
        /// <summary>
        /// 深粉色
        /// <para>9639167   #FF1493  255,20,147</para>
        /// </summary>
        deepPink = 30,
        /// <summary>
        /// 纯红
        /// <para>255       #FF0000  255,0,0</para>
        /// </summary>
        red = 40
    }
    /// <summary>
    /// 点赞的结果
    /// </summary>
    public enum LikeResult
    {
        /// <summary>
        /// 赞成功
        /// </summary>
        success,
        /// <summary>
        /// 对方不允许非好友点赞
        /// </summary>
        notAllowed,
        /// <summary>
        /// 今天已赞满
        /// </summary>
        full,
        /// <summary>
        /// 指定的目标不是正确的 QQ 号
        /// </summary>
        notQQ,
        /// <summary>
        /// 其他错误
        /// </summary>
        other
    }

    public class FriendMsgArgs : EventArgs
    {
        public FriendMsgArgs(long fq, string m)
        {
            FromQQ = fq.ToString();
            Msg = m;
        }
        /// <summary>
        /// 发送这条消息的 QQ
        /// </summary>
        public string FromQQ { get; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; }

        /// <summary>
        /// 快捷回复这条消息，即向 FromQQ 发送好友消息
        /// </summary>
        /// <param name="msg">回复消息内容</param>
        /// <returns>是否成功</returns>
        public bool Reply(string msg) => Robot.Send.Friend(FromQQ, msg);
    }
    public class GroupTmpMsgArgs : FriendMsgArgs
    {
        public GroupTmpMsgArgs(long fq, long fg, string m) : base(fq, m)
        {
            FromGroup = fg.ToString();
        }
        /// <summary>
        /// 发送者所在的群号
        /// </summary>
        public string FromGroup { get; }
        
        /// <summary>
        /// 快捷回复这条消息，即向 FromGroup 的 FromQQ 发送群临时消息
        /// </summary>
        /// <param name="msg">回复消息内容</param>
        /// <returns>是否成功</returns>
        public new bool Reply(string msg) => Robot.Send.GTmp(FromGroup, FromQQ, msg);
    }
}
