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
        private static int AuthCode;

        /// <summary>
        /// 获取应用数据目录
        /// <para>返回如：D:\robot\plugin\com.contract.testapp\</para>
        /// <para>应用的所有数据、配置【必须】存放于此目录，避免给用户带来困扰。</para>
        /// </summary>
        public static string AppDirectory => Marshal.PtrToStringAnsi(QY_getAppDirectory(AuthCode));
        [DllImport("QYOffer.dll")]
        private static extern IntPtr QY_getAppDirectory(int authCode);


    }
}
