// 建议用户不要更改此文件，除非你确信你看得懂这里面所有代码并且你的操作没有问题
namespace QYPlugin
{
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

}
