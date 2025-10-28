using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_AdminHeader : System.Web.UI.UserControl
{
    dalNotice objNotice = new dalNotice();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNotice();
            lblUser.Text = Page.User.Identity.Name;
            LoadUserProfileImage();
        }
    }


    private void LoadNotification()
    {
        DataTable dt = new dalPerson().GetByUserName(Page.User.Identity.Name);

        string txtHtml = @"<li class='header'>You have 4 messages</li>
                        <li>
                            <!-- inner menu: contains the actual data -->
                            <ul class='menu'>
                                <li>
                                    <!-- start message -->
                                    <a href='#'>
                                        <div class='pull-left'>
                                            <img src='dist/img/user2-160x160.jpg' class='img-circle' alt='User Image'>
                                        </div>
                                        <h4>Support Team
                       
                                                        <small><i class='fa fa-clock-o'></i>5 mins</small>
                                        </h4>
                                        <p>Why not buy a new awesome theme?</p>
                                    </a>
                                </li>
                                <!-- end message -->
                                <li>
                                    <a href='#'>
                                        <div class='pull-left'>
                                            <img src='dist/img/user3-128x128.jpg' class='img-circle' alt='User Image'>
                                        </div>
                                        <h4>AdminLTE Design Team
                       
                                                        <small><i class='fa fa-clock-o'></i>2 hours</small>
                                        </h4>
                                        <p>Why not buy a new awesome theme?</p>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <div class='pull-left'>
                                            <img src='dist/img/user4-128x128.jpg' class='img-circle' alt='User Image'>
                                        </div>
                                        <h4>Developers
                       
                                                        <small><i class='fa fa-clock-o'></i>Today</small>
                                        </h4>
                                        <p>Why not buy a new awesome theme?</p>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <div class='pull-left'>
                                            <img src='dist/img/user3-128x128.jpg' class='img-circle' alt='User Image'>
                                        </div>
                                        <h4>Sales Department
                       
                                                        <small><i class='fa fa-clock-o'></i>Yesterday</small>
                                        </h4>
                                        <p>Why not buy a new awesome theme?</p>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <div class='pull-left'>
                                            <img src='dist/img/user4-128x128.jpg' class='img-circle' alt='User Image'>
                                        </div>
                                        <h4>Reviewers
                       
                                                        <small><i class='fa fa-clock-o'></i>2 days</small>
                                        </h4>
                                        <p>Why not buy a new awesome theme?</p>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li class='footer'><a href='#'>See All Messages</a></li>";
    }



    private void LoadUserProfileImage()
    {
        DataTable dt = new dalPerson().GetByUserName(Page.User.Identity.Name);


        if (dt.Rows.Count > 0)
        {
            string imageName = Server.MapPath("/Images/Person/" + dt.Rows[0]["PersonImage"].ToString());
            if (!System.IO.File.Exists(imageName))
            {
                imageName = "../Images/Student/" + dt.Rows[0]["PersonImage"].ToString();
            }
            else
            {
                imageName = "../Images/Person/" + dt.Rows[0]["PersonImage"].ToString();
            }

            imgHeaderProfile.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["PersonImage"].ToString()) ? "../Images/Common/avatar5.png" : imageName;
            imgtopProfile.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["PersonImage"].ToString()) ? "../Images/Common/avatar5.png" : imageName;
        }
    }

    protected void LoadNotice()
    {
        if (Page.User.IsInRole("SystemAdmin") || Page.User.IsInRole("Admin"))
        {
            lblNotice.Visible = false;
            lblcount.Text = "no";
        }
        else if (Page.User.IsInRole("Student"))
        {
            DataTable dt = objNotice.GetNoticeForStudent(Common.SessionInfo.StudentToClassId);

            if (dt.Rows.Count > 0)
            {
                lblcount.Text = dt.Rows.Count.ToString();
                lblNotice.InnerHtml = dt.Rows.Count.ToString();
                rptNotice.DataSource = dt;
                rptNotice.DataBind();
            }
            else
            {
                lblcount.Text = "no";
                lblNotice.Visible = false;
            }
        }
        else
        {
            DataTable dt = objNotice.UnseenNoticeForTeacher(16);
            if (dt.Rows.Count > 0)
            {
                lblcount.Text = dt.Rows.Count.ToString();
                lblNotice.InnerHtml = dt.Rows.Count.ToString();
                rptNotice.DataSource = dt;
                rptNotice.DataBind();
            }
            else
            {
                lblcount.Text = "no";
                lblNotice.Visible = false;
            }

        }
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}