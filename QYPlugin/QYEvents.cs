using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYPlugin
{
    public static class PluginInfo
    {
        public const string name = "某个 C# 契约应用";
        public const string appid = "com.clansty.qyapp";
        public const string version = "1.0";
        public const int versionId = 1;
        public const string author = "凌莞";
        public const string description = "凌莞优化重制版 C# SDK";
    }

    public static class QYEvents
    {
        public static void FriendMsg(FriendMsgArgs e)
        {
            // 这里处理好友消息
            e.Reply("你发了好友消息 " + e.Msg);
        }
        public static void GroupTmpMsg(GroupTmpMsgArgs e)
        {
            // 这里处理群临时消息
            e.Reply("你发了群临时消息 " + e.Msg);
        }
    }
}
