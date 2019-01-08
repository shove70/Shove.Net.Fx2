using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Collections;
using System.Text.RegularExpressions;

[assembly: TagPrefix("Shove.Web.UI", "ShoveWebUI")]
//[assembly: WebResource("Shove.Web.UI.Script.ShoveWebPartDesignMenu.js", "application/javascript")]
namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart基页
    /// </summary>
    public class ShoveWebPartBasePage : System.Web.UI.Page
    {
        #region 控件

        /// <summary>
        /// shoveWebPart容器层
        /// </summary>
        public System.Web.UI.WebControls.Panel ShoveWebPart_bodyMain;

        /// <summary>
        /// 编辑菜单 Panel
        /// </summary>
        public System.Web.UI.WebControls.Panel ShoveWebPart_panel;

        /// <summary>
        /// 打开关闭 Panel 的按钮
        /// </summary>
        public System.Web.UI.HtmlControls.HtmlImage ShoveWebPart_btnEdit;


        /// <summary>
        /// 设计、运行模式切换
        /// </summary>
        public System.Web.UI.WebControls.DropDownList ShoveWebPart_ddlMode;

        /// <summary>
        /// 设计菜单标题
        /// </summary>
        public System.Web.UI.WebControls.Label ShoveWebPart_labTitle;

        /// <summary>
        /// 设计菜单副标题
        /// </summary>
        public System.Web.UI.WebControls.Label ShoveWebPart_labSubTitle;

        /// <summary>
        /// 添加新页按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnAddNewPage;

        /// <summary>
        /// 添加ShoveWebPart按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnAddShoveWebPart;

        /// <summary>
        /// 备份本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnBackupSiteLayout;

        /// <summary>
        /// 恢复本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnRestoreSiteLayout;

        /// <summary>
        /// 删除本站布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnClearSiteLayout;

        /// <summary>
        /// 备份本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnBackupPageLayout;

        /// <summary>
        /// 恢复本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnRestorePageLayout;

        /// <summary>
        /// 删除本页布局按钮
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnClearPageLayout;

        /// <summary>
        /// 上传/下载样式表
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnUpLoadStyle;

        /// <summary>
        /// 查看网页列表
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnPageList;

        /// <summary>
        /// 复制页面
        /// </summary>
        public System.Web.UI.WebControls.Button ShoveWebPart_btnCopyPage;

        #endregion

        /// <summary>
        /// 站点ID默认为-1
        /// </summary>
        public long SiteID
        {
            get
            {
                object obj = this.ViewState["SiteID"];

                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState["SiteID"] = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID
        {
            get
            {
                object obj = this.ViewState["UserID"];

                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState["UserID"] = value;
            }
        }

        /// <summary>
        /// 是否允许设计，由继承页面根据用户是否登陆，是否有设计权限而复制
        /// </summary>
        public bool IsAllowDesign
        {
            get
            {
                object obj = this.ViewState["IsAllowDesign"];

                try
                {
                    return Convert.ToBoolean(obj);
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                this.ViewState["IsAllowDesign"] = value;
            }
        }

        /// <summary>
        /// 是否是设计状态
        /// </summary>
        public bool IsDesigning
        {
            get
            {
                if (!IsAllowDesign)
                {
                    return false;
                }

                return (this.Request.QueryString["Designing"] == "True");
            }
        }

        /// <summary>
        /// 页名
        /// </summary>
        public string PageName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Request["PN"]))
                {
                    return this.Request["PN"];
                }
                else
                {
                    return "Default";
                }
            }
        }

        // 相对于根目录的相对路径
        private string SupportDir
        {
            get
            {
                return PublicFunction.GetCurrentRelativePath() + "ShoveWebUI_client";
            }
        }

        /// <summary>
        /// ShoveWebPart基页构造函数
        /// </summary>
        public ShoveWebPartBasePage()
        {
            ShoveWebPart_panel = new Panel();

            ShoveWebPart_btnEdit = new HtmlImage();

            ShoveWebPart_ddlMode = new DropDownList();

            ShoveWebPart_labTitle = new Label();
            ShoveWebPart_labSubTitle = new Label();

            ShoveWebPart_btnAddNewPage = new Button();
            ShoveWebPart_btnAddShoveWebPart = new Button();
            ShoveWebPart_btnBackupSiteLayout = new Button();
            ShoveWebPart_btnRestoreSiteLayout = new Button();
            ShoveWebPart_btnClearSiteLayout = new Button();
            ShoveWebPart_btnBackupPageLayout = new Button();
            ShoveWebPart_btnRestorePageLayout = new Button();
            ShoveWebPart_btnClearPageLayout = new Button();
            ShoveWebPart_btnUpLoadStyle = new Button();
            ShoveWebPart_btnPageList = new Button();
            ShoveWebPart_btnCopyPage = new Button();

            SiteID = -1;
            UserID = -1;
            IsAllowDesign = true;
        }

        /// <summary>
        /// 设置站点ID的虚拟方法
        /// </summary>
        protected virtual void SetSiteID()
        {
            // 如果需要多站，需要实现此方法
        }

        /// <summary>
        /// 设置用户的虚拟方法
        /// </summary>
        protected virtual void SetUserID()
        {
            // 如果需要多站，需要实现此方法
        }

        /// <summary>
        /// 设置页面是否允许设计
        /// </summary>
        protected virtual void SetAllowDesign()
        {
            // 根据用户是否具有设计权限设置
        }

        /// <summary>
        /// 重写页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            SetSiteID();
            SetUserID();
            SetAllowDesign();

            this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPart", "ShoveWebUI_client/Script/ShoveWebPart.js");

            if (IsDesigning)
            {
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveWebPart));
                AjaxPro.Utility.RegisterTypeForAjax(typeof(ShoveWebPartBasePage));

                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPartDesigning", "ShoveWebUI_client/Script/ShoveWebPartDesigning.js");
            }

            #region 引用样式表

            if (!System.IO.File.Exists(Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css")))
            {
                if (System.IO.File.Exists(Server.MapPath("~/ShoveWebUI_client/Style/0.css")))
                {
                    System.IO.File.Copy(Server.MapPath("~/ShoveWebUI_client/Style/0.css"), Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css"));
                }
                else
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/ShoveWebUI_client/Style/" + SiteID.ToString() + ".css"), "<!-- Style -->\r\n");
                }
            }

            HtmlLink hl = new HtmlLink();
            hl.Href = SupportDir + "/Style/" + SiteID.ToString() + ".css";
            hl.Attributes.Add("type", "text/css");
            hl.Attributes.Add("rel", "stylesheet");

            try
            {
                this.Page.Header.Controls.Add(hl);
            }
            catch { }

            #endregion

            #region 创建主 Part 容器 Div

            Control c = this.Form.FindControl("bodyMain");

            if (c == null)
            {
                ShoveWebPart_bodyMain = new Panel();
                ShoveWebPart_bodyMain.ID = "bodyMain";
                ShoveWebPart_bodyMain.CssClass = "bodyMainDiv";

                this.Form.Controls.Add(ShoveWebPart_bodyMain);
            }

            #endregion

            if (IsAllowDesign)
            {
                this.Page.ClientScript.RegisterClientScriptInclude("Shove.Web.UI.ShoveWebPartDesignMenu", "ShoveWebUI_client/Script/ShoveWebPartDesignMenu.js");

                #region 画：设计 Panel 菜单浮动层

                #region ShoveWebPart_divMenu

                ShoveWebPart_panel.ID = "ShoveWebPart_divMenu";
                ShoveWebPart_panel.Width = new Unit("132px");
                ShoveWebPart_panel.Height = new Unit("440px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Left, "0px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Top, "5px");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.ZIndex, "20000");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                ShoveWebPart_panel.Style.Add(HtmlTextWriterStyle.BackgroundImage, SupportDir + "/Images/Designer_bg.gif");

                #endregion

                #region ShoveWebPart_btnEdit

                ShoveWebPart_btnEdit.ID = "ShoveWebPart_btnEdit";
                ShoveWebPart_btnEdit.Src = SupportDir + "/Images/DesignerMenuOpen.gif";
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Left, "132px");
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Top, "100px");
                ShoveWebPart_btnEdit.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_btnEdit.Attributes.Add("onclick", "ShoveWebPartDesignMenu_moveMenu()");

                #endregion

                #region ShoveWebPart_ddlMode

                ShoveWebPart_ddlMode.ID = "ShoveWebPart_ddlMode";
                ShoveWebPart_ddlMode.Items.Clear();
                ShoveWebPart_ddlMode.Width = new Unit("109px");
                ShoveWebPart_ddlMode.Height = new Unit("25px");
                ShoveWebPart_ddlMode.Items.Add(new ListItem("浏览模式", "0"));
                ShoveWebPart_ddlMode.Items.Add(new ListItem("设计模式", "1"));
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Top, "70px");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_ddlMode.Style.Add(HtmlTextWriterStyle.FontSize, "15");
                ShoveWebPart_ddlMode.AutoPostBack = true;
                ShoveWebPart_ddlMode.SelectedIndexChanged += new EventHandler(ShoveWebPart_ddlMode_SelectedIndexChanged);
                ShoveWebPart_ddlMode.SelectedIndex = IsDesigning ? 1 : 0;

                #endregion

                IniFile ini = new IniFile(this.Server.MapPath("~/ShoveWebUI_client/Data/ShoveWebPart.UserControls.ini"));

                #region ShoveWebPart_labTitle

                ShoveWebPart_labTitle.ID = "ShoveWebPart_labTitle";
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Top, "15px");
                ShoveWebPart_labTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_labTitle.Font.Name = "tahoma";
                ShoveWebPart_labTitle.Font.Size = FontUnit.Point(8);

                string _title = ini.Read("Options", "DesignerTitle").Trim();

                if (_title == "")
                {
                    _title = "ShoveWebPart";
                }

                ShoveWebPart_labTitle.Text = _title;

                #endregion

                #region ShoveWebPart_labSubTitle

                ShoveWebPart_labSubTitle.ID = "ShoveWebPart_labSubTitle";
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Left, "10px");
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Top, "35px");
                ShoveWebPart_labSubTitle.Style.Add(HtmlTextWriterStyle.Position, "absolute");
                ShoveWebPart_labSubTitle.Font.Name = "tahoma";
                ShoveWebPart_labSubTitle.Font.Size = FontUnit.Point(8);

                _title = ini.Read("Options", "DesignerSubTitle").Trim();

                if (_title == "")
                {
                    _title = "设计菜单";
                }

                ShoveWebPart_labSubTitle.Text = _title;

                #endregion

                string supportdir = SupportDir;

                #region ShoveWebPart_btnAddNewPage

                SetButtonStyle(ShoveWebPart_btnAddNewPage, "ShoveWebPart_btnAddNewPage", "添加一个新网页", "100px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnAddNewPage.OnClientClick = "this.disabled=true";
                ShoveWebPart_btnAddNewPage.OnClientClick = "var NewPageName = ShoveWebPartDesignMenu_AddNewPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!NewPageName) return false; window.location.href = 'Default.aspx?PN=' + NewPageName; return false;";

                #endregion

                #region ShoveWebPart_btnAddShoveWebPart

                SetButtonStyle(ShoveWebPart_btnAddShoveWebPart, "ShoveWebPart_btnAddShoveWebPart", "添加一个Part", "130px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnAddShoveWebPart.OnClientClick = "this.disabled=true";
                ShoveWebPart_btnAddShoveWebPart.Click += new EventHandler(ShoveWebPart_btnAddShoveWebPart_Click);

                #endregion

                #region ShoveWebPart_btnBackupSiteLayout

                SetButtonStyle(ShoveWebPart_btnBackupSiteLayout, "ShoveWebPart_btnBackupSiteLayout", "备份本站布局", "160px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnBackupSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要备份本站布局吗？") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShoveWebPartBasePage.BackupSite(" + SiteID.ToString() + "); if (CallAjaxResult.value == '') alert('备份成功。'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShoveWebPart_btnRestoreSiteLayout

                SetButtonStyle(ShoveWebPart_btnRestoreSiteLayout, "ShoveWebPart_btnRestoreSiteLayout", "恢复本站布局", "190px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnRestoreSiteLayout.OnClientClick = "if (ShoveWebPartDesignMenu_RestoreSiteLayout_Open(" + SiteID + ", '" + supportdir + "') != 'Restored') return false;";
                ShoveWebPart_btnRestoreSiteLayout.Click += new EventHandler(ShoveWebPart_btnRestoreSiteLayout_Click);

                #endregion

                #region ShoveWebPart_btnClearSiteLayout

                SetButtonStyle(ShoveWebPart_btnClearSiteLayout, "ShoveWebPart_btnClearSiteLayout", "删除本站布局", "220px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnClearSiteLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要删除本站布局吗？") + "')) return false; this.disabled=true";
                ShoveWebPart_btnClearSiteLayout.Click += new EventHandler(ShoveWebPart_btnClearSiteLayout_Click);

                #endregion

                #region ShoveWebPart_btnBackupPageLayout

                SetButtonStyle(ShoveWebPart_btnBackupPageLayout, "ShoveWebPart_btnBackupPageLayout", "备份本页布局", "250px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnBackupPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要备份本页布局吗？") + "')) return false; this.disabled = true; var CallAjaxResult = Shove.Web.UI.ShoveWebPartBasePage.BackupPage(" + SiteID.ToString() + ", '" + PageName + "'); if (CallAjaxResult.value == '') alert('备份成功。'); else alert(CallAjaxResult.value); this.disabled = false; return false;";

                #endregion

                #region ShoveWebPart_btnRestorePageLayout

                SetButtonStyle(ShoveWebPart_btnRestorePageLayout, "ShoveWebPart_btnRestorePageLayout", "恢复本页布局", "280px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnRestorePageLayout.OnClientClick = "if (ShoveWebPartDesignMenu_RestorePageLayout_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Restored') return false;";
                ShoveWebPart_btnRestorePageLayout.Click += new EventHandler(ShoveWebPart_btnRestorePageLayout_Click);

                #endregion

                #region ShoveWebPart_btnClearPageLayout

                SetButtonStyle(ShoveWebPart_btnClearPageLayout, "ShoveWebPart_btnClearPageLayout", "删除本页布局", "310px", supportdir + "/Images/botton_bg_2.gif", IsDesigning);
                ShoveWebPart_btnClearPageLayout.OnClientClick = "if (!confirm('" + HttpUtility.HtmlEncode("确信要删除本页布局吗？") + "')) return false; this.disabled=true";
                ShoveWebPart_btnClearPageLayout.Click += new EventHandler(ShoveWebPart_btnClearPageLayout_Click);

                #endregion

                #region ShoveWebPart_btnUpLoadStyle

                SetButtonStyle(ShoveWebPart_btnUpLoadStyle, "ShoveWebPart_btnUpLoadStyle", "上传/下载样式表", "340px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnUpLoadStyle.OnClientClick = "if (ShoveWebPartDesignMenu_UpLoadStyle_Open(" + SiteID + ", '" + supportdir + "') != 'Uploaded') return false;";
                ShoveWebPart_btnUpLoadStyle.Click += new EventHandler(ShoveWebPart_btnUpLoadStyle_Click);

                #endregion

                #region ShoveWebPart_btnPageList

                SetButtonStyle(ShoveWebPart_btnPageList, "ShoveWebPart_btnPageList", "查看网页列表", "370px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnPageList.OnClientClick = "var PageName = ShoveWebPartDesignMenu_PageList_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "'); if (!PageName) return false; window.location.href = 'Default.aspx?PN=' + PageName + '&Designing=False'; return false;";

                #endregion

                #region ShoveWebPart_btnCopyPage

                SetButtonStyle(ShoveWebPart_btnCopyPage, "ShoveWebPart_btnCopyPage", "复制页面", "400px", supportdir + "/Images/botton_bg.gif", IsDesigning);
                ShoveWebPart_btnCopyPage.OnClientClick = "if (ShoveWebPartDesignMenu_CopyPage_Open(" + SiteID + ", '" + PageName + "', '" + supportdir + "') != 'Copied') return false;";
                ShoveWebPart_btnCopyPage.Click += new EventHandler(ShoveWebPart_btnCopyPage_Click);

                #endregion

                try
                {
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnEdit);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_ddlMode);

                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_labTitle);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_labSubTitle);

                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnAddShoveWebPart);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnAddNewPage);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnBackupSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnRestoreSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnClearSiteLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnBackupPageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnRestorePageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnClearPageLayout);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnUpLoadStyle);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnPageList);
                    ShoveWebPart_panel.Controls.Add(ShoveWebPart_btnCopyPage);
                }
                catch { }

                #endregion

                try
                {
                    this.Form.Controls.Add(ShoveWebPart_panel);
                }
                catch { }
            }

            #region 装载 Part

            int RowNo = -1;
            DataTable dtParts = new ShoveWebPartFile(SiteID, PageName).Read(null, null, ref RowNo);

            foreach (DataRow dr in dtParts.Rows)
            {
                ShoveWebPart swp = new ShoveWebPart();

                swp.ID = dr["id"].ToString();
                swp.SiteID = SiteID;
                swp.UserID = UserID;
                swp.PageName = PageName;

                if (ShoveWebPart_bodyMain.FindControl(swp.ID) == null)
                {
                    try
                    {
                        ShoveWebPart_bodyMain.Controls.Add(swp);
                    }
                    catch
                    {
                        this.Response.Write(String.Format("ID 为 {0} 的 Part 装载失败。<br />", swp.ID));
                    }
                }
                else
                {
                    this.Response.Write(String.Format("有多个重复的相同 ID 为 {0} 的 Part。系统只装载了 1 个。<br />", swp.ID));
                }
            }

            #endregion

            if (IsDesigning)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShoveWebUI_ShoveWebPart_SetOnKeyDown", "<script type=\"text/javascript\">ShoveWebUI_ShoveWebPart_SetOnKeyDown(" + SiteID.ToString() + ", '" + PageName + "');</script>");
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// 设置 button 的外观
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="top"></param>
        /// <param name="backgroundimage"></param>
        /// <param name="enabled"></param>
        private void SetButtonStyle(Button btn, string id, string text, string top, string backgroundimage, bool enabled)
        {
            btn.ID = id;
            btn.Text = text;
            btn.Width = new Unit("109px");
            btn.Height = new Unit("25px");
            btn.Style.Add(HtmlTextWriterStyle.Left, "10px");
            btn.Style.Add(HtmlTextWriterStyle.Top, top);
            btn.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            btn.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            btn.Style.Add(HtmlTextWriterStyle.BackgroundImage, backgroundimage);
            if (enabled)
            {
                btn.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
            }
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.UseSubmitBehavior = false;
            btn.CausesValidation = false;
            btn.Enabled = enabled;
        }

        #region 事件处理区

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsDesigning)
            {
                this.Session.Remove("Shove.Web.UI.ShoveWebPart.RunMode");
            }
            else
            {
                this.Session["Shove.Web.UI.ShoveWebPart.RunMode"] = "DESIGN";
            }

            this.Response.Redirect(BuildUrl(!IsDesigning), true);
        }

        private string BuildUrl(bool DesignState)
        {
            string Url = this.Request.Url.AbsoluteUri;

            if (Url.IndexOf("Designing=" + DesignState.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Url;
            }

            if (Url.IndexOf("Designing=" + (!DesignState).ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Url.Replace("Designing=" + (!DesignState).ToString(), "Designing=" + DesignState.ToString());
            }

            if (String.IsNullOrEmpty(this.Request.Url.Query))
            {
                Url += "?";
            }
            else
            {
                Url += "&";
            }

            Url += "Designing=" + DesignState.ToString();

            return Url;
        }

        /// <summary>
        /// 动态添加shoveWebPart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnAddShoveWebPart_Click(object sender, EventArgs e)
        {
            Control panelDiv = this.Form.FindControl("bodyMain");

            if (panelDiv == null)
            {
                JavaScript.Alert(this.Page, "没有找到可以放置 ShoveWebPart 部件的父容器控件(bodyMain)。");

                return;
            }

            // 将原来的 Part 的 ZIndex 全部减去1
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.ZIndexSubtractOne();

            // 增加一个 Part
            ShoveWebPart shoveWebPart = new ShoveWebPart();
            shoveWebPart.ID = swpf.GetNewPartID();
            shoveWebPart.ZIndex = 10000;

            try
            {
                panelDiv.Controls.Add(shoveWebPart);
            }
            catch(Exception ee)
            {
                JavaScript.Alert(this.Page, "增加新 ShoveWebPart 部件发生错误。(" + ee.Message + ")");

                return;
            }
        }

        /// <summary>
        /// 备份站点布局
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupSite(long siteid)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, null);
            swpf.BackupSite();

            return "";
        }

        /// <summary>
        /// 恢复本站布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnRestoreSiteLayout_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Restored"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 删除本站布局 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnClearSiteLayout_Click(object sender, EventArgs e)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.DeleteSite();

            string Designing = this.Request.QueryString["Designing"];

            if (Designing != "True")
            {
                Designing = "False";
            }

            this.Response.Redirect("Default.aspx?Designing=" + Designing.ToString(), true);
        }

        /// <summary>
        /// 备份页面布局
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="pagename"></param>
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.Read)]
        public string BackupPage(long siteid, string pagename)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(siteid, pagename);
            swpf.BackupPage();

            return "";
        }

        /// <summary>
        /// 恢复本页布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnRestorePageLayout_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Restored"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 删除本页布局 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnClearPageLayout_Click(object sender, EventArgs e)
        {
            ShoveWebPartFile swpf = new ShoveWebPartFile(SiteID, PageName);
            swpf.DeletePage();

            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 上传、下载样式表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnUpLoadStyle_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Uploaded"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }

        /// <summary>
        /// 复制页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShoveWebPart_btnCopyPage_Click(object sender, EventArgs e)
        {
            // 本按钮先在客户端弹出页面，如果对话框返回值等于"Copied"，才会执行下面代码
            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }
    
        #endregion
    }
}
