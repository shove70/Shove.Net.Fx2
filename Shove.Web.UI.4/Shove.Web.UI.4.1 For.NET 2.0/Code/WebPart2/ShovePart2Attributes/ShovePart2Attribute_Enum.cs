using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 下拉类型（枚举）属性
    /// </summary>
    public class ShovePart2Attribute_Enum : ShovePart2Attribute
    {
        /// <summary>
        /// 创建下拉类型(枚举)属性实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="range">属性取值范围</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShovePart2Attribute_Enum(string name, string range, object defaultValue, string vote)
        {
            Type = "Enum";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}
