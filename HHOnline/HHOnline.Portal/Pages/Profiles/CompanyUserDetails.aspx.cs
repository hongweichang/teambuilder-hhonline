using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHOnline.Framework.Web;
using HHOnline.Framework;
using HHOnline.Framework.Web.Enums;

public partial class Pages_Profiles_CompanyUserDetails : HHPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
            BindUser();
    }
    void BindUser()
    {
        string id = Request.QueryString["ID"];
        int userId = 0;
        if (int.TryParse(id, out userId))
        {
            User u = Users.GetUser(userId);
            if (u == null)
            {
                throw new HHException(ExceptionType.NoMasterError, "无查询到此用户数据，请确认此用户存在并且未被逻辑删除。");
            }
            else
            {
                List<User> users = new List<User>();
                users.Add(u);
                rpUser.DataSource = users;
                rpUser.DataBind();
            }
        }
        else
        {
            throw new HHException(ExceptionType.NoMasterError, "无法将获取传的参数值转换成数字，请确认未对地址栏Url做任何修改。");
        }
    }
    public string ParseString(string s)
    {
        if (string.IsNullOrEmpty(s))
            return "--";
        else
            return s;
    }
    public string GetUserStatus(string state)
    {
        AccountStatus s = (AccountStatus)Enum.Parse(typeof(AccountStatus), state);
        string msg = string.Empty;
        switch (s)
        {
            case AccountStatus.Deleted:
                msg = "已删除";
                break;
            case AccountStatus.Authenticated:
                msg = "正常";
                break;
            case AccountStatus.ApprovalPending:
                msg = "待审核";
                break;
            case AccountStatus.Disapproved:
                msg = "审核未通过";
                break;
            case AccountStatus.Lockon:
                msg = "已锁定";
                break;
            case AccountStatus.Anonymous:
                msg = "匿名";
                break;
            case AccountStatus.All:
                msg = "所有";
                break;
        }
        return msg;
    }
    public string GetYesNo(string b)
    {
        try
        {
            return (b == "1") ? "是" : "否";
        }
        catch
        {
            return "未知";
        }
    }
    public string GetUserName(string userId)
    {
        try
        {
            return Users.GetUser(int.Parse(userId)).DisplayName;
        }
        catch
        {
            return "佚名";
        }
    }
    public string ParseDateTime(string dt)
    {
        try
        {
            DateTime d = DateTime.Parse(dt);
            if (GlobalSettings.MinValue == d)
            {
                return "--";
            }
            else
            {
                return d.ToString();
            }
        }
        catch
        {
            return "--";
        }
    }
    public override void OnPageLoaded()
    {
        this.PageInfoType = InfoType.IframeInfo;
    }
}
