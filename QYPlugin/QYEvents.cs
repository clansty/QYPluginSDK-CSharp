using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYPlugin
{
    public static class PluginInfo
    {
        public static readonly string name = "某个 C# 契约应用";
        public static readonly string appid = "com.clansty.qyapp";
        public static readonly string version = "1.0";
        public static readonly int versionId = 1;
        public static readonly string author = "凌莞";
        public static readonly string description = "凌莞优化重制版 C# SDK";
    }

    public static class QYEvents
    {
        public static void FriendMsg(FriendMsgArgs e)
        {
            // 这里处理好友消息
        }
        public static void GroupTmpMsg(GroupTmpMsgArgs e)
        {
            // 这里处理群临时消息
        }
    }
}
