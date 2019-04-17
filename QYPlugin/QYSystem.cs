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
                Dictionary<string, string> ifo = InfoParse(fromInfo);

                Unpack u = new Unpack(ifo["GExtraInfo"]);
                string nick = u.NextStr;
                string gc = u.NextStr;
                u.Skip(12);
                string title = u.NextStr;

                QYEvents.GroupMsg(new GroupMsgArgs(fromQQ, fromGroup, msg, ifo["srcmsg"], gc == "" ? nick : gc, title, ifo["MsgInfo"], AuthCode));

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

            private static Dictionary<string, string> InfoParse(string base64)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();
                Unpack u = new Unpack(base64);
                int count = u.NextInt;
                for(int i = 0; i < count; i++)
                {
                    Unpack n = new Unpack(u.NextToken);
                    string key = n.NextStr;
                    pairs[key] = n.NextStr;
                }
                return pairs;
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
            public short NextShort
            {
                get
                {
                    byte[] buffer = br.ReadBytes(2);
                    Array.Reverse(buffer);
                    return BitConverter.ToInt16(buffer, 0);
                }
            }
            public string NextStr
            {
                get
                {
                    short l = NextShort;
                    byte[] buffer = br.ReadBytes(l);
                    return Encoding.Default.GetString(buffer);
                }
            }
            public byte[] NextToken => br.ReadBytes(NextShort);

            public void Skip(short l) => br.ReadBytes(l);
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

        /// <summary>
        /// 进行与群组相关的操作
        /// </summary>
        public static class Group
        {
            /// <summary>
            /// 设置群全员禁言
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool AllMuteOn(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupWholeBanSpeak(AuthCode, LongQQ, t, 1) == 0;
            }
            /// <summary>
            /// 解除群全员禁言
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool AllMuteOff(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupWholeBanSpeak(AuthCode, LongQQ, t, 0) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupWholeBanSpeak(int authCode, long qqID, long targ, int s);

            /// <summary>
            /// 群成员禁言
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">成员 QQ</param>
            /// <param name="time">以秒为单位的时间，解禁则为 0</param>
            /// <returns>是否成功</returns>
            public static bool MuteMember(string group, string member, long time = 0)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setGroupMembersBanSpeak(AuthCode, LongQQ, t, m, time) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupMembersBanSpeak(int authCode, long qqID, long targ, long s, long a);

            /// <summary>
            /// 更改群成员名片
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">成员 QQ</param>
            /// <param name="name">新的群名片</param>
            /// <returns>是否成功</returns>
            public static bool SetCard(string group, string member, string name)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setModifyGroupMemberCard(AuthCode, LongQQ, t, m, name) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setModifyGroupMemberCard(int authCode, long qqID, long targ, long s, string a);

            /// <summary>
            /// 群踢人
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">被踢的人</param>
            /// <param name="RejectJoin">是否拒绝其再次申请加入</param>
            /// <returns>是否成功</returns>
            public static bool RemoveMember(string group, string member, bool RejectJoin = false)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setGroupMembersKick(AuthCode, LongQQ, t, m, RejectJoin ? 1 : 0) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupMembersKick(int authCode, long qqID, long targ, long s, int a);

            /// <summary>
            /// 允许群匿名聊天
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool EnableAnonymous(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupAnonymousBan(AuthCode, LongQQ, t, 0) == 0;
            }
            /// <summary>
            /// 禁止群匿名聊天
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool DisableAnonymous(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupAnonymousBan(AuthCode, LongQQ, t, 1) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupAnonymousBan(int authCode, long qqID, long targ, int s);

            /// <summary>
            /// 设置管理员
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">群成员</param>
            /// <returns>是否成功</returns>
            public static bool AddAdmin(string group, string member)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setGroupAdmini(AuthCode, LongQQ, t, m, 1) == 0;
            }
            /// <summary>
            /// 撤销管理员
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">成员</param>
            /// <returns></returns>
            public static bool RemoveAdmin(string group, string member)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setGroupAdmini(AuthCode, LongQQ, t, m, 0) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupAdmini(int authCode, long qqID, long targ, long s, int a);

            /// <summary>
            /// 退出群，如果指定自己创建的群，则解散群
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns></returns>
            public static bool Exit(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setExitGroupChat(AuthCode, LongQQ, t, 1) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setExitGroupChat(int authCode, long qqID, long targ, int s);

            /// <summary>
            /// 设置专属头衔
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">成员</param>
            /// <param name="title">专属头衔，删除留空</param>
            /// <returns></returns>
            public static bool SetTitle(string group, string member, string title = "")
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return false;
                return QY_setGroupSpecialTitle(AuthCode, LongQQ, t, m, title, -1) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupSpecialTitle(int authCode, long qqID, long targ, long s, string q, int a);

            /// <summary>
            /// 添加群
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="msg">附加申请信息</param>
            /// <returns></returns>
            public static bool Add(string group, string msg = "")
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setAddGroup(AuthCode, LongQQ, t, msg) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setAddGroup(int authCode, long qqID, long targ, string s);

            /// <summary>
            /// 上传群文件
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="path">文件本地路径</param>
            /// <param name="folder">群内的文件夹</param>
            /// <returns></returns>
            public static bool UploadFile(string group, string path, string folder = "/")
            {
                bool res = long.TryParse(group, out long t);
                if (!res)
                    return false;
                res = File.Exists(path);
                if (!res)
                    return false;
                return QY_setGroupFileUpload(AuthCode, LongQQ, t, folder, path) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupFileUpload(int authCode, long qqID, long targ, string s, string a);

            /// <summary>
            /// 取群名片
            /// </summary>
            /// <param name="group">群号</param>
            /// <param name="member">成员</param>
            /// <returns></returns>
            public static string GetCard(string group, string member)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return "";
                isok = long.TryParse(member, out long m);
                if (!isok)
                    return "";
                string b64 = Marshal.PtrToStringAnsi(QY_getGroupMemberCard(AuthCode, LongQQ, t, m, false));
                Unpack u = new Unpack(b64);
                return u.NextStr;
            }
            [DllImport("QYOffer.dll")]
            private static extern IntPtr QY_getGroupMemberCard(int authCode, long qqID, long targ, long s, bool a);

            /// <summary>
            /// 取群组的信息
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns></returns>
            public static GroupInfo GetInfo(string group)
            {
                long.TryParse(group, out long t);
                string b64 = Marshal.PtrToStringAnsi(QY_getGroupInfo(AuthCode, LongQQ, t));
                Unpack u = new Unpack(b64);
                long a = u.NextLong;
                long b = u.NextLong;
                u.Skip(4);
                int c = u.NextInt;
                int d = u.NextInt;
                string e = u.NextStr;
                u.Skip(u.NextShort);
                int f = u.NextInt;
                return new GroupInfo(a, b, c, d, e, f, u.NextStr);
            }
            [DllImport("QYOffer.dll")]
            private static extern IntPtr QY_getGroupInfo(int authCode, long qqID, long targ);

            /// <summary>
            /// 允许群临时会话
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool EnableTmpSession(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupPrivateSession(AuthCode, LongQQ, t, 1) == 0;
            }
            /// <summary>
            /// 禁止群临时会话
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool DisableTmpSession(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupPrivateSession(AuthCode, LongQQ, t, 0) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupPrivateSession(int authCode, long qqID, long targ, int s);

            /// <summary>
            /// 允许发起多人聊天
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool EnableCreateMultiChat(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupManyPeopleChat(AuthCode, LongQQ, t, 1) == 0;
            }
            /// <summary>
            /// 禁止发起多人聊天
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns>是否成功</returns>
            public static bool DisableCreateMultiChat(string group)
            {
                bool isok = long.TryParse(group, out long t);
                if (!isok)
                    return false;
                return QY_setGroupManyPeopleChat(AuthCode, LongQQ, t, 0) == 0;
            }
            [DllImport("QYOffer.dll")]
            private static extern int QY_setGroupManyPeopleChat(int authCode, long qqID, long targ, int s);

            /// <summary>
            /// 取群成员列表
            /// </summary>
            /// <param name="group">群号</param>
            /// <returns></returns>
            public static List<GroupMember> GetMembers(string group)
            {
                long.TryParse(group, out long t);
                string b64 = Marshal.PtrToStringAnsi(QY_getGroupMemberList(AuthCode, LongQQ, t));
                Unpack u = new Unpack(b64);
                int count = u.NextInt;
                List<GroupMember> l = new List<GroupMember>();
                for (int i = 0; i < count; i++)
                {
                    Unpack n = new Unpack(u.NextToken);
                    l.Add(new GroupMember(n.NextLong, n.NextStr, n.NextStr, n.NextInt, n.NextInt, n.NextStr, n.NextInt, n.NextInt, n.NextStr, n.NextInt, n.NextInt, n.NextStr, n.NextInt, n.NextInt));
                }
                return l;
            }
            [DllImport("QYOffer.dll")]
            private static extern IntPtr QY_getGroupMemberList(int authCode, long qqID, long targ);

        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="target">被删除的好友QQ</param>
        /// <returns></returns>
        public static bool RemoveFriend(string target)
        {
            bool res = long.TryParse(target, out long targ);
            if (!res)
                return false;
            return QY_setDelFriend(AuthCode, LongQQ, targ) == 0;
        }
        [DllImport("QYOffer.dll")]
        private static extern int QY_setDelFriend(int authCode, long qqID, long targ);


    }

    /// <summary>
    /// 进行 LQ 码生成
    /// </summary>
    public static class LQ
    {
        /// <summary>
        /// 艾特全员
        /// </summary>
        public const string AtAll = "[@all] ";
        /// <summary>
        /// 艾特人
        /// </summary>
        /// <param name="qq">人</param>
        /// <returns></returns>
        public static string At(string qq) => $"[@{qq}] ";

        /// <summary>
        /// 表示本地图片，插入到待发送的消息里
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns></returns>
        public static string LocalPic(string path) => $"[LQ:image,path={path.Escape()}]";
        /// <summary>
        /// 表示网络图片，插入到待发送的消息里
        /// </summary>
        /// <param name="url">网络图片 URL 地址</param>
        /// <returns></returns>
        public static string WebPic(string url) => $"[LQ:image,urls={url.Escape()}]";
        /// <summary>
        /// 闪照
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns></returns>
        public static string FlashPic(string path) => $"[LQ:flashpic,path={path.Escape()}]";
        /// <summary>
        /// 发送本地语音
        /// </summary>
        /// <param name="path">语音文件路径</param>
        /// <returns></returns>
        public static string Record(string path) => $"[LQ:record,path={path.Escape()}]";

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Escape(this string str) => str.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;").Replace(",", "&#44;");
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
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男性
        /// </summary>
        male,
        /// <summary>
        /// 女性
        /// </summary>
        female
    }
    /// <summary>
    /// 群成员的身份
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// 群员
        /// </summary>
        member = 1,
        /// <summary>
        /// 管理员
        /// </summary>
        admin = 2,
        /// <summary>
        /// 群主
        /// </summary>
        master = 3
    }

    /// <summary>
    /// 群组信息
    /// </summary>
    public struct GroupInfo
    {
        /// <summary>
        /// 群号
        /// </summary>
        public string ID { get; }
        /// <summary>
        /// 群主QQ
        /// </summary>
        public string Master { get; }
        /// <summary>
        /// 最大人口容量
        /// </summary>
        public int MaxPopu { get; }
        /// <summary>
        /// 当前人口数
        /// </summary>
        public int Popu { get; }
        /// <summary>
        /// 群名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 群等级
        /// </summary>
        public int Level { get; }
        /// <summary>
        /// 群介绍
        /// </summary>
        public string Description { get; }

        public GroupInfo(long a, long b, int c, int d, string e, int f, string g)
        {
            ID = a.ToString();
            Master = b.ToString();
            MaxPopu = c;
            Popu = d;
            Name = e;
            Level = f;
            Description = g;
        }
    }
    /// <summary>
    /// 群成员信息
    /// </summary>
    public struct GroupMember
    {
        /// <summary>
        /// QQ 号
        /// </summary>
        public string QQ { get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; }
        /// <summary>
        /// 群名片
        /// </summary>
        public string Card { get; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Region { get; }
        /// <summary>
        /// 加群时间
        /// </summary>
        public int JoinTime { get; }
        /// <summary>
        /// 上次发言时间
        /// </summary>
        public int LastSeen { get; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; }
        /// <summary>
        /// 身份
        /// </summary>
        public Role Role { get; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// 专属头衔过期时间
        /// </summary>
        public int TitleExp { get; }
        /// <summary>
        /// 不良记录成员
        /// </summary>
        public bool BadGuy { get; }
        /// <summary>
        /// 允许修改群名片
        /// </summary>
        public bool AllowedCardModify { get; }
        public GroupMember(long a, string b, string c, int d, int e, string f, int g, int h, string i, int j, int k, string l, int m, int n)
        {
            QQ = a.ToString();
            Nick = b;
            Card = c;
            Gender = (Gender)d;
            Age = e;
            Region = f;
            JoinTime = g;
            LastSeen = h;
            Level = i;
            Role = (Role)j;
            BadGuy = k == 1;
            Title = l;
            TitleExp = m;
            AllowedCardModify = n == 1;
        }
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
    public class GroupMsgArgs : EventArgs
    {
        public GroupMsgArgs(long a, long b, string c, string d, string e, string f, string g, int h)
        {
            FromQQ = a.ToString();
            FromGroup = b.ToString();
            Msg = c;
            SrcMsg = d;
            Card = e;
            Title = f;
            MsgInfo = g;
            AuthCode = h;
        }
        /// <summary>
        /// 发送这条消息的人
        /// </summary>
        public string FromQQ { get; }
        /// <summary>
        /// 来源群组
        /// </summary>
        public string FromGroup { get; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; }
        /// <summary>
        /// 用于引用的消息头，如需回复消息则把它加在发送的消息的最前面。如果快捷回复则无需加入，把第二个参数设为 true 即可
        /// </summary>
        public string SrcMsg { get; }

        /// <summary>
        /// 群名片,若无群名片则为昵称
        /// </summary>
        public string Card { get; }
        /// <summary>
        /// 专属头衔
        /// </summary>
        public string Title { get; }

        private string MsgInfo { get; }//撤回用的
        private int AuthCode { get; }

        /// <summary>
        /// 快捷回复
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="includeSrcMsg">是否引用原始消息，设为 true 相当于在 msg 开头加入 SrcMsg</param>
        public void Reply(string msg, bool includeSrcMsg) => Robot.Send.Group(FromGroup, (includeSrcMsg ? SrcMsg : "") + msg);
        /// <summary>
        /// 撤回这条消息
        /// </summary>
        /// <returns></returns>
        public bool Recall() => QY_setMessageSvcMsgWithDraw(AuthCode, Convert.ToInt64(Robot.LoginQQ), MsgInfo) == 0;
        [DllImport("QYOffer.dll")]
        private static extern int QY_setMessageSvcMsgWithDraw(int authCode, long qqID, string msg);
    }

}
