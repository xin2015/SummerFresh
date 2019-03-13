using SummerFresh.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Entity
{

    [UnValidateInputeClass]
    [DisplayName("外置文件")]
    [Table("APP_ExternalFile")]
    public class ExternalFileEntity : CustomEntity
    {
        [TableField(IsShow = false)]
        [PrimaryKey]
        public string FileId { get; set; }

        [TitleField]
        [DisplayName("文件名称")]
        public string FileName { get; set; }

        [DisplayName("文件路径")]
        [TableField(TextAlign = CellTextAlign.Left)]
        public string FilePath { get; set; }

        [DisplayName("文件类型")]
        [SearchField]
        [DictionaryDataSource("ExternalFileType")]
        [FormField(ControlType = ControlType.DropDownList)]
        [TableField(Sortable = true)]
        [DefaultSortField(Business.OrderByType.DESC)]
        public string FileType { get; set; }

        [DisplayName("排序号")]
        [DefaultSortField(Business.OrderByType.ASC)]
        [TableField(Sortable = true)]
        public int Rank { get; set; }
    }
}
