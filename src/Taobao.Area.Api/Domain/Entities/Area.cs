using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taobao.Area.Api.Domain.Entities
{
    public class Area
    {
        // 行政编码
        public int Id { get; set; }
        // 简体
        public string Name { get; set; }
        // 繁体
        public string NameTraditional { get; set; }
        // 父Id
        public int ParentId { get; set; }
        // 数据类型 0默认;1又名;2、3属于;11已合并到;12已更名为 
        public int DataType { get; set; }
        // 类别名称
        public string DataTypeName { get; set; }
        // 根据类别名称填写
        public string OtherName { get; set; }
        // 格式化全称
        public string FormatName { get; set; }
    }
}
