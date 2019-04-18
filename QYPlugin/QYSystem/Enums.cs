// 建议用户不要更改此文件，除非你确信你看得懂这里面所有代码并且你的操作没有问题
namespace QYPlugin
{
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

}
