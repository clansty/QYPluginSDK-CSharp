// 建议用户不要更改此文件，除非你确信你看得懂这里面所有代码并且你的操作没有问题
namespace QYPlugin
{
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

}
