using System;
using System.Runtime.InteropServices;

// 建议用户不要更改此文件，除非你确信你看得懂这里面所有代码并且你的操作没有问题
namespace QYPlugin
{
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
    public class GroupAdminChangedArgs : EventArgs
    {
        /// <summary>
        /// 群号
        /// </summary>
        public string Group { get; }
        /// <summary>
        /// 被设为或取消管理员的 QQ 号
        /// </summary>
        public string QQ { get; }

        public GroupAdminChangedArgs(long a, long b)
        {
            Group = a.ToString();
            QQ = b.ToString();
        }
    }
    public class RequestAddFriendArgs : EventArgs
    {
        public RequestAddFriendArgs(long a ,string b, string c, int d)
        {
            FromQQ = a.ToString();
            Msg = b;
            ResponseFlag = c;
            AuthCode = d;
        }

        /// <summary>
        /// 申请加你的人
        /// </summary>
        public string FromQQ { get; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Msg { get; }

        private string ResponseFlag { get; }
        private int AuthCode { get; }

        /// <summary>
        /// 同意该请求
        /// </summary>
        /// <param name="remark">你给别人的备注</param>
        public void Accept(string remark = "") => QY_setFriendAddRequest(AuthCode, Convert.ToInt64(Robot.LoginQQ), ResponseFlag, 0, remark);
        /// <summary>
        /// 拒绝该请求
        /// </summary>
        /// <param name="reason">理由</param>
        public void Reject(string reason = "") => QY_setFriendAddRequest(AuthCode, Convert.ToInt64(Robot.LoginQQ), ResponseFlag, 1, reason);

        [DllImport("QYOffer.dll")]
        private static extern int QY_setFriendAddRequest(int authCode, long qqID, string msg, int a, string b);

    }
    public class RequestGroupArgs : EventArgs
    {
        public RequestGroupArgs(long a, string b, string c, int d, string e, int f)
        {
            Group = a.ToString();
            Msg = b;
            ResponseFlag = c;
            AuthCode = d;
            FromQQ = e;
            St = f;
        }

        /// <summary>
        /// 申请加群或邀请你的人
        /// </summary>
        public string FromQQ { get; }
        /// <summary>
        /// 申请加入的群组
        /// </summary>
        public string Group { get; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Msg { get; }

        private string ResponseFlag { get; }
        private int AuthCode { get; }
        private int St { get; }

        /// <summary>
        /// 同意该请求
        /// </summary>
        public void Accept() => QY_setGroupAddRequest(AuthCode, Convert.ToInt64(Robot.LoginQQ), ResponseFlag, St == 1 ? 0 : 1, 0, "");
        /// <summary>
        /// 拒绝该请求
        /// </summary>
        /// <param name="reason">理由</param>
        public void Reject(string reason = "") => QY_setGroupAddRequest(AuthCode, Convert.ToInt64(Robot.LoginQQ), ResponseFlag, St == 1 ? 0 : 1, 1, reason);
        [DllImport("QYOffer.dll")]
        private static extern int QY_setGroupAddRequest(int authCode, long qqID, string msg, int a, int c, string b);

    }
}
