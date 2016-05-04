using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainBoundedContext.UserAgg
{
    /// <summary>
    /// 用户信用实体类
    /// </summary>
    public class UserCredit
    {
        /// <summary>
        /// 好评率
        /// </summary>
        public string GoodRate { get; set; }

        /// <summary>
        /// 买家信用
        /// </summary>
        public string BuyersCredit { get; set; }

        /// <summary>
        /// 买家信用图片
        /// </summary>
        public string BuyersCreditImg { get; set; }

        /// <summary>
        /// 认证情况
        /// </summary>
        public string Auth { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegisteDate { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 是否查封
        /// </summary>
        public string IsSealUp { get; set; }

        /// <summary>
        /// 是否盘点
        /// </summary>
        public string IsError { get; set; }
    }
}
