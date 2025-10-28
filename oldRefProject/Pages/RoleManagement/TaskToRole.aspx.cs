using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RoleManagement_TaskToRole : BasePage
{
    dalTaskManager objTask = new dalTaskManager();
    dalRole objRole = new dalRole();
    public int count = 0;
    public static int RoleId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRole();
            LoadParent();
            LoadTaskToRole();
        }
    }

    protected void LoadRole()
    {
        DataTable dt = objRole.GetAllRole();
        ddlRole.DataSource = dt;
        ddlRole.DataBind();
    }

    public DataTable TaskToRole()
    {
        DataTable dt = new DataTable("TaskToRole");
        dt.Columns.Add("TId", typeof(int));
        dt.Columns.Add("RId", typeof(int));
        dt.Columns.Add("CdBy", typeof(string));
        dt.Columns.Add("CDate", typeof(DateTime));
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objTask.DeleteTaskToRole(Convert.ToInt32(ddlRole.SelectedValue));
        DataTable dt = TaskToRole();
        for (int i = 0; i < TreeView1.Nodes.Count; i++)
        {
            if (TreeView1.Nodes[i].Checked == true && TreeView1.Nodes[i].ChildNodes.Count > 0)
            {
                dt.Rows.Add(TreeView1.Nodes[i].Value, Convert.ToInt32(ddlRole.SelectedValue), Page.User.Identity.Name, DateTime.Now);
                for (int ii = 0; ii < TreeView1.Nodes[i].ChildNodes.Count; ii++)
                {
                    if (TreeView1.Nodes[i].ChildNodes[ii].Checked == true || TreeView1.Nodes[i].ChildNodes[ii].ChildNodes.Count > 0)
                    {
                        dt.Rows.Add(TreeView1.Nodes[i].ChildNodes[ii].Value, Convert.ToInt32(ddlRole.SelectedValue), Page.User.Identity.Name, DateTime.Now);
                    }
                }
            }
            else if (TreeView1.Nodes[i].Checked == false && TreeView1.Nodes[i].ChildNodes.Count > 0)
            {
                for (int ii = 0; ii < TreeView1.Nodes[i].ChildNodes.Count; ii++)
                {
                    if (TreeView1.Nodes[i].ChildNodes[ii].Checked == true)
                    {
                        dt.Rows.Add(TreeView1.Nodes[i].ChildNodes[ii].Value, Convert.ToInt32(ddlRole.SelectedValue), Page.User.Identity.Name, DateTime.Now);
                        count++;
                    }
                }
                if (count > 0)
                {
                    dt.Rows.Add(TreeView1.Nodes[i].Value, Convert.ToInt32(ddlRole.SelectedValue), Page.User.Identity.Name, DateTime.Now);
                    count = 0;
                }
            }
            else if (TreeView1.Nodes[i].Checked == true)
            {
                dt.Rows.Add(TreeView1.Nodes[i].Value, Convert.ToInt32(ddlRole.SelectedValue), Page.User.Identity.Name, DateTime.Now);
            }
        }
        objTask.TaskToRoleInsert(dt);
        MessageController.Show(MessageCode.SaveSucceeded, MessageType.Information, Page);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

    }

    protected void LoadParent()
    {
      //  DataTable dt = objTask.GetParanet(Controller.RoleIdByUserName(Page.User.Identity.Name));

        DataTable dt = objTask.GetAllParanet();

        TreeView1.Nodes.Clear();

        foreach (DataRow dr in dt.Rows)
        {
                TreeNode tnParent = new TreeNode();

                tnParent.Text = dr["TextEng"].ToString();

                tnParent.Value = dr["Id"].ToString();

                //  tnParent.PopulateOnDemand = true;

                tnParent.ToolTip = "Click to get Child";

                tnParent.SelectAction = TreeNodeSelectAction.SelectExpand;

                tnParent.CollapseAll();

                tnParent.Selected = true;

                TreeView1.Nodes.Add(tnParent);

                FillChild(tnParent, Convert.ToInt32(tnParent.Value));          
        }       
    }

    public void FillChild(TreeNode parent, int ParentId)
    {
      //  DataTable dtChild = objTask.GetChild(ParentId, Controller.RoleIdByUserName(Page.User.Identity.Name));
        DataTable dtChild = objTask.GetAllChildByParentID(ParentId);

        parent.ChildNodes.Clear();

        foreach (DataRow dr in dtChild.Rows)
        {
            parent.ShowCheckBox = true;
            TreeNode child = new TreeNode();

            child.Text = dr["TextEng"].ToString().Trim();

            child.Value = dr["Id"].ToString().Trim();

            child.ToolTip = "Click to get Child";
            child.SelectAction = TreeNodeSelectAction.SelectExpand;
            child.CollapseAll();
            child.Selected = true;
            parent.ChildNodes.Add(child);

        }

    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTaskToRole();
    }

    protected void LoadTaskToRole()
    {
        Clear();
        RoleId = int.Parse(ddlRole.SelectedValue);
        DataTable dt = objTask.GetParanet(Convert.ToInt32(ddlRole.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < TreeView1.Nodes.Count; i++)
                {
                    if (TreeView1.Nodes[i].Value == dr["Id"].ToString())
                    {
                        TreeView1.Nodes[i].Checked = true;
                        DataTable dtChild = objTask.GetChild(Convert.ToInt32(dr["Id"].ToString()), RoleId);
                        if (dtChild.Rows.Count > 0)
                        {
                            foreach (DataRow drc in dtChild.Rows)
                            {
                                for (int ii = 0; ii < TreeView1.Nodes[i].ChildNodes.Count; ii++)
                                {
                                    if (TreeView1.Nodes[i].ChildNodes[ii].Value == drc["TaskId"].ToString())
                                    {
                                        TreeView1.Nodes[i].ChildNodes[ii].Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void Clear()
    {
        for (int i = 0; i < TreeView1.Nodes.Count; i++)
        {
            TreeView1.Nodes[i].Checked = false;
            for (int ii = 0; ii < TreeView1.Nodes[i].ChildNodes.Count; ii++)
            {
                TreeView1.Nodes[i].ChildNodes[ii].Checked = false;
            }
        }
    }
}