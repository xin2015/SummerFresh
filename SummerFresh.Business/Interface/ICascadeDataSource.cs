using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SummerFresh.Business
{
    public class TreeNode
    {
        public string id { get; set; }

        public string name { get; set; }

        public string pId { get; set; }

        public string icon { get; set; }

        public bool isParent { get; set; }

        public bool open { get; set; }

        public string url { get; set; }

        public bool @checked { get; set; }

        public bool chkDisabled { get; set; }
    }

    public interface ICascadeDataSource : IFieldConverter
    {
        IList<TreeNode> SelectRootItems(TreeNode parent = null);
    }
}
