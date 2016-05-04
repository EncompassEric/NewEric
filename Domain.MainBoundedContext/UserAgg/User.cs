using System;
using System.Collections.Generic;
using Domain.MainBoundedContext.RoleAgg;

namespace Domain.MainBoundedContext.UserAgg
{
    using Seedwork;

    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 账户V币
        /// </summary>
        public double VCurrency { get; set; }

        /// <summary>
        /// 账户V点
        /// </summary>
        public double VPoint { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// SessionId用于自动登录
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        ///// <summary>
        ///// 权限
        ///// </summary>
        public List<Role> Roles { get; set; } 
    }
}
