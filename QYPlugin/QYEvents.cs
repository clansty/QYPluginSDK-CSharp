using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QYPlugin
{
    public static class PluginInfo //插件的相关信息
    {
        public const string name = "某个 C# 契约应用"; //名称
        public const string appid = "com.clansty.qyapp"; //标识
        public const string version = "1.0"; //版本
        public const int versionId = 1; //版本ID
        public const string author = "凌莞"; //作者
        public const string description = "凌莞优化重制版 C# SDK"; // 简介
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
        public static void GroupMsg(GroupMsgArgs e)
        {
            // 处理群消息
            e.Reply(e.Msg, true);
            e.Recall(); //撤回
        }
    }
}
